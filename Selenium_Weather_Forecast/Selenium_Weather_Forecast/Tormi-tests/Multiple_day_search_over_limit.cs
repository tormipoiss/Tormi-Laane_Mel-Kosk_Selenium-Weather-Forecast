using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System.Collections.ObjectModel;
using static Selenium_Weather_Forecast.Tormi_tests.Login;

namespace Selenium_Weather_Forecast.Tormi_tests
{
    internal class Multiple_day_search_over_limit
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
            driver.FindElement(By.Id("DayAmount")).SendKeys("100");
            driver.FindElement(By.XPath("//span[text()='Search multiple days']")).Click();

            Assert.That(wait.Until(ExpectedConditions.TextToBePresentInElementLocated(By.XPath("//p[text()='Cannot forecast weather longer than 15 days, defaulted to day 15']"), "Cannot forecast weather longer than 15 days, defaulted to day 15")));

            Assert.That(wait.Until(ExpectedConditions.TextToBePresentInElementLocated(By.Id("multipleDayAddress"), "Tallinn")));

            ReadOnlyCollection<IWebElement> tables = driver.FindElements(By.CssSelector("table"));

            Assert.That(tables.Count == 15);
        }
        [TearDown]
        public void EndTest()
        {
            driver.Close();
        }
    }
}
