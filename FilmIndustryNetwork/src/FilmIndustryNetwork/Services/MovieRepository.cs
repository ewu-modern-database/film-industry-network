using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FilmIndustryNetwork.Interfaces;
using FilmIndustryNetwork.Entities;
using FilmIndustryNetwork.Utilities;
using Neo4jClient;

namespace FilmIndustryNetwork.Services
{
    public class MovieRepository : IMovieRepository
    {
        private readonly IContext _db;

        public MovieRepository(IContext context)
        {
            _db = context;
        }

        public Task<Movie> Get(string title, string year)
        {
            return Task.FromResult(_db.MovieMatch()
                .Where((Movie movie) => movie.Title == title && movie.Year == year)
                .Return(movie => movie.As<Movie>())
                .ResultsAsync
                .Result
                .FirstOrDefault());
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
            var query = _db.MovieMatch()
                .Where((Movie movie) => movie.Id == updatedMovie.Id)
                .Set("movie = {updatedMovie}")
                .WithParam("updatedMovie", updatedMovie);
            var q = query.Query;
            await query.ExecuteWithoutResultsAsync();
        }

        public async Task AddRelationToPerson(Movie existingMovie, Person existingPerson, string relationType)
        {
            var query = _db.Match($"(movie:Movie)",
                $"(person:Person)")
                .Where((Movie movie) => movie.Id == existingMovie.Id)
                .AndWhere((Person person) => person.Id == existingPerson.Id)
                .CreateUnique(
                    $"person-[:{relationType}]->movie");

            var q = query.Query;
            await query.ExecuteWithoutResultsAsync();

        }

    }
}
