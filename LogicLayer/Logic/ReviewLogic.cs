using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Security.Cryptography;
using DataLayer.Data;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;
using DataLayer.Data.ModelData;
using Models;
using Interfaces.Interfaces;

namespace LogicLayer.Logic
{
    public class ReviewLogic
    {
        public ReviewLogic(IReviewContext context)
        {
            Repository = new ReviewRepository(context);
        }
        private ReviewRepository Repository { get; }

        public void AddReview(Review review, int MovieId)
        {
            Repository.AddReview(review, MovieId);
        }
        public List<Review> GetReviews(int MovieId)
        {
            return Repository.GetReviews(MovieId);
        }
    }
}