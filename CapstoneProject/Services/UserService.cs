using CapstoneProject.Data;
using CapstoneProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapstoneProject.Services
{
    public class UserService
    {
        UserDAO service = new UserDAO();

        /// <summary>
        /// Calls service method to add a new User
        /// </summary>
        /// <param name="user"></param>
        /// <returns>int</returns>
        public int insertUser(User user) 
        {
            return service.insertUser(user);
        }

        /// <summary>
        /// Calls service method to authenticate user
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns>int</returns>
        public int authenticateUser(string email, string password) 
        {
            return service.authenticateUser(email, password);
        }

        /// <summary>
        /// Retrieves user by id from service method
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>User</returns>
        public User getUserById(int userId) 
        {
            return service.getUserById(userId);
        }
    }
}
