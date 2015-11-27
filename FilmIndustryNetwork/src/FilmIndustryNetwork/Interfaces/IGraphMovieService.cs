using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FilmIndustryNetwork.Entities;
using FilmIndustryNetwork.Entities.Graph;

namespace FilmIndustryNetwork.Interfaces
{
    public interface IGraphMovieService
    {
        Task<IEnumerable<MixedResult>> GetMovieGraphByTitleAndYearAsync(string title, string year);
        Task<MovieResult> GetMovieGraphById(string id);
    }
}
