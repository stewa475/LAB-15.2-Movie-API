using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LAB_15._2_MovieAPI.Models;
using LAB_15._2_MovieAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LAB_15._2_MovieAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private IDAL dal;

        public MovieController(IDAL dalObject)
        {
            dal = dalObject;
        }

        [HttpDelete]
        public Object Delete(int id)
        {
            int result = dal.DeleteMovieById(id);

            if (result > 0)
            {
                return new { sucess = true };
            }
            else
            {
                return new { success = false };
            }
        }

        [HttpGet("{id}")]
        public Movie GetSingleMovie(int id)
        {
            Movie m = dal.GetMovieById(id);
            return m; //serialize the parameter into JSON and return an Ok (20x)
        }

        [HttpGet]
        public IEnumerable<Movie> Get(string category = null)
        {
            if (category == null)
            {
                IEnumerable<Movie> movies = dal.GetMoviesAll();
                return movies; //serialize the parameter into JSON and return an Ok (20x)
            }
            else
            {
                IEnumerable<Movie> movies = dal.GetMoviesByCategory(category);
                return movies;
            }
        }

        [HttpGet("random")]
        public IEnumerable<Movie> GetRandom(string category = null)
        {
            if (category == null)
            {
                IEnumerable<Movie> movies = dal.GetRandomMovie();
                return movies; //serialize the parameter into JSON and return an Ok (20x)
            }
            else
            {
                IEnumerable<Movie> Movies = dal.GetRandomMoviesByCategory(category);
                return Movies;
            }
        }

        //valid  but superceded by Category controller
        [HttpGet("categories")]
        public string[] GetCategories()
        {
            return dal.GetMovieCategories();
        }

        [HttpPost]
        public Object Post(Movie m)
        {
            int newId = dal.CreateMovie(m);

            if (newId < 0)
            {
                return new { success = false };
            }
            return new { status = true, id = newId };
        }

        
    }
}