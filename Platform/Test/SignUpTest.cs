using Automation.UI.Core.Selenium.ExternalMail;
using Automation.UI.Core.Selenium.QsrUluruAccountAuthenPages;
using Automation.UI.Core.CommonUtilities;
using Automation.UI.Core.DataProcessing;
using Automation.UI.Core.Selenium;
using Automation.UI.Core.Selenium.PageObjects.Interpris.Platform;
using Automation.UI.Core.TestAttributes;
using Automation.UI.Core.TestBase;
using Automation.UI.Platform.TestAttributes;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Automation.UI.Platform.Test
{
    /// <summary>
    /// Test Suite for Sign Up feature
    /// </summary>
    [TestFixture]
    public class SignUpTest : PlatformTestBase
    {
        public const string NEW_ACCOUNT_REGISTER_MSG_TITLE = "Assisted Transcription";

        #region Test Cases
        [Test]
        [TestID(TestID.TC_ID_0062), StoryID(StoryID.SR_ID_013)]
        [Priority(PriorityLevel.Highest)]
        [TestCaseSource(typeof(DataProvider), "PrepareTestCases", new object[] { TestID.TC_ID_0062 })]
        public void TC_SIGN_UP_VerifyUserCannotSignUpWithInvalidEmailAndValidPassword(Dictionary<string, string> Data)
        {
            TestContext.Out.WriteLine("Start Test Case - {0}", TestID.TC_ID_0062);

            LoginPage loginPage = new LoginPage(Driver, TranscriptionBaseURL);

            TestContext.Out.WriteLine("Go to Login Page");
            loginPage.Navigate();

            string errorMessage = "";

            switch (Browser)
            {
                case BrowserConstants.BROWSER_CHROME:
                    TestContext.Out.WriteLine("Fill in invalid email 1");
                    loginPage.InputSignUpInfo(Data["invalid_email_1"], Data["password"]);

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
            loginPage.InputSignUpInfo(Data["invalid_email_2"], Data["password"]);

            ThreadUtils.SleepShortTime();

            TestContext.Out.WriteLine("Verify if the message displayed correctly");
            Assert.IsTrue(loginPage.IsValidateMessageInvalidEmaiVisible(errorMessage),
                "Validate Message not visible.");

            TestContext.Out.WriteLine("End Test Case - {0}", TestID.TC_ID_0062);
        }

        [Test]
        [TestID(TestID.TC_ID_0058), StoryID(StoryID.SR_ID_013)]
        [Priority(PriorityLevel.Highest)]
        [TestCaseSource(typeof(DataProvider), "PrepareTestCases", new object[] { TestID.TC_ID_0058 })]
        public void TC_SIGN_UP_VerifyUserCannotSignUpWithValidEmailAndInvalidPassword(Dictionary<string, string> Data)
        {
            TestContext.Out.WriteLine("Start Test Case - {0}", TestID.TC_ID_0058);

            LoginPage loginPage = new LoginPage(Driver, TranscriptionBaseURL);
            IEmailEmailPage emailEmailPage = new YopMailEmailPage(Driver);

            // Create a random email address
            string randomEmailAddr = RandomUtils.GetRandomEmailAddress(Data["username_head"], emailEmailPage.EmailDomainName);

            TestContext.Out.WriteLine("Go to Login Page");
            loginPage.Navigate();

            TestContext.Out.WriteLine("Click Sign Up tab and Fill required information");
            loginPage.InputSignUpInfo(randomEmailAddr, Data["invalid_password"]);

            TestContext.Out.WriteLine("Verify password reminder dialog displayed");
            Assert.IsTrue(loginPage.IsPasswordReminderDialogVisible(), "Password reminder dialog not displayed.");

            TestContext.Out.WriteLine("End Test Case - {0}", TestID.TC_ID_0058);
        }

        [Test]
        [TestID(TestID.TC_ID_0059), StoryID(StoryID.SR_ID_013)]
        [Priority(PriorityLevel.Highest)]
        [TestCaseSource(typeof(DataProvider), "PrepareTestCases", new object[] { TestID.TC_ID_0059 })]
        public void TC_SIGN_UP_VerifyUserCannotSignUpWithInvalidEmailAndInValidPassword(Dictionary<string, string> Data)
        {
            TestContext.Out.WriteLine("Start Test Case - {0}", TestID.TC_ID_0059);

            LoginPage loginPage = new LoginPage(Driver, TranscriptionBaseURL);

            TestContext.Out.WriteLine("Go to Login Page");
            loginPage.Navigate();

            string errorMessage = "";

            switch (Browser)
            {
                case BrowserConstants.BROWSER_CHROME:
                    TestContext.Out.WriteLine("Fill in invalid email 1");
                    loginPage.InputSignUpInfo(Data["invalid_email_1"], Data["invalid_password"]);

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

            TestContext.Out.WriteLine("Input invalid password");
            loginPage.ASignUpTab.Click();
            loginPage.InputPassword.SendKeys(Data["invalid_password"]);

            TestContext.Out.WriteLine("Verify password reminder dialog displayed");
            Assert.IsTrue(loginPage.IsPasswordReminderDialogVisible(), "Password reminder dialog not displayed.");

            TestContext.Out.WriteLine("Fill in invalid email 2");
            loginPage.InputEmail.SendKeys(Data["invalid_email_2"]);

            ThreadUtils.SleepShortTime();

            TestContext.Out.WriteLine("Verify if the message displayed correctly");
            Assert.IsTrue(loginPage.IsValidateMessageInvalidEmaiVisible(errorMessage),
                "Validate Message not visible.");

            TestContext.Out.WriteLine("End Test Case - {0}", TestID.TC_ID_0059);
        }

        [Test]
        [TestID(TestID.TC_ID_0060), StoryID(StoryID.SR_ID_013)]
        [Priority(PriorityLevel.Highest)]
        [TestCaseSource(typeof(DataProvider), "PrepareTestCases", new object[] { TestID.TC_ID_0060 })]
        public void TC_SIGN_UP_VerifyUserCannotSignUpWithExistingEmail(Dictionary<string, string> Data)
        {
            TestContext.Out.WriteLine("Start Test Case - {0}", TestID.TC_ID_0060);

            LoginPage loginPage = new LoginPage(Driver, TranscriptionBaseURL);

            TestContext.Out.WriteLine("Go to Login Page");
            loginPage.Navigate();

            TestContext.Out.WriteLine("Click Sign Up tab and Fill required information");
            loginPage.InputSignUpInfo(Data["username"], Data["password"]);

            ThreadUtils.SleepMediumTime();

            string informMsgUI = loginPage.GetErrorMessage().Trim();

            TestContext.Out.WriteLine("Verify error message from Sign In page");
            Assert.IsTrue(informMsgUI.Equals(LoginPage.ERR_MSG_USER_EXISTING, StringComparison.OrdinalIgnoreCase),
                "Inform message Sign Up not correct {0} not {1}.", informMsgUI, LoginPage.ERR_MSG_USER_EXISTING);

            TestContext.Out.WriteLine("End Test Case - {0}", TestID.TC_ID_0060);
        }

        [Test]
        [TestID(TestID.TC_ID_0061), StoryID(StoryID.SR_ID_013)]
        [Priority(PriorityLevel.Highest)]
        public void TC_SIGN_UP_VerifyUserCannotSignUpEmptyEmailAndPassword()
        {
            TestContext.Out.WriteLine("Start Test Case - {0}", TestID.TC_ID_0061);

            LoginPage loginPage = new LoginPage(Driver, TranscriptionBaseURL);

            TestContext.Out.WriteLine("Go to Login Page");
            loginPage.Navigate();

            TestContext.Out.WriteLine("Click Sign Up tab and Fill required information");
            loginPage.InputSignUpInfo("", "");

            TestContext.Out.WriteLine("Get error messages from username and password");
            string errorMsg = loginPage.GetUsernameErrorMessage();
            Assert.IsTrue(errorMsg.Equals(LoginPage.ERR_MSG_USERNAME_EMPTY, StringComparison.OrdinalIgnoreCase),
                "Invalid Username error message {0} not {1}", errorMsg, LoginPage.ERR_MSG_USERNAME_EMPTY);

            // TODO: currently Password box does not show message
            //errorMsg = loginPage.GetPasswordErrorMessage();
            //Assert.IsTrue(errorMsg.Equals(LoginPage.ERR_MSG_PASSWORD_EMPTY, StringComparison.OrdinalIgnoreCase),
            //    "Invalid Password error message {0} not {1}", errorMsg, LoginPage.ERR_MSG_PASSWORD_EMPTY);

            TestContext.Out.WriteLine("End Test Case - {0}", TestID.TC_ID_0061);
        }
        #endregion

        #region Public methods
        /// <summary>
        /// Wait and switch to the email confirm page to verify confirm success message
        /// Close the email confirm page
        /// </summary>
        public void VerifySignUpEmailConfirmMessage()
        {
            int MAX_WAIT = 5;
            int i = 0;

            // wait for the 2 tab opened
            while (i < MAX_WAIT && Driver.WindowHandles.Count <= 1)
            {
                i++;
                ThreadUtils.SleepShortTime();
            }

            // switch to email confirm page
            Driver.SwitchTo().Window(Driver.WindowHandles.Last());
            ThreadUtils.SleepShortTime();

            EmailVerificationConfirmPage confirmPage = new EmailVerificationConfirmPage(Driver);
            Assert.IsTrue(confirmPage.IsConfirmSuccessMessageVisible(), "Email confirm message not appears");

            // go back the first tab
            Driver.Close();
            Driver.SwitchTo().Window(Driver.WindowHandles.First());
        }

        /// <summary>
        /// Sign Up a new account and activate by email
        /// </summary>
        /// <param name="loginPage">Page object to Log In page</param>
        /// <param name="emailHomePage">Page object to email home page</param>
        /// <param name="emailEmailPage">Page object to email items page</param>
        /// <param name="emailAddress">Email address to login</param>
        /// <param name="emailPassword">Email password to login</param>
        public static void SignUpNewAccountAndActivate(LoginPage loginPage, IEmailHomePage emailHomePage,
            IEmailEmailPage emailEmailPage, string emailAddress, string emailPassword)
        {
            SubmitSignUpNewAccount(loginPage, emailAddress, emailPassword);
            ThreadUtils.SleepShortTime();

            emailHomePage.OpenEmail(emailAddress, emailPassword);
            ThreadUtils.SleepShortTime();

            emailEmailPage.ActivateAccountByEmail(NEW_ACCOUNT_REGISTER_MSG_TITLE);
            ThreadUtils.SleepShortTime();
        }

        /// <summary>
        /// Sign Up a new account but not activate it yet
        /// </summary>
        /// <param name="loginPage">Page object to Log In page</param>
        /// <param name="emailHomePage">Page object to email home page</param>
        /// <param name="emailEmailPage">Page object to email items page</param>
        /// <param name="emailAddress">Email address to login</param>
        /// <param name="emailPassword">Email password to login</param>
        public static void SignUpNewAccountWithoutActivate(LoginPage loginPage, IEmailHomePage emailHomePage,
            IEmailEmailPage emailEmailPage, string emailAddress, string emailPassword)
        {
            SubmitSignUpNewAccount(loginPage, emailAddress, emailPassword);
            ThreadUtils.SleepShortTime();

            emailHomePage.OpenEmail(emailAddress, emailPassword);
            ThreadUtils.SleepShortTime();

            string emailMsgTitle = emailEmailPage.GetFirstEmailItemTitle();

            Assert.IsTrue(emailMsgTitle.Contains(NEW_ACCOUNT_REGISTER_MSG_TITLE),
                "New account register message not correct {0} not {1}.", emailMsgTitle, NEW_ACCOUNT_REGISTER_MSG_TITLE);
        }

        /// <summary>
        /// Fill in the correct information to Sign Up a new account
        /// </summary>
        /// <param name="loginPage">Page object to Log In page</param>
        /// <param name="username">Username</param>
        /// <param name="password">Password</param>
        /// <returns>Inform message after submitting Sign Up infor</returns>
        public static void SubmitSignUpNewAccount(LoginPage loginPage,
            string username, string password)
        {
            loginPage.InputSignUpInfo(username, password);

            string informMsgUI = loginPage.GetErrorMessage().Trim();

            Assert.IsTrue(informMsgUI.Equals(LoginPage.ERR_MSG_VERIFY_EMAIL_BEFORE_LOGGING_IN,
                StringComparison.OrdinalIgnoreCase),
                "Inform message Sign Up not correct {0} not {1}.", informMsgUI,
                LoginPage.ERR_MSG_VERIFY_EMAIL_BEFORE_LOGGING_IN);
        }
        #endregion
    }
}
