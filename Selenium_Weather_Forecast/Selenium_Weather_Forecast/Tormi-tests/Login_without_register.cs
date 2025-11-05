using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace Selenium_Weather_Forecast.Tormi_tests
{
    internal class Login_without_register
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

            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[contains(text(), 'Login') and contains(@class, 'btn-primary')]"))).Click();

            wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("Username"))).SendKeys(Guid.NewGuid().ToString());
            driver.FindElement(By.Id("Password")).SendKeys("Abc-1");
            driver.FindElement(By.XPath("//*[contains(@class, 'btn-primary')]")).Submit();

            Assert.That(wait.Until(ExpectedConditions.TextToBePresentInElementLocated(By.CssSelector("h1"), "Error: Username or password is incorrect")));
        }
        [TearDown]
        public void EndTest()
        {
            driver.Close();
        }
    }
}
