using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FilmIndustryNetwork.Entities;
using FilmIndustryNetwork.Entities.Graph;
using FilmIndustryNetwork.Interfaces;
using FilmIndustryNetwork.Utilities;

namespace FilmIndustryNetwork.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepo;

        public MovieService(IMovieRepository movieRepo)
        {
            _movieRepo = movieRepo;
        }

        public async Task RunRawQuery(string query)
        {
            await _movieRepo.RawQuery(query);
        }

        public async Task AddMovieAsync(Movie movie)
        {
            ThrowIfMovieNull(movie);
            await _movieRepo.Add(movie);
        }

        public async Task UpdateMovieAsync(Movie movie)
        {
            ThrowIfMovieNull(movie);
            if ((await GetMovieByTitleAndYearAsync(movie.Title, movie.Year)) == null)
            {
                await AddMovieAsync(movie);
                return;
            }
            await _movieRepo.Update(movie);
        }

        public async Task AddOrUpdateMovieWithRelationAsync(Movie movie, Person person, string relationType)
        {
            ThrowIfMovieNull(movie);
            ThrowIfPersonNull(person);
            if ((await GetMovieByTitleAndYearAsync(movie.Title, movie.Year)) == null)
            {
                await _movieRepo.AddWithRelationToPerson(movie, person, relationType);
                return;
            }
            //await UpdateMovieAsync(movie);
            await _movieRepo.AddRelationToPerson(movie, person, relationType);
        }

        public async Task<Movie> GetMovieByIdAsync(string id)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id));
            return await _movieRepo.Get((Movie movie) => movie.Id == id);
        }

        public async Task<Movie> GetMovieByTitleAndYearAsync(string title, string year)
        {
            if (title == null)
                throw new ArgumentNullException(nameof(title));
            if (year == null)
                throw new ArgumentNullException(nameof(year));
            return await _movieRepo.Get((Movie movie) => movie.Title == title && movie.Year == year);
        }

        private void ThrowIfMovieNull(Movie movie)
        {
            if (movie == null)
                throw new ArgumentNullException(nameof(movie));
            if (string.IsNullOrEmpty(movie.Title))
                throw new ArgumentNullException(nameof(movie.Title));
            if (string.IsNullOrEmpty(movie.Id))
                throw new ArgumentNullException(nameof(movie.Id));
        }

        private void ThrowIfPersonNull(Person person)
        {
            if (person == null)
                throw new ArgumentNullException(nameof(person));
            if (string.IsNullOrEmpty(person.Name))
                throw new ArgumentNullException(nameof(person.Name));
            if (string.IsNullOrEmpty(person.Id))
                throw new ArgumentNullException(nameof(person.Id));
        }
    }
}
