using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

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
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        }
        [Test]
        public void Test()
        {
            driver.FindElement(By.XPath("//*[contains(text(), 'Register') and contains(@class, 'btn-primary')]")).Click();
            string username = Guid.NewGuid().ToString();
            driver.FindElement(By.Id("Username")).SendKeys(username);
            driver.FindElement(By.Id("Password")).SendKeys("Abc-1");
            driver.FindElement(By.Id("ConfirmPassword")).SendKeys("Abc-1");
            driver.FindElement(By.XPath("//*[contains(@class, 'btn-primary')]")).Submit();
            IWebElement successHeader = driver.FindElement(By.CssSelector("h1"));

            Assert.That(successHeader.Text == "You have successfully registered your account, please log in with it");

            driver.FindElement(By.XPath("//*[contains(text(), 'Login') and contains(@class, 'nav-link')]")).Click();
            driver.FindElement(By.Id("Username")).SendKeys(username);
            driver.FindElement(By.Id("Password")).SendKeys("Abc-1");
            driver.FindElement(By.XPath("//*[contains(@class, 'btn-primary')]")).Submit();

            IWebElement heading = driver.FindElement(By.CssSelector("h1"));
            Assert.That(heading.Text == "Welcome to the weather forecast app");

            driver.FindElement(By.XPath("//*[contains(text(), 'Shared link statistics') and contains(@class, 'nav-link')]")).Click();

            IAlert simpleAlert = driver.SwitchTo().Alert();
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
