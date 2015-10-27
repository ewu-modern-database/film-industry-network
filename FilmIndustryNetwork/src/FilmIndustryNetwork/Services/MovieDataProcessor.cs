using System;
using System.Collections.Generic;
using System.Linq;
using FilmIndustryNetwork.Entities;
using FilmIndustryNetwork.Interfaces;
using FilmIndustryNetwork.Utilities;
using Movie = FilmIndustryNetwork.MyApiFilms.Entities.Movie;

namespace FilmIndustryNetwork.Services
{
    public class MovieDataProcessor : IMovieDataProcessor
    {
        /// <summary>
        /// Use this only to see what can be extracted
        /// </summary>
        public Dictionary<string, object> ExtractDataFromMovieObject(Movie movie)
        {
            var people = new List<Entities.Person>();

            var m = new Entities.Movie
            {
                FilmingLocations = movie.FilmingLocations,
                Genres = movie.Genres,
                Countries = movie.Countries,
                Languages = movie.Languages,
                Plot = movie.Plot,
                Rated = movie.Rated,
                Title = movie.Title,
                Rating = movie.Rating,
                Year = movie.Year
            };

            people.AddRange(movie.Actors.Select(a => new Person {Name = a.ActorName}));
            people.AddRange(movie.Directors.Select(d => new Person {Name = d.Name}));
            people.AddRange(movie.Writers.Select(w => new Person {Name = w.Name}));

            return new Dictionary<string, object>
            {
                {"Movie", m},
                {"People", people}
            };
        }

        public List<Person> ExtractPeopleFromMovieObject(Movie movie, ExtractType type)
        {
            List<Entities.Person> people;

            switch (type)
            {
                case ExtractType.Actor:
                    people = movie.Actors.Select(x => new Person {Name = x.ActorName}).ToList();
                    break;
                case ExtractType.Director:
                    people = movie.Directors.Select(x => new Person {Name = x.Name}).ToList();
                    break;
                case ExtractType.Writer:
                    people = movie.Writers.Select(x => new Person {Name = x.Name}).ToList();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }

            return people;
        }

        public Entities.Movie ExtractImportantDataFromMovieObject(Movie movie)
        {
            return new Entities.Movie
            {
                FilmingLocations = movie.FilmingLocations,
                Genres = movie.Genres,
                Countries = movie.Countries,
                Languages = movie.Languages,
                Plot = movie.Plot,
                Rated = movie.Rated,
                Title = movie.Title,
                Rating = movie.Rating,
                Year = movie.Year
            };
        }
    }

    
}
