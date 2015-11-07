using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FilmIndustryNetwork.Interfaces;
using FilmIndustryNetwork.Entities;
using FilmIndustryNetwork.MyApiFilms;

namespace FilmIndustryNetwork.Utilities
{
    public class DataCollector
    {
        public async Task PopulateDatabaseAsync(IMovieService movieservice, IPersonService personservice, MyApiFilmsClient client)
        {
            var movieresualts = client.GetDataAsObject<MyApiFilms.Entities.Movie>("Titanic", DataSetType.Movie);
            // var personresualts = client.GetDataAsObject<MyApiFilms.Entities.Person>("Robert diNero", DataSetType.Person);

            for(int i=0; i < 5; i++)
            {
                var newMovie = new Movie
                {
                    Id = movieresualts.IdIMDB,
                    Title = movieresualts.Title,
                    Plot = movieresualts.Plot,
                    Rated = movieresualts.Rated,
                    Rating = movieresualts.Rating,
                    Genres =  movieresualts.Genres,
                    FilmingLocations = movieresualts.FilmingLocations,
                    Countries = movieresualts.Countries,
                    Languages = movieresualts.Languages,
                    Year = movieresualts.Year

                };
                movieresualts.Actors.Select(async(x) => await personservice.AddPersonAsync(new Person
                {
                    Name = x.ActorName,
                    Id = x.ActorId
                   
                }
                ));
                movieresualts.Directors.Select(async (x) => await personservice.AddPersonAsync(new Person
                {
                    Name = x.Name,
                    Id = x.NameId

                }
              ));
                movieresualts.Writers.Select(async (x) => await personservice.AddPersonAsync(new Person
                {
                    Name = x.Name,
                    Id = x.NameId

                }
                ));
            }
        }
    }
}
