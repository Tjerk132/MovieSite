using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using Models;
using Helpers;
using Interfaces.ContextInterfaces;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Context
{
    public class AccountContext : IAccountContext
    {
        SqlConnection conn = new SqlConnection(Connection.ConnectionString);   
        public Account LoginUser(Account account)
        {  
            using (conn)
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("ValidateUser")
                {
                    CommandType = CommandType.StoredProcedure,
                    Connection = conn
                };
                cmd.Parameters.AddWithValue("@username", account.Name);
                cmd.ExecuteNonQuery();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            account.AccountId = reader.GetInt32(0);
                            account.Name = reader.GetString(1);
                            account.Watched = reader.GetInt32(2);
                            account.passwordhash = reader.GetString(3);
                        }
                    }
                }
            }
            PasswordHelper passwordHelper = new PasswordHelper();
            if (account.passwordhash == null || !passwordHelper.ValidatePassword(account.Password, account.passwordhash))
            {
                account = new Account();               
            }
            return account;
        }
        public void CreateNew(Account account)
        {
            using (conn)
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("AddUser")
                {
                    CommandType = CommandType.StoredProcedure,
                    Connection = conn
                };
                cmd.Parameters.AddRange(new[]
                {
                    new SqlParameter("@Name", account.Name),
                    new SqlParameter("@Password", account.passwordhash)
                });
                cmd.ExecuteNonQuery();
            }
        }
        public List<Review> GetUserReviews(Account account)
        {
            List<Review> reviews = new List<Review>();
            using (conn)
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("GetUserReviews")
                {
                    CommandType = CommandType.StoredProcedure,
                    Connection = conn
                };
                cmd.Parameters.AddWithValue("@Name", account.Name);
                
                DataTable dtResult = new DataTable();
                dtResult.Load(cmd.ExecuteReader());
                foreach (DataRow dr in dtResult.Rows)
                {
                    string Title = dr[0].ToString();
                    DateTime.TryParse(dr[1].ToString(), out DateTime ReviewDate);
                    int.TryParse(dr[2].ToString(), out int StarRating);

                    Review review = new Review(ReviewDate, StarRating, Title);

                    reviews.Add(review);
                }
            }
            return reviews;
        }
    }
}
