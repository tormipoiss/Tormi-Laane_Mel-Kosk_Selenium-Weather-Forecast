using Microsoft.Testing.Platform.Extensions.Messages;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace Selenium_Weather_Forecast.Tormi_tests
{
    internal class Search_history
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

            driver.FindElement(By.Id("cityNameInput")).SendKeys("Tallinn");
            driver.FindElement(By.XPath("//span[text()='Search specific day']")).Click();
            Assert.That(wait.Until(ExpectedConditions.TextToBePresentInElementLocated(By.Id("weatherAddress"), "Tallinn")));
        }
        [Test]
        public void Test()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));

            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//span[text()='Get Search History']"))).Click();

            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//span[text()='Go back']"))).Click();

            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//span[text()='Get Search History']"))).Click();

            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//span[text()='Search location weather']"))).Click();

            Assert.That(wait.Until(ExpectedConditions.TextToBePresentInElementLocated(By.Id("weatherAddress"), "Tallinn")));
        }
        [TearDown]
        public void EndTest()
        {
            driver.Close();
        }
    }
}
