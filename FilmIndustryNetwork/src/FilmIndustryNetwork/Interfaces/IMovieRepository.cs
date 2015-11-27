using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using FilmIndustryNetwork.Entities;
using FilmIndustryNetwork.Entities.Graph;

namespace FilmIndustryNetwork.Interfaces
{

    public interface IMovieRepository
    {
        Task RawQuery(string query);
        Task<Movie> Get(Expression<Func<Movie, bool>> expression);
        Task<IEnumerable<MixedResult>> GetGraph(Expression<Func<Movie, bool>> expression);
        Task Add(Movie movie);
        Task AddWithRelationToPerson(Movie movie, Person person, string relationType);
        Task Update(Movie movie);
        Task AddRelationToPerson(Movie movie, Person person, string relationType);
    }

}

