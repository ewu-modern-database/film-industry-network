using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FilmIndustryNetwork.Interfaces;
using FilmIndustryNetwork.Entities;
namespace FilmIndustryNetwork.Services
{
    public class MovieRepository : IMovieRepository
    {
        Movie Get(string Title, string Year)
        {
            var client = new GraphClient(new Uri("http://localhost:7474/db/data"));
            client.Connect();
            client.Cypher
            .Match("(movie:Movie)")
            .Where((Movie movie) => movie.Title == Title && movie.Year == Year)
            .Return(movie => movie.As<Movie>())
            .Results
        }

        void Add(Movie movie)
        {
          
        }

        void Delete(string Title, string Year)
        {
            var client = new GraphClient(new Uri("http://localhost:7474/db/data"));
            client.Connect();
            client.Cypher
            .Match("(movie:Movie)")
            .Where((Movie movie) => movie.Title == Title && movie.Year == Year)
            .Delete("movie")
            .ExecuteWithoutResults();
        }

    }
}
