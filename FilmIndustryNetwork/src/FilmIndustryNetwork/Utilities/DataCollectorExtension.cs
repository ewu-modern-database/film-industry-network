using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FilmIndustryNetwork.Entities;
using FilmIndustryNetwork.Interfaces;
using FilmIndustryNetwork.MyApiFilms;
using FilmIndustryNetwork.MyApiFilms.Entities;
using Microsoft.AspNet.Builder;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.OptionsModel;
using ApiMovie = FilmIndustryNetwork.MyApiFilms.Entities.Movie;
using ApiPerson = FilmIndustryNetwork.MyApiFilms.Entities.Person;
using Movie = FilmIndustryNetwork.Entities.Movie;
using Person = FilmIndustryNetwork.Entities.Person;

namespace FilmIndustryNetwork.Utilities
{
    public static class DataCollectorExtension
    {
        public static IApplicationBuilder UseDataCollector(this IApplicationBuilder app, IServiceProvider provider)
        {
            if (DataCollector.InstanceAlreadyRunning) return app;
            DataCollector.InstanceAlreadyRunning = true;
            var movieService = provider.GetService<IMovieService>();
            var personService = provider.GetService<IPersonService>();
            var factory = provider.GetService<IDataCollectorFactory>();
            var options = provider.GetService<IOptions<AppSettings>>();

            //var collector = factory.CreateNewInstance(movieService, personService, options);
            //collector.RunInParallel();
            return app;
        }
    }

    public class DataCollectorFactory : IDataCollectorFactory
    {
        public IDataCollector CreateNewInstance(IMovieService movieService, IPersonService personService, IOptions<AppSettings> options)
        {
            return new DataCollector(movieService, personService, options);
        }
    }

    public class DataCollector : IDataCollector
    {
        public static bool InstanceAlreadyRunning { get; set; }

        private readonly string _token;
        private readonly IMovieService _movieService;
        private readonly IPersonService _personService;

        /// <summary>
        /// Incomplete movies and persons queues need to always have "---" at the end
        /// This will prevent threads from prematurely terminating giving each thread a chance to populate
        /// each others queue
        /// </summary>
        private readonly ConcurrentQueue _queue;

        public DataCollector(IMovieService movieService, IPersonService personService, IOptions<AppSettings> options)
        {
            _movieService = movieService;
            _personService = personService;

            var appSettings = options.Options;
            _token = appSettings.MyApiFilmsToken;

            _queue = new ConcurrentQueue();
            _queue.PushIncompleteMovie("The Adventures of Mickey Matson and Copperhead Treasure");
            _queue.PushIncompleteMovie("Lost in the Future");
            _queue.PushIncompleteMovie("American Scream King");
            _queue.PushIncompleteMovie("Eddie and the Alternate Universe");
            _queue.PushIncompleteMovie("The Christmas Bunny");
            _queue.PushIncompleteMovie("---"); // "---" is used to indicate end, this is so it properly runs in parallel
            _queue.PushIncompletePerson("Matthew Settle");
            _queue.PushIncompletePerson("Jennifer Esposito");
            _queue.PushIncompletePerson("John Hawkes");
            _queue.PushIncompletePerson("Tate Donovan");
            _queue.PushIncompletePerson("Michael Greene");
            _queue.PushIncompletePerson("---"); // "---" is used to indicate end, this is so it properly runs in parallel
        }

        public void Run()
        {
            var client = new MyApiFilmsClient(_token);

            var movieCollectorThread =
                new Thread(
                    () => MovieCollector(_movieService, _personService, client, _queue)
                    ) {Name = "MovieCollectorThread"};
            var personCollectorThread = 
                new Thread(
                    () => PersonCollector(_personService, _movieService, client, _queue)
                    ) {Name = "PersonCollectorThread"};
            var relationshipMakerThread = 
                new Thread(
                    () => RelationshipMaker(_movieService, _personService, _queue)
                    ) { Name = "RelationshipMakerThread" };

            movieCollectorThread.Start();
            personCollectorThread.Start();
            relationshipMakerThread.Start();

            movieCollectorThread.Join();
            personCollectorThread.Join();
            relationshipMakerThread.Join();
        }

        public Task RunAsync()
        {
            return Task.Run(delegate { Run(); });
        }

        public Thread RunInParallel()
        {
            var t = new Thread(Run) {Name = "DataCollectorThread"};
            t.Start();
            return t;
        }

