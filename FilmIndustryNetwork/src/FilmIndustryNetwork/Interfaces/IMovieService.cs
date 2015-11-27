using System.Collections.Generic;
using System.Threading.Tasks;
using FilmIndustryNetwork.Utilities;
using FilmIndustryNetwork.Entities;


namespace FilmIndustryNetwork.Interfaces
{
    public interface IMovieService
    {
        Task RunRawQuery(string query);
        Task AddMovieAsync(Movie movie);
        Task UpdateMovieAsync(Movie movie);
        Task AddOrUpdateMovieWithRelationAsync(Movie movie, Person person, string relationType);
        Task<Movie> GetMovieByIdAsync(string id);
        Task<Movie> GetMovieByTitleAndYearAsync(string title, string year);
    }
}
