using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Selenium_Weather_Forecast.Tormi_tests
{
    internal class Register
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
            driver.FindElement(By.XPath("//*[contains(text(), 'Register') and contains(@class, 'btn-primary')]")).Click();
            driver.FindElement(By.Id("Username")).SendKeys(Guid.NewGuid().ToString());
            driver.FindElement(By.Id("Password")).SendKeys("Abc-1");
            driver.FindElement(By.Id("ConfirmPassword")).SendKeys("Abc-1");
            driver.FindElement(By.XPath("//*[contains(@class, 'btn-primary')]")).Submit();
            IWebElement successHeader = driver.FindElement(By.CssSelector("h1"));

            Assert.That(successHeader.Text == "You have successfully registered your account, please log in with it");
        }
        [TearDown]
        public void EndTest()
        {
            driver.Close();
        }
    }
}
