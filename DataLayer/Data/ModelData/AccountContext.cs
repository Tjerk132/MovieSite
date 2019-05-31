using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Web;
using Models;
using Helpers;
using Interfaces.Interfaces;

namespace DataLayer.Data
{
    public class AccountContext : IAccountContext
    {    
        ConnectionString conn = new ConnectionString();
        public Account LoginUser(Account account)
        {
            using (conn.connectionstring)
            {
                conn.connectionstring.Open();
                SqlCommand cmd = new SqlCommand("ValidateUser")
                {
                    CommandType = CommandType.StoredProcedure,
                    Connection = conn.connectionstring
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
            if (account.passwordhash == null || !PasswordHelper.ValidatePassword(account.Password, account.passwordhash))
            {
                account = new Account();               
            }
            return account;
        }
        public void CreateNew(Account account)
        {
            ConnectionString conn = new ConnectionString();
            using (conn.connectionstring)
            {
                conn.connectionstring.Open();
                SqlCommand cmd = new SqlCommand("AddUser")
                {
                    CommandType = CommandType.StoredProcedure,
                    Connection = conn.connectionstring
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
            using (conn.connectionstring)
            {
                conn.connectionstring.Open();
                SqlCommand cmd = new SqlCommand("GetUserReviews")
                {
                    CommandType = CommandType.StoredProcedure,
                    Connection = conn.connectionstring
                };
                cmd.Parameters.AddWithValue("@Name", account.Name);
                
                DataTable dtResult = new DataTable();
                dtResult.Load(cmd.ExecuteReader());
                foreach (DataRow dr in dtResult.Rows)
                {
                    DateTime.TryParse(dr[1].ToString(), out DateTime ReviewDate);
                    int.TryParse(dr[2].ToString(), out int StarRating);

                    Review review = new Review
                    (
                        ReviewDate,
                        dr[2].ToString(),
                        dr[3].ToString(),
                        StarRating
                    );

                    reviews.Add(review);
                }
            }
            return reviews;
        }
    }
}
