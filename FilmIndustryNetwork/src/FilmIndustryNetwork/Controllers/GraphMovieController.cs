using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using FilmIndustryNetwork.Entities.Graph;
using FilmIndustryNetwork.Interfaces;
using FilmIndustryNetwork.ViewModels;
using Microsoft.AspNet.Mvc;
using Neo4jClient;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace FilmIndustryNetwork.Controllers
{
    [Route("graph/movie")]
    public class GraphMovieController : Controller
    {
        private readonly IGraphMovieService _graphMovieService;
        private readonly IMovieService _movieService;

        public GraphMovieController(IGraphMovieService graphMovieService, IMovieService movieService)
        {
            _graphMovieService = graphMovieService;
            _movieService = movieService;
        }

        // GET graph/2011/Iron Man/g
        // GET graph/2011/Iron Man/n
        [HttpGet("{year}/{title}/{format}")]
        public async Task<IActionResult> Get(string title, int year, string format)
        {
            switch (format)
            {
                case "g":
                    {
                        var results = await _graphMovieService.GetMovieGraphByTitleAndYearAsync(title, Convert.ToString(year));
                        var model = new GraphViewModel(results);
                        return Ok(model);
                    }
                case "n":
                    {
                        var results = await _movieService.GetMovieByTitleAndYearAsync(title, Convert.ToString(year));
                        return Ok(results);
                    }
            }
            return HttpBadRequest("Invalid Format");
        }

        
    }
}
