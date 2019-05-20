using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MovieSite.Models.ViewModels;
using Models;
using LogicLayer.Logic;
using DataLayer.Data;
using Microsoft.AspNetCore.Http;
using Interfaces.Interfaces;

namespace MovieSite.Controllers
{
    public class ReviewController : Controller
    {
        private readonly IReviewContext _context;
        public ReviewController(IReviewContext reviewcontext)
        {
            _context = reviewcontext;
        }
        public IActionResult NewReview(int MovieId, string Title, string Message)
        {
            var ReviewLogic = new ReviewLogic(_context);
            List<Review> reviews = ReviewLogic.GetReviews(MovieId);
            ReviewViewModel reviewViewModel = new ReviewViewModel
            {
                Reviews = reviews,
                MovieId = MovieId,
                MovieTitle = Title,
                AverageRating = ReviewLogic.AverageRating(reviews),
                RatingPercentages = ReviewLogic.GetRatingPercentages(reviews),
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
                var Reviewlogic = new ReviewLogic(_context);
                Reviewlogic.AddReview(Review, MovieId);
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