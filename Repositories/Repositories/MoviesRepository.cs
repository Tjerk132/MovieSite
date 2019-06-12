using Interfaces.ContextInterfaces;
using Interfaces.RepositoryInterfaces;
using Models;
using System.Collections.Generic;

namespace Repositories.Repositories
{
    public class MoviesRepository : IMoviesRepository
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
        public void AddMovie(Movie movie)
        {
            _context.AddMovie(movie);
        }
        public void ChangeMovieWatched(int MovieId)
        {
            _context.ChangeMovieWatched(MovieId);
        }
    }
}
