using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FilmIndustryNetwork.Interfaces;
using FilmIndustryNetwork.Entities;
using FilmIndustryNetwork.Entities.RawGraph;
using FilmIndustryNetwork.Utilities;
using Neo4jClient.Cypher;
using Neo4jClient.Execution;
using Neo4jClient;

namespace FilmIndustryNetwork.Services
{
    public class PersonRepository : IPersonRepository
    {
        private readonly IContext _db;

        public PersonRepository(IContext context)
        {
            _db = context;
        }

        public Task<Person> Get(string name)
        {
            return Task.FromResult(_db.PersonMatch()
                .Where((Person person) => person.Name == name)
                .Return(person => person.As<Person>())
                .Results
                .FirstOrDefault());
        }

        public Task<List<RawResult>> GetPath(Person startingPerson, Person endingPerson)
        {
            return Task.FromResult(_db
                .Match($"x=shortestPath((start:Person)" +
                       $"-[*]-(end:Person))")
                .Where((Person start) => start.Name == startingPerson.Name)
                .AndWhere((Person end) => end.Name == endingPerson.Name)
                .Return(x => new RawResult
                {
                    Nodes = Return.As<IEnumerable<Node<Data>>>("nodes(x)"),
                    Relationships = Return.As<IEnumerable<RelationshipInstance<object>>>("rels(x)")
                })
                .Results
                .ToList());
        }

        public async Task Add(Person person)
        {
            await _db.PersonCreate()
                .WithParam("newPerson", person)
                .ExecuteWithoutResultsAsync();
        }

        public async Task Update(Person updatedPerson)
        {
            await _db.PersonMatch()
                .Where((Person person) => person.Id == updatedPerson.Id)
                .Set("person = {updatedPerson}")
                .WithParam("updatedPerson", updatedPerson)
                .ExecuteWithoutResultsAsync();
        }
    }
}
