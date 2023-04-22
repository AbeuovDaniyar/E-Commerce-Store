/* This is a C# class called `UserDAO` that contains methods for interacting with a database table
called `Users`. It uses the `Microsoft.Data.SqlClient` namespace for connecting to a SQL Server
database and the `CapstoneProject.Models` namespace for the `User` model class. The `string
connectionString` variable contains the connection string for the database. The class has three
methods: `getAllUsers()`, `getUserById(int userId)`, `insertUser(User user)`, and
`authenticateUser(string email, string password)`. These methods respectively retrieve all users
from the `Users` table, retrieve a user by their ID, insert a new user into the `Users` table, and
authenticate a user by their email and password. */
using CapstoneProject.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapstoneProject.Data
{
    /* The UserDAO class contains methods for retrieving, inserting, and authenticating users in a
    database. */
    public class UserDAO
    {
        string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=aspnet-CapstoneProject-96D9AAD4-C520-424C-AF81-4A8C2D540CE1;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        /// The function returns a null list of users.
        /// 
        /// @return A null value is being returned.
        public List<User> getAllUsers() 
        {
            return null;
        }

        
        /// This function retrieves a user from a database by their ID.
        /// 
        /// @param userId an integer representing the ID of the user to retrieve from the database.
        /// 
        /// @return The method is returning a User object with the details of the user whose Id matches
        /// the input parameter userId.
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

        
        /// This function inserts a new user into a database and returns the ID of the inserted user.
        /// 
        /// @param User The User class is likely a custom class that represents a user in the system. It
        /// probably has properties such as FirstName, LastName, Email, Password, and Role.
        /// 
        /// @return The method is returning an integer value, which is the ID of the newly inserted user
        /// in the database. If the insertion is successful, the ID is returned. If there is an
        /// exception or error, the method returns -1.
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

        
        /// This function authenticates a user by checking if their email and password match those
        /// stored in a SQL database.
        /// 
        /// @param email The email of the user trying to authenticate.
        /// @param password The password parameter is a string variable that represents the password
        /// entered by the user during the authentication process.
        /// 
        /// @return The method is returning an integer value, which represents the user ID of the
        /// authenticated user. If the authentication fails, the method returns -1.
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
