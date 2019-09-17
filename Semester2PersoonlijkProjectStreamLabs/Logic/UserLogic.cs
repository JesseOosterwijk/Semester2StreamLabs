using System;
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
    }
}
