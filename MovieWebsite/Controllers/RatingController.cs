using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Interfaces.Interfaces;
using DataLayer.Context;
using LogicLayer.Logic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using MovieSite.ViewModels.MovieViewModels;
using MovieViewer;

namespace MovieSite.Controllers
{
    public class RatingController : Controller
    {
        private readonly RatingLogic _ratinglogic;
        private readonly MoviesLogic _movielogic;
        private readonly IUserSession _userSession;
        public RatingController(RatingLogic ratinglogic, MoviesLogic movielogic, IUserSession usersession)
        {
            _ratinglogic = ratinglogic;
            _movielogic = movielogic;
            _userSession = usersession;
        }
        [HttpPost]
        public ActionResult SubmitRating(int Rating, int MovieId)
        {
            string Message;
            Account account = _userSession.GetSession;
            if (Rating >= 0 && Rating <= 100)
            {
                Message = _ratinglogic.SubmitRating(Rating, MovieId, account);
            }
            else Message = "Please insert a rating between 0 and 100";

            MovieIndexViewModel viewModel = new MovieIndexViewModel
            {
                Account = account,
                Movies = _movielogic.GetMovies()
            };
            viewModel.Message = Message;

            return View("Views/Movies/Index.cshtml", viewModel);
        }
    }
}