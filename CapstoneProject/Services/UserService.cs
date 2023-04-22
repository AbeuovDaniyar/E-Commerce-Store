using CapstoneProject.Data;
using CapstoneProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapstoneProject.Services
{
    /* The UserService class provides methods to insert, authenticate, and retrieve users using a
    UserDAO object. */
    public class UserService
    {
        UserDAO service = new UserDAO();

        
        /// This function inserts a user into a database using a service and returns the result.
        /// 
        /// @param User User is a class representing a user in the system. It likely contains properties
        /// such as username, password, email, and other relevant information about the user. The
        /// insertUser method takes an instance of the User class as a parameter and inserts it into the
        /// system, returning an integer value indicating the success or
        /// 
        /// @return The method `insertUser` is being called on an object of type `service` with the
        /// argument `user`. The return value of this method is being returned by the `insertUser`
        /// method in the current class. Therefore, the return value of this method is whatever is
        /// returned by the `insertUser` method in the `service` object. The return type of this method
        /// is not specified in
        public int insertUser(User user) 
        {
            return service.insertUser(user);
        }

        
        /// This function authenticates a user by taking in their email and password and returning an
        /// integer value.
        /// 
        /// @param email A string representing the email address of the user trying to authenticate.
        /// @param password The password parameter is a string that represents the user's password that
        /// they are trying to authenticate with.
        /// 
        /// @return The method `authenticateUser` is returning an integer value, which is the result of
        /// calling the `authenticateUser` method of the `service` object with the `email` and
        /// `password` parameters. The specific meaning of the returned integer value depends on the
        /// implementation of the `authenticateUser` method in the `service` object.
        public int authenticateUser(string email, string password) 
        {
            return service.authenticateUser(email, password);
        }

        
        /// This function retrieves a user object by its ID.
        /// 
        /// @param userId The parameter "userId" is an integer value representing the unique identifier
        /// of a user. This method is used to retrieve a user object from the service layer based on the
        /// provided userId.
        /// 
        /// @return A User object is being returned.
        public User getUserById(int userId) 
        {
            return service.getUserById(userId);
        }
    }
}
