﻿using System;
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
        public IActionResult NewReview(int MovieId, string Title)
        {
            var ReviewLogic = new ReviewLogic(_context);
            List<Review> reviews = ReviewLogic.GetReviews(MovieId);
            ReviewViewModel reviewViewModel = new ReviewViewModel
            {
                Reviews = reviews,
                MovieId = MovieId,
                MovieTitle = Title,
                AverageRating = ReviewLogic.AverageRating(reviews),
                RatingPercentages = ReviewLogic.GetRatingPercentages(reviews)
            };  
            return View(reviewViewModel);
        }
        [HttpPost]
        public IActionResult AddReview(int MovieId, Review review, string Title)
        {
            Account account = HttpContext.Session.GetObject<Account>("User");
            Review Review = new Review
            {
                Date = review.Date,
                Text = review.Text,
                StarRating = review.StarRating,
                Autor = account.Name
            };
            var Reviewlogic = new ReviewLogic(_context);
            Reviewlogic.AddReview(Review, MovieId);
            return RedirectToAction("NewReview", new { MovieId, Title });
        }
    }
}