using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Interfaces.Interfaces
{
    public interface IMoviesContext
    {
        List<Movie> GetMovies();
        void Add(Movie movie);
        void ChangeMovieWatched(int MovieId);
    }
}
