using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using AventStack.ExtentReports;
using space_web_test.Utility;
using System;

namespace space_web_test.StepDefinition
{
    class SpaceTest
    {
        private IWebDriver driver;
        ExtentReporting _extentReport = new ExtentReporting();
        ExtentTest test;

        [SetUp]
        public void SetupTest()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://localhost:5001/");
        }

        [Test]
        public void TestOne()
        {
            _extentReport.SetupExtentReport("Space_web_regression_test", "Space_web_test");
            test = _extentReport.CreateTest("Test One");

            string expectedText = "An example site for learning1";
            string introText = driver.FindElement(By.XPath("//div[@class='container']/p")).Text;

            try
            {
                Assert.AreEqual(expectedText, introText);

                _extentReport.TestStatusWithMsg(Status.Pass.ToString(), "Test one passed.");

            }
            catch(AssertionException e)
            {
                _extentReport.TestStatusWithMsg(Status.Fail.ToString(), e.Message);
            }
            _extentReport.FlushReport();
        }

        [TearDown]
        public void TearDownTest()
        {
            driver.Close();
            driver.Quit();
        }
    }
}
