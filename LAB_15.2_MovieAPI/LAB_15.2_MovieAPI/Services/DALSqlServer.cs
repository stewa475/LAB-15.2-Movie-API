using Dapper;
using LAB_15._2_MovieAPI.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace LAB_15._2_MovieAPI.Services
{
    public class DALSqlServer : IDAL
    {

        private string connectionString;

        public DALSqlServer(IConfiguration config)
        {
            connectionString = config.GetConnectionString("MovieDB");
        }

        public int CreateMovie(Movie p)
        {
            SqlConnection connection = null;
            string queryString = "INSERT INTO MovieDatabase (Title, Category, Year)";
            queryString += " VALUES (@Title, @Category, @Year);";
            queryString += " SELECT SCOPE_IDENTITY();";
            int newId;

            try
            {
                connection = new SqlConnection(connectionString);
                newId = connection.ExecuteScalar<int>(queryString, p);
            }
            catch (Exception e)
            {
                newId = -1;
                //log the error--get details from e
            }
            finally //cleanup!
            {
                if (connection != null)
                {
                    connection.Close(); //explicitly closing the connection
                }
            }

            return newId;
        }

        public int DeleteMovieById(int id)
        {
            //TODO: Refactor with try/catch
            SqlConnection connection = new SqlConnection(connectionString);
            string deleteCommand = "DELETE FROM MovieDatabase WHERE ID = @id";

            int rows = connection.Execute(deleteCommand, new { id = id });

            return rows;
        }

        public IEnumerable<Movie> GetMoviesAll()
        {
            SqlConnection connection = null;
            string queryString = "SELECT * FROM MovieDatabase";
            IEnumerable<Movie> Products = null;

            try
            {
                connection = new SqlConnection(connectionString);
                Products = connection.Query<Movie>(queryString);
            }
            catch (Exception e)
            {
                //log the error--get details from e
            }
            finally //cleanup!
            {
                if (connection != null)
                {
                    connection.Close(); //explicitly closing the connection
                }
            }

            return Products;
        }

        public IEnumerable<Movie> GetMoviesByCategory(string category)
        {
            SqlConnection connection = null;
            string queryString = "SELECT * FROM MovieDatabase WHERE Category = @cat";
            IEnumerable<Movie> movies = null;

            try
            {
                connection = new SqlConnection(connectionString);
                movies = connection.Query<Movie>(queryString, new { cat = category });
            }
            catch (Exception e)
            {
                //log the error--get details from e
            }
            finally //cleanup!
            {
                if (connection != null)
                {
                    connection.Close(); //explicitly closing the connection
                }
            }

            return movies;
        }

        public string[] GetMovieCategories()
        {
            SqlConnection connection = null;
            string queryString = "SELECT DISTINCT Category FROM MovieDatabase";
            IEnumerable<Movie> movies = null;

            try
            {
                connection = new SqlConnection(connectionString);
                movies = connection.Query<Movie>(queryString);
            }
            catch (Exception e)
            {
                //log the error--get details from e
            }
            finally //cleanup!
            {
                if (connection != null)
                {
                    connection.Close(); //explicitly closing the connection
                }
            }

            if (movies == null)
            {
                return null;
            }
            else
            {
                string[] categories = new string[movies.Count()];
                int count = 0;

                foreach (Movie m in movies)
                {
                    categories[count] = m.Category;
                    count++;
                }

                return categories;
            }

        }

        public Movie GetMovieById(int id)
        {
            SqlConnection connection = null;
            string queryString = "SELECT * FROM Products WHERE Id = @id";
            Movie m = null;

            try
            {
                connection = new SqlConnection(connectionString);
                m = connection.QueryFirstOrDefault<Movie>(queryString, new { id = id });
            }
            catch (Exception e)
            {
                //log the error--get details from e
            }
            finally //cleanup!
            {
                if (connection != null)
                {
                    connection.Close(); //explicitly closing the connection
                }
            }

            return m;
        }

        public IEnumerable<Movie> GetRandomMovie()
        {
            SqlConnection connection = null;
            string queryString = "SELECT TOP 1 * FROM MovieDatabase ORDER BY NEWID()";
            IEnumerable<Movie> m = null;

            try
            {
                connection = new SqlConnection(connectionString);
                m = connection.Query<Movie>(queryString);
            }
            catch (Exception e)
            {
                //log the error--get details from e
            }
            finally //cleanup!
            {
                if (connection != null)
                {
                    connection.Close(); //explicitly closing the connection
                }
            }

            return m;
        }

        public IEnumerable<Movie> GetRandomMoviesByCategory(string category)
        {
            SqlConnection connection = null;
            string queryString = "SELECT TOP 1 * FROM MovieDatabase WHERE Category = @cat ORDER BY NEWID()";
            IEnumerable<Movie> movies = null;

            try
            {
                connection = new SqlConnection(connectionString);
                movies = connection.Query<Movie>(queryString, new { cat = category });
            }
            catch (Exception e)
            {
                //log the error--get details from e
            }
            finally //cleanup!
            {
                if (connection != null)
                {
                    connection.Close(); //explicitly closing the connection
                }
            }

            return movies;
        }

    }
}
