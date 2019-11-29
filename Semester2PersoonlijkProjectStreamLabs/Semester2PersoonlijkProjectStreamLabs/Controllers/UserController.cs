using System;
using System.Security.Claims;
using Logic;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Models;
using ProftaakASP_S2.Models;
using Semester2PersoonlijkProjectStreamLabs.Models;

namespace Semester2PersoonlijkProjectStreamLabs.Controllers
{
    public class UserController : Controller
    {
        private readonly UserLogic _userLogic;

        public UserController(UserLogic userLogic)
        {
            _userLogic = userLogic;
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
                    new Claim(ClaimTypes.Name, newCustomer.FirstName + " " + newCustomer.LastName),
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
                        return RedirectToAction("Index", "Admin");
                    case global::Models.User.AccountType.Viewer:
                        return RedirectToAction("UploadVideo", "Video");
                    default:
                        return RedirectToAction("Overview", "Viewer");
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

    }
}