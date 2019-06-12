using Interfaces.ContextInterfaces;
using Interfaces.LogicInterfaces;
using Interfaces.RepositoryInterfaces;
using LogicLayer.Logic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;
using Models;
using Moq;
using MovieSite.Controllers;
using MovieSite.ViewModels.ReviewViewModels;
using MovieViewer;
using System;
using System.Collections.Generic;
using Xunit;

namespace MovieSiteTestProject.ControllerTests
{
    public class ReviewControllerTests
    {
        private Mock<IReviewLogic> logicmock;
        private Mock<IUserSession> sessionmock;
        private Mock<IReviewRepository> repositorymock;

        private readonly ReviewLogic logic;
        private ReviewController controller;

        private readonly List<Review> reviews;
        public ReviewControllerTests()
        {
            logicmock = new Mock<IReviewLogic>();
            sessionmock = new Mock<IUserSession>();

            repositorymock = new Mock<IReviewRepository>();

            logic = new ReviewLogic(repositorymock.Object);
            controller = new ReviewController(logicmock.Object, sessionmock.Object);

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
            logicmock.Setup(x => x.GetReviews(1)).Returns(reviews);

            //Act
            ViewResult result = controller.NewReview(1,"Shrek","") as ViewResult;
            var viewmodel = result.Model as ReviewViewModel;

            //Assert
            Assert.Equal(2, viewmodel.Reviews.Count);
        }
        [Fact]
        public void TestAddReview()
        {
            //Arrange
            logicmock.Setup(x => x.GetReviews(1)).Returns(reviews);

            Review review = new Review(DateTime.Now, "", "Sebastian", 4);

            //Act
            var result = controller.AddReview(1, review.Date, review.Text, review.StarRating, "Shrek") as RedirectToActionResult;

            RouteValueDictionary RouteDictionary = result.RouteValues;
            var Message = RouteDictionary["Message"];

            //Assert
            Assert.Equal("Please insert all fields", Message);
        }
    }
}
