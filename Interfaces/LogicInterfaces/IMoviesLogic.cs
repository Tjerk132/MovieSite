using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.LogicInterfaces
{
    public interface IMoviesLogic
    {
        void AddMovie(string Title, DateTime ReleaseDate);
        List<Movie> GetMovies();
        void ChangeWatched(int MovieId);
        List<Movie> Filtermovie(List<Movie> movies, string Title);
    }
}
