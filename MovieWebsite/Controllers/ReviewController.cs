using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MovieSite.ViewModels.ReviewViewModels;
using Models;
using LogicLayer.Logic;
using DataLayer.Context;
using Microsoft.AspNetCore.Http;
using Interfaces.LogicInterfaces;
using MovieViewer;

namespace MovieSite.Controllers
{
    public class ReviewController : Controller
    {
        private readonly IReviewLogic _logic;
        private readonly IUserSession _userSession;
        public ReviewController(IReviewLogic logic, IUserSession usersession)
        {
            _userSession = usersession;
            _logic = logic;
        }
        public IActionResult NewReview(int MovieId, string Title, string Message)
        {
            List<Review> reviews = _logic.GetReviews(MovieId);
            ReviewViewModel ViewModel = new ReviewViewModel
            {
                Reviews = reviews,
                MovieId = MovieId,
                MovieTitle = Title,
                AverageRating = _logic.AverageRating(reviews),
                RatingPercentages = _logic.GetRatingPercentages(reviews),
                Message = Message
            };  
            return View(ViewModel);
        }
        [HttpPost]
        public IActionResult AddReview(int MovieId, DateTime Date, string Text, int StarRating, string Title)
        {
            string Message = "";
            if (StarRating > 0 && !string.IsNullOrWhiteSpace(Text))
            {
                Account account = _userSession.GetSession;
                Review Review = new Review(Date,Text, account.Name, StarRating);
                _logic.AddReview(Review, MovieId);
            }
            else
            {
                Message = "Please insert all fields";
            }
            return RedirectToAction("NewReview", new { MovieId, Title, Message });
        }
    }
}