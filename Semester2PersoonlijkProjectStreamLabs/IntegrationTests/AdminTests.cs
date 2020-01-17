using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.IO;
using System.Reflection;
using OpenQA.Selenium.Support.UI;

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
            _driver.FindElement(By.Id("CategoryBtn")).Click();
            _driver.FindElement(By.Id("NewCategoryBtn")).Click();
            _driver.FindElement(By.Id("Description")).SendKeys("testcat");
            _driver.FindElement(By.Id("Name")).SendKeys("testname");
            _driver.FindElement(By.Id("AddCategory")).Click();
        }

        private void EditCategory()
        {
            _driver.FindElement(By.Id("CategoryBtn")).Click();
            _driver.FindElement(By.Id("EditCategoryBtn")).Click();
            _driver.FindElement(By.Id("Description")).SendKeys("editcat");
            _driver.FindElement(By.Id("Name")).SendKeys("editname");
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

            Assert.AreEqual("Category Overiew", _driver.Title);
        }

        [Test]
        public void AddNewCategoryTests()
        {
            LoadHome();
            AcceptCookies();
            _driver.FindElement(By.Id("Login")).Click();
            LoginAsAdmin();
            AddNewCategory();

            Assert.True(_driver.PageSource.Contains("editcat"));
            Assert.True(_driver.PageSource.Contains("editname"));
            Assert.AreEqual("Category Overview", _driver.Title);
        }

        [Test]
        public void EditCategoryTests()
        {
            LoadHome();
            AcceptCookies();
            _driver.FindElement(By.Id("Login")).Click();
            LoginAsAdmin();
            EditCategory();

            Assert.True(_driver.PageSource.Contains("testcat"));
            Assert.True(_driver.PageSource.Contains("testname"));
            Assert.AreEqual("Category Overview", _driver.Title);
        }

        [Test]
        public void DeleteCategoryTests()
        {
            LoadHome();
            AcceptCookies();
            _driver.FindElement(By.Id("Login")).Click();
            LoginAsAdmin();
            _driver.FindElement(By.Id("CategoryOverview")).Click();

            _driver.FindElement(By.Id("DeleteLink")).Click();

            Assert.False(_driver.PageSource.Contains("testcat"));
            Assert.False(_driver.PageSource.Contains("testname"));
            Assert.AreEqual("Category Overview", _driver.Title);
        }

        [Test]
        public void GoToUserOverview()
        {
            LoadHome();
            AcceptCookies();
            _driver.FindElement(By.Id("Login")).Click();
            LoginAsAdmin();
            _driver.FindElement(By.Id("UserOverview")).Click();

            Assert.AreEqual("User Overview", _driver.Title);
        }

        [Test]
        public void GoToReportOverview()
        {
            LoadHome();
            AcceptCookies();
            _driver.FindElement(By.Id("Login")).Click();
            LoginAsAdmin();
            _driver.FindElement(By.Id("ReportOverview")).Click();

            Assert.AreEqual("Report Overview", _driver.Title);
        }

        [TearDown]
        public void TearDownTest()
        {
            _driver.Close();
            _driver.Dispose();
        }

    }
}