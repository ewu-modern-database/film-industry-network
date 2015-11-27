using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FilmIndustryNetwork.MyApiFilms.Entities;

namespace FilmIndustryNetwork.MyApiFilms
{
    public class MovieResponse
    {
        public MovieData Data { get; set; }
        public About About { get; set; }
    }

    public class MovieData
    {
        public List<Movie> Movies { get; set; } 
    }

    public class About
    {
        public string Version { get; set; }
    }
}
