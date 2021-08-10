using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Threading;

namespace TestProjectSeleniumDev
{
    [TestFixture]
    public class SeleniumDevTests
    {
        private IWebDriver _driver;
        const string _url = "https://www.selenium.dev/documentation/getting_started/";

        [SetUp]
        public void Setup()
        {
            _driver = new ChromeDriver();         
            
        }

        [TearDown]
        public void CleanUp()
        {
            _driver.Quit(); 
        }

        [Test]
        public void SeleniumDevPageLoadedCorrectly_When_PageUrlEntered()
        {
            _driver.Navigate().GoToUrl(_url);
            Assert.AreEqual("https://www.selenium.dev/documentation/getting_started/", _driver.Url);
        }

        [Test]
        public void ComponentsPageLoadedCorrectly_When_LinkClickedInSideBar()
        {
            _driver.Navigate().GoToUrl(_url);
            var gridLink = _driver.FindElement(By.LinkText("Grid"));
            gridLink.Click();
            var componentsLink = _driver.FindElement(By.LinkText("Components"));
            componentsLink.Click();
            var componentsText = _driver.FindElement(By.TagName("h1")).Text;

            Assert.AreEqual("Components", componentsText);
            Assert.AreEqual("https://www.selenium.dev/documentation/grid/components_of_a_grid/", _driver.Url);
        }

        [Test]
        public void GitHubRepoPageLoadedCorrectly_When_LinkClicked()
        {
            _driver.Navigate().GoToUrl(_url);
            var js = (IJavaScriptExecutor)_driver;    		
            var gitHubRepoLink = _driver.FindElement(By.XPath("//*[@class='fab fa-github']"));                        
            js.ExecuteScript("arguments[0].scrollIntoView();", gitHubRepoLink);
            
            gitHubRepoLink.Click();                                    // opens new tab!?
            
            _driver.SwitchTo().Window(_driver.WindowHandles[1]);
            var headingGitHub = _driver.FindElement(By.LinkText("selenium"));

            Assert.AreEqual("selenium", headingGitHub.Text);
            Assert.AreEqual("https://github.com/seleniumhq/selenium", _driver.Url);
            _driver.SwitchTo().Window(_driver.WindowHandles[0]);
        }

        public IWebElement waitForElementToAppearUsingId(string Id)
        {
            var globalTimeout = TimeSpan.FromSeconds(8); 
            var sleepInterval = TimeSpan.FromSeconds(3); 

            var wait = new WebDriverWait
                (new SystemClock(), _driver, globalTimeout, sleepInterval);

            var element = wait.Until(ExpectedConditions.ElementExists(By.Id(Id))) ;           

            return element;
        }
       
    }
}