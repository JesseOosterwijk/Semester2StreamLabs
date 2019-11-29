﻿using System;

namespace Models
{
    public class Viewer : User
    {
        public Viewer(int userId, string userName, string firstName, string lastName, AccountType accountType, DateTime birthDate, Gender gender, string email, string address, string postalCode, string city, bool status) : base(userId, userName, firstName, lastName, accountType, birthDate, gender, email, address, postalCode, city, status)
        {
        }

        public Viewer(int userId, AccountType accountType, string userName, string firstName, string lastName, DateTime birthDate, Gender gender, string email, string address, string city, string postalCode, string password, bool status) : base(userId, accountType, userName, firstName, lastName, birthDate, gender, email, address, city, postalCode, password, status)
        {

        }

        public Viewer(AccountType accountType, string userName, string firstName, string lastName, DateTime birthDate, Gender gender, string email, string address, string city, string postalCode, string password, bool status) : base(accountType, userName, firstName, lastName, birthDate, gender, email, address, city, postalCode, password, status)
        {

        }
    }
}
