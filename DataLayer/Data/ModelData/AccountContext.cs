using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Web;
using Models;
using Interfaces.Interfaces;

namespace DataLayer.Data
{
    public class AccountContext : IAccountContext
    {    
        ConnectionString conn = new ConnectionString();
        public Account LoginResult(Account account)
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
                cmd.Parameters.AddWithValue("@password", account.Password);
                cmd.ExecuteNonQuery();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    account = new Account();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            account.AccountId = reader.GetInt32(0);
                            account.Name = reader.GetString(1);
                            account.Password = reader.GetString(2);
                        }
                    }
                    return account;
                }
            }
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
                    new SqlParameter("@Password", account.Password)
                });
                cmd.ExecuteNonQuery();
            }
        }
    }
}
