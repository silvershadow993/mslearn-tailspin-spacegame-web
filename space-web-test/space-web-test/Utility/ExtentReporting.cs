using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Configuration;
using OpenQA.Selenium;
using System.IO;
using System.Reflection;

namespace space_web_test.Utility
{
    class ExtentReporting
    {
        public static ExtentReports extRptDrv;
        private ExtentReports extent;
        public ExtentHtmlReporter htmlReporter;
        public static ExtentTest testCase;
        public object LogStatus { get; private set; }
        
        public void SetupExtentReport(string reportName, string documentTitle)
        {

            string currentTime = DateTime.Now.ToString("yyyy-dd-M--HH-mm-ss");

            extent = new ExtentReports();
            htmlReporter = new ExtentHtmlReporter(GetSolutionPath() + "\\Reports" + "\\" + currentTime);
            htmlReporter.Config.Theme = Theme.Dark;
            htmlReporter.Config.DocumentTitle = documentTitle;
            htmlReporter.Config.ReportName = reportName + "Execution: Space Web";
            extent.AttachReporter(htmlReporter);
            extRptDrv = extent;
        }

        public ExtentTest CreateTest(string testName)
        {
            testCase = extent.CreateTest(testName);
            return testCase;
        }

        public void LogReportStatement(Status status, string message)
        {
            testCase.Log(status, message);
        }

        public void FlushReport()
        {
            extent.Flush();
        }

        public void AttachScreenShot(IWebDriver driver, ExtentTest test)
        {
            string currentTime = DateTime.Now.ToString("yyyy-dd-M--HH-mm-ss");

            Screenshot ss = ((ITakesScreenshot)driver).GetScreenshot();
            string fileName = currentTime + ".png";
            ss.SaveAsFile(GetSolutionPath() + "\\Reports" + "\\" + fileName, ScreenshotImageFormat.Png);
            
            test.Log(AventStack.ExtentReports.Status.Error, "Screenshot: " + testCase.AddScreenCaptureFromPath(GetSolutionPath() + "\\Reports\\" + currentTime + ".png"));
            test.Log(AventStack.ExtentReports.Status.Error, Environment.StackTrace);

            extRptDrv.Flush();
        }

        public void TestStatusWithMsg(string status, string message)
        {
            if (status.Equals("Pass"))
            {
                testCase.Pass(message);
            }
            else
            {
                testCase.Fail(message);
            }
        }

        public string GetSolutionPath()
        {
            DirectoryInfo assemblyPath = new DirectoryInfo(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
            DirectoryInfo binPath = Directory.GetParent(assemblyPath.ToString());
            DirectoryInfo solutionPath = Directory.GetParent(binPath.ToString());

            return solutionPath.ToString();
        }

    }
}