        private static void FillQueueFromList(IMovieService movieService, IPersonService personService, 
            IEnumerable<string> values, ConcurrentQueue queue, int option)
        {
            switch (option)
            {
                case 0:
                    foreach (var value in values)
                    {
                        if (movieService.GetMovieByIdAsync(value).Result != null) continue;
                        if (!queue.IncompleteMoviesWaiting())
                        {
                            queue.PushIncompleteMovie(value);
                            queue.PushIncompleteMovie("---"); // "---" is for concurrency
                            continue;
                        }
                        var last = queue.PopLastIncompleteMovie();
                        queue.PushIncompleteMovie(value);
                        queue.PushIncompleteMovie(last);
                    }
                    break;
                case 1:
                    foreach (var value in values)
                    {
                        if (personService.GetPersonByNameAsync(value).Result != null) continue;
                        if (!queue.IncompletePersonsWaiting())
                        {
                            queue.PushIncompletePerson(value);
                            queue.PushIncompletePerson("---"); // "---" is for concurrency
                            continue;
                        }
                        var last = queue.PopLastIncompletePerson();
                        queue.PushIncompletePerson(value);
                        queue.PushIncompletePerson(last);
                    }
                    break;
            }
        }

        private static void AddRelationshipsToQueue(IEnumerable<string> movies, IEnumerable<string> people,
            ConcurrentQueue queue, string relationshipType)
        {
            foreach (var movie in movies)
            {
                foreach (var person in people)
                {
                    queue.PushRelationship(new Relationship
                    {
                        MovieId = movie,
                        RelationType = relationshipType,
                        PersonName = person
                    });
                }
            }
        }

        private static void MovieCollector(IMovieService movieService, IPersonService personService, 
            MyApiFilmsClient client, ConcurrentQueue queue)
        {
            while (queue.IncompleteMoviesWaiting() || queue.IncompletePersonsWaiting())
            {
                if (!queue.IncompleteMoviesWaiting()) continue;
                // ApiMovie is an alias for FilmIndustryNetwork.MyApiFilms.Entities.Movie
                // This is to distinguish between FilmIndustryNetwork.MyApiFilms.Entities.Movie
                // and FilmIndustryNetwork.Entities.Movie
                ApiMovie apiMovie;
                var title = queue.PopIncompleteMovie();

                if (title == "---") continue;

                try
                {
                    apiMovie =
                        client.GetDataAsObject<MovieResponse>(title, DataSetType.Movie)?.Data?.Movies?.FirstOrDefault();

                    if (apiMovie == null) throw new Exception();

                    if (apiMovie.Type.Contains("TV")) continue;
                }
                catch (MyApiFilmsTimeoutException e)
                {
                    // TODO: setup logging
                    FillQueueFromList(movieService, null, new [] {title}, queue, 0);
                    continue;
                }
                catch (NoMyApiFilmsResponseException e)
                {
                    // TODO: setup logging
                    continue;
                }
                catch (Exception)
                {
                    // TODO: setup logging
                    FillQueueFromList(movieService, null, new[] { title }, queue, 0);
                    continue;
                }

                var newMovie = new Movie
                {
                    Countries = apiMovie.Countries,
                    FilmingLocations = apiMovie.FilmingLocations,
                    Genres = apiMovie.Genres,
                    Id = apiMovie.IdIMDB,
                    Languages = apiMovie.Languages,
                    Plot = apiMovie.Plot,
                    NeedsApiLookup = false,
                    Rated = apiMovie.Rated,
                    Rating = apiMovie.Rating,
                    Title = apiMovie.Title,
                    Year = apiMovie.Year
                };

                FillQueueFromList(null, personService, apiMovie.Actors.Select(x => x.ActorName).ToList(), queue, 1);
                FillQueueFromList(null, personService, apiMovie.Directors.Select(x => x.Name).ToList(), queue, 1);
                FillQueueFromList(null, personService, apiMovie.Writers.Select(x => x.Name).ToList(), queue, 1);

                AddRelationshipsToQueue(new[] {apiMovie.IdIMDB}, apiMovie.Actors.Select(x => x.ActorName).ToList(), queue,
                    RelationTypes.ActedIn);
                AddRelationshipsToQueue(new[] {apiMovie.IdIMDB}, apiMovie.Directors.Select(x => x.Name).ToList(), queue,
                    RelationTypes.DirectorFor);
                AddRelationshipsToQueue(new[] {apiMovie.IdIMDB}, apiMovie.Writers.Select(x => x.Name).ToList(), queue,
                    RelationTypes.WriterOf);

                var existingMovie = movieService.GetMovieByIdAsync(newMovie.Id).Result;

                if (existingMovie != null) continue;

                movieService.AddMovieAsync(newMovie).Wait();
            }
        }

