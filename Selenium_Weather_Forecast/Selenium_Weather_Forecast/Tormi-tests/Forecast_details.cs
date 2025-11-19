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
    internal class Forecast_details
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

            Assert.That(wait.Until(ExpectedConditions.TextToBePresentInElementLocated(By.Id("weatherAddress"), "Tallinn")));

            driver.FindElement(By.XPath("//span[text()='Forecast Details']")).Click();

            IWebElement closeButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("#modal_forecast_details .modal-footer span")));

            Actions actions = new Actions(driver);
            actions.ScrollToElement(closeButton);
            actions.Perform();

            closeButton.Click();

            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//span[text()='Forecast Details']"))).Click();

            IWebElement xButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("#modal_forecast_details .modal-header button")));

            actions.ScrollToElement(xButton);
            actions.Perform();

            xButton.Click();

            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//span[text()='Forecast Details']"))).Click();

            Assert.That(wait.Until(ExpectedConditions.TextToBePresentInElementLocated(By.CssSelector("#modal_forecast_details div div div:nth-child(2) div div div table tbody tr:first-child th"), "Tallinn")));
        }
        [TearDown]
        public void EndTest()
        {
            driver.Close();
        }
    }
}
