using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.IO;
using System.Reflection;
using OpenQA.Selenium.Support.UI;

namespace IntegrationTests
{
    public class CommentTests
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

        private void CommentOnVideo()
        {
            _driver.FindElement(By.Id("CommentBox")).SendKeys("testcomment");
            _driver.FindElement(By.Id("CommentBtn")).Click();

        }

        private void ReportVideo()
        {
            _driver.FindElement(By.Id("ReportLink")).Click();
            _driver.FindElement(By.Id("ReportContent")).SendKeys("testreport");
            _driver.FindElement(By.Id("ReportBtn")).Click();
        }

        [Test]
        public void CommentOnVideoTest()
        {
            LoadHome();
            AcceptCookies();
            _driver.FindElement(By.Id("Login")).Click();
            LoginAsViewer();

            _driver.Navigate().GoToUrl("https://localhost:44397/Video/CommentOnVideo?VideoId=30&VideoCategory=Gaming%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20&Name=WhatsApp%20Video%202020-01-07%20at%2011.25.30.mp4&Description=This%20is%20a%20snowboard%20lesson&DateOfUpload=01%2F16%2F2020%2016%3A20%3A25&VideoLength=0&Views=0&ContentUrl=%5Cvideo%5CTurtleSandwich%5CWhatsApp%20Video%202020-01-07%20at%2011.25.30.mp4&CategoryId=0");

            CommentOnVideo();

            Assert.IsTrue(_driver.PageSource.Contains("testcomment"));
            Assert.AreEqual(_driver.Url, "https://localhost:44397/Video/CommentOnVideo?VideoId=30&VideoCategory=Gaming%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20&Name=WhatsApp%20Video%202020-01-07%20at%2011.25.30.mp4&Description=This%20is%20a%20snowboard%20lesson&DateOfUpload=01%2F16%2F2020%2016%3A20%3A25&VideoLength=0&Views=0&ContentUrl=%5Cvideo%5CTurtleSandwich%5CWhatsApp%20Video%202020-01-07%20at%2011.25.30.mp4&CategoryId=0");
        }

        [Test]
        public void ReportVideoTest()
        {
            LoadHome();
            AcceptCookies();
            _driver.FindElement(By.Id("Login")).Click();
            LoginAsViewer();

            _driver.Navigate().GoToUrl("https://localhost:44397/Video/CommentOnVideo?VideoId=30&VideoCategory=Gaming%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20&Name=WhatsApp%20Video%202020-01-07%20at%2011.25.30.mp4&Description=This%20is%20a%20snowboard%20lesson&DateOfUpload=01%2F16%2F2020%2016%3A20%3A25&VideoLength=0&Views=0&ContentUrl=%5Cvideo%5CTurtleSandwich%5CWhatsApp%20Video%202020-01-07%20at%2011.25.30.mp4&CategoryId=0");

            ReportVideo();

            Assert.IsTrue(_driver.PageSource.Contains("testreport"));
            Assert.AreEqual(_driver.Url, "https://localhost:44397/Video/CommentOnVideo?VideoId=30&VideoCategory=Gaming%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20&Name=WhatsApp%20Video%202020-01-07%20at%2011.25.30.mp4&Description=This%20is%20a%20snowboard%20lesson&DateOfUpload=01%2F16%2F2020%2016%3A20%3A25&VideoLength=0&Views=0&ContentUrl=%5Cvideo%5CTurtleSandwich%5CWhatsApp%20Video%202020-01-07%20at%2011.25.30.mp4&CategoryId=0");
        }
        

        [TearDown]
        public void TearDownTest()
        {
            _driver.Close();
            _driver.Dispose();
        }

    }
}