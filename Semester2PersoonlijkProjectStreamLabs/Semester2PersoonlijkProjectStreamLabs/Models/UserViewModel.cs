﻿using System;
using System.Collections.Generic;
using Models;

namespace Semester2PersoonlijkProjectStreamLabs.Models
{
    public class UserViewModel
    {
        public enum AccountType { CareRecipient, Volunteer, Professional, Admin }
        public enum Gender { Man, Vrouw, Anders }

        public int UserId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string EmailAddress { get; set; }
        public DateTime BirthDate { get; set; }
        public string UserGender { get; set; }
        public User.AccountType UserAccountType { get; set; }
        public bool Status { get; set; }
        public User Professional { get; set; }
        public List<User> Users { get; internal set; }

        public UserViewModel(User user)
        {
            UserId = user.UserId;
            UserName = user.UserName;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Address = user.Address;
            City = user.City;
            PostalCode = user.PostalCode;
            EmailAddress = user.EmailAddress;
            BirthDate = user.BirthDate.Date;
            UserGender = user.UserGender.ToString();
            UserAccountType = user.UserAccountType;
            Status = user.Status;
        }

        public UserViewModel()
        {

        }
    }
}
