using System;
using System.Collections.Generic;
using System.Linq;
using FilmIndustryNetwork.Entities;
using FilmIndustryNetwork.Interfaces;
using FilmIndustryNetwork.Utilities;

namespace FilmIndustryNetwork.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepo;

        public Movie CreateMovie(string Title, string Plot, string Rated, string Rating, List<string> Genres, List<string> FilmingLocations, List<string> Countries, List<string> Languages, string Year)
        {
            if (Title == null)
            {
                throw new ArgumentNullException(nameof(Title));
            }



            var movie = new Movie
            {
                Title = Title,
                Plot = Plot,
                Rated = Rated,
                Rating = Rating,
                Genres = Genres,
                FilmingLocations = FilmingLocations,
                Countries = Countries,
                Languages = Languages,
                Year = Year,


            };
            _movieRepo.Add(movie);
            return movie;
        }

        public Movie UpdateMovie(string Title, string Plot, string Rated, string Rating, List<string> Genres, List<string> FilmingLocations, List<string> Countries, List<string> Languages, string Year)
        {

        }

        public Movie GetMovie(string Title, string Year)

        {
            return _movieRepo.Get(Title, Year);
        }

        public void DeleteMovie(string Title, string Year)
        {
            return _movieRepo.Delete(Title, Year);
        }

    }


}
