﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using Models;
using Interfaces.Interfaces;

namespace DataLayer.Data
{
    public class MovieContext : IMoviesContext
    {
        ConnectionString conn = new ConnectionString();
        public List<Movie> GetMovies()
        {
            List<Movie> movies = new List<Movie>();
            using (conn.connectionstring)
            {
                conn.connectionstring.Open();
                SqlCommand cmd = new SqlCommand("GetMovies")
                {
                    CommandType = CommandType.StoredProcedure,
                    Connection = conn.connectionstring
                };
                DataTable dtResult = new DataTable();
                dtResult.Load(cmd.ExecuteReader());
                foreach (DataRow dr in dtResult.Rows)
                {
                    Movie movie = new Movie();

                    int.TryParse(dr[0].ToString(), out int movieId);
                    movie.MovieId = movieId;

                    movie.Title = dr[1].ToString();

                    DateTime.TryParse(dr[2].ToString(), out DateTime ReleaseDate);
                    movie.ReleaseDate = ReleaseDate;

                    int.TryParse(dr[3].ToString(), out int timesWatched);
                    movie.Watched = timesWatched;

                    int.TryParse(dr[4].ToString(), out int Rating);
                    movie.Rating = Rating;

                    movies.Add(movie);
                }
            }
            return movies;
        }
        public void ChangeMovieWatched(int MovieId)
        {
            using (conn.connectionstring)
            {
                conn.connectionstring.Open();
                SqlCommand cmd = new SqlCommand("UpdateMovie")
                {
                    CommandType = CommandType.StoredProcedure,
                    Connection = conn.connectionstring
                };
                cmd.Parameters.AddWithValue("@MovieId", MovieId);
                cmd.ExecuteNonQuery();
            }
        }
        public void Add(Movie movie)
        {
            using (conn.connectionstring)
            {
                conn.connectionstring.Open();
                SqlCommand cmd = new SqlCommand("AddMovie")
                {
                    CommandType = CommandType.StoredProcedure,
                    Connection = conn.connectionstring
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
        }
    }
}