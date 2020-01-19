using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.IO;
using System.Reflection;

namespace IntegrationTests
{
    public class UserTests
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

        private void LoginAsViewer()
        {
            _driver.FindElement(By.Id("EmailAddress")).SendKeys("testviewer@test.com");
            _driver.FindElement(By.Id("Password")).SendKeys("123");
            _driver.FindElement(By.Id("LoginBtn")).Click();
        }

        [Test]
        public void SearchForVideos()
        {
            LoadHome();
            AcceptCookies();
            _driver.FindElement(By.Id("Login")).Click();
            LoginAsViewer();
            _driver.FindElement(By.Id("Searchfield")).SendKeys("testsearch");
            _driver.FindElement(By.Id("SearchFieldBtn")).Click();

            Assert.AreEqual("ViewerVideoList - Semester2PersoonlijkProjectStreamLabs", _driver.Title);
        }

        [Test]
        public void GoToSettings()
        {
            LoadHome();
            AcceptCookies();
            _driver.FindElement(By.Id("Login")).Click();
            LoginAsViewer();
            _driver.FindElement(By.Id("Settings")).Click();

            Assert.AreEqual("SettingsMenu - Semester2PersoonlijkProjectStreamLabs", _driver.Title);
        }

        [TearDown]
        public void TearDownTest()
        {
            _driver.Close();
            _driver.Dispose();
        }
    }
}
