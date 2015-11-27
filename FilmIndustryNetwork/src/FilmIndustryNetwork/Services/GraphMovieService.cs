using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FilmIndustryNetwork.Entities;
using FilmIndustryNetwork.Entities.Graph;
using FilmIndustryNetwork.Interfaces;

namespace FilmIndustryNetwork.Services
{
    public class GraphMovieService : IGraphMovieService
    {
        private readonly IMovieRepository _movieRepo;

        public GraphMovieService(IMovieRepository movieRepo)
        {
            _movieRepo = movieRepo;
        }

        public async Task<IEnumerable<MixedResult>> GetMovieGraphByTitleAndYearAsync(string title, string year)
        {
            if (title == null)
                throw new ArgumentNullException(nameof(title));
            if (year == null)
                throw new ArgumentNullException(nameof(year));
            return await _movieRepo.GetGraph((Movie m) => m.Title == title && m.Year == year);
        }

        public async Task<MovieResult> GetMovieGraphById(string id)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id));
            return null;// await _movieRepo.GetGraph((Movie m) => m.Id == id);
        }
    }
}
