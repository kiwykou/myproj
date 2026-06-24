using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Text;

namespace AvaloniaApplication26.Services
{
    public class DatabaseService
    {
        private readonly string _connectionString = "Data Source=appsss.db";
        public SqliteConnection GetConnection()
        {
            return new SqliteConnection(_connectionString); 
        }
    }
}
