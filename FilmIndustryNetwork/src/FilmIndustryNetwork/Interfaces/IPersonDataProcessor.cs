using System.Collections.Generic;
using FilmIndustryNetwork.MyApiFilms.Entities;
using FilmIndustryNetwork.Utilities;

namespace FilmIndustryNetwork.Interfaces
{
    interface IPersonDataProcessor
    {
        Dictionary<string, object> ExtractDataFromPersonObject(Person person);

        List<Entities.Movie> ExtractMoviesFromPersonObject(Person person, ExtractType type);

        Entities.Person ExtractImportantDataFromPersonObject(Person person);
    }
}
