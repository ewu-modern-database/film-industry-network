using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmIndustryNetwork.Entities.Graph
{
    public class MixedData
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Plot { get; set; }
        public string Rated { get; set; }
        public string Rating { get; set; }
        public List<string> Genres { get; set; }
        public List<string> FilmingLocations { get; set; }
        public List<string> Countries { get; set; }
        public List<string> Languages { get; set; }
        public string Year { get; set; }
        public string Name { get; set; }
        public string BirthName { get; set; }
        public string Bio { get; set; }
        public string PlaceOfBirth { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string UrlPhoto { get; set; }
    }
}
