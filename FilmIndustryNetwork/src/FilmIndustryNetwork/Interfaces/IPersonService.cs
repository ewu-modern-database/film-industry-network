using System.Collections.Generic;
using System.Threading.Tasks;
using FilmIndustryNetwork.Utilities;
using FilmIndustryNetwork.Entities;
using FilmIndustryNetwork.Entities.Graph;


namespace FilmIndustryNetwork.Interfaces
{
    public interface IPersonService
    {
        Task AddPersonAsync(Person person);
        Task UpdatePersonAsync(Person person);
        Task<Person> GetPersonByNameAsync(string name);
        Task<List<MixedResult>> GetDegreesOfSeparation(Person startingPerson, Person endingPerson);
    }
}
