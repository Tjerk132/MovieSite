﻿using Interfaces.ContextInterfaces;
using Interfaces.RepositoryInterfaces;
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
        private Mock<IRatingRepository> repositorymock;
        private RatingLogic logic;
        public RatingLogicTests()
        {
            repositorymock = new Mock<IRatingRepository>();
            logic = new RatingLogic(repositorymock.Object);
        }
        [Theory]
        [InlineData("Pieter")]
        public void TestRatingMessage(string Name)
        {
            //Arrange
            Account account = new Account { Name = Name };
            repositorymock.Setup(x => x.SubmitRating(65, 3, account))
                .Returns("Rating added, Thank you for your feedback");

            //Act
            string result = logic.SubmitRating(65, 3, account);

            //Assert
            Assert.Equal("Rating added, Thank you for your feedback", result);
        }
    }
}
