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
        public IActionResult NewReview(int MovieId, string Title)
        {
            var ReviewLogic = new ReviewLogic(_context);
            ReviewViewModel reviewViewModel = new ReviewViewModel
            {
                Reviews = ReviewLogic.GetReviews(MovieId),
                MovieId = MovieId,
                MovieTitle = Title
            };
            return View(reviewViewModel);
        }
        [HttpPost]
        public IActionResult AddReview(int MovieId, DateTime ReviewDate, string review, string Title)
        {
            Account account = HttpContext.Session.GetObject<Account>("User");
            Review Review = new Review
            {
                Date = ReviewDate,
                Text = review, 
                Autor = account.Name
            };
            return RedirectToAction("NewReview", new { MovieId, Title });
        }
    }
}