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
using MovieSite.Models.ViewModels;
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
                SharedIndexViewModel viewModel = new SharedIndexViewModel
                {
                    AccountViewModel = new AccountViewModel
                    {
                        Account = HttpContext.Session.GetObject<Account>("User")
                    },
                    Movies = new List<Movie>()
                };

                var MovieLogic = new MoviesLogic(_context);
                foreach (var Movie in MovieLogic.GetMovies())
                {
                    viewModel.Movies.Add(new Movie
                    {
                        MovieId = Movie.MovieId,
                        Title = Movie.Title,
                        Watched = Movie.Watched,
                        ReleaseDate = Movie.ReleaseDate,
                        Rating = Movie.Rating
                    });
                }
                if (HttpContext.Session.GetString("Message") != null)
                {
                    viewModel.AccountViewModel.Message = HttpContext.Session.GetString("Message").ToString();
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
            SharedIndexViewModel SharedviewModel = new SharedIndexViewModel
            {
                Movies = new List<Movie>(),
                AccountViewModel = new AccountViewModel()
                {
                    Account = HttpContext.Session.GetObject<Account>("User")
                }
            };
            var MovieLogic = new MoviesLogic(_context);
            List<Movie> movies = MovieLogic.GetMovies();

            SharedviewModel.Movies = MovieLogic.Filtermovie(movies, Title);

            if (SharedviewModel.Movies.Count == 0)
            {
                SharedviewModel.AccountViewModel.Message = "No movies found";
            }
            return View("Index", SharedviewModel);
        }
        public ActionResult ClearSession()
        {
            if (HttpContext.Session.GetString("Message") != null)
            {
                HttpContext.Session.SetString("Message", "");
            }
            return RedirectToAction("Index");
        }
    }
}

