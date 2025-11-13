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
    internal class Search_history_multiple_days
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
            driver.FindElement(By.Id("DayAmount")).SendKeys("4");
            driver.FindElement(By.XPath("//span[text()='Search multiple days']")).Click();

            Assert.That(wait.Until(ExpectedConditions.TextToBePresentInElementLocated(By.Id("multipleDayAddress"), "Tallinn")));

            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//span[text()='Get Search History']"))).Click();

            Assert.That(wait.Until(ExpectedConditions.TextToBePresentInElementLocated(By.CssSelector("tbody tr td:nth-child(2) span"), "/ Multiple day (4)")));

            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//span[text()='Search location weather']"))).Click();

            Assert.That(wait.Until(ExpectedConditions.TextToBePresentInElementLocated(By.Id("multipleDayAddress"), "Tallinn")));

            ReadOnlyCollection<IWebElement> tables = driver.FindElements(By.CssSelector("table"));

            Assert.That(tables.Count == 4);
        }
        [TearDown]
        public void EndTest()
        {
            driver.Close();
        }
    }
}
