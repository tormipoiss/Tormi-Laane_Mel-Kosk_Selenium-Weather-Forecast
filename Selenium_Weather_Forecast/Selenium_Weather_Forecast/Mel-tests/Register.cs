using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selenium_Weather_Forecast.Mel_tests
{
    internal class Register
    {
        IWebDriver driver;
        [SetUp]
        public void Initialize()
        {
            driver = new ChromeDriver();
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
            Assert.That(driver.Url == "https://localhost:5001/Account/Register");

        }
        [TearDown]
        public void EndTest()
        {
            driver.Close();
            driver.Quit();
        }
    }
}
