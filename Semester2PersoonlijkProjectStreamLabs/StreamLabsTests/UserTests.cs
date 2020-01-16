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
    public class UserTests
    {

        private UserLogic _userLogic;
        private IUserContext _userContext;
        private List<User> userList = new List<User>();

        private void InstanceLogic()
        {
            userList.Add(new Viewer(1, "Henkie", "Henk", "de Slager", User.AccountType.Viewer, DateTime.Now, User.Gender.Male, "testmail", "Kleidonk 1", "6641LM", "Dodewaard", true));
            userList.Add(new Viewer(2, "Henkie", "Henk", "de Slager", User.AccountType.Viewer, DateTime.Now, User.Gender.Male, "testmailnonactive", "Kleidonk 1", "6641LM", "Dodewaard", false));
            userList.Add(new Viewer(3, "Henkie", "Henk", "de Slager", User.AccountType.Viewer, DateTime.Now, User.Gender.Male, "testmail", "Kleidonk 1", "6641LM", "Dodewaard", true));
            userList.Add(new Viewer(4, "Henkie", "Henk", "de Slager", User.AccountType.Viewer, DateTime.Now, User.Gender.Male, "testmail123", "Kleidonk 1", "6641LM", "Dodewaard", true));
            userList.Add(new Viewer(5, "Henkie", "Henk", "de Slager", User.AccountType.Viewer, DateTime.Now, User.Gender.Male, "testmail", "Kleidonk 1", "6642LM", "Dodewaard", true));
            userList[4].Password = "123";
            _userContext = new UserMemory(userList);
            _userLogic = new UserLogic(_userContext);

        }

        [TestMethod]
        public void GetAllUsers()
        {
            //Arrange
            InstanceLogic();

            var expected = userList;
            var result = _userLogic.GetAllUsers();

            for (int i = 0; i < userList.Count; i++)
            {
                Assert.AreEqual(expected[i], result[i]);
            }
        }

        [TestMethod]
        public void GetUserById()
        {
            InstanceLogic();
            var expected = userList[4];
            var result = _userLogic.GetUserById(userList[4].UserId);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void CheckIfUserAlreadyExists_TrueCheck()
        {
            InstanceLogic();

            var result = _userLogic.CheckIfUserAlreadyExists("testmail");

            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void CheckIfUserAlreadyExists_FalseCheck()
        {
            InstanceLogic();

            var result = _userLogic.CheckIfUserAlreadyExists("newtestmail");

            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void CheckIfAcountIsActive_TrueCheck()
        {
            InstanceLogic();

            var result = _userLogic.CheckIfAccountIsActive("testmail");

            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void CheckIfAcountIsActive_FalseCheck()
        {
            InstanceLogic();

            var result = _userLogic.CheckIfAccountIsActive("testmailnonactive");

            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void GetUserInfo()
        {
            InstanceLogic();
            var expected = userList[3];
            var result = _userLogic.GetUserInfo(userList[3].EmailAddress);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void CheckValidityUser_TrueCheck()
        {
            InstanceLogic();
            var result = _userLogic.CheckValidityUser(userList[4].EmailAddress, userList[4].Password);

            Assert.AreEqual(userList[4], result);
        }

        [TestMethod]
        public void CheckValidityUser()
        {
            InstanceLogic();
            var result = _userLogic.CheckValidityUser(userList[4].EmailAddress, "wrongpass");

            Assert.AreNotEqual(result, userList[4]);
        }
    }
}
