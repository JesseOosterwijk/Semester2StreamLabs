using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Interfaces
{
    public interface IUserContext
    {
        void CreateUser(User user);
    }
}
