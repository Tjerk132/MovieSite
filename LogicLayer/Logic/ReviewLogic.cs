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
        public double AverageRating(List<Review> reviews)
        {
            if (reviews.Count > 0)
            {
                return Math.Round(reviews.Average(x => x.StarRating), 1);
            }
            else return 0;
        }
        public List<double> GetRatingPercentages(List<Review> reviews)
        {
            List<double> Percentages = new List<double>();
            for (int i = 1; i < 6; i++)
            {
                Percentages.Add(
                           Math.Round(
                                  (from Review review
                                    in reviews
                                    where review.StarRating == i
                                    select review).ToList().Count /
                                  (double)reviews.Count * 100.0
                                   ,0)
                               );
            }
            return Percentages;
        }
    }
}