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
    internal class Bitdefender_map
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

            driver.FindElement(By.XPath("//a[@href='/DdosMap']")).Click();

            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//span[text()='Bitdefender cyberthreat map']"))).Click();

            IWebElement iframeRadware = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//iframe[@src='https://threatmap.bitdefender.com/']")));

            driver.SwitchTo().Frame(iframeRadware);

            Assert.That(wait.Until(ExpectedConditions.ElementIsVisible(By.Id("container"))).Displayed);
        }
        [TearDown]
        public void EndTest()
        {
            driver.Close();
        }
    }
}
