using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FilmIndustryNetwork.Entities;
using FilmIndustryNetwork.Entities.Graph;

namespace FilmIndustryNetwork.Interfaces
{
    public interface IGraphPersonService
    {

        Task<IEnumerable<MixedResult>> GetPersonGraphByNameAsync(string name);

        Task<IEnumerable<MixedResult>> GetPersonGraphByBirthYearAsync(int year);

        Task<IEnumerable<MixedResult>> GetPersonGraphByBirthDateAsync(DateTime birthDate);

        Task<List<MixedResult>> GetDegreesOfSeparationGraph(Person startingPerson, Person endingPerson);
    }
}
