using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;

namespace Selenium_Weather_Forecast.Mel_tests
{
    internal class Search_history_count
    {
        IWebDriver driver;
        [SetUp]
        public void Initialize()
        {
            driver = new ChromeDriver("./chromedriver.exe");
            driver.Url = "https://localhost:5001/";
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        }
        [Test]
        public void Test()
        {
            driver.Navigate().GoToUrl("https://localhost:5001/Account/Register");
            string username = Guid.NewGuid().ToString();
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            driver.FindElement(By.Id("Username")).SendKeys(username);
            driver.FindElement(By.Id("Password")).SendKeys("Abba@Babba22");
            driver.FindElement(By.Id("ConfirmPassword")).SendKeys("Abba@Babba22");

            driver.FindElement(By.XPath("//*[contains(text(), 'Register') and contains(@class, 'btn-primary')]")).Click();
            wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.XPath("/html/body/div/main/h1")));
            IWebElement successHeader = driver.FindElement(By.XPath("/html/body/div/main/h1"));
            Assert.That(successHeader.Text == "You have successfully registered your account, please log in with it");

            driver.FindElement(By.XPath("//*[contains(text(), 'Login') and contains(@class, 'nav-link')]")).Click();
            driver.FindElement(By.Id("Username")).SendKeys(username);
            driver.FindElement(By.Id("Password")).SendKeys("Abba@Babba22");
            driver.FindElement(By.XPath("//*[contains(@class, 'btn-primary')]")).Submit();
            IWebElement heading = driver.FindElement(By.ClassName("display-4"));
            Assert.That(heading.Displayed == true);
            Assert.That(heading.Text == "Welcome to the weather forecast app");

            void Search(string place)
            {
                driver.FindElement(By.Id("cityNameInput")).SendKeys(place);
                driver.FindElement(By.XPath("//span[text()='Search specific day']")).Click();
                wait.Until(ExpectedConditions.ElementExists(By.Id("weatherAddress")));
                Thread.Sleep(100);
                string realPlace = driver.FindElement(By.Id("weatherAddress")).Text;
                Assert.That(driver.Url == "https://localhost:5001/Home/City");
                Assert.That(realPlace == place);
                driver.FindElement(By.Id("cityNameInput")).Clear();
            }
            string[] places = new string[] { "Tallinn", "Pärnu", "Paris" };
            foreach(var place in places)
            {
                Search(place);
            }
            driver.FindElement(By.XPath("//span[text()='Get Search History']")).Click();
            int tbCount = driver.FindElements(By.CssSelector("td[style*=\"text-align: left; word-wrap: break-word;\"]")).Count;
            Assert.That(tbCount == places.Length);

        }
        [TearDown]
        public void EndTest()
        {
            driver.Close();
            driver.Quit();
        }
    }
}
