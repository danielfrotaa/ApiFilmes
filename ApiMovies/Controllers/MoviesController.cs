using ApiMovies.Models;
using ApiMovies.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiMovies.Controllers
{
    [ApiController]
    [Route("/api")]
    public class MoviesController : ControllerBase
    {
        private GetMoviesService _getMoviesService;

        public MoviesController(GetMoviesService getMoviesService)
        {
            _getMoviesService = getMoviesService;
        }

        [HttpGet("movies/count")]
        public object Get([FromQuery] string Title)
        {
            return _getMoviesService.Get(Title);
        }
    }
}
