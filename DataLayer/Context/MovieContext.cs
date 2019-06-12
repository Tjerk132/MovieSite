using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using Models;
using Interfaces.ContextInterfaces;
using DataLayer.Context;
using Helpers;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Context
{
    public class MovieContext : IMoviesContext
    {
        SqlConnection conn = new SqlConnection(Connection.ConnectionString);
        public List<Movie> GetMovies()
        {
            List<Movie> movies = new List<Movie>();
            using (conn)
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("GetMovies")
                {
                    CommandType = CommandType.StoredProcedure,
                    Connection = conn
                };
                DataTable dtResult = new DataTable();
                dtResult.Load(cmd.ExecuteReader());
                foreach (DataRow dr in dtResult.Rows)
                {

                    int.TryParse(dr[0].ToString(), out int movieId);

                    string Title = dr[1].ToString();

                    DateTime.TryParse(dr[2].ToString(), out DateTime ReleaseDate);

                    int.TryParse(dr[3].ToString(), out int timesWatched);

                    int.TryParse(dr[4].ToString(), out int Rating);

                    Movie movie = new Movie(movieId, Title, ReleaseDate, timesWatched, Rating);

                    movies.Add(movie);
                }
            }
            return movies;
        }
        public void ChangeMovieWatched(int MovieId)
        {
            using (conn)
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("UpdateMovie")
                {
                    CommandType = CommandType.StoredProcedure,
                    Connection = conn
                };
                cmd.Parameters.AddWithValue("@MovieId", MovieId);
                cmd.ExecuteNonQuery();
            }
        }
        public Movie AddMovie(Movie movie)
        {
            using (conn)
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("AddMovie")
                {
                    CommandType = CommandType.StoredProcedure,
                    Connection = conn
                };

                cmd.Parameters.AddRange(new[]
                {
                    new SqlParameter("@Title", movie.Title),
                    new SqlParameter("@ReleaseDate", movie.ReleaseDate),
                    new SqlParameter("@Watched", movie.Watched),
                    //Create a rating so movie doesn't have 0 ratings for avg rating to avoid movie not getting displayed
                    new SqlParameter("@AccountId", 1),
                    new SqlParameter("@Rating", 50)
                });
                cmd.ExecuteNonQuery();
            }
            return movie;
        }
    }
}
