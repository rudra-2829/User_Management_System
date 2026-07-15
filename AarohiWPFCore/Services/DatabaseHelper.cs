using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using System.Configuration;

namespace AarohiWPFCore.Services
{
    public class DatabaseHelper
    {
        private readonly string connectionString = @"Server=(localdb)\MSSQLLocalDB;
                                                    Database=UserManagementDB;
                                                    Trusted_Connection=True;
                                                    TrustServerCertificate=True";

        public SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}
