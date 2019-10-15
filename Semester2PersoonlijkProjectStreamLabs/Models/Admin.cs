using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class Admin : User
    {
        public Admin(int userId, string userName, string firstName, string lastName, AccountType accountType, DateTime birthDate, Gender gender, string email, string address, string postalCode, string city, bool status) : base(userId, userName, firstName, lastName, accountType, birthDate, gender, email, address, postalCode, city, status)
        {
        }

        public Admin(int userId, string firstName, string lastName, string address, string city, string postalCode, string emailAddress, DateTime dateBirth, Gender userGender, bool status, AccountType accountType, string password) : base(userId, firstName, lastName, address, city, postalCode, emailAddress, dateBirth, userGender, status, accountType, password)
        {
        }
    }
}
