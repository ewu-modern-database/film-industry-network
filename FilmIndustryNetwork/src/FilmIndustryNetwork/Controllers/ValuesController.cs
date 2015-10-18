using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FilmIndustryNetwork.MyApiFilms;
using FilmIndustryNetwork.MyApiFilms.Enities;
using FilmIndustryNetwork.Services;
using Microsoft.AspNet.Mvc;

namespace FilmIndustryNetwork.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
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
        public IActionResult GetActor(string name)
        {
            var client = new MyApiFilmsClient();
            var result = client.GetDataAsObject<List<Person>>(name, DataSetType.Person);
            var personDataProcessor = new PersonDataProcessor();
            var resp = personDataProcessor.ExtractDataFromPersonObject(result[0]);
            return Ok(resp);
        }

        [HttpGet("movie/{title}")]
        public IActionResult GetMovie(string title)
        {
            var client = new MyApiFilmsClient();
            var result = client.GetDataAsObject<List<Movie>>(title, DataSetType.Movie);
            return Ok(result);
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