        private static void PersonCollector(IPersonService personService, IMovieService movieService, 
            MyApiFilmsClient client, ConcurrentQueue queue)
        {
            while (queue.IncompleteMoviesWaiting() || queue.IncompletePersonsWaiting())
            {
                if (!queue.IncompletePersonsWaiting()) continue;
                // ApiPerson is an alias for FilmIndustryNetwork.MyApiFilms.Entities.Person
                // This is to distinguish between FilmIndustryNetwork.MyApiFilms.Entities.Person
                // and FilmIndustryNetwork.Entities.Person
                ApiPerson apiPerson;
                var name = queue.PopIncompletePerson();

                if (name == "---") continue;

                try
                {
                    apiPerson =
                        client.GetDataAsObject<PersonResponse>(name, DataSetType.Person)?.Data?.Names?.FirstOrDefault();

                    if (apiPerson == null) throw new Exception();
                }
                catch (MyApiFilmsTimeoutException e)
                {
                    // TODO: setup logging
                    FillQueueFromList(null, personService, new [] {name}, queue, 1);
                    continue;
                }
                catch (NoMyApiFilmsResponseException e)
                {
                    // TODO: setup logging
                    continue;
                }
                catch (Exception)
                {
                    // TODO: setup logging
                    FillQueueFromList(null, personService, new[] { name }, queue, 1);
                    continue;
                }

                var person = new Person
                {
                    Bio = apiPerson.Bio,
                    BirthName = apiPerson.BirthName,
                    DateOfBirth = apiPerson.DateOfBirth,
                    Id = apiPerson.IdIMDB,
                    Name = apiPerson.Name,
                    NeedsApiLookup = false,
                    PlaceOfBirth = apiPerson.PlaceOfBirth,
                    UrlPhoto = apiPerson.UrlPhoto
                };

                FillQueueFromList(movieService, null, apiPerson.Filmographies
                    .Where(x => x.Section == "Actor" || x.Section == "Director" || x.Section == "Writer")
                    .SelectMany(f => f.Filmography.Select(m => m.Title)).ToList(), queue, 0);

                AddRelationshipsToQueue(
                    apiPerson.Filmographies
                        .Where(x => x.Section == "Actor")
                        .SelectMany(f => f.Filmography
                                          .Where(mm => mm.Remarks != null && !mm.Remarks.Contains("(TV Series)"))
                                          .Select(m => m.IMDBId)), new[] {person.Name}, queue,
                    RelationTypes.ActedIn);
                AddRelationshipsToQueue(
                    apiPerson.Filmographies
                        .Where(x => x.Section == "Director")
                        .SelectMany(f => f.Filmography
                                          .Where(mm => mm.Remarks != null && !mm.Remarks.Contains("(TV Series)"))
                                          .Select(m => m.IMDBId)), new[] { person.Name }, queue,
                    RelationTypes.DirectorFor);
                AddRelationshipsToQueue(
                    apiPerson.Filmographies
                        .Where(x => x.Section == "Writer")
                        .SelectMany(f => f.Filmography
                                          .Where(mm => mm.Remarks != null && !mm.Remarks.Contains("(TV Series)"))
                                          .Select(m => m.IMDBId)), new[] { person.Name }, queue,
                    RelationTypes.WriterOf);

                if (personService.GetPersonByNameAsync(person.Name).Result != null) continue;

                personService.AddPersonAsync(person).Wait();
            }
        }

        private static void RelationshipMaker(IMovieService movieService, IPersonService personService, ConcurrentQueue queue)
        {
            while (queue.IncompleteMoviesWaiting() || queue.IncompletePersonsWaiting() || queue.RelationshipsPending())
            {
                if (!queue.RelationshipsPending()) continue;

                var relationship = queue.PopRelationship();

                var movie = movieService.GetMovieByIdAsync(relationship.MovieId).Result;
                var person = personService.GetPersonByNameAsync(relationship.PersonName).Result;

                if (movie == null ||
                    person == null)
                {
                    queue.PushRelationship(relationship);
                    continue;
                }

                movieService.AddOrUpdateMovieWithRelationAsync(movie, person, relationship.RelationType).Wait();
            }
        }
    }
}
