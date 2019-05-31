using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Interfaces.Interfaces;
using DataLayer.Data;
using LogicLayer.Logic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using MovieSite.Models.ViewModels.MovieViewModels;

namespace MovieSite.Controllers
{
    public class RatingController : Controller
    {
        private readonly RatingLogic _ratinglogic;
        private readonly MoviesLogic _movielogic;
        public RatingController(RatingLogic ratinglogic, MoviesLogic movielogic)
        {
            _ratinglogic = ratinglogic;
            _movielogic = movielogic;
        }
        [HttpPost]
        public ActionResult SubmitRating(int Rating, int MovieId)
        {
            string Message;
            if (Rating >= 0 && Rating <= 100)
            {
                Account account = HttpContext.Session.GetObject<Account>("User");
                Message = _ratinglogic.SubmitRating(Rating, MovieId, account);
            }
            else Message = "Please insert a rating between 0 and 100";

            MovieIndexViewModel viewModel = new MovieIndexViewModel
            {
                Account = HttpContext.Session.GetObject<Account>("User"),
                Movies = _movielogic.GetMovies()
            };
            viewModel.Message = Message;

            return View("Views/Movies/Index.cshtml", viewModel);
        }
    }
}