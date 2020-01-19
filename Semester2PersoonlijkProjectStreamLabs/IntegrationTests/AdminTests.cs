using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.IO;
using System.Reflection;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;

namespace IntegrationTests
{
    public class AdminTests
    {
        private IWebDriver _driver;
        public string _homeUrl;

        [SetUp]
        public void Setup()
        {
            _homeUrl = "https://localhost:44397/";
            _driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
         }

        private void LoadHome()
        {
            _driver.Manage().Window.Maximize();
            _driver.Navigate().GoToUrl(_homeUrl);
        }

        private void AcceptCookies()
        {
            _driver.FindElement(By.Id("AcceptCookie")).Click();
        }
        
        private void AddNewCategory()
        {
            _driver.FindElement(By.Id("CategoryOverview")).Click();
            _driver.FindElement(By.Id("NewCategoryBtn")).Click();
            _driver.FindElement(By.Id("Description")).SendKeys("testcat");
            _driver.FindElement(By.Id("CategoryName")).SendKeys("testname");
            _driver.FindElement(By.Id("AddCategory")).Click();
        }

        private void EditDefaultCategory()
        {
            _driver.Navigate().GoToUrl("https://localhost:44397/Admin/EditCategory/10");
            _driver.FindElement(By.Id("Description")).Clear();
            _driver.FindElement(By.Id("Description")).SendKeys("Edited default category");
            _driver.FindElement(By.Id("CategoryName")).Clear();
            _driver.FindElement(By.Id("CategoryName")).SendKeys("New default cat");
            _driver.FindElement(By.Id("EditCategory")).Click();
        }

        private void LoginAsAdmin()
        {
            _driver.FindElement(By.Id("EmailAddress")).SendKeys("testadmin@test.com");
            _driver.FindElement(By.Id("Password")).SendKeys("123");
            _driver.FindElement(By.Id("LoginBtn")).Click();
        }

        [Test]
        public void GoToCategoryOverview()
        {
            LoadHome();
            AcceptCookies();
            _driver.FindElement(By.Id("Login")).Click();
            LoginAsAdmin();
            _driver.FindElement(By.Id("CategoryOverview")).Click();

            Assert.AreEqual("CategoryOverview - Semester2PersoonlijkProjectStreamLabs", _driver.Title);
        }

        [Test]
        public void AddNewCategoryTests()
        {
            LoadHome();
            AcceptCookies();
            _driver.FindElement(By.Id("Login")).Click();
            LoginAsAdmin();
            AddNewCategory();

            Assert.True(_driver.PageSource.Contains("testcat"));
            Assert.True(_driver.PageSource.Contains("testname"));
            Assert.AreEqual("CategoryOverview - Semester2PersoonlijkProjectStreamLabs", _driver.Title);
        }

        [Test]
        public void EditCategoryTests()
        {
            LoadHome();
            AcceptCookies();
            _driver.FindElement(By.Id("Login")).Click();
            LoginAsAdmin();
            EditDefaultCategory();

            Assert.True(_driver.PageSource.Contains("Edited default category"));
            Assert.True(_driver.PageSource.Contains("New default cat"));
            Assert.AreEqual("CategoryOverview - Semester2PersoonlijkProjectStreamLabs", _driver.Title);
        }

        [Test]
        public void DeleteCategoryTests()
        {
            LoadHome();
            AcceptCookies();
            _driver.FindElement(By.Id("Login")).Click();
            LoginAsAdmin();
            _driver.FindElement(By.Id("CategoryOverview")).Click();

            IList<IWebElement> eleList = _driver.FindElements(By.LinkText("Delete Category"));
            eleList[eleList.Count - 1].Click();

            Assert.False(_driver.PageSource.Contains("testcat"));
            Assert.False(_driver.PageSource.Contains("testname"));
            Assert.AreEqual("CategoryOverview - Semester2PersoonlijkProjectStreamLabs", _driver.Title);
        }

        [Test]
        public void GoToUserOverview()
        {
            LoadHome();
            AcceptCookies();
            _driver.FindElement(By.Id("Login")).Click();
            LoginAsAdmin();
            _driver.FindElement(By.Id("UserOverview")).Click();

            Assert.AreEqual("UserOverview - Semester2PersoonlijkProjectStreamLabs", _driver.Title);
        }

        [Test]
        public void GoToReportOverview()
        {
            LoadHome();
            AcceptCookies();
            _driver.FindElement(By.Id("Login")).Click();
            LoginAsAdmin();
            _driver.FindElement(By.Id("ReportOverview")).Click();

            Assert.AreEqual("ReportOverview - Semester2PersoonlijkProjectStreamLabs", _driver.Title);
        }

        //TODO: Make it work so it deletes testreport
        //[Test]
        //public void DeleteReport()
        //{
        //    LoadHome();
        //    AcceptCookies();
        //    _driver.FindElement(By.Id("Login")).Click();
        //    LoginAsAdmin();
        //    _driver.FindElement(By.Id("ReportOverview")).Click();
        //    IList<IWebElement> eleList = _driver.FindElements(By.Id("DeleteReport"));
        //    eleList[eleList.Count - 1].Click();

        //    Assert.AreEqual("ReportOverview - Semester2PersoonlijkProjectStreamLabs", _driver.Title);
        //    Assert.False(_driver.PageSource.Contains("testreport"));
        //}

        [TearDown]
        public void TearDownTest()
        {
            _driver.Close();
            _driver.Dispose();
        }

    }
}