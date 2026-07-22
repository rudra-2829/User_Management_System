using AarohiWPFCore.Models;
using Microsoft.Data.SqlClient;
using System.Text.Json;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Windows;
using Aarohi.Classes;

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
                    user.UserName = reader["UserName"].ToString();
                    user.FirstName = reader["FirstName"].ToString();
                    user.LastName = reader["LastName"].ToString();
                    user.EmailAddress = reader["EmailAddress"].ToString();
                    user.CreatedAt = (DateTime)reader["CreatedAt"];

                    users.Add(user);
                }
            }

            return users;
        }

        private string GetPasswordByUserId(int id)
        {
            string password = string.Empty;

            string query = "SELECT Password FROM Users WHERE Id = @Id";

            using (SqlConnection connection = db.GetConnection())
            {
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", id);

                object? result = command.ExecuteScalar();

                if (result != null && result != DBNull.Value)
                {
                    password = result.ToString() ?? string.Empty;
                }
            }

            return password;
        }

        public void AddUser(User user)
        {
            //string query = @"INSERT INTO Users (FirstName, LastName, EmailAddress, UserName, Password) VALUES (@FirstName, @LastName, @EmailAddress, @UserName, @Password)";

            //using (SqlConnection connection = db.GetConnection())
            //{
            //    connection.Open();
            //    SqlCommand command = new SqlCommand(query, connection);
            //    command.Parameters.AddWithValue("@Id", user.Id);
            //    command.Parameters.AddWithValue("@UserName", user.UserName);
            //    command.Parameters.AddWithValue("@Password", user.Password);
            //    command.Parameters.AddWithValue("@FirstName", user.FirstName);
            //    command.Parameters.AddWithValue("@LastName", user.LastName);
            //    command.Parameters.AddWithValue("@EmailAddress", user.EmailAddress);
            //    command.ExecuteNonQuery();
            //}
            DynamicClass dc = new DynamicClass("dbo","Users","Id");

            dc.Values["UserName"] = user.UserName;
            dc.Values["Password"] = user.Password;
            dc.Values["FirstName"] = user.FirstName;
            dc.Values["LastName"] = user.LastName;
            dc.Values["EmailAddress"] = user.EmailAddress;

            dc.Insert();
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
            //string query =
            //    @"UPDATE Users SET FirstName = @FirstName, LastName = @LastName, EmailAddress = @EmailAddress, UserName = @UserName WHERE Id = @Id";

            //using (SqlConnection connection =
            //       db.GetConnection())
            //{
            //    connection.Open();
            //    SqlCommand command = new SqlCommand(query, connection);
            //    command.Parameters.AddWithValue("@FirstName", user.FirstName);
            //    command.Parameters.AddWithValue("@LastName", user.LastName);
            //    command.Parameters.AddWithValue("@EmailAddress", user.EmailAddress);
            //    command.Parameters.AddWithValue("@Id", user.Id);
            //    command.Parameters.AddWithValue("@UserName", user.UserName);

            //    Console.WriteLine("Id = " + user.Id);
            //    Console.WriteLine("Parameters Count = " + command.Parameters.Count);
            //    Console.WriteLine(query);

            //    command.ExecuteNonQuery();
            //}
            DynamicClass dc = new DynamicClass("dbo", "Users", "Id");

            string existingPassword = GetPasswordByUserId(user.Id);

            dc.Values["Id"] = user.Id;
            dc.Values["UserName"] = user.UserName;
            dc.Values["Password"] = existingPassword;
            dc.Values["FirstName"] = user.FirstName;
            dc.Values["LastName"] = user.LastName;
            dc.Values["EmailAddress"] = user.EmailAddress;

            dc.UpdateByKey();

        }

        public User? UserLogin(string username, string password)
        {
            User? user = null;

            using (SqlConnection connection = db.GetConnection())
            {
                connection.Open();

                // Explicitly naming columns prevents mapping breaks if your DB changes
                string query = "SELECT Id, UserName, FirstName, LastName, EmailAddress FROM Users WHERE UserName = @UserName AND Password = @Password";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserName", username);
                    command.Parameters.AddWithValue("@Password", password);

                    // Added 'using' here to properly close the reader when done
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read()) // Moves the pointer to the first row found
                        {
                            user = new User
                            {
                                // Map these fields based on your exact 'User' model properties
                                Id = Convert.ToInt32(reader["Id"]),
                                FirstName = reader["FirstName"].ToString() ?? string.Empty,
                                LastName = reader["LastName"].ToString() ?? string.Empty,
                                EmailAddress = reader["EmailAddress"].ToString() ?? string.Empty,
                                UserName = reader["UserName"].ToString() ?? string.Empty
                            };
                        }
                    }
                }
            }

            return user; // This fixes the missing return error!
        }
    }
}