using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmIndustryNetwork.MyApiFilms.Enities
{
    public class SalaryWrapper
    {
        public SalaryFilm NameId { get; set; }
        public string Salary { get; set; }
        public string Year { get; set; }
    }

    public class SalaryFilm
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
