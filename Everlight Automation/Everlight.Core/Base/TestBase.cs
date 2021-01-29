using System;
using System.Configuration;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AventStack.ExtentReports;
using log4net;
using System.Diagnostics;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Firefox;
using System.IO;
using System.Threading;


namespace Everlight.Core
{
    [TestClass]
    public abstract class TestBase
    {
        public static readonly ILog logger = LogManager.GetLogger(typeof(TestBase));
        public static IWebDriver Driver { get; set; }
        public TestContext TestContext { get; set; }
        public ExtentTest extentTest;
        public static ThreadLocal<bool> isTestEnabled = new ThreadLocal<bool>();
        public static bool dataDrivenTest;
        public static string extentMode = null;
        public static string runningDriverType = null;
        public static bool closeBrowserAfterTest;
        public static string environment = null;
        private static bool _chromeHeadless;
        private static bool _chromeIncognito;
        private static ExtentReports _extentReport;
        private static Reporter _Reporter;


        [AssemblyInitialize]
        public static void AssemblyInit(TestContext TestContext)
        {
            //setup runtime variables from run settings
            
            environment = TestContext.Properties["environment"].ToString();
            bool.TryParse(TestContext.Properties["dataDrivenTest"].ToString(), out dataDrivenTest);
            bool.TryParse(TestContext.Properties["CloseBrowserAfterEachTest"].ToString(), out closeBrowserAfterTest);
            bool.TryParse(TestContext.Properties["ChromeModeHeadless"].ToString(), out _chromeHeadless);
            bool.TryParse(TestContext.Properties["ChromeModeIncognito"].ToString(), out _chromeIncognito);


            //Initialise Extent Reports
            _Reporter = new Reporter();
            _extentReport = _Reporter.InitialiseExtentReports();
            _extentReport.AddSystemInfo("OS", TestContext.Properties["applicationOs"].ToString());
            _extentReport.AddSystemInfo("Host Name", TestContext.Properties["hostName"].ToString());
            _extentReport.AddSystemInfo("Environment", TestContext.Properties["environment"].ToString());
            _extentReport.AddSystemInfo("User Name", TestContext.Properties["executionUsername"].ToString());
            WorkflowBase.InitializeExtentValue(TestContext.Properties["ExtentMode"].ToString());

            //Create log4net directory
            var log4LogFileDir = ".\\Logs\\CaptureLogs\\";
            var log4CreateDir = Directory.CreateDirectory(log4LogFileDir);

        }

        [AssemblyCleanup]
        public static void TestSuiteCleanup()
        {
            var path = _Reporter.FlushExtentReport();

            //Close browser at end of test run
           
                    Driver?.Close();
                    Driver?.Quit();
        }


        #region Logging
        public static void StartTestCase(string testcasename)
        {
            logger.Info("****************************************************************************************");
            logger.Info("$$$$$$$$$$$$$$$$$$$$$               " + testcasename + "       $$$$$$$$$$$$$$$$$$$$$$$$$");
            logger.Info("****************************************************************************************");
        }

        public static void EndTestCase(string testcasename)
        {
            logger.Info("****************************************************************************************");
            logger.Info("XXXXXXXXXXXXXXXXXXXXXXX" + testcasename + "-E---N---D-" + "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX");
            logger.Info("****************************************************************************************");
        }
        public void LogInfo(string text)
        {
            var msg = string.Empty;
            if (TestContext.DataRow != null)
                msg = "[Iteration: " + TestContext.DataRow.Table.Rows.IndexOf(TestContext.DataRow) + "] ";
            msg += text;
            logger.Info(msg);
            Console.WriteLine(msg);
            Debug.WriteLine(msg);
        }

        public void LogError(string text)
        {
            var msg = string.Empty;
            if (TestContext.DataRow != null)
                msg = "[Iteration: " + TestContext.DataRow.Table.Rows.IndexOf(TestContext.DataRow) + "] ";
            msg += text;
            logger.Error(msg);
            Console.WriteLine(msg);
            Debug.WriteLine(msg);
        }

        public void LogError(string text, Exception e)
        {
            var msg = string.Empty;
            if (TestContext.DataRow != null)
                msg = "[Iteration: " + TestContext.DataRow.Table.Rows.IndexOf(TestContext.DataRow) + "] ";
            msg += text;
            logger.Error(text, e);
            msg += "Exception:" + e;
            Console.WriteLine(msg);
            Debug.WriteLine(msg);
        }
        #endregion


        protected void Test(Action action)
        {
            try
            {
                if (isTestEnabled.Value)
                {
                    action();
                }
                else
                {
                    const string msg = "Test is not " +
                        "enabled in input file. This line of input file is being ignored.";
                    LogInfo(msg);
                    Assert.Inconclusive(msg);
                }
            }
            catch (Exception e) when (!(e is AssertInconclusiveException))
            {
                try
                {
                    var screenShot = TakeScreenShot.ScreenShot(Driver);
                    extentTest.Log(Status.Fail, "Unhandled Exception " + e, MediaEntityBuilder.CreateScreenCaptureFromPath(screenShot).Build());
                }
                catch (Exception)
                {
                    extentTest.Log(Status.Fail, "Unhandled Exception " + e);
                }
                LogError("Unhandled Exception", e);
                throw;
            }
        }


