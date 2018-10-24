using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using NUnit.Framework.Interfaces;
using System;
using Automation.UI.Core.Selenium;
using Automation.UI.Core.Selenium.PageObjects.Interpris.Product;
using Automation.UI.Core.Selenium.PageObjects.Interpris.Platform;

namespace Automation.UI.Core.TestBase
{
    /// <summary>
    /// Base class of a Test Fixture to initilize the required entities
    /// </summary>
    public class InterprisTestBase : WebUITestBaseClass
    {
        public static string PlatformBaseURL { get; set; }
        //public static string TranscriptionBaseURL { get; set; }
        public static string InterprisBaseURL { get; set; }

        #region SetUp/TearDown methods
        [OneTimeSetUp]
        public void RunBeforeAnyTests()
        {
            string testEnv = Environment.GetEnvironmentVariable(ProjectConfigParams.SUPPORTED_ENV_NAME);

            switch(testEnv)
            {
                case ProjectConfigParams.SUPPORTED_ENV_QA:
                    break;
                case ProjectConfigParams.SUPPORTED_ENV_TEST:
                    break;
                default:
                    Assert.Inconclusive("Test Environment {0} not correct!", testEnv);
                    break;
            }

            // get base URL
            PlatformBaseURL = ProjectConfigParams.GetConfigParamValue(
                ProjectConfigParams.HEAD_CONF_KEY_PLATFORM_BASEURL + testEnv);           

            // get base URL
            InterprisBaseURL = ProjectConfigParams.GetConfigParamValue(
                ProjectConfigParams.HEAD_CONF_KEY_INTERPRIS_BASEURL + testEnv);

            // get browser type
            Browser = TestContext.Parameters.Get(ProjectConfigParams.CONF_KEY_DEFAULT_BROWSER,
                ProjectConfigParams.GetConfigParamValue(ProjectConfigParams.CONF_KEY_DEFAULT_BROWSER));

            // get screen size
            ScreenSize = TestContext.Parameters.Get(ProjectConfigParams.CONF_KEY_DEFAULT_SCREEN_SIZE,
                ProjectConfigParams.GetConfigParamValue(ProjectConfigParams.CONF_KEY_DEFAULT_SCREEN_SIZE));

            // get browser stack config infor
            BrowserStackEnabled = bool.Parse(TestContext.Parameters.Get(ProjectConfigParams.CONF_KEY_BROWSERSTACK_ENABLED,
                "false"));
        }

        [SetUp]
        public void RunBeforeTest()
        {
            // create driver
            if (Browser != null && ScreenSize != null)
            {
                Driver = SetUpWebDriverInstance(Browser, ScreenSize, BrowserStackEnabled,
                    int.Parse(ProjectConfigParams.GetConfigParamValue(ProjectConfigParams.CONF_KEY_PAGE_LOAD_TIMEOUT)));
            }
        }

        [TearDown]
        public void RunAfterTest()
        {
            // capture screenshot
            try
            {
                if (TestContext.CurrentContext.Result.Outcome != ResultState.Success &&
                    TestContext.CurrentContext.Result.Outcome != ResultState.Ignored &&
                    TestContext.CurrentContext.Result.Outcome != ResultState.Inconclusive)
                {
                    TestContext.AddTestAttachment(CaptureSreenshot(TestContext.CurrentContext.WorkDirectory),
                        "Screenshot of the failure");
                }
            }
            catch (Exception ex)
            {
                TestContext.Error.WriteLine("RunAfterTest sreen capture ERR: {0}", ex.Message);
            }

            // try to log out before closing the browser
            if (TestContext.CurrentContext.Result.Outcome != ResultState.Ignored &&
                    TestContext.CurrentContext.Result.Outcome != ResultState.Inconclusive)
            {
                TryToLogOut();
            }

            // close the driver
            DriverClose();
        }

        /// <summary>
        /// Try to Log Out from Uluru page
        /// </summary>
        public void TryToLogOut()
        {
            try
            {
                if (Driver != null)
                {
                    // go back to the base url
                    if (!Driver.Url.Contains(InterprisBaseURL))
                    {
                        Driver.Navigate().GoToUrl(InterprisBaseURL);
                    }

                (new HeaderSubPage(Driver, InterprisBaseURL)).LogOut();
                }
            }
            catch (WebDriverException wex)
            {
                TestContext.Out.WriteLine("TryToLogOut ERR: {0}", wex.Message);
            }
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Create the desired capabilities for BrowserStack
        /// This method only serves for BrowserStack as it is depricated in new Selenium library 3.14.0
        /// </summary>
        /// <returns>Desired capabilities for BrowserStack</returns>
        public override ICapabilities CreateBrowserStackCapabilities()
        {
            DesiredCapabilities cap = new DesiredCapabilities();

            cap.SetCapability("browser", BrowserConstants.BROWSERSTACK_BROWSER_NAMES[Browser]);
            cap.SetCapability("os", ProjectConfigParams.GetConfigParamValue(ProjectConfigParams.CONF_KEY_BROWSERSTACK_OS));
            cap.SetCapability("os_version", ProjectConfigParams.GetConfigParamValue(ProjectConfigParams.CONF_KEY_BROWSERSTACK_OS_VERSION));

            string screenSize = ScreenSize;
            if (screenSize.Equals("max", StringComparison.OrdinalIgnoreCase))
                screenSize = ProjectConfigParams.GetConfigParamValue(ProjectConfigParams.CONF_KEY_BROWSERSTACK_RESOLUTION);

            cap.SetCapability("resolution", screenSize);

            cap.SetCapability("browserstack.user", ProjectConfigParams.GetConfigParamValue(ProjectConfigParams.CONF_KEY_BROWSERSTACK_USER));
            cap.SetCapability("browserstack.key", ProjectConfigParams.GetConfigParamValue(ProjectConfigParams.CONF_KEY_BROWSERSTACK_KEY));

            cap.AcceptInsecureCerts = true;

            return cap;
        }
        #endregion
    }
}