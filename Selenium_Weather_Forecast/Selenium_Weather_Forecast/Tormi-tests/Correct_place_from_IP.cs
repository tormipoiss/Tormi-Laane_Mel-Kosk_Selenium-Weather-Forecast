using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using static Selenium_Weather_Forecast.Tormi_tests.Login;

namespace Selenium_Weather_Forecast.Tormi_tests
{
    internal class Correct_place_from_IP
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

            driver.FindElement(By.XPath("//span[text()='Get my location']")).Click();

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

            Assert.That(cityInputValue == "Tallinn");
        }
        [TearDown]
        public void EndTest()
        {
            driver.Close();
        }
    }
}
