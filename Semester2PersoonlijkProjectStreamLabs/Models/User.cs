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

        protected User(int userId, AccountType accountType, string userName, string firstName, string lastName, DateTime birthDate, Gender gender, string email, string address, string city, string postalCode, string password, bool status)
        {
            UserId = userId;
            UserAccountType = accountType;
            UserName = userName;
            FirstName = firstName;
            LastName = lastName;
            BirthDate = birthDate;
            UserGender = gender;
            EmailAddress = email;
            Address = address;
            City = city;
            PostalCode = postalCode;
            Password = password;
            Status = status;
        }

        protected User(AccountType accountType, string userName, string firstName, string lastName, DateTime birthDate, Gender gender, string email, string address, string city, string postalCode, string password, bool status)
        {
            UserAccountType = accountType;
            UserName = userName;
            FirstName = firstName;
            LastName = lastName;
            BirthDate = birthDate;
            UserGender = gender;
            EmailAddress = email;
            Address = address;
            City = city;
            PostalCode = postalCode;
            Password = password;
            Status = status;
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
