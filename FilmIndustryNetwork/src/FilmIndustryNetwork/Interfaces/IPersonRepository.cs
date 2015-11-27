using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using FilmIndustryNetwork.Entities;
using FilmIndustryNetwork.Entities.Graph;
using Neo4jClient.Cypher;

namespace FilmIndustryNetwork.Interfaces
{
    public interface IPersonRepository
    {
        Task<Person> Get(Expression<Func<Person, bool>> expression);
        Task<IEnumerable<MixedResult>> GetGraph(Expression<Func<Person, bool>> expression);
        Task<List<MixedResult>> GetPath(Person startingPerson, Person endingPerson);
        Task Add(Person person);
        Task Update(Person person);
    }
}
