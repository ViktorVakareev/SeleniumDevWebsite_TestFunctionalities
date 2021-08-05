using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using System.Threading;

namespace TestProjectSeleniumDev
{
    [TestFixture]
    public class SeleniumDevTests
    {
        IWebDriver _driver;

        [SetUp]
        public void Setup()
        {
            _driver = new ChromeDriver();
            _driver.Navigate().GoToUrl("https://www.selenium.dev/documentation/en/getting_started/");
        }

        [TearDown]
        public void CleanUp()
        {
            _driver.Quit(); ;
        }

        [Test]
        public void SeleniumDevPageLoadedCorrectly()
        {

            Assert.AreEqual("https://www.selenium.dev/documentation/en/getting_started/", _driver.Url);
        }

        [Test]
        public void SeleniumDevSelectGrid_Components()
        {
            var gridLink = _driver.FindElement(By.XPath("//*[@id=\"sidebar\"]/div[2]/ul/li[9]/a"));
            gridLink.Click();
            var componentsLink = _driver.FindElement(By.XPath("//*[@id=\"sidebar\"]/div[2]/ul/li[9]/ul/li[3]/ul/li[1]/a"));
            componentsLink.Click();
            var componentsText = _driver.FindElement(By.XPath("//*[@id='body-inner']/h1")).Text;

            Assert.AreEqual("Components", componentsText);
        }


        [Test]
        public void SeleniumDevClickGitHubRepoLink()
        {

            //var gitHubRepoLink = _driver.FindElement(By.XPath("//*[@id='shortcuts']/ul/li[2]/a"));           

            //Actions actions = new Actions(_driver);
            //actions.MoveToElement(_driver.FindElement(By.XPath("//*[@id='shortcuts']/ul/li[2]/a")));
            //actions.Perform();

            var js = (IJavaScriptExecutor)_driver;



            //Find element by link text and store in variable "Element"        		
            var gitHubRepoLink = _driver.FindElement(By.XPath("//*[@id='shortcuts']/ul/li[2]/a"));


            //This will scroll the page till the element is found		
            js.ExecuteScript("arguments[0].scrollIntoView();", gitHubRepoLink);
            Thread.Sleep(2000);
            gitHubRepoLink.Click();

            Assert.AreEqual("https://github.com/SeleniumHQ/seleniumhq.github.io", _driver.Url);
        }

    }
}