﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Interfaces.ContextInterfaces
{
    public interface IMoviesContext
    {
        List<Movie> GetMovies();
        Movie AddMovie(Movie movie);
        void ChangeMovieWatched(int MovieId);
    }
}
