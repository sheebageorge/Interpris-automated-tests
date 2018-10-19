using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using System;
using System.IO;
using System.Linq;
using Automation.UI.Core.CommonUtilities;
using Automation.UI.Core.Selenium;
using Automation.UI.Core.BrowserStack;

namespace Automation.UI.Core.TestBase
{
    /// <summary>
    /// Base class of a Test Fixture to initilize the required entities
    /// </summary>
    public abstract class WebUITestBaseClass
    {
        /// <summary>
        /// Each driver for each test case
        /// </summary>
        protected IWebDriver Driver { get; set; }

        public static readonly string DownloadDataFolder = Path.Combine(
            Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location),
            ProjectConfigParams.GetConfigParamValue(ProjectConfigParams.CONF_KEY_DOWNLOAD_FOLDER_NAME));

        public static string Browser { get; set; }
        public static string ScreenSize { get; set; }
        public static bool BrowserStackEnabled { get; set; }

        #region Public Methods
        /// <summary>
        /// Close the test case if it doesn't support BrowserStack yet
        /// </summary>
        public void IgnoreNotSupportingBrowserStackTest()
        {
            if (BrowserStackEnabled)
            {
                Assert.Inconclusive("This test case does not support BrowserStack now [{0}]",
                    TestContext.CurrentContext.Test.MethodName);
            }
        }

        /// <summary>
        /// Capture screenshot of current active page on the testing browser
        /// </summary>
        /// <param name="screenshotFolderPath">Folder to save the screenshot file</param>
        /// <returns>Absolute file path of the screenshot file</returns>
        public string CaptureSreenshot(string screenshotFolderPath)
        {
            // save the screenshot for failed test case
            var screenshot = ((ITakesScreenshot)Driver).GetScreenshot();
            string screenshotFilePath = Path.Combine(screenshotFolderPath,
                string.Format("Screenshot_{0}.png", RandomUtils.GetMilisecondString()));
            screenshot.SaveAsFile(screenshotFilePath, ScreenshotImageFormat.Png);

            return screenshotFilePath;
        }

        /// <summary>
        /// Close the driver and browser
        /// </summary>
        public void DriverClose()
        {
            // Clearn up driver and dispose it
            try
            {
                if (Driver != null) Driver.Quit();

                Driver = null;
            }
            catch (Exception ex)
            {
                TestContext.Error.WriteLine("DriverClose ERR: {0}", ex.Message);
            }
        }

        /// <summary>
        /// Open a new tab in the same browser window
        /// Close the current tab
        /// Switch to the new tab
        /// </summary>
        public void CloseCurrentTabAndOpenNewTab()
        {
            TestContext.Out.WriteLine("Open new tab");
            TryToOpenNewTab(Driver);

            ThreadUtils.SleepShortTime();

            TestContext.Out.WriteLine("Close the first tab {0}", Driver.WindowHandles.Count);
            Driver.SwitchTo().Window(Driver.WindowHandles.First());
            Driver.Close();

            Driver.SwitchTo().Window(Driver.WindowHandles.First());
        }

        /// <summary>
        /// Try to open new tab
        /// </summary>
        /// <param name="driver">Driver of the browser</param>
        public void TryToOpenNewTab(IWebDriver driver)
        {
            try
            {
                ((IJavaScriptExecutor)driver).ExecuteScript("window.open();");
            }
            catch (Exception ex)
            {
                TestContext.Out.WriteLine("TryToOpenNewTab ERR: {0}", ex.Message);
            }
        }

        /// <summary>
        /// Try to make the target browser window active
        /// </summary>
        /// <param name="driver">Driver of the browser</param>
        public void MakeBrowserWindowActive(IWebDriver driver)
        {
            try
            {
                ((IJavaScriptExecutor)driver).ExecuteScript("window.focus();");
            }
            catch (Exception ex)
            {
                TestContext.Out.WriteLine("MakeBrowserWindowActive ERR: {0}", ex.Message);
            }
        }

        /// <summary>
        /// Create the desired capabilities for BrowserStack
        /// This method only serves for BrowserStack as it is depricated in new Selenium library 3.14.0
        /// </summary>
        /// <returns>Desired capabilities for BrowserStack</returns>
        public abstract ICapabilities CreateBrowserStackCapabilities();

