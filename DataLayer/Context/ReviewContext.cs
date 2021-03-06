﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Web;
using Models;
using Interfaces.ContextInterfaces;
using Helpers;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Context
{
    public class ReviewContext : DbContext, IReviewContext
    {    
        SqlConnection conn = new SqlConnection(Connection.ConnectionString);
        public void AddReview(Review review, int MovieId)
        {
            using (conn)
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("AddReview")
                {
                    CommandType = CommandType.StoredProcedure,
                    Connection = conn
                };
                cmd.Parameters.AddRange(new[]
                {
                    new SqlParameter("@MovieId", MovieId),
                    new SqlParameter("@ReviewDate", review.Date),
                    new SqlParameter("@Review", review.Text),
                    new SqlParameter("@Autor", review.Autor),
                    new SqlParameter("@StarRating", review.StarRating)
                });
                cmd.ExecuteNonQuery();
            }
        }
        public List<Review> GetReviews(int MovieId)
        {
            List<Review> reviews = new List<Review>();

            using (conn)
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("GetReviews")
                {
                    CommandType = CommandType.StoredProcedure,
                    Connection = conn
                };
                cmd.Parameters.AddWithValue("@MovieId", MovieId);
                cmd.ExecuteNonQuery();

                DataTable dtResult = new DataTable();
                dtResult.Load(cmd.ExecuteReader());
                foreach (DataRow dr in dtResult.Rows)
                {
                    DateTime.TryParse(dr[1].ToString(), out DateTime ReviewDate);
                    string Text = dr[2].ToString();
                    string Autor = dr[3].ToString();
                    int.TryParse(dr[4].ToString(), out int StarRating);;

                    Review review = new Review (ReviewDate, Text, Autor, StarRating);

                    reviews.Add(review);
                }
            }
            return reviews;
        }
    }
}
