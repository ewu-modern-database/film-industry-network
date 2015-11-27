using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using FilmIndustryNetwork.Interfaces;
using FilmIndustryNetwork.Entities;
using FilmIndustryNetwork.Entities.Graph;
using Neo4jClient.Cypher;
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

        public Task<Person> Get(Expression<Func<Person, bool>> expression)
        {
            return Task.FromResult(_db.PersonMatch()
                .Where(expression)
                .Return(person => person.As<Person>())
                .Results
                .FirstOrDefault());
        }

        public Task<IEnumerable<MixedResult>> GetGraph(Expression<Func<Person, bool>> expression)
        {
            return Task.FromResult(_db.PersonMatchWithRelationships()
                .Where(expression)
                .Return(person => new MixedResult
                {
                    Nodes = Return.As<IEnumerable<Node<MixedData>>>("nodes(x)"),
                    Relationships = Return.As<IEnumerable<RelationshipInstance<object>>>("rels(x)")
                })
                .Results);
        }

        public Task<List<MixedResult>> GetPath(Person startingPerson, Person endingPerson)
        {
            var query = _db
                .Match("x=shortestPath((start:Person)" +
                       "-[*]-(end:Person))")
                .Where((Person start) => start.Name == startingPerson.Name)
                .AndWhere((Person end) => end.Name == endingPerson.Name)
                .Return(x => new MixedResult
                {
                    Nodes = Return.As<IEnumerable<Node<MixedData>>>("nodes(x)"),
                    Relationships = Return.As<IEnumerable<RelationshipInstance<object>>>("rels(x)")
                });

            var str = query.Query.QueryText;

            return Task.FromResult(query
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
            await _db.PersonMerge()
                .Where((Person person) => person.Id == updatedPerson.Id)
                .OnCreate()
                .Set("person = {_updatedPerson}")
                .WithParams(new
                {
                    _updatedPerson = updatedPerson
                })
                .ExecuteWithoutResultsAsync();
        }
    }
}
