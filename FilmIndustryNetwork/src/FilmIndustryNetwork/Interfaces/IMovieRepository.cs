using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FilmIndustryNetwork.Entities;
namespace FilmIndustryNetwork.Interfaces
{

        public interface IMovieRepository
        {
            Movie Get(string Title, string Year);
            void Add(Movie movie);
            void Delete(string Title, string Year);
        }
    
}

