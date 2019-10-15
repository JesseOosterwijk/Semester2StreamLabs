using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Interfaces
{
    public interface IUserContext
    {
        void CreateUser(User user);
        void EditUser(User user);
        void DeleteUser(User user);
        List<User> GetAllUsers();
        void BanUser(int userId);
        bool CheckIfUserAlreadyExists(string email);
        bool CheckIfAccountIsActive(string email);
        User GetUserInfo(string email);
        User GetUserById(int userId);
        bool CheckIfEmailIsValid(string userEmail);
        User CheckValidityUser(string emailAddress, string password);
    }
}
