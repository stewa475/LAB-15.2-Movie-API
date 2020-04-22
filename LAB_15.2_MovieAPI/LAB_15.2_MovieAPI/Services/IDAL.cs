using LAB_15._2_MovieAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LAB_15._2_MovieAPI.Services
{
    public interface IDAL
    {
        public int CreateMovie(Movie p);
        public int DeleteMovieById(int id);
        public IEnumerable<Movie> GetMoviesAll();
        public IEnumerable<Movie> GetMoviesByCategory(string category);
        public string[] GetMovieCategories();
        public Movie GetMovieById(int id);
        public IEnumerable<Movie> GetRandomMovie();
        public IEnumerable<Movie> GetRandomMoviesByCategory(string category);

    }
}
