using System.Collections.Generic;
using FilmIndustryNetwork.MyApiFilms.Entities;
using FilmIndustryNetwork.Utilities;

namespace FilmIndustryNetwork.Interfaces
{
    interface IMovieDataProcessor
    {
        Dictionary<string, object> ExtractDataFromMovieObject(Movie movie);

        List<Entities.Person> ExtractPeopleFromMovieObject(Movie movie, ExtractType type);

        Entities.Movie ExtractImportantDataFromMovieObject(Movie movie);
    }
}
