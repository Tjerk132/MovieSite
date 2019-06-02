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
using Interfaces.Interfaces;

namespace MovieSite.Controllers
{
    public class ReviewController : Controller
    {
        private readonly ReviewLogic _context;
        public ReviewController(ReviewLogic reviewlogic)
        {
            _context = reviewlogic;
        }
        public IActionResult NewReview(int MovieId, string Title)
        {
            List<Review> reviews = _context.GetReviews(MovieId);
            ReviewViewModel ViewModel = new ReviewViewModel
            {
                Reviews = reviews,
                MovieId = MovieId,
                MovieTitle = Title,
                AverageRating = _context.AverageRating(reviews),
                RatingPercentages = _context.GetRatingPercentages(reviews)
            };  
            return View(ViewModel);
        }
        [HttpPost]
        public IActionResult AddReview(int MovieId, DateTime Date, string Text, int StarRating, string Title)
        {
            string Message = "";
            if (StarRating > 0 && !string.IsNullOrWhiteSpace(Text))
            {
                Account account = HttpContext.Session.GetObject<Account>("User");
                Review Review = new Review(Date,Text, account.Name, StarRating);
                _context.AddReview(Review, MovieId);
            }
            else
            {
                Message = "Please insert all fields";
            }
            List<Review> reviews = _context.GetReviews(MovieId);
            ReviewViewModel ViewModel = new ReviewViewModel
            {
                Reviews = reviews,
                MovieId = MovieId,
                MovieTitle = Title,
                AverageRating = _context.AverageRating(reviews),
                RatingPercentages = _context.GetRatingPercentages(reviews),
                Message = Message
            };
            return View("NewReview", ViewModel);
        }
    }
}