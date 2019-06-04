using Interfaces.ContextInterfaces;
using LogicLayer.Logic;
using Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace MovieSiteTestProject.LogicTests
{
    public class RatingLogicTests
    {
        private Mock<IRatingContext> ratingcontextmock;
        private RatingLogic logic;
        public RatingLogicTests()
        {
            ratingcontextmock = new Mock<IRatingContext>();
            logic = new RatingLogic(ratingcontextmock.Object);
        }
        [Theory]
        [InlineData("Pieter")]
        public void TestRatingMessage(string Name)
        {
            //Arrange
            Account account = new Account { Name = Name };
            ratingcontextmock.Setup(x => x.SubmitRating(65, 3, account))
                .Returns("Rating added, Thank you for your feedback");

            //Act
            string result = logic.SubmitRating(65, 3, account);

            //Assert
            Assert.Equal("Rating added, Thank you for your feedback", result);
        }
    }
}
