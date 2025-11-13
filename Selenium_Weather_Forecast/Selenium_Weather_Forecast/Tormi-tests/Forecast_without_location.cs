using Microsoft.Testing.Platform.Extensions.Messages;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using static Selenium_Weather_Forecast.Tormi_tests.Login;

namespace Selenium_Weather_Forecast.Tormi_tests
{
    internal class Forecast_without_location
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

            driver.FindElement(By.XPath("//span[text()='Search specific day']")).Click();

            Assert.That(wait.Until(ExpectedConditions.TextToBePresentInElementLocated(By.XPath("//span[text()='The city name field is required.']"), "The city name field is required.")));

            driver.FindElement(By.XPath("//img[@src='https://www.svgrepo.com/show/483341/home.svg']")).Click();

            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//span[text()='Search multiple days']"))).Click();

            Assert.That(wait.Until(ExpectedConditions.TextToBePresentInElementLocated(By.XPath("//span[text()='The city name field is required.']"), "The city name field is required.")));
        }
        [TearDown]
        public void EndTest()
        {
            driver.Close();
        }
    }
}
