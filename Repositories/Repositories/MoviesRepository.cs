using Interfaces.ContextInterfaces;
using Models;
using System.Collections.Generic;

namespace Repositories.Repositories
{
    public class MoviesRepository
    {
        private readonly IMoviesContext _context;
        public MoviesRepository(IMoviesContext context)
        {
            _context = context;
        }
        public List<Movie> GetMovies()
        {
            return _context.GetMovies();
        }
        public void Add(Movie movie)
        {
            _context.Add(movie);
        }
        public void ChangeMovieWatched(int MovieId)
        {
            _context.ChangeMovieWatched(MovieId);
        }
    }
}
