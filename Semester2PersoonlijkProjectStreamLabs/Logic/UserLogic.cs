using System;
using System.Collections.Generic;
using Data.Interfaces;
using Models;

namespace Logic
{
    public class UserLogic
    {
        private readonly IUserContext _user;

        public UserLogic(IUserContext user)
        {
            _user = user;
        }

        public void CreateUser(User newUser)
        {
            newUser.Password = Hasher.SecurePasswordHasher.Hash(newUser.Password);

            _user.CreateUser(newUser);
        }

        public void EditUser(User user)
        {
            _user.EditUser(user);
        }

        public void DeleteUser(User user)
        {
            _user.DeleteUser(user);
        }

        public List<User> GetAllUsers()
        {
            return _user.GetAllUsers();
        }

        public void BanUser(int userId)
        {
            _user.BanUser(userId);
        }

        public bool CheckIfUserAlreadyExists(string email)
        {
            return _user.CheckIfUserAlreadyExists(email);
        }

        public bool CheckIfAccountIsActive(string email)
        {
            return _user.CheckIfAccountIsActive(email);
        }

        public User GetUserInfo(string email)
        {
            return _user.GetUserInfo(email);
        }

        public User GetUserById(int userId)
        {
            return _user.GetUserById(userId);
        }

        public bool CheckIfEmailIsValid(string userEmail)
        {
            return _user.CheckIfEmailIsValid(userEmail);
        }

        public User CheckValidityUser(string emailAddress, string password)
        {
            return _user.CheckValidityUser(emailAddress, password);
        }
    }
}
