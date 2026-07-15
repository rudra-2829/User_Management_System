using AarohiWPFCore.Models;
using Microsoft.Data.SqlClient;
using System.Text.Json;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Windows;

namespace AarohiWPFCore.Services
{
    public class UserServices
    {
        DatabaseHelper db = new DatabaseHelper();
        public List<User> GetUsers()
        {
            List<User> users = new List<User>();

            string query = "SELECT * FROM Users";

            using (SqlConnection connection = db.GetConnection())
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    User user = new User();

                    user.Id = Convert.ToInt32(reader["Id"]);
                    user.FirstName = reader["FirstName"].ToString();
                    user.LastName = reader["LastName"].ToString();
                    user.EmailAddress = reader["EmailAddress"].ToString();
                    user.CreatedAt = (DateTime)reader["CreatedAt"];

                    users.Add(user);
                }
            }

            return users;
        }

        public void AddUser(User user)
        {
            string query = @"INSERT INTO Users (FirstName, LastName, EmailAddress) VALUES (@FirstName, @LastName, @EmailAddress)";

            using (SqlConnection connection = db.GetConnection())
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@FirstName", user.FirstName);
                command.Parameters.AddWithValue("@LastName", user.LastName);
                command.Parameters.AddWithValue("@EmailAddress", user.EmailAddress);
                command.ExecuteNonQuery();
            }
        }

        public void DeleteUser(int userId)
        {
            string query = @"DELETE FROM Users WHERE Id = @Id";
            using (SqlConnection connection = db.GetConnection())
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", userId);
                command.ExecuteNonQuery();
            }
        }

        public void UpdateEmployee(User user)
        {
            string query =
                @"UPDATE Users
          SET FirstName = @FirstName,
                LastName = @LastName,
              EmailAddress = @EmailAddress
          WHERE Id = @Id";

            using (SqlConnection connection =
                   db.GetConnection())
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@FirstName", user.FirstName);
                command.Parameters.AddWithValue("@LastName", user.LastName);
                command.Parameters.AddWithValue("@EmailAddress", user.EmailAddress);
                command.Parameters.AddWithValue("@Id", user.Id);

                Console.WriteLine("Id = " + user.Id);
                Console.WriteLine("Parameters Count = " + command.Parameters.Count);
                Console.WriteLine(query);

                command.ExecuteNonQuery();
            }
        }

        public User? UserLogin(string username, string password)
        {
            User? user = null;
            using (SqlConnection connection = db.GetConnection())
            {
                connection.Open();
                string query = "SELECT * FROM Users WHERE UserName = @UserName AND Password = @Password";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@UserName", username);
                command.Parameters.AddWithValue("@Password", password);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    user = new User
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        FirstName = reader["FirstName"].ToString(),
                        LastName = reader["LastName"].ToString(),
                        EmailAddress = reader["EmailAddress"].ToString(),
                        UserName = reader["UserName"].ToString(),
                        Password = reader["Password"].ToString(),
                        CreatedAt = (DateTime)reader["CreatedAt"]
                    };
                }
            }
            return user;
        }
    }
}