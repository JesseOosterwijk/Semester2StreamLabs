using Models;
using System.Collections.Generic;

namespace Data.Interfaces
{
    public interface IUserContext
    {
        void CreateUser(User user);
        void EditUser(User user);
        List<User> GetAllUsers();
        void BanUser(int userId);
        bool CheckIfUserAlreadyExists(string email);
        bool CheckIfAccountIsActive(string email);
        User GetUserInfo(string email);
        User GetUserById(int userId);
        bool CheckIfEmailIsValid(string userEmail);
        User CheckValidityUser(string emailAddress, string password);
        bool SendEmail(string emailaddress, string newPassword);
    }
}
