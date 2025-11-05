using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace Selenium_Weather_Forecast.Tormi_tests
{
    internal class Register_existing_user
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

            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[contains(text(), 'Register') and contains(@class, 'nav-link')]"))).Click();

            wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("Username"))).SendKeys(username);
            driver.FindElement(By.Id("Password")).SendKeys("Abc-1");
            driver.FindElement(By.Id("ConfirmPassword")).SendKeys("Abc-1");
            driver.FindElement(By.XPath("//*[contains(@class, 'btn-primary')]")).Submit();

            Assert.That(wait.Until(ExpectedConditions.TextToBePresentInElementLocated(By.CssSelector(".text-danger ul li"), "This username is already taken., Please choose another one!")));
        }
        [TearDown]
        public void EndTest()
        {
            driver.Close();
        }
    }
}
