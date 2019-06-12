using Interfaces.ContextInterfaces;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Repositories.Repositories;
using Interfaces.LogicInterfaces;
using Interfaces.RepositoryInterfaces;

namespace LogicLayer.Logic
{
    public class MoviesLogic : IMoviesLogic
    {
        private readonly IMoviesRepository Repository;
        public MoviesLogic(IMoviesRepository repository)
        {
            Repository = repository;
        }
        public void AddMovie(string Title, DateTime ReleaseDate)
        {
            Movie movie = new Movie(Title, ReleaseDate);
            Repository.AddMovie(movie);
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