using Data.Interfaces;
using Models;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Data.Memory
{
    public class UserMemory : IUserContext
    {

        private List<User> testUsers = new List<User>();

        public UserMemory(List<User> testUserList)
        {
            foreach (User user in testUserList)
            {
                testUsers.Add(user);
            }
        }

        public void CreateUser(User user)
        {
            User newUser = new Viewer(user.UserId, user.UserName, user.FirstName, user.LastName, user.UserAccountType, user.BirthDate, user.UserGender, user.EmailAddress, user.Address, user.PostalCode, user.City, user.Status);
        }

        public void EditUser(User user)
        {

        }

        public List<User> GetAllUsers()
        {
            List<User> newUsers = new List<User>();

            foreach(var item in testUsers)
            {
                newUsers.Add(item);
            }
            return newUsers;
        }

        public void BanUser(int userId)
        {

        }

        public bool CheckIfUserAlreadyExists(string email)
        {
            List<User> testedUsers = new List<User>();
            foreach(var item in testUsers.Where(c => c.EmailAddress == email))
            {
                testedUsers.Add(item);
            }

            if (testedUsers.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool CheckIfEmailIsValid(string email)
        {
            return false;
        }

        public bool CheckIfAccountIsActive(string email)
        {
            User currentUser = new Viewer();
            foreach (var item in testUsers.Where(c => c.EmailAddress == email))
            {
                currentUser = item;
            }

            return currentUser.Status;
        }

        public User GetUserInfo(string email)
        {
            User currentUser = new Viewer();
            foreach (var item in testUsers.Where(c => c.EmailAddress == email))
            {
                currentUser = item;
            }

            return currentUser;
        }

        public User GetUserById(int userId)
        {
            User currentUser = new Viewer();
            foreach (var item in testUsers.Where(c => c.UserId == userId))
            {
                currentUser = item;
            }

            return currentUser;
        }

        public User CheckValidityUser(string email, string password)
        {
            User currentUser = new Viewer();
            foreach(var item in testUsers.Where(c => c.EmailAddress == email))
            {
                if(item.Password == password)
                {
                    currentUser = item;
                }
            }
            return currentUser;
        }

        public bool SendEmail(string email, string newPass)
        {
            return false;
        }
    }
}
