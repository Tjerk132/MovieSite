using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Models;
using Interfaces.ContextInterfaces;
using Repositories.Repositories;
using Interfaces.LogicInterfaces;
using Interfaces.RepositoryInterfaces;

namespace LogicLayer.Logic
{
    public class ReviewLogic : IReviewLogic
    {
        private readonly IReviewRepository Repository;
        public ReviewLogic(IReviewRepository repository)
        {
            Repository = repository;
        }

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
            var RatingPercentages = 

            from Review in reviews
            group Review by Review.StarRating into ReviewGroup
            orderby ReviewGroup.Key
            select Math.Round(ReviewGroup.ToList().Count / (double)reviews.Count * 100.0,0);

            List<double> Percentages = RatingPercentages.ToList();
            //If movie has no reviews for one or more ratingstars insert 0 value for missing ratingstar review 
            //(to avoid displaying ratingpercentage at wrong ratingstar)
            //Add missing review(s) after linq query to avoid added review being taken in total percentage
            for (int i = 0; i < 5; i++)
            {
                if (!reviews.Exists(x => x.StarRating == i+1))
                {
                    Percentages.Insert(i, 0);
                }
            }
            return Percentages;
        }
    }
}