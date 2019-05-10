using Interfaces.Interfaces;
using DataLayer.Data;
using Models;
using System;
using System.Collections.Generic;

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
            var movie = new Movie
            { Title = Title, Watched = 0, ReleaseDate = ReleaseDate };
            Repository.Add(movie);
        }
        public List<Movie>GetMovies()
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
                movies = movies.FindAll(x => x.Title.Contains(Title));
            }
            return movies;
        }
    }
}