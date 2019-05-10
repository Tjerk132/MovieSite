using Interfaces.Interfaces;
using DataLayer.Data;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Data
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
