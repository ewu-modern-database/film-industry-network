using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FilmIndustryNetwork.Interfaces;
using FilmIndustryNetwork.Entities;
using Neo4jClient.Cypher;
using Neo4jClient.Execution;
using Neo4jClient;

namespace FilmIndustryNetwork.Services
{
    public class PersonRepository : IPersonRepository
    {
        Person Get(string Name)
        {
            var client = new GraphClient(new Uri("http://localhost:7474/db/data"));
 
            client.Connect();
            client.Cypher
            .Match("(person:Person)")
            .Where((Person person) => person.Name == Name)
            .Return(person => person.As<Person>())
            .Results;
        }
        void Add(Person person)
        {

        }
        void Delete(string id)
        {
            var client = new GraphClient(new Uri("http://localhost:7474/db/data"));
            client.Connect();
            client.Cypher
            .Match("(person:Person)")
            .Where((Person person) => person.Id == id)
            .Delete("person")
            .ExecuteWithoutResults();
        }
    }
}
