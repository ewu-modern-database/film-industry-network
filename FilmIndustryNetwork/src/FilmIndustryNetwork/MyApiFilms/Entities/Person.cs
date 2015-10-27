using System.Collections.Generic;

namespace FilmIndustryNetwork.MyApiFilms.Entities
{
    public class Person
    {
        public string ActorActress { get; set; }
        public string Bio { get; set; }
        public string BirthName { get; set; }
        public string DateOfBirth { get; set; }
        public List<Filmographies> Filmographies { get; set; }
        public string Height { get; set; }
        public string IdIMDB { get; set; }
        public string Name { get; set; }
        public string Nickname { get; set; }
        public string PlaceOfBirth { get; set; }
        public string UrlPhoto { get; set; }
        public List<string> PersonalQuotes { get; set; } 
        public List<SalaryWrapper> Salaries { get; set; }
        public List<string> Spouses { get; set; }
        public List<string> TradeMark { get; set; }
        public List<string> Trivia { get; set; }
        public string StarMeter { get; set; }
        public string StarSign { get; set; }  
    }
}
