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
    internal class Weather_map
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

            IWebElement weathermapSpan = driver.FindElement(By.Id("weathermapSpan"));

            Actions actions = new Actions(driver);
            actions.ScrollToElement(weathermapSpan);
            actions.Perform();

            weathermapSpan.Click();

            IWebElement iframe = driver.FindElement(By.Id("weathermap"));

            string? hidden = iframe.GetDomProperty("hidden");

            Assert.That(hidden == "False");

            driver.SwitchTo().Frame(iframe);

            Assert.That(wait.Until(ExpectedConditions.ElementIsVisible(By.Id("w"))).Displayed);

            driver.SwitchTo().DefaultContent();

            driver.FindElement(By.Id("weathermapSpan")).Click();

            string? hidden2 = iframe.GetDomProperty("hidden");

            Assert.That(hidden2 == "True");
        }
        [TearDown]
        public void EndTest()
        {
            driver.Close();
        }
    }
}
