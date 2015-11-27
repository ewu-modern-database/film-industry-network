using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FilmIndustryNetwork.Entities;
using FilmIndustryNetwork.Interfaces;
using FilmIndustryNetwork.ViewModels;
using Microsoft.AspNet.Mvc;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace FilmIndustryNetwork.Controllers
{
    [Route("graph/person")]
    public class GraphPersonController : Controller
    {
        private readonly IGraphPersonService _graphPersonService;
        private readonly IPersonService _personService;

        public GraphPersonController(IGraphPersonService graphPersonService, IPersonService personService)
        {
            _graphPersonService = graphPersonService;
            _personService = personService;
        }

        // GET graph/person/Christian Bale/
        [HttpGet("{name}")]
        public async Task<IActionResult> Get(string name)
        {
            var person = await _personService.GetPersonByNameAsync(name);
            return Ok(person);
        }

        // GET graph/person/Christian Bale/filmography
        [HttpGet("{name}/filmography")]
        public async Task<IActionResult> GetFilmography(string name)
        {
            var results = await _graphPersonService.GetPersonGraphByNameAsync(name);
            var model = new GraphViewModel(results);
            return Ok(model);
        }

        // GET graph/person/1987
        [HttpGet("{year}")]
        public async Task<IActionResult> GetByBirthYear(int year)
        {
            var results = await _graphPersonService.GetPersonGraphByBirthYearAsync(year);
            var model = new GraphViewModel(results);
            return Ok(model);
        }

        // GET graph/person/1987/5/21
        [HttpGet("{birthDate:datetime:regex(\\d{{4}}/\\d{{2}}/\\d{{2}})}")]
        public async Task<IActionResult> GetByBirthDate(DateTime birthDate)
        {
            var results = await _graphPersonService.GetPersonGraphByBirthDateAsync(birthDate);
            var model = new GraphViewModel(results);
            return Ok(model);
        }

        // GET graph/person/Christian Bale/Jon Burton
        [HttpGet("degrees/{from}/{to}")]
        public async Task<IActionResult> GetDegreesOfSeparation(string from, string to)
        {
            var results =
                await _graphPersonService.GetDegreesOfSeparationGraph(new Person {Name = from}, new Person {Name = to});
            var model = new GraphViewModel(results);
            return Ok(model);
        } 
    }
}
