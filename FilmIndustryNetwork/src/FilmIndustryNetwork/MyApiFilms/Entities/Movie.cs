using System.Collections.Generic;

namespace FilmIndustryNetwork.MyApiFilms.Entities
{
    public class Movie
    {
        public List<Actor> Actors { get; set; }
        public List<string> Countries { get; set; }
        public List<Director> Directors { get; set; }
        public List<string> FilmingLocations { get; set; }
        public List<string> Genres { get; set; }
        public string IdIMDB { get; set; }
        public List<string> Languages { get; set; }
        public string Metascore { get; set; }
        public List<string> MovieTrivia { get; set; } 
        public string OriginalTitle { get; set; }
        public string Plot { get; set; }
        public string Rated { get; set; }
        public string Rating { get; set; }
        public string ReleaseDate { get; set; }
        public List<string> Runtime { get; set; }
        public string SimplePlot { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public string UrlIMDB { get; set; }
        public string UrlPoster { get; set; }
        public string Votes { get; set; }
        public List<Writer> Writers { get; set; }
        public string Year { get; set; }  
    }
}
