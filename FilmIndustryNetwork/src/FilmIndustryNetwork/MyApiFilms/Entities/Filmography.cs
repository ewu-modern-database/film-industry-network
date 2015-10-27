using System.Collections.Generic;

namespace FilmIndustryNetwork.MyApiFilms.Entities
{
    public class Filmographies
    {
        public List<Film> Filmography { get; set; }
        public string Section { get; set; }
    }

    public class Film
    {
        public string IMDBId { get; set; }
        public List<string> Remarks { get; set; }
        public string Title { get; set; }
        public string Year { get; set; }
    }
}
