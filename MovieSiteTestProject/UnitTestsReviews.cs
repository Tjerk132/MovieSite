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

namespace MovieSiteTestProject
{
    public class UnitTestsReviews
    {
        [Fact]
        public void TestGetReviews()
        {
            //Arrange
            List<Review> reviews = new List<Review>
            {
                new Review
                {
                    Autor = "Simon",
                    Date = DateTime.Now,
                    Text = "Great Movie",
                    Title = "Shrek",
                    StarRating = 4
                },
                new Review
                {
                    Autor = "Henk",
                    Date = DateTime.Now,
                    Text = "Excellent",
                    Title = "Shrek",
                    StarRating = 5
                },
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
            Review review = new Review {
 
                Autor = "Simon",
                Date = DateTime.Now,
                Text = "",
                Title = "Shrek",
                StarRating = 3
            };          

            Mock<IReviewContext> mock = new Mock<IReviewContext>();
            mock.Setup(x => x.Add(review, 1));
            ReviewLogic logic = new ReviewLogic(mock.Object);

            ReviewController controller = new ReviewController(logic);

            //Act
            ViewResult result = controller.AddReview(1, review, "Shrek") as ViewResult;
            var viewmodel = result.Model as string;
            //Assert
            Assert.Equal("Please insert all fields", viewmodel);
        }
    }
}
