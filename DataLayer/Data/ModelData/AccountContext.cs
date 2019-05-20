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
    }
}