        //entry point for runner to start data driven test
        public bool Start()
        {
            var isEnabled = DataLoad.GetData("IsEnabled");
            if (isEnabled != null && isEnabled.ToUpper() == "FALSE")
                return false;
            extentTest = _extentReport.CreateTest("[Iteration: " + TestContext.DataRow.Table.Rows.IndexOf(TestContext.DataRow) + "] - " + DataLoad.GetData("Description"));
            SeleniumExtensions.InitialiseExtentTest(extentTest);
            log4net.Config.XmlConfigurator.Configure();

            var driverType = GetDriverType();
            if (!(closeBrowserAfterTest))
            {
                if (runningDriverType == null || runningDriverType != driverType)
                {
                    Driver?.Close();
                    StartDriver(driverType);
                }

            }
            else StartDriver(driverType);
            return true;
        }

        //Start non data driven test
        public void Start(string testName, string driverType)
        {
            extentTest = _extentReport.CreateTest(testName);
            SeleniumExtensions.InitialiseExtentTest(extentTest);
            log4net.Config.XmlConfigurator.Configure();
            StartDriver(driverType);
        }

        //Get the driver type.  
        public string GetDriverType()
        {
           
            var driver = DataLoad.GetData("Browser");
            return driver;

        }

        //Star the driver that is read from the get driver type method
        private void StartDriver(string driverType)
        {
            Driver = GetDriver(driverType);
            runningDriverType = driverType;
        }

        //Setup options/capabilities for the selected driver type
        private IWebDriver GetDriver(string browser)
        {
            try
            {
                switch (browser.ToUpper())
                {
                    case "CHROME":
                        ChromeOptions options = new ChromeOptions();
                        Console.Out.WriteLine("Setting Chrome Options");
                        options.AddArguments("--start-maximized");
                        if (_chromeHeadless)
                            options.AddArguments("--headless");
                        if (_chromeIncognito)
                            options.AddArguments("--incognito");
                        Driver = new ChromeDriver(options);
                        Driver.Manage().Timeouts().PageLoad = Constants.PageLoad;
                        Driver.Manage().Timeouts().ImplicitWait = Constants.ImplicitWait;
                        Console.Out.WriteLine("Created Driver");
                        break;
                    case "IE":
                        var ieOptions = new InternetExplorerOptions()
                        {
                            IntroduceInstabilityByIgnoringProtectedModeSettings = true,
                            IgnoreZoomLevel = true
                        };
                        Driver = new InternetExplorerDriver(ieOptions);
                        break;
                    case "FIREFOX":
                        var ffOptions = new FirefoxOptions();
                        Driver = new FirefoxDriver(ffOptions);
                        Driver.Manage().Timeouts().PageLoad = Constants.PageLoad;
                        Driver.Manage().Timeouts().ImplicitWait = Constants.ImplicitWait;
                        break;
                    case "EDGE":
                        throw new NotImplementedException();
                   
                    default:
                        extentTest.Log(Status.Error, "Platform Not Set");
                        throw new ArgumentOutOfRangeException();
                }
                return Driver;
            }
            catch (Exception e)
            {
                Console.Out.WriteLine("Couldnt start driver" + e);
                extentTest.Log(Status.Error, "Unable to load browser driver " + e);
                throw;
            }
        }

        //Gets the application url.  COmbination of a base URL in app settings and a resource path from the data sheet
        public string GetApplicationUrl(string appBaseUrl, string key = "ApplicationUrl", params object[] args)
        {
            var baseUrl = ConfigurationManager.AppSettings[appBaseUrl];

            if (string.IsNullOrEmpty(baseUrl))
            {
                extentTest.Log(Status.Error, "AppSettings['BaseApplicationUr'] is not set");
                throw new ArgumentException("AppSettings['BaseApplicationUr'] is not set");
            }
            string subUrl = DataLoad.GetData(key);
            var fullUrl = baseUrl + subUrl;
            if (args != null && args.Length > 0)
                fullUrl = string.Format(fullUrl, args);
            logger.Info("This is the url " + fullUrl);
            return fullUrl;
        }

        public static void KillRunningProcesses()
        {
            KillAllProcessesOfName("node");
        }

        private static void KillAllProcessesOfName(string name)
        {
            bool finished;

            do
            {
                finished = !KillProcess(name);
            } while (!finished);
        }

        private static bool KillProcess(string processName)
        {
            var processes = System.Diagnostics.Process.GetProcesses();
            var process = processes.FirstOrDefault(x => x.ProcessName == processName);


            if (process != null)
            {
                try
                {
                    process.Kill();
                    process.WaitForExit();
                }
                catch (Exception)
                {
                    // can get an exception if the process has already died etc. In this situation
                    // return true to go around the loop again and attempt again
                }
                return true;
            }
            return false;
        }
    }
}
