using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Models;

namespace Semester2PersoonlijkProjectStreamLabs.Models
{
    public class UserViewModel
    {
        public enum AccountType { CareRecipient, Volunteer, Professional, Admin }
        public enum Gender { Man, Vrouw, Anders }

        public int UserId { get; set; }
        [Required(ErrorMessage = "Please fill in your username.")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Please fill in your first name.")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Please fill in your last name.")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Please fill in your address.")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Please fill in your residence.")]
        public string City { get; set; }
        [Required(ErrorMessage = "Please fill in your postal code.")]
        public string PostalCode { get; set; }
        [Required(ErrorMessage = "Please fill in your email.")]
        public string EmailAddress { get; set; }
        [Required(ErrorMessage = "Please fill in your birthdate.")]
        public DateTime BirthDate { get; set; }
        [Required(ErrorMessage = "Please fill in your gender.")]
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
