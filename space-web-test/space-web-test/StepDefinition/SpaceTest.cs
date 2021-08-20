using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace space_web_test.StepDefinition
{
    class SpaceTest
    {
        private IWebDriver driver;

        [SetUp]
        public void setupTest()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://localhost:5001/");
        }

        [Test]
        public void testOne()
        {
            string introText = driver.FindElement(By.XPath("//div[@class='container']/p")).Text;

            Assert.AreEqual("An example site for learning", introText);
        }

        [TearDown]
        public void tearDownTest()
        {
            driver.Close();
            driver.Quit();
        }
    }
}
