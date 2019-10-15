using System;

namespace Models
{
    public class User
    {
        public enum AccountType { Admin, Viewer }
        public enum Gender { Male, Female, Other }
        public int UserId { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string UserName { get; }
        public string Address { get; }
        public string City { get; }
        public string PostalCode { get; }
        public string EmailAddress { get; }
        public DateTime BirthDate { get; }
        public Gender UserGender { get; }
        public AccountType UserAccountType { get; }
        public bool Status { get; set; }
        public string Password { get; set; }

        public User(string firstName, string lastName, string userName, string address, string city, string postalCode, string emailAddress, DateTime birthDate, Gender userGender, bool status, AccountType accountType, string password)
        {
            FirstName = firstName;
            LastName = lastName;
            UserName = userName;
            Address = address;
            City = city;
            PostalCode = postalCode;
            EmailAddress = emailAddress;
            BirthDate = birthDate;
            UserGender = userGender;
            Status = status;
            UserAccountType = accountType;
            Password = password;
        }

        public User(int userId, string firstName, string lastName, string address, string city, string postalCode, string emailAddress, DateTime birthDate, Gender userGender, bool status, AccountType accountType, string password)
        {
            UserId = userId;
            FirstName = firstName;
            LastName = lastName;
            Address = address;
            City = city;
            PostalCode = postalCode;
            EmailAddress = emailAddress;
            BirthDate = birthDate;
            UserGender = userGender;
            Status = status;
            UserAccountType = accountType;
            Password = password;
        }

        protected User(int userId, string userName, string firstName, string lastName, AccountType accountType, DateTime birthDate, Gender gender, string email, string address, string postalCode, string city, bool status)
        {
            UserId = userId;
            UserName = userName;
            FirstName = firstName;
            LastName = lastName;
            UserAccountType = accountType;
            BirthDate = birthDate;
            UserGender = gender;
            EmailAddress = email;
            Address = address;
            PostalCode = postalCode;
            City = city;
            Status = status;
        }

        protected User()
        {
            ;
        }
    }
}
