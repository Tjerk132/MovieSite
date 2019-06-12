using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Interfaces.RepositoryInterfaces
{
    public interface IMoviesRepository
    {            
        List<Movie> GetMovies();
        void AddMovie(Movie movie);
        void ChangeMovieWatched(int MovieId);
    }
}
