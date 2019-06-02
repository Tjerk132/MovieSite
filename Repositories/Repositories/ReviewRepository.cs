﻿using Interfaces.Interfaces;
using Models;
using System.Collections.Generic;

namespace MovieSite.Repoisitories.Repositories
{
    public class ReviewRepository
    {
        private readonly IReviewContext _context;
        public ReviewRepository(IReviewContext context)
        {
            _context = context;
        }
        public void AddReview(Review review, int MovieId)
        {
            _context.Add(review, MovieId);
        }
        public List<Review> GetReviews(int MovieId)
        {
            return _context.GetReviews(MovieId);
        }
    }
}