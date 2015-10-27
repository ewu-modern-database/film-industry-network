namespace FilmIndustryNetwork.MyApiFilms.Entities
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
