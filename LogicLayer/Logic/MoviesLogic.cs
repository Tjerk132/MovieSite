using Interfaces.Interfaces;
using DataLayer.Data;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LogicLayer.Logic
{
    public class MoviesLogic
    {
        public MoviesLogic(IMoviesContext context)
        {
            Repository = new MoviesRepository(context);
        }
        private MoviesRepository Repository { get; }

        public void AddMovie(string Title, DateTime ReleaseDate)
        {
            Movie movie = new Movie(0, Title, ReleaseDate, 0, 0);
            Repository.Add(movie);
        }
        public virtual List<Movie>GetMovies()
        {
            return Repository.GetMovies();
        }
        public void ChangeWatched(int MovieId)
        {
            Repository.ChangeMovieWatched(MovieId);
        }
        public List<Movie> Filtermovie(List<Movie> movies, string Title)
        {
            if (!string.IsNullOrWhiteSpace(Title))
            {
                movies = movies.Where(x => x.Title.ToLower().Contains(Title.ToLower())).ToList();
            }
            return movies;
        }
    }
}