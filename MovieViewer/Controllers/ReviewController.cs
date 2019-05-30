using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MovieSite.Models.ViewModels.ReviewViewModels;
using Models;
using LogicLayer.Logic;
using DataLayer.Data;
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
        public IActionResult NewReview(int MovieId, string Title, string Message)
        {
            List<Review> reviews = _context.GetReviews(MovieId);
            ReviewViewModel reviewViewModel = new ReviewViewModel
            {
                Reviews = reviews,
                MovieId = MovieId,
                MovieTitle = Title,
                AverageRating = _context.AverageRating(reviews),
                RatingPercentages = _context.GetRatingPercentages(reviews),
                Message = Message
            };  
            return View(reviewViewModel);
        }
        [HttpPost]
        public IActionResult AddReview(int MovieId, Review review, string Title)
        {
            if (review.StarRating > 0 && !string.IsNullOrWhiteSpace(review.Text))
            {
                Account account = HttpContext.Session.GetObject<Account>("User");
                Review Review = new Review
                {
                    Date = review.Date,
                    Text = review.Text,
                    Autor = account.Name,
                    StarRating = review.StarRating
                };
                _context.AddReview(Review, MovieId);
                return RedirectToAction("NewReview", new { MovieId, Title });
            }
            else
            {
                string Message = "Please insert all fields";
                return RedirectToAction("NewReview", new { Message, MovieId, Title });
            }
        }
    }
}