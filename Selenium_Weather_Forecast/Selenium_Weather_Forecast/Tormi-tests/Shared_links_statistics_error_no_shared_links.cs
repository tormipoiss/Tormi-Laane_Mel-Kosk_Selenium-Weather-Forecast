using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace Selenium_Weather_Forecast.Tormi_tests
{
    internal class Shared_links_statistics_error_no_shared_links
    {
        IWebDriver driver;
        [SetUp]
        public void Initialize()
        {
            driver = new ChromeDriver();
            driver.Url = "https://localhost:5001/";
        }
        [Test]
        public void Test()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));

            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[contains(text(), 'Register') and contains(@class, 'btn-primary')]"))).Click();

            string username = Guid.NewGuid().ToString();
            wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("Username"))).SendKeys(username);
            driver.FindElement(By.Id("Password")).SendKeys("Abc-1");
            driver.FindElement(By.Id("ConfirmPassword")).SendKeys("Abc-1");
            driver.FindElement(By.XPath("//*[contains(@class, 'btn-primary')]")).Submit();

            Assert.That(wait.Until(ExpectedConditions.TextToBePresentInElementLocated(By.CssSelector("h1"), "You have successfully registered your account, please log in with it")));

            driver.FindElement(By.XPath("//*[contains(text(), 'Login') and contains(@class, 'nav-link')]")).Click();

            wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("Username"))).SendKeys(username);
            driver.FindElement(By.Id("Password")).SendKeys("Abc-1");
            driver.FindElement(By.XPath("//*[contains(@class, 'btn-primary')]")).Submit();

            Assert.That(wait.Until(ExpectedConditions.TextToBePresentInElementLocated(By.CssSelector("h1"), "Welcome to the weather forecast app")));

            driver.FindElement(By.XPath("//*[contains(text(), 'Shared link statistics') and contains(@class, 'nav-link')]")).Click();

            IAlert simpleAlert = wait.Until(ExpectedConditions.AlertIsPresent());
            Assert.That(simpleAlert.Text == "No shared links to display");
            simpleAlert.Accept();
        }
        [TearDown]
        public void EndTest()
        {
            driver.Close();
        }
    }
}
