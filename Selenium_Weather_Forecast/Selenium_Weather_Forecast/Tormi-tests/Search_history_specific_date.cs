using Microsoft.Testing.Platform.Extensions.Messages;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System.Collections.ObjectModel;
using static Selenium_Weather_Forecast.Tormi_tests.Login;

namespace Selenium_Weather_Forecast.Tormi_tests
{
    internal class Search_history_specific_date
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

            DateTime currentDate = DateTime.Now;
            DateTime futureDate = currentDate.AddDays(10);
            string dateString = futureDate.ToString();
            string futureHourString = dateString.Substring(11, 2);
            string futureMinuteString = dateString.Substring(11, 5);
            string shortDateString = futureDate.ToShortDateString();
            dateString = String.Join($"{Keys.Tab}", dateString.Split(" "));
            dateString = dateString.Substring(0, 16);

            driver.FindElement(By.Id("cityNameInput")).SendKeys("Tallinn");
            
            driver.FindElement(By.Id("ForecastDate")).SendKeys(dateString);

            driver.FindElement(By.XPath("//span[text()='Search specific day']")).Click();

            Assert.That(wait.Until(ExpectedConditions.TextToBePresentInElementLocated(By.Id("weatherAddress"), "Tallinn")));

            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//span[text()='Get Search History']"))).Click();

            string expected = $"/ {shortDateString} {futureMinuteString}:00";

            Assert.That(wait.Until(ExpectedConditions.TextToBePresentInElementLocated(By.CssSelector("tbody tr td:nth-child(2) span"), expected)));

            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//span[text()='Search location weather']"))).Click();

            Assert.That(wait.Until(ExpectedConditions.TextToBePresentInElementLocated(By.Id("weatherAddress"), "Tallinn")));

            Assert.That(wait.Until(ExpectedConditions.TextToBePresentInElementLocated(By.CssSelector("table tbody:first-child"), $"{shortDateString} {futureHourString}:00:00")));
        }
        [TearDown]
        public void EndTest()
        {
            driver.Close();
        }
    }
}
