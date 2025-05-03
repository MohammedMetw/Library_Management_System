// Connection.cs
using Microsoft.Data.SqlClient;
using System.Configuration;

namespace Library_Management_System
{
  
    public class Connection
    {
        private readonly string _connectionString;

        public Connection()
        {
            
            _connectionString = ConfigurationManager.ConnectionStrings["LibraryDb"].ConnectionString;
        }

       
        public SqlConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
