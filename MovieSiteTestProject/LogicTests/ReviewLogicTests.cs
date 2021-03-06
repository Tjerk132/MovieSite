﻿using Interfaces.Interfaces;
using LogicLayer.Logic;
using Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace MovieSiteTestProject.LogicTests
{
    public class ReviewLogicTests
    {
        private Mock<IReviewContext> _context;
        private ReviewLogic logic;
        public ReviewLogicTests()
        {
            _context = new Mock<IReviewContext>();
            logic = new ReviewLogic(_context.Object);
        }
        [Fact]
        public void TestRatingAverages()
        {
            List<Review> Reviews = new List<Review>
            {
                new Review(DateTime.Now, "Great Movie", "Simon", 4),
                new Review(DateTime.Now, "Excellent", "Henk", 5),
                new Review(DateTime.Now, "Superb", "Pieter", 5)
            };
            List<double> averages = logic.GetRatingPercentages(Reviews);
            //Method will always make 5 averages for all 5 StarRating possibilities
            Assert.Equal(5, averages.Count);
            //averages list should have one item with 33% and one with 67%
            Assert.True(averages.IndexOf(33) != 0 && averages.IndexOf(100-33) != 0);
        }
        [Fact]
        public void TestTotalAverageReviews()
        {
            List<Review> Reviews = new List<Review>
            {
                new Review(DateTime.Now, "Superb", "Pieter", 3),
                new Review(DateTime.Now, "Great Movie", "Simon", 4),
                new Review(DateTime.Now, "Excellent", "Henk", 5)
            };
            double totalaverage = logic.AverageRating(Reviews);
            Assert.Equal(4, totalaverage);
        }

    }
}
