using System.Collections.Generic;
using FilmIndustryNetwork.Utilities;

namespace FilmIndustryNetwork.Interfaces
{
    interface IPersonDataProcessor
    {
        Dictionary<string, object> ExtractDataFromPersonObject(MyApiFilms.Enities.Person person);

        List<Entities.Movie> ExtractMoviesFromPersonObject(MyApiFilms.Enities.Person person, ExtractType type);

        Entities.Person ExtractImportantDataFromPersonObject(MyApiFilms.Enities.Person person);
    }
}
