using Automation.UI.Core.CommonUtilities;
using Automation.UI.Core.DataProcessing;
using Automation.UI.Core.Selenium;
using Automation.UI.Core.Selenium.PageObjects.Interpris.Platform;
using Automation.UI.Core.Selenium.PageObjects.Interpris.Transcription;
using Automation.UI.Core.TestAttributes;
using Automation.UI.Core.TestBase;
using Automation.UI.Functionality.TestAttributes;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Automation.UI.Functionality.Test
{
    /// <summary>
    /// Test Suite for Sign In feature
    /// </summary>
    [TestFixture]
    public class SignInTest : InterprisTestBase
    {
        #region Test Cases
        [Test]
        [TestID(TestID.TC_ID_0001), StoryID(StoryID.SR_ID_001)]
        [Priority(PriorityLevel.Highest)]
        [TestCaseSource(typeof(DataProvider), "PrepareTestCases", new object[] { TestID.TC_ID_0008 })]
        public void TC_SIGN_IN_VerifyUserCanSignInWithActivatedAccount(Dictionary<string, string> Data)
        {
            TestContext.Out.WriteLine("Start Test Case - {0}", TestID.TC_ID_0001);

            LoginPage loginPage = new LoginPage(Driver, InterprisBaseURL);
            HeaderSubPage headerSubPage = new HeaderSubPage(Driver, InterprisBaseURL);

            // Verify sign in successfully with the activated account
            SignInSuccess(loginPage, headerSubPage, Data["username"], Data["password"]);

            TestContext.Out.WriteLine("End Test Case - {0}", TestID.TC_ID_0001);
        }
        
        [Test]
        [TestID(TestID.TC_ID_0010), StoryID(StoryID.SR_ID_002)]
        [Priority(PriorityLevel.High)]
        [TestCaseSource(typeof(DataProvider), "PrepareTestCases", new object[] { TestID.TC_ID_0010 })]
        public void TC_SIGN_IN_VerifyUserStillSignedInAfterCloseAndReOpenBrowserTab(Dictionary<string, string> Data)
        {
            TestContext.Out.WriteLine("Start Test Case - {0}", TestID.TC_ID_0010);

            LoginPage loginPage = new LoginPage(Driver, InterprisBaseURL);
            HeaderSubPage headerSubPage = new HeaderSubPage(Driver, InterprisBaseURL);

            // Verify sign in successfully with the activated account
            SignInSuccess(loginPage, headerSubPage, Data["username"], Data["password"]);

            CloseCurrentTabAndOpenNewTab();

            TestContext.Out.WriteLine("Go to Login Page");
            loginPage.Navigate();

            TestContext.Out.WriteLine("Verify landed to Home Page automatically");
            Assert.IsTrue(headerSubPage.TextHomeScreen != null, "Home is not visible");

            TestContext.Out.WriteLine("End Test Case - {0}", TestID.TC_ID_0010);
        }

        [Test]
        [TestID(TestID.TC_ID_0011), StoryID(StoryID.SR_ID_002)]
        [Priority(PriorityLevel.High)]
        [TestCaseSource(typeof(DataProvider), "PrepareTestCases", new object[] { TestID.TC_ID_0011 })]
        public void TC_SIGN_IN_VerifyUserCanMultipleSignInAtTheSameTime(Dictionary<string, string> Data)
        {
            // stop the test case if BrowserStack execution enabled
            IgnoreNotSupportingBrowserStackTest();

            TestContext.Out.WriteLine("Start Test Case - {0}", TestID.TC_ID_0011);

            // list of supported browsers to test
            List<string> listOfBrowsers = new List<string>(
            new string[] { BrowserConstants.BROWSER_IE,
                BrowserConstants.BROWSER_CHROME,
                BrowserConstants.BROWSER_FIREFOX,
                BrowserConstants.BROWSER_EDGE,
                // BROWSER_SAFARI
            });

            // remove the default driver from the list
            var itemToRemove = listOfBrowsers.SingleOrDefault(r => r.Equals(Browser));

            if (itemToRemove != null)
                listOfBrowsers.Remove(itemToRemove);

            // list of web drivers to test
            List<IWebDriver> listOfWebDrivers = new List<IWebDriver>();

            // add the default one to the list
            listOfWebDrivers.Add(Driver);

            try
            {
                // init other drivers
                foreach (string browserName in listOfBrowsers)
                {
                    IWebDriver testDriver = SetUpWebDriverInstance(browserName, ScreenSize);

                    if (testDriver != null)
                    {
                        listOfWebDrivers.Add(testDriver);
                    }
                }

                // run test over the drivers
                foreach (IWebDriver testDriver in listOfWebDrivers)
                {
                    TestContext.Out.WriteLine("Run test on other browsers");

                    LoginPage loginPage = new LoginPage(testDriver, InterprisBaseURL);
                    HeaderSubPage headerSubPage = new HeaderSubPage(testDriver, InterprisBaseURL);

                    // Verify sign in successfully with the activated account
                    SignInSuccess(loginPage, headerSubPage, Data["username"], Data["password"]);
                }
            }
            catch (Exception ex)
            {
                TestContext.Out.WriteLine("TC_ID_0011 ERR: {0}", ex.Message);
            }
            finally
            {
                // remove the default driver from the list
                var driverToRemove = listOfWebDrivers.SingleOrDefault(r => r.Equals(Driver));

                if (driverToRemove != null)
                    listOfWebDrivers.Remove(driverToRemove);

                // close other browsers
                foreach (IWebDriver testDriver in listOfWebDrivers)
                {
                    try
                    {
                        testDriver.Quit();
                    }
                    catch (Exception ex)
                    {
                        TestContext.Out.WriteLine("TC_ID_0011 Close Driver ERR: {0}", ex.Message);
                    }
                }
            }

            TestContext.Out.WriteLine("End Test Case - {0}", TestID.TC_ID_0011);
        }

        [Test]
        [TestID(TestID.TC_ID_0012), StoryID(StoryID.SR_ID_002)]
        [Priority(PriorityLevel.Medium)]
        [TestCaseSource(typeof(DataProvider), "PrepareTestCases", new object[] { TestID.TC_ID_0012 })]
        public void TC_SIGN_IN_VerifyUserCannotSignedInWithIncorrectEmailAndCorrectPassword(Dictionary<string, string> Data)
        {
            TestContext.Out.WriteLine("Start Test Case - {0}", TestID.TC_ID_0012);

            LoginPage loginPage = new LoginPage(Driver, InterprisBaseURL);

            string errorMessage = "";

            // Verify sign in unsuccessfully
            if (Browser.Equals(BrowserConstants.BROWSER_FIREFOX))
            {
                errorMessage = LoginPage.ERR_MSG_FF_SIGN_IN_INVALID_EMAIL_OR_PASSWORD;
            }
            else
            {
                errorMessage = LoginPage.ERR_MSG_SIGN_IN_INVALID_EMAIL_OR_PASSWORD;
            }

            // sign in unsuccessfully
            SignInUnSuccess(loginPage, "new" + Data["username"], Data["password"], errorMessage);

            switch (Browser)
            {
                case BrowserConstants.BROWSER_CHROME:
                    TestContext.Out.WriteLine("Fill in invalid email 1");
                    loginPage.InputLoginInfo(Data["invalid_email_1"], Data["password"]);

                    ThreadUtils.SleepShortTime();

                    TestContext.Out.WriteLine("Verify if the message displayed correctly");
                    Assert.IsTrue(loginPage.IsValidateMessageInvalidEmaiVisible(LoginPage.ERR_MSG_GC_INVALID_EMAIL_TYPE_1),
                        "Validate Message not visible.");

                    errorMessage = LoginPage.ERR_MSG_GC_INVALID_EMAIL_TYPE_2;

                    break;
                case BrowserConstants.BROWSER_FIREFOX:
                    errorMessage = LoginPage.ERR_MSG_FF_INVALID_EMAIL;
                    break;
                case BrowserConstants.BROWSER_IE:
                case BrowserConstants.BROWSER_EDGE:
                    errorMessage = LoginPage.ERR_MSG_IE_INVALID_EMAIL;
                    break;
                case BrowserConstants.BROWSER_SAFARI:
                    break;
                default:
                    break;
            }

            TestContext.Out.WriteLine("Fill in invalid email 2");
            loginPage.InputLoginInfo(Data["invalid_email_2"], Data["password"]);

            ThreadUtils.SleepShortTime();

            TestContext.Out.WriteLine("Verify if the message displayed correctly");
            Assert.IsTrue(loginPage.IsValidateMessageInvalidEmaiVisible(errorMessage),
                "Validate Message not visible.");

            TestContext.Out.WriteLine("End Test Case - {0}", TestID.TC_ID_0012);
        }

        [Test]
        [TestID(TestID.TC_ID_0013), StoryID(StoryID.SR_ID_002)]
        [Priority(PriorityLevel.Medium)]
        [TestCaseSource(typeof(DataProvider), "PrepareTestCases", new object[] { TestID.TC_ID_0013 })]
        public void TC_SIGN_IN_VerifyUserCannotSignInWithValidEmailAndInvalidPassword(Dictionary<string, string> Data)
        {
            TestContext.Out.WriteLine("Start Test Case - {0}", TestID.TC_ID_0013);

            LoginPage loginPage = new LoginPage(Driver, InterprisBaseURL);

            // Verify sign in unsuccessfully
            SignInUnSuccess(loginPage, Data["username"], Data["invalid_password"],
                LoginPage.ERR_MSG_SIGN_IN_INVALID_EMAIL_OR_PASSWORD);

            TestContext.Out.WriteLine("End Test Case - {0}", TestID.TC_ID_0013);
        }

        [Test]
        [TestID(TestID.TC_ID_0014), StoryID(StoryID.SR_ID_002)]
        [Priority(PriorityLevel.Medium)]
        [TestCaseSource(typeof(DataProvider), "PrepareTestCases", new object[] { TestID.TC_ID_0014 })]
        public void TC_SIGN_IN_VerifyUserCannotSignInWithInValidEmailAndValidPassword(Dictionary<string, string> Data)
        {
            TestContext.Out.WriteLine("Start Test Case - {0}", TestID.TC_ID_0014);

            LoginPage loginPage = new LoginPage(Driver, InterprisBaseURL);

            // Verify sign in unsuccessfully
            SignInUnSuccess(loginPage, "invalid_" + Data["username"], Data["password"],
                LoginPage.ERR_MSG_SIGN_IN_INVALID_EMAIL_OR_PASSWORD);

            TestContext.Out.WriteLine("End Test Case - {0}", TestID.TC_ID_0014);
        }

        [Test]
        [TestID(TestID.TC_ID_0015), StoryID(StoryID.SR_ID_002)]
        [Priority(PriorityLevel.Medium)]
        public void TC_SIGN_IN_VerifyUserCannotSignInWithEmptyEmailAndPassword()
        {
            TestContext.Out.WriteLine("Start Test Case - {0}", TestID.TC_ID_0015);

            LoginPage loginPage = new LoginPage(Driver, InterprisBaseURL);

            TestContext.Out.WriteLine("Go to Login Page");
            loginPage.Navigate();

            TestContext.Out.WriteLine("Click Log In tab and Fill required information");
            loginPage.InputLoginInfo("", "");

            TestContext.Out.WriteLine("Get error messages from username and password");
            string errorMsg = loginPage.GetUsernameErrorMessage();
            Assert.IsTrue(errorMsg.Equals(LoginPage.ERR_MSG_USERNAME_EMPTY, StringComparison.OrdinalIgnoreCase),
                "Invalid Username error message {0} not {1}", errorMsg, LoginPage.ERR_MSG_USERNAME_EMPTY);

            errorMsg = loginPage.GetPasswordErrorMessage();
            Assert.IsTrue(errorMsg.Equals(LoginPage.ERR_MSG_PASSWORD_EMPTY, StringComparison.OrdinalIgnoreCase),
                "Invalid Password error message {0} not {1}", errorMsg, LoginPage.ERR_MSG_PASSWORD_EMPTY);

            TestContext.Out.WriteLine("End Test Case - {0}", TestID.TC_ID_0015);
        }

        [Test]
        [Ignore("Account blocking feature not available yet")]
        [TestID(TestID.TC_ID_0016), StoryID(StoryID.SR_ID_002)]
        [Priority(PriorityLevel.High)]
        [TestCaseSource(typeof(DataProvider), "PrepareTestCases", new object[] { TestID.TC_ID_0016 })]
        public void TC_SIGN_IN_VerifyUserisBlockedAfterTryingMultiAttempts(Dictionary<string, string> Data)
        {
            TestContext.Out.WriteLine("Start Test Case - {0}", TestID.TC_ID_0016);

            LoginPage loginPage = new LoginPage(Driver, InterprisBaseURL);
            LandingPage landingPage = new LandingPage(Driver, InterprisBaseURL);

            int iTry = int.Parse(Data["multi_time_loop"]);

            string informMsgUI = "";

            for (int i = 0; i < iTry; i++)
            {
                TestContext.Out.WriteLine("Go to Login Page");
                loginPage.Navigate();

                TestContext.Out.WriteLine("Click Log In tab and Fill required information");
                loginPage.InputLoginInfo(Data["username_multi_time"], Data["password_multi_time"]);

                // thread some seconds for error message to displayed
                ThreadUtils.SleepShortTime();

                informMsgUI = loginPage.GetErrorMessage().Trim();

                TestContext.Out.WriteLine("Verify error message from Sign In page");
                if (informMsgUI.Equals(LoginPage.ERR_MSG_SIGN_IN_INVALID_EMAIL_OR_PASSWORD, StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }
                else if (informMsgUI.Equals(LoginPage.ERR_MSG_ACCOUNT_MULTI_LOGIN_BLOCKED, StringComparison.OrdinalIgnoreCase))
                {
                    break;
                }
            }

            TestContext.Out.WriteLine("Verify error message for multiple access");
            Assert.IsTrue(informMsgUI.Equals(LoginPage.ERR_MSG_ACCOUNT_MULTI_LOGIN_BLOCKED,
                StringComparison.OrdinalIgnoreCase),
                "Inform message Sign Up not correct {0} not {1}.",
                informMsgUI, LoginPage.ERR_MSG_ACCOUNT_MULTI_LOGIN_BLOCKED);

            TestContext.Out.WriteLine("End Test Case - {0}", TestID.TC_ID_0016);
        }
        #endregion

        #region Public methods
        /// <summary>
        /// Try to log in with valid username and password
        /// Expect to get in the Landing page
        /// </summary>
        /// <param name="loginPage">Login page</param>
        /// <param name="landingPage">Landing page</param>
        /// <param name="username">Valid username</param>
        /// <param name="password">Valid password</param>
        public static void SignInSuccess(LoginPage loginPage, HeaderSubPage headerSubPage,
            string username, string password)
        {
            TestContext.Out.WriteLine("Go to Login Page");
            loginPage.Navigate();

            TestContext.Out.WriteLine("Click Log In tab and Fill required information");
            loginPage.InputLoginInfo(username, password);

            TestContext.Out.WriteLine("Verify landed to Home Page");
            Assert.IsTrue(headerSubPage.TextHomeScreen != null, "Home text is not visible");
        }

        /// <summary>
        /// Try to log in with invalid username or invalid password
        /// </summary>
        /// <param name="loginPage">Login page</param>
        /// <param name="username">Username to log in</param>
        /// <param name="password">Password to log in</param>
        /// <param name="errorMessage">Expected error message</param>
        public static void SignInUnSuccess(LoginPage loginPage, string username, string password, string errorMessage)
        {
            TestContext.Out.WriteLine("Go to Login Page");
            loginPage.Navigate();

            TestContext.Out.WriteLine("Click Log In tab and Fill required information");
            loginPage.InputLoginInfo(username, password);

            // thread some seconds for error message to displayed
            ThreadUtils.SleepMediumTime();

            string informMsgUI = loginPage.GetErrorMessage().Trim();

            TestContext.Out.WriteLine("Verify error message from Sign In page");
            Assert.IsTrue(informMsgUI.Equals(errorMessage, StringComparison.OrdinalIgnoreCase),
                "Inform message Sign Up not correct {0} not {1}.", informMsgUI, errorMessage);
        }
        #endregion
    }
}
