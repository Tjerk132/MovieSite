using Interfaces.ContextInterfaces;
using LogicLayer.Logic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Moq;
using MovieSite.Controllers;
using MovieSite.ViewModels.ReviewViewModels;
using System;
using System.Collections.Generic;
using Xunit;

namespace MovieSiteTestProject.ControllerTests
{
    public class ReviewControllerTests
    {
        private Mock<IReviewContext> reviewcontextmock;
        private readonly ReviewLogic logic;
        private ReviewController controller;

        private readonly List<Review> reviews;
        public ReviewControllerTests()
        {
            reviewcontextmock = new Mock<IReviewContext>();

            logic = new ReviewLogic(reviewcontextmock.Object);
            controller = new ReviewController(logic);

            reviews = new List<Review>
            {
                new Review(DateTime.Now, "Great Movie", "Simon", 4),
                new Review(DateTime.Now, "Excellent", "Henk", 5)
            };
        }
        [Fact]
        public void TestGetReviews()
        {
            //Arrange
            reviewcontextmock.Setup(x => x.GetReviews(1)).Returns(reviews);

            //Act
            ViewResult result = controller.NewReview(1,"Shrek") as ViewResult;
            var viewmodel = result.Model as ReviewViewModel;

            //Assert
            Assert.Equal(2, viewmodel.Reviews.Count);
        }
        [Fact]
        public void TestAddReview()
        {
            //Arrange
            reviewcontextmock.Setup(x => x.GetReviews(1)).Returns(reviews);

            Review review = new Review(DateTime.Now, "", "Sebastian", 4);
            //Act
            var result = controller.AddReview(1, review.Date, review.Text, review.StarRating, "Shrek") 
                as ViewResult;

            var viewmodel = result.Model as ReviewViewModel;

            //Assert
            Assert.Equal("Please insert all fields", viewmodel.Message);
        }
    }
}
