using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using Models;
using Interfaces.ContextInterfaces;
using Helpers;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Context
{
    public class RatingContext : DbContext, IRatingContext
    {
        SqlConnection conn = new SqlConnection(Connection.ConnectionString);
        public string SubmitRating(int Rating, int MovieId, Account account)
        {
            string message;
            using (conn)
            {
                conn.Open();
                //Check if user has already rated the movie            
                SqlCommand cmd = new SqlCommand("Check&InsertRating")
                {
                    CommandType = CommandType.StoredProcedure,
                    Connection = conn
                };
                cmd.Parameters.AddRange(new[]
                {
                    new SqlParameter("@AccountId", account.AccountId),
                    new SqlParameter("@MovieId", MovieId),
                    new SqlParameter("@Rating", Rating)
                });
                message = cmd.ExecuteScalar().ToString();
                
                cmd.ExecuteNonQuery();
            }
            return message;
        }   
    }
}
