using Models;
using System.ComponentModel.DataAnnotations;

namespace Semester2PersoonlijkProjectStreamLabs.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Please fill in your e-mail")]
        public string EmailAddress { get; set; }
        [Required(ErrorMessage = "Please fill in your password.")]
        public string Password { get; set; }

        public LoginViewModel(User user, string password)
        {
            EmailAddress = user.EmailAddress;
            Password = password;
        }

        public LoginViewModel()
        {

        }
    }
}
