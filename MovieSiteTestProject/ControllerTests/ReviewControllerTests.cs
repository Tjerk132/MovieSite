using Interfaces.Interfaces;
using LogicLayer.Logic;
using Microsoft.AspNetCore.Mvc;
using Models;
using Moq;
using MovieSite.Controllers;
using MovieSite.Models.ViewModels.ReviewViewModels;
using System;
using System.Collections.Generic;
using Xunit;

namespace MovieSiteTestProject.ControllerTests
{
    public class ReviewControllerTests
    {
        [Fact]
        public void TestGetReviews()
        {
            //Arrange
            List<Review> reviews = new List<Review>
            {
                new Review(DateTime.Now, "Great Movie", "Simon", 4),
                new Review(DateTime.Now, "Excellent", "Henk", 5)
            };

            Mock<IReviewContext> mock = new Mock<IReviewContext>();
            mock.Setup(x => x.GetReviews(1)).Returns(reviews);
            ReviewLogic logic = new ReviewLogic(mock.Object);

            ReviewController controller = new ReviewController(logic);

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
            List<Review> reviews = new List<Review>
            {
                new Review(DateTime.Now,"Great Movie", "Simon", 4),
                new Review(DateTime.Now, "Excellent","Henk", 5)
            };

            Review review = new Review(DateTime.Now, "", "Sebastian", 4);

            Mock<IReviewContext> mock = new Mock<IReviewContext>();
            mock.Setup(x => x.GetReviews(1)).Returns(reviews);
            ReviewLogic logic = new ReviewLogic(mock.Object);

            ReviewController controller = new ReviewController(logic);

            //Act
            var result = controller.AddReview(1, "Shrek", review.Date, review.Text, review.StarRating) 
                as ViewResult;

            var viewmodel = result.Model as ReviewViewModel;
            //Assert
            Assert.Equal("Please insert all fields", viewmodel.Message);
        }
    }
}
