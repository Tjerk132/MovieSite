using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Models;
using LogicLayer.Logic;
using DataLayer.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using MovieSite;
using MovieSite.Models.ViewModels.MovieViewModels;
using Interfaces.Interfaces;

namespace MovieSite.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMoviesContext _context;
        public MoviesController(IMoviesContext moviescontext)
        {
            _context = moviescontext;
        }
        public ActionResult Index()
        {
            if (HttpContext.Session.GetObject<Account>("User") != null)
            {
                var MovieLogic = new MoviesLogic(_context);

                MovieIndexViewModel viewModel = new MovieIndexViewModel
                {
                    Account = HttpContext.Session.GetObject<Account>("User"),
                    Movies = MovieLogic.GetMovies()
                };

                if (HttpContext.Session.GetString("Message") != null)
                {
                    viewModel.Message = HttpContext.Session.GetString("Message").ToString();
                    HttpContext.Session.SetString("Message", "");
                }
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
            var MovieLogic = new MoviesLogic(_context);
            MovieLogic.ChangeWatched(MovieId);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult AddMovie(string Title, DateTime ReleaseDate)
        {
            var MovieLogic = new MoviesLogic(_context);
            MovieLogic.AddMovie(Title, ReleaseDate);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult FilterMovies(string Title)
        {
            var MovieLogic = new MoviesLogic(_context);
            List<Movie> movies = MovieLogic.GetMovies();

            MovieIndexViewModel viewModel = new MovieIndexViewModel
            {
                 Account = HttpContext.Session.GetObject<Account>("User"),
                 Movies = MovieLogic.Filtermovie(movies, Title)
            };
            if (viewModel.Movies.Count == 0)
            {
                viewModel.Message = "No movies found";
            }
            return View("Index", viewModel);
        }
    }
}

