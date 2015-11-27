using FilmIndustryNetwork.Interfaces;
using Microsoft.Framework.OptionsModel;

namespace FilmIndustryNetwork.Utilities
{
    public interface IDataCollectorFactory
    {
        IDataCollector CreateNewInstance(IMovieService movieService, IPersonService personService, IOptions<AppSettings> options);
    }
}