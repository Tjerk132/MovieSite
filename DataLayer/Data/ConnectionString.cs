﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Data
{
    public class ConnectionString
    {
        public SqlConnection connectionstring = new SqlConnection
        (
            //@"Data Source = (localdb)\MSSQLLocalDB;
            //Initial Catalog = Database;
            //Integrated Security = True;
            //Connect Timeout = 30; 
            //Encrypt=False;
            //TrustServerCertificate=False;
            //ApplicationIntent=ReadWrite; 
            //MultiSubnetFailover=False"
            @"Server = mssql.fhict.local;
            Database=dbi386599;
            User Id = dbi386599;
            Password=12345;"
        );
    }
}
