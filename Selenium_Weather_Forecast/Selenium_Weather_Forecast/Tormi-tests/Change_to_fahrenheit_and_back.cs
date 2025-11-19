using Microsoft.Testing.Platform.Extensions.Messages;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using static Selenium_Weather_Forecast.Tormi_tests.Login;

namespace Selenium_Weather_Forecast.Tormi_tests
{
    internal class Change_to_fahrenheit_and_back
    {
        IWebDriver driver;
        [SetUp]
        public void Initialize()
        {
            driver = new ChromeDriver();
            driver.Url = "https://localhost:5001/";
            LoginInitialize(driver);
        }
        [Test]
        public void Test()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));

            driver.FindElement(By.Id("cityNameInput")).SendKeys("Tallinn");

            driver.FindElement(By.XPath("//span[text()='Search specific day']")).Click();

            Assert.That(wait.Until(ExpectedConditions.TextToBePresentInElementLocated(By.CssSelector("tbody tr:nth-child(2) td:nth-child(2)"), "°C")));

            driver.FindElement(By.XPath("//a[@href='/Account/AccountSettings']")).Click();

            wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("typeCheckbox"))).Click();

            driver.FindElement(By.XPath("//img[@src='https://www.svgrepo.com/show/483341/home.svg']")).Click();

            wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("cityNameInput"))).SendKeys("Tallinn");

            driver.FindElement(By.XPath("//span[text()='Search specific day']")).Click();

            Assert.That(wait.Until(ExpectedConditions.TextToBePresentInElementLocated(By.CssSelector("tbody tr:nth-child(2) td:nth-child(2)"), "°F")));

            driver.FindElement(By.XPath("//a[@href='/Account/AccountSettings']")).Click();

            wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("typeCheckbox"))).Click();

            driver.FindElement(By.XPath("//img[@src='https://www.svgrepo.com/show/483341/home.svg']")).Click();

            wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("cityNameInput"))).SendKeys("Tallinn");

            driver.FindElement(By.XPath("//span[text()='Search specific day']")).Click();

            Assert.That(wait.Until(ExpectedConditions.TextToBePresentInElementLocated(By.CssSelector("tbody tr:nth-child(2) td:nth-child(2)"), "°C")));
        }
        [TearDown]
        public void EndTest()
        {
            driver.Close();
        }
    }
}
