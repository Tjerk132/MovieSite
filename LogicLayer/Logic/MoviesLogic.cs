using Interfaces.ContextInterfaces;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Repositories.Repositories;
using Interfaces.LogicInterfaces;

namespace LogicLayer.Logic
{
    public class MoviesLogic : IMoviesLogic
    {
        private readonly IMoviesContext _context;
        public MoviesLogic(IMoviesContext context)
        {
            _context = context;
            Repository = new MoviesRepository(_context);
        }
        private MoviesRepository Repository { get; }

        public void AddMovie(string Title, DateTime ReleaseDate)
        {
            Movie movie = new Movie(0, Title, ReleaseDate, 0, 0);
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
                movies = movies.Where(x => x.Title.ToLower().Contains(Title.ToLower())).ToList();
            }
            return movies;
        }
    }
}