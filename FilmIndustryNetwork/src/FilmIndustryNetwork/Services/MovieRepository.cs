using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using FilmIndustryNetwork.Interfaces;
using FilmIndustryNetwork.Entities;
using FilmIndustryNetwork.Entities.Graph;
using FilmIndustryNetwork.Utilities;
using Neo4jClient;
using Neo4jClient.Cypher;

namespace FilmIndustryNetwork.Services
{
    public class MovieRepository : IMovieRepository
    {
        private readonly IContext _db;

        public MovieRepository(IContext context)
        {
            _db = context;
        }

        public Task RawQuery(string query)
        {
            _db.RunRawQuery(query);
            return Task.FromResult(Task.Delay(0));
        }

        public Task<Movie> Get(Expression<Func<Movie, bool>> expression)
        {
            return Task.FromResult(_db.MovieMatch()
                .Where(expression)
                .Return(movie => movie.As<Movie>())
                .Results
                .FirstOrDefault());
        }

        public Task<List<Movie>> GetAll(Expression<Func<Movie, bool>> expression)
        {
            return Task.FromResult(_db.MovieMatch()
                .Where(expression)
                .Return(movie => movie.As<Movie>())
                .Results
                .ToList());
        }

        public Task<IEnumerable<MixedResult>> GetGraph(Expression<Func<Movie, bool>> expression)
        {
            return Task.FromResult(_db.MovieMatchWithRelationships()
                .Where(expression)
                .Return(x => new MixedResult
                {
                    Nodes = Return.As<IEnumerable<Node<MixedData>>>("nodes(x)"),
                    Relationships = Return.As<IEnumerable<RelationshipInstance<object>>>("rels(x)")
                })
                .Results
                .ToList()
                .AsEnumerable());
        } 

        public async Task Add(Movie movie)
        {
            await _db.MovieCreate()
                .WithParam("newMovie", movie)
                .ExecuteWithoutResultsAsync();
        }

        public async Task AddWithRelationToPerson(Movie movie, Person existingPerson, string relationType)
        {
            await _db.PersonMatch()
                .Where((Person person) => person.Name == existingPerson.Name)
                .CreateUnique(
                    $"person-[:{relationType}]->(movie:Movie {{newMovie}})")
                .WithParam("newMovie", movie)
                .ExecuteWithoutResultsAsync();
        }

        public async Task Update(Movie updatedMovie)
        {
            var query = _db.MovieMerge()
                .Where((Movie movie) => movie.Id == updatedMovie.Id)
                .OnCreate()
                .Set("movie = {_updatedMovie}")
                .WithParams(new
                {
                    _updatedMovie = updatedMovie
                });
            var q = query.Query;
            await query.ExecuteWithoutResultsAsync();
        }

        public async Task AddRelationToPerson(Movie existingMovie, Person existingPerson, string relationType)
        {
            var query = _db.Match($"(movie:Movie)",
                $"(person:Person)")
                .Where((Movie movie) => movie.Title == existingMovie.Title)
                .AndWhere((Person person) => person.Name == existingPerson.Name)
                .CreateUnique(
                    $"person-[:{relationType}]->movie");

            var q = query.Query;
            await query.ExecuteWithoutResultsAsync();

        }

    }
}
