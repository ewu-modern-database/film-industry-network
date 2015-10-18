using System.Collections.Generic;
using FilmIndustryNetwork.Utilities;

namespace FilmIndustryNetwork.Interfaces
{
    interface IMovieDataProcessor
    {
        Dictionary<string, object> ExtractDataFromMovieObject(MyApiFilms.Enities.Movie movie);

        List<Entities.Person> ExtractPeopleFromMovieObject(MyApiFilms.Enities.Movie movie, ExtractType type);

        Entities.Movie ExtractImportantDataFromMovieObject(MyApiFilms.Enities.Movie movie);
    }
}
