using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FilmIndustryNetwork.Entities;
using FilmIndustryNetwork.Entities.Graph;
using FilmIndustryNetwork.Interfaces;

namespace FilmIndustryNetwork.Services
{
    public class GraphPersonService : IGraphPersonService
    {
        private readonly IPersonRepository _personRepo;

        public GraphPersonService(IPersonRepository personRepo)
        {
            _personRepo = personRepo;
        }

        public async Task<IEnumerable<MixedResult>> GetPersonGraphByNameAsync(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));
            return await _personRepo.GetGraph((Person person) => person.Name == name);
        }

        public async Task<IEnumerable<MixedResult>> GetPersonGraphByBirthYearAsync(int year)
        {
            return await _personRepo.GetGraph((Person person) => person.DateOfBirth.Year == year);
        }

        public async Task<IEnumerable<MixedResult>> GetPersonGraphByBirthDateAsync(DateTime birthDate)
        {
            return await _personRepo.GetGraph((Person person) => person.DateOfBirth == birthDate);
        }

        public async Task<List<MixedResult>> GetDegreesOfSeparationGraph(Person startingPerson, Person endingPerson)
        {
            if (startingPerson == null)
                throw new ArgumentNullException(nameof(startingPerson));
            if (endingPerson == null)
                throw new ArgumentNullException(nameof(endingPerson));

            var results = await _personRepo.GetPath(startingPerson, endingPerson);
            var r = results;
            return r;
        }
    }
}
