using System.Collections.Generic;
using FilmIndustryNetwork.Utilities;
using FilmIndustryNetwork.Entities;


namespace FilmIndustryNetwork.Interfaces
{
    public interface IMovieService
    {
        Movie CreateMovie(string Title, string Plot, string Rated, string Rating, List<string> Genres, List<string> FilmingLocations, List<string> Countries, List<string> Languages, string Year);
        Movie UpdateMovie(string Title, string Plot, string Rated, string Rating, List<string> Genres, List<string> FilmingLocations, List<string> Countries, List<string> Languages, string Year);
        Movie GetMovie(string Title, string Year);
        void DeleteMovie(string Title, string Year);

    }
}
