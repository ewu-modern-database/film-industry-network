using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FilmIndustryNetwork.Entities;
using FilmIndustryNetwork.Interfaces;
using FilmIndustryNetwork.MyApiFilms;
using FilmIndustryNetwork.Services;
using FilmIndustryNetwork.Utilities;
using Microsoft.AspNet.Mvc;
using Movie = FilmIndustryNetwork.Entities.Movie;

namespace FilmIndustryNetwork.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly IPersonService _personService;
        private readonly IMovieService _movieService;

        public ValuesController(IPersonService personService, IMovieService movieService)
        {
            _personService = personService;
            _movieService = movieService;
        }
        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpGet("actor/{name}")]
        public async Task<IActionResult> GetActor(string name)
        {
            var r = await _personService.GetDegreesOfSeparation(new Person {Name = "Will Czifro"}, new Person {Name = "Aiden Buckles"});
            //var p = await _personService.GetPersonByNameAsync("Will Czifro");

            return Ok(r);
        }

        [HttpPost("actor")]
        public async Task<IActionResult> AddActor()
        {
            Person p = new Person
            {
                Name = "Aiden Buckles",
                Id = "2345"
            };

            await _personService.AddPersonAsync(p);
            var r = await _personService.GetPersonByNameAsync("Will Czifro");

            return Ok(r);
        }

        [HttpGet("movie/{title}")]
        public async Task<IActionResult> GetMovie(string title)
        {
            //var client = new MyApiFilmsClient();
            var result = await _movieService.GetMovieByTitleAndYearAsync("Awesome Movie", "2015");
            return Ok(result);
        }

        [HttpPost("movie")]
        public async Task<IActionResult> AddMovie()
        {
            Movie movie = new Movie
            {
                Title = "Awesome Movie",
                Year = "2014",
                Id = "12345"
            };

            await
                _movieService.AddOrUpdateMovieWithRelationAsync(movie,
                   (await _personService.GetPersonByNameAsync("Will Czifro")), RelationTypes.ActedIn);
            var m = await _movieService.GetMovieByTitleAndYearAsync(movie.Title, movie.Year);

            return Ok(m);
        }

            // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