        /// <summary>
        /// Set up web driver instance based on input parameters
        /// </summary>
        /// <param name="browser">Type of Browser including: chrome, firefox, ie, edge, safari (OS X only)</param>
        /// <param name="screenSize">Size of browser window with format "WidthxHeigh" or just "Max"</param>
        /// <param name="isBrowserStackEnabled">True if the test will run on BrowserStack</param>
        /// <param name="defaultTimeOut">Timeout to wait for Web Element loading</param>
        /// <returns>Web driver instance</returns>
        public IWebDriver SetUpWebDriverInstance(string browser, string screenSize,
            bool isBrowserStackEnabled = false, int defaultTimeOut = 30)
        {
            IWebDriver driver = null;

            if (isBrowserStackEnabled)
            {
                driver = new BSRemoteWebDriver(CreateBrowserStackCapabilities());
            }
            else
            {
                // create download folder if not exists
                Directory.CreateDirectory(DownloadDataFolder);

                switch (browser)
                {
                    case BrowserConstants.BROWSER_CHROME:
                        ChromeOptions chromeOptions = new ChromeOptions();
                        
                        // download folder
                        chromeOptions.AddUserProfilePreference("download.prompt_for_download", false);
                        chromeOptions.AddUserProfilePreference("download.directory_upgrade", true);
                        chromeOptions.AddUserProfilePreference("download.default_directory", DownloadDataFolder);
                        chromeOptions.AddUserProfilePreference("safebrowsing.enabled", true);

                        chromeOptions.AddArguments("disable-infobars");
                        chromeOptions.AddArguments("enable-automation");
                        chromeOptions.AcceptInsecureCertificates = true;

                        driver = new ChromeDriver(chromeOptions);
                        break;
                    case BrowserConstants.BROWSER_FIREFOX:
                        FirefoxOptions ffOptions = new FirefoxOptions();

                        // download folder
                        ffOptions.SetPreference("browser.download.dir", DownloadDataFolder);
                        ffOptions.SetPreference("browser.download.folderList", 2);
                        ffOptions.SetPreference("browser.helperApps.neverAsk.saveToDisk", "text/plain;");
                        ffOptions.SetPreference("browser.download.manager.showWhenStarting", false);
                        ffOptions.SetPreference("pdfjs.disabled", true);

                        ffOptions.SetPreference("security.insecure_field_warning.contextual.enabled", false);
                        ffOptions.SetPreference("security.insecure_password.ui.enabled", false);
                        ffOptions.AddAdditionalCapability("acceptInsecureCerts", true, true);
                        ffOptions.AcceptInsecureCertificates = true;

                        driver = new FirefoxDriver(ffOptions);
                        break;
                    case BrowserConstants.BROWSER_EDGE:
                        Environment.SetEnvironmentVariable("webdriver.edge.driver",
                            Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location));

                        driver = new EdgeDriver();
                        break;
                    case BrowserConstants.BROWSER_IE:
                        InternetExplorerOptions ieOptions = new InternetExplorerOptions
                        {
                            IntroduceInstabilityByIgnoringProtectedModeSettings = true,
                            EnableNativeEvents = true,
                            ElementScrollBehavior = InternetExplorerElementScrollBehavior.Top,
                            EnablePersistentHover = true,
                            IgnoreZoomLevel = true
                        };

                        driver = new InternetExplorerDriver(ieOptions);
                        break;
                    case BrowserConstants.BROWSER_SAFARI:
                        return null;
                    default:
                        return null;
                }

                // set browser position on screen
                driver.Manage().Window.Position = new System.Drawing.Point(0, 0);

                // set browser window size
                int idx = screenSize.IndexOf('x');

                if (idx > 0)
                {
                    try
                    {
                        driver.Manage().Window.Size = new System.Drawing.Size(int.Parse(screenSize.Substring(0, idx)),
                            int.Parse(screenSize.Substring(idx + 1)));
                    }
                    catch (FormatException)
                    {
                        driver.Manage().Window.Maximize();
                    }
                }
                else
                {
                    driver.Manage().Window.Maximize();
                }
            }

            Assert.IsNotNull(driver, "Non-supported Web driver found!");

            // set driver page loading/web element finding timeout
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(defaultTimeOut * 2);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(defaultTimeOut);

            return driver;
        }

        /// <summary>
        /// Convert rgb color to hex value
        /// </summary>
        /// <param name="rgbColor"></param>
        /// <returns>Hex value of the rgb color</returns>
        public string GetColorHexFromRGB(string rgbColor)
        {
            string[] hexValue = rgbColor.Replace("rgba(", "").Replace(")", "").Split(',');

            int hexValue1 = int.Parse(hexValue[0]);
            hexValue[1] = hexValue[1].Trim();

            int hexValue2 = int.Parse(hexValue[1]);
            hexValue[2] = hexValue[2].Trim();

            int hexValue3 = int.Parse(hexValue[2]);

            return string.Format("#{0:X2}{1:X2}{2:X2}", hexValue1, hexValue2, hexValue3);
        }
        #endregion
    }
}