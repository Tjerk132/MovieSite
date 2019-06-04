using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Models;
using LogicLayer.Logic;
using DataLayer.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using MovieSite;
using MovieSite.ViewModels.MovieViewModels;
using MovieViewer;
using Interfaces.LogicInterfaces;
using Interfaces.ContextInterfaces;

namespace MovieSite.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMoviesLogic _logic;
        private readonly IUserSession _userSession;

        public MoviesController(IMoviesLogic logic, IUserSession userSession)
        {
            _logic = logic;
            _userSession = userSession;
        }
        public ActionResult Index()
        {
            if (_userSession.GetSession != null)
            {
                MovieIndexViewModel viewModel = new MovieIndexViewModel
                {
                    Account = _userSession.GetSession,
                    Movies = _logic.GetMovies()
                };
                return View(viewModel);
            }
            else
            {
                return View("NotLoggedIn");
            }
        }

        public ActionResult AddMovie()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ChangeWatched(int MovieId)
        {
            _logic.ChangeWatched(MovieId);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult AddMovie(string Title, DateTime ReleaseDate)
        {
            _logic.AddMovie(Title, ReleaseDate);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult FilterMovies(string Title)
        {
            List<Movie> movies = _logic.GetMovies();

            MovieIndexViewModel viewModel = new MovieIndexViewModel
            {
                 Account = _userSession.GetSession,
                 Movies = _logic.Filtermovie(movies, Title)
            };
            if (viewModel.Movies.Count == 0)
            {
                viewModel.Message = "No movies found";
            }
            return View("Index", viewModel);
        }
    }
}

