using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FilmIndustryNetwork.Entities;
using FilmIndustryNetwork.Interfaces;
using FilmIndustryNetwork.Utilities;
using Person = FilmIndustryNetwork.MyApiFilms.Entities.Person;

namespace FilmIndustryNetwork.Services
{
    public class PersonDataProcessor : IPersonDataProcessor
    {
        /// <summary>
        /// Use this only to see what can be extracted
        /// </summary>
        public Dictionary<string, object> ExtractDataFromPersonObject(Person person)
        {
            var movies = new List<Entities.Movie>();

            var p = new Entities.Person
            {
                Bio = person.Bio,
                BirthName = person.BirthName,
                Name = person.Name,
                PlaceOfBirth = person.PlaceOfBirth,
                DateOfBirth = person.DateOfBirth,
                UrlPhoto = person.UrlPhoto
            };

            movies.AddRange(
                person.Filmographies.SelectMany(
                    f => f.Filmography.Select(film => new Movie {Title = film.Title, Year = film.Year}).ToList())
                    .ToList());
            return new Dictionary<string, object>
            {
                {"Person", p},
                {"Movies", movies}
            };
        }

        public List<Movie> ExtractMoviesFromPersonObject(Person person, ExtractType type)
        {
            var filter = (type == ExtractType.Actor
                ? "Actor"
                : (type == ExtractType.Director
                    ? "Director" : "Writer"));
            return
                person.Filmographies.Where(x => x.Section == filter)
                    .SelectMany(
                        f => f.Filmography.Select(film => new Movie {Title = film.Title, Year = film.Year}).ToList())
                    .ToList();
        }

        public Entities.Person ExtractImportantDataFromPersonObject(Person person)
        {
            return new Entities.Person
            {
                Bio = person.Bio,
                BirthName = person.BirthName,
                Name = person.Name,
                PlaceOfBirth = person.PlaceOfBirth,
                DateOfBirth = person.DateOfBirth,
                UrlPhoto = person.UrlPhoto
            };
        }
    }

}
