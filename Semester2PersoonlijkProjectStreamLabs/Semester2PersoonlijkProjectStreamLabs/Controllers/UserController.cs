using System;
using System.Linq;
using System.Security.Claims;
using Logic;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Semester2PersoonlijkProjectStreamLabs.Models;

namespace Semester2PersoonlijkProjectStreamLabs.Controllers
{
    public class UserController : Controller
    {
        private readonly UserLogic _userLogic;
        private readonly AccountLogic _accountLogic;

        public UserController(UserLogic userLogic, AccountLogic accountLogic)
        {
            _userLogic = userLogic;
            _accountLogic = accountLogic;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel userViewModel)
        {
            try
            {
                User newCustomer = _userLogic.CheckValidityUser(userViewModel.EmailAddress, userViewModel.Password);

                if (!newCustomer.Status)
                {
                    ViewBag.Message = "Dit account is geblokkeerd. Neem contact op met de administrator.";
                    return View();
                }

                var identity = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Sid, newCustomer.UserId.ToString()),
                    new Claim(ClaimTypes.Name, newCustomer.UserName),
                    new Claim(ClaimTypes.Gender, newCustomer.UserGender.ToString()),
                    new Claim(ClaimTypes.Email, newCustomer.EmailAddress),
                    new Claim(ClaimTypes.PostalCode, newCustomer.PostalCode),
                    new Claim(ClaimTypes.StreetAddress, newCustomer.Address),
                    new Claim(ClaimTypes.Role, newCustomer.UserAccountType.ToString())
                }, CookieAuthenticationDefaults.AuthenticationScheme);

                var principal = new ClaimsPrincipal(identity);

                HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    principal);

                switch (newCustomer.UserAccountType)
                {
                    case global::Models.User.AccountType.Admin:
                        return RedirectToAction("UserOverview", "Viewer");
                    case global::Models.User.AccountType.Viewer:
                        return RedirectToAction("UploadVideo", "Video");
                    default:
                        return RedirectToAction("UploadVideo", "Video");
                }
            }
            catch (NullReferenceException)
            {
                ViewBag.Message = "De gegevens zijn niet ingevuld";
                return View();
            }
            catch (IndexOutOfRangeException)
            {
                ViewBag.Message = "De gegevens komen niet overeen";
                return View();
            }
            catch (ArgumentException)
            {
                ViewBag.Message = "Wachtwoord verkeerd ingevuld";
                return View();
            }
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction(nameof(Login));
        }

        [HttpGet]
        public ActionResult CreateAccount()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateAccount(UserViewModel userViewModel, string password, string passwordValidation)
        {
            try
            {
                if (password == passwordValidation)
                {
                    if (_userLogic.CheckIfUserAlreadyExists(userViewModel.EmailAddress))
                    {
                        if (_userLogic.CheckIfEmailIsValid(userViewModel.EmailAddress))
                        {
                            switch (userViewModel.UserAccountType)
                            {
                                case global::Models.User.AccountType.Viewer:
                                    _userLogic.CreateUser(new Viewer(global::Models.User.AccountType.Viewer, userViewModel.UserName, userViewModel.FirstName, userViewModel.LastName,
                                        Convert.ToDateTime(userViewModel.BirthDate), (User.Gender)Enum.Parse(typeof(User.Gender), userViewModel.UserGender), userViewModel.EmailAddress, userViewModel.Address, userViewModel.City,
                                        userViewModel.PostalCode, password, true));
                                    break;
                                case global::Models.User.AccountType.Admin:
                                    _userLogic.CreateUser(new Admin(global::Models.User.AccountType.Viewer, userViewModel.UserName, userViewModel.FirstName, userViewModel.LastName,
                                        Convert.ToDateTime(userViewModel.BirthDate), (User.Gender)Enum.Parse(typeof(User.Gender), userViewModel.UserGender), userViewModel.EmailAddress, userViewModel.Address, userViewModel.City,
                                        userViewModel.PostalCode, password, true));
                                    break;
                                default:
                                    _userLogic.CreateUser(
                                        new Viewer(global::Models.User.AccountType.Viewer, userViewModel.UserName, userViewModel.FirstName, userViewModel.LastName,
                                        Convert.ToDateTime(userViewModel.BirthDate), (User.Gender)Enum.Parse(typeof(User.Gender), userViewModel.UserGender), userViewModel.EmailAddress, userViewModel.Address, userViewModel.City,
                                        userViewModel.PostalCode, password, true));
                                    break;
                            }
                        }
                        else
                        {
                            ViewBag.Message = "Foutieve email ingevoerd";
                            return View();
                        }
                    }
                    else
                    {
                        ViewBag.Message = "Er bestaat al een account met deze e-mail";
                        return View();
                    }
                }
                else
                {
                    ViewBag.Message = "De wachtwoorden komen niet overheen";
                    return View();
                }
            }
            catch (Exception)
            {
                ViewBag.Message = "De gebruiker is niet aangemaakt";
                return View();
            }
            return RedirectToAction("Login");
        }

        public ActionResult EditAccount()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult EditAccount(UserViewModel userViewModel, string password)
        {
            var userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid).Value);
            if (!_userLogic.CheckIfUserAlreadyExists(userViewModel.EmailAddress))
            {
                if (_userLogic.CheckIfEmailIsValid(userViewModel.EmailAddress))
                {
                    try
                    {
                        switch (userViewModel.UserAccountType)
                        {
                            case global::Models.User.AccountType.Viewer:
                                _userLogic.EditUser(new Viewer(userId, global::Models.User.AccountType.Viewer, userViewModel.UserName, userViewModel.FirstName, userViewModel.LastName,
                                        Convert.ToDateTime(userViewModel.BirthDate), (User.Gender)Enum.Parse(typeof(User.Gender), userViewModel.UserGender), userViewModel.EmailAddress, userViewModel.Address, userViewModel.City,
                                        userViewModel.PostalCode, password, true));
                                break;
                            case global::Models.User.AccountType.Admin:
                                _userLogic.EditUser(new Viewer(userId, global::Models.User.AccountType.Viewer, userViewModel.UserName, userViewModel.FirstName, userViewModel.LastName,
                                        Convert.ToDateTime(userViewModel.BirthDate), (User.Gender)Enum.Parse(typeof(User.Gender), userViewModel.UserGender), userViewModel.EmailAddress, userViewModel.Address, userViewModel.City,
                                        userViewModel.PostalCode, password, true));
                                break;
                            default:
                                _userLogic.EditUser(new Viewer(userId, global::Models.User.AccountType.Viewer, userViewModel.UserName, userViewModel.FirstName, userViewModel.LastName,
                                        Convert.ToDateTime(userViewModel.BirthDate), (User.Gender)Enum.Parse(typeof(User.Gender), userViewModel.UserGender), userViewModel.EmailAddress, userViewModel.Address, userViewModel.City,
                                        userViewModel.PostalCode, password, true));
                                break;
                        }
                    }
                    catch (Exception)
                    {
                        ViewBag.Message = "De gegevens zijn onjuist ingevoerd.";
                        return RedirectToAction("SettingsMenu");
                    }
                }
                else
                {
                    ViewBag.Message = "Foutieve email ingevoerd";
                    return View();
                }
            }
            else
            {
                ViewBag.Message = "Er bestaat al een account met dit e-mailadres";
                return View();
            }

            return RedirectToAction("SettingsMenu");
        }

        public ActionResult DeleteAccount(UserViewModel user)
        {
            _accountLogic.DeleteUser(user.UserId);
            return View();
        }

        public IActionResult SettingsMenu()
        {
            var userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid).Value);
            var accountType = (User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value);
            UserViewModel model = new UserViewModel(_userLogic.GetUserById(userId))
            {
                Type = accountType.ToString()               
            };
            return View("../Viewer/SettingsMenu", model);
        }

    }
}