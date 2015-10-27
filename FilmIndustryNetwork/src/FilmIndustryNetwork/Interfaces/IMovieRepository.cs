using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FilmIndustryNetwork.Entities;
namespace FilmIndustryNetwork.Interfaces
{

    public interface IMovieRepository
    {
        Task<Movie> Get(string title, string year);
        Task Add(Movie movie);
        Task AddWithRelationToPerson(Movie movie, Person person, string relationType);
        Task Update(Movie movie);
        Task AddRelationToPerson(Movie movie, Person person, string relationType);
    }

}

