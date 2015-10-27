using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FilmIndustryNetwork.Entities;
using FilmIndustryNetwork.Entities.RawGraph;
using Neo4jClient.Cypher;

namespace FilmIndustryNetwork.Interfaces
{
    public interface IPersonRepository
    {
        Task<Person> Get(string name);
        Task<List<RawResult>> GetPath(Person startingPerson, Person endingPerson);
        Task Add(Person person);
        Task Update(Person person);
    }
}
