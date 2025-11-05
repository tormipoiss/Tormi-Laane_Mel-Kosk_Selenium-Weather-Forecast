using Microsoft.Testing.Platform.Extensions.Messages;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace Selenium_Weather_Forecast.Tormi_tests
{
    internal class Choose_location_on_map
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

            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//span[text()='Choose location on map']"))).Click();

            wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("#modal_map .modal-footer span"))).Click();

            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//span[text()='Choose location on map']"))).Click();

            wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("#modal_map .modal-header button"))).Click();

            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//span[text()='Choose location on map']"))).Click();

            wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("map"))).Click();

            driver.FindElement(By.XPath("//span[text()='Submit Location']")).Click();

            string cityInputValue = wait.Until(driver =>
            {
                IWebElement element = driver.FindElement(By.Id("cityNameInput"));
                string value = element.GetAttribute("value");
                if (!string.IsNullOrEmpty(value))
                {
                    return value;
                }
                return null;
            });

            Assert.That(cityInputValue == "Tallinn, Harju County, Estonia");
        }
        [TearDown]
        public void EndTest()
        {
            driver.Close();
        }
    }
}
