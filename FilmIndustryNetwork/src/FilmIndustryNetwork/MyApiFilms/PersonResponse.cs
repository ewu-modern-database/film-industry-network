using System.Collections.Generic;
using FilmIndustryNetwork.MyApiFilms.Entities;

namespace FilmIndustryNetwork.MyApiFilms
{
    public class PersonResponse
    {
        public PersonData Data { get; set; }
        public About About { get; set; }
    }

    public class PersonData
    {
        public List<Person> Names { get; set; }
    }
}