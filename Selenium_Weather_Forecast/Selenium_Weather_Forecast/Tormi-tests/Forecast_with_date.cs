using Microsoft.Testing.Platform.Extensions.Messages;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace Selenium_Weather_Forecast.Tormi_tests
{
    internal class Forecast_with_date
    {
        IWebDriver driver;
        [SetUp]
        public void Initialize()
        {
            driver = new ChromeDriver();
            driver.Url = "https://localhost:5001/";
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));

            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[contains(text(), 'Register') and contains(@class, 'btn-primary')]"))).Click();

            string username = Guid.NewGuid().ToString();
            wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("Username"))).SendKeys(username);
            driver.FindElement(By.Id("Password")).SendKeys("Abc-1");
            driver.FindElement(By.Id("ConfirmPassword")).SendKeys("Abc-1");
            driver.FindElement(By.XPath("//*[contains(@class, 'btn-primary')]")).Submit();

            Assert.That(wait.Until(ExpectedConditions.TextToBePresentInElementLocated(By.CssSelector("h1"), "You have successfully registered your account, please log in with it")));

            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[contains(text(), 'Login') and contains(@class, 'nav-link')]"))).Click();
            driver.FindElement(By.Id("Username")).SendKeys(username);
            driver.FindElement(By.Id("Password")).SendKeys("Abc-1");
            driver.FindElement(By.XPath("//*[contains(@class, 'btn-primary')]")).Submit();

            Assert.That(wait.Until(ExpectedConditions.TextToBePresentInElementLocated(By.CssSelector("h1"), "Welcome to the weather forecast app")));
        }
        [Test]
        public void Test()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));

            driver.FindElement(By.Id("cityNameInput")).SendKeys("Tallinn");

            DateTime currentDate = DateTime.Now;
            DateTime futureDate = currentDate.AddDays(10);
            string dateString = futureDate.ToString();
            string currentHourString = dateString.Substring(11, 2);
            string shortDateString = futureDate.ToShortDateString();
            dateString = String.Join($"{Keys.Tab}", dateString.Split(" "));

            driver.FindElement(By.Id("ForecastDate")).SendKeys(dateString);

            driver.FindElement(By.XPath("//span[text()='Search specific day']")).Click();

            Assert.That(wait.Until(ExpectedConditions.TextToBePresentInElementLocated(By.Id("weatherAddress"), "Tallinn")));

            Assert.That(wait.Until(ExpectedConditions.TextToBePresentInElementLocated(By.CssSelector("table tbody:first-child"), $"{shortDateString} {currentHourString}:00:00")));
        }
        [TearDown]
        public void EndTest()
        {
            driver.Close();
        }
    }
}
