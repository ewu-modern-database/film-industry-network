using System;
using System.Collections.Generic;
using System.Linq;
using FilmIndustryNetwork.Entities;
using FilmIndustryNetwork.Interfaces;
using FilmIndustryNetwork.Utilities;
using Movie = FilmIndustryNetwork.MyApiFilms.Entities.Movie;
using Neo4jClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace FilmIndustryNetwork.Services
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepo;

        public PersonService(IPersonRepository personRepo)
        {
            _personRepo = personRepo;
        }

        public async Task AddPersonAsync(Person person)
        {
            ThrowIfPersonNull(person);
            if ((await GetPersonByNameAsync(person.Name)) != null)
            {
                await UpdatePersonAsync(person);
                return;
            }
            await _personRepo.Add(person);
        }

        public async Task UpdatePersonAsync(Person person)
        {
            ThrowIfPersonNull(person);
            await _personRepo.Update(person);
        }

        public async Task<Person> GetPersonByNameAsync(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));
            return await _personRepo.Get(name);
        }

        public async Task<object> GetDegreesOfSeparation(Person startingPerson, Person endingPerson)
        {
            if (startingPerson == null)
                throw new ArgumentNullException(nameof(startingPerson));
            if (endingPerson == null)
                throw new ArgumentNullException(nameof(endingPerson));

            var results = await _personRepo.GetPath(startingPerson, endingPerson);
            var r = results;
            return r;
        }

        private void ThrowIfPersonNull(Person person)
        {
            if (person == null)
                throw new ArgumentNullException(nameof(person));
            if (string.IsNullOrEmpty(person.Name))
                throw new ArgumentNullException(nameof(person.Name));
            if (string.IsNullOrEmpty(person.Id))
                throw new ArgumentNullException(nameof(person.Id));
        }
    }


}
