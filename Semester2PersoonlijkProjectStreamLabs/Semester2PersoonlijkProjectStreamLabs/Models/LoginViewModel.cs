using Models;
using System.ComponentModel.DataAnnotations;

namespace Semester2PersoonlijkProjectStreamLabs.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Het emailadres moet worden ingevuld")]
        public string EmailAddress { get; set; }
        [Required(ErrorMessage = "Het wachtwoord moet worden ingevuld.")]
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
