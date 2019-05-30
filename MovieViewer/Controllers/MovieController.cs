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
        private readonly MoviesLogic _context;
        public MoviesController(MoviesLogic moviescontext)
        {
            _context = moviescontext;
        }
        public ActionResult Index()
        {
            if (HttpContext.Session.GetObject<Account>("User") != null)
            {
                MovieIndexViewModel viewModel = new MovieIndexViewModel
                {
                    Account = HttpContext.Session.GetObject<Account>("User"),
                    Movies = _context.GetMovies()
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
            _context.ChangeWatched(MovieId);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult AddMovie(string Title, DateTime ReleaseDate)
        {
            _context.AddMovie(Title, ReleaseDate);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult FilterMovies(string Title)
        {
            List<Movie> movies = _context.GetMovies();

            MovieIndexViewModel viewModel = new MovieIndexViewModel
            {
                 Account = HttpContext.Session.GetObject<Account>("User"),
                 Movies = _context.Filtermovie(movies, Title)
            };
            if (viewModel.Movies.Count == 0)
            {
                viewModel.Message = "No movies found";
            }
            return View("Index", viewModel);
        }
    }
}

