using Data.Interfaces;
using Data.Memory;
using Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Models;
using System;

namespace StreamLabsTests
{
    [TestClass]
    public class AccountTests
    {
        private AccountLogic _accountLogic;
        private IAccountContext _accountContext;
        private List<User> userList = new List<User>();

        private void InstanceLogic()
        {
            userList.Add(new Viewer(1, "Henkie", "Henk", "de Slager", User.AccountType.Viewer, DateTime.Now, User.Gender.Male, "testmail", "Kleidonk 1", "6641LM", "Dodewaard", true));
            userList.Add(new Viewer(2, "Henkie", "Henk", "de Slager", User.AccountType.Viewer, DateTime.Now, User.Gender.Male, "testmailnonactive", "Kleidonk 1", "6641LM", "Dodewaard", false));
            userList.Add(new Viewer(3, "Henkie", "Henk", "de Slager", User.AccountType.Viewer, DateTime.Now, User.Gender.Male, "testmail", "Kleidonk 1", "6641LM", "Dodewaard", true));
            userList.Add(new Viewer(4, "Henkie", "Henk", "de Slager", User.AccountType.Viewer, DateTime.Now, User.Gender.Male, "testmail123", "Kleidonk 1", "6641LM", "Dodewaard", true));
            userList.Add(new Viewer(5, "Henkie", "Henk", "de Slager", User.AccountType.Viewer, DateTime.Now, User.Gender.Male, "testmail", "Kleidonk 1", "6642LM", "Dodewaard", true));
            userList[4].Password = "123";
            _accountContext = new AccountMemory(userList);
            _accountLogic = new AccountLogic(_accountContext);
        }

        [TestMethod]
        public void ChangePassword()
        {
            InstanceLogic();
            var passBefore = userList[4].Password;
            var result = _accountLogic.ChangePassword(userList[4].UserId);

            Assert.AreNotEqual(passBefore, result);
        }
    }
}
