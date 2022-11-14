using CapstoneProject.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapstoneProject.Data
{
    public class UserDAO
    {
        string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=aspnet-CapstoneProject-96D9AAD4-C520-424C-AF81-4A8C2D540CE1;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public List<User> getAllUsers() 
        {
            return null;
        }

        /// <summary>
        /// Retrieves a User by user Id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>User</returns>
        public User getUserById(int userId) 
        {
            User user = new User();
            string sqlStatement = "SELECT * FROM dbo.Users WHERE Id LIKE @userId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);

                command.Parameters.AddWithValue("@userId", userId);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        
                        while (reader.Read())
                        {
                            user = new User
                            {
                                Id = userId,
                                FirstName = (string)reader[1],
                                LastName = (string)reader[2],
                                Email = (string)reader[3],
                                Role = (int)reader[5]
                            };
                        }      

                        connection.Close();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            return user;
        }

        /// <summary>
        /// Adds a new entry in the users table
        /// </summary>
        /// <param name="user"></param>
        /// <returns>int</returns>
        public int insertUser(User user) 
        {
            int result = -1;
            string sqlStatement = "INSERT INTO dbo.Users (FirstName, LastName, Email, Password, Role) OUTPUT INSERTED.Id VALUES (@FirstName, @LastName, @Email, @Password, @Role)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);

                command.Parameters.AddWithValue("@FirstName", user.FirstName);
                command.Parameters.AddWithValue("@LastName", user.LastName);
                command.Parameters.AddWithValue("@Email", user.Email);
                command.Parameters.AddWithValue("@Password", user.Password);
                command.Parameters.AddWithValue("@Role", 0);

                try
                {
                    connection.Open();

                    if (command.ExecuteNonQuery() > 0)
                    {
                        result = (int)command.ExecuteScalar();
                        connection.Close();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            return result;
        }

        /// <summary>
        /// Looks for any email and password matches within a users table
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns>user Id as int</returns>
        public int authenticateUser(string email, string password) 
        {
            User user = new User();
            int result = -1;
            string sqlStatement = "SELECT * FROM dbo.Users WHERE Email LIKE @Email AND Password LIKE @Password";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);

                command.Parameters.AddWithValue("@Email", '%' + email + '%');
                command.Parameters.AddWithValue("@Password", '%' + password + '%');

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            result = (int)reader[0];
                        }

                        connection.Close();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            return result;
        }
    }
}
