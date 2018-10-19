using Automation.UI.Core.Selenium.ExternalMail;
using Automation.UI.Core.Selenium.QsrUluruAccountAuthenPages;
using Automation.UI.Core.CommonUtilities;
using Automation.UI.Core.Selenium.PageObjects.Interpris.Platform;
using Automation.UI.Core.Selenium.PageObjects.Interpris.Transcription;
using Automation.UI.Core.DataProcessing;
using Automation.UI.Core.TestAttributes;
using Automation.UI.Core.TestBase;
using Automation.UI.Platform.TestAttributes;
using NUnit.Framework;
using System.Collections.Generic;
using System;
using System.Linq;

namespace Automation.UI.Platform.Test
{
    /// <summary>
    /// Test Suite for Transcription to Platform Auth0 feature
    /// </summary>
    [TestFixture]
    public class TranscriptionToPlatformAuth0Test : PlatformTestBase
    {
        public const string NEW_ACCOUNT_REGISTER_MSG_TITLE = "Assisted Transcription";

        #region Test Cases
        [Test]
        [Ignore("Sign Up function has been changed")]
        [TestID(TestID.TC_ID_0006), StoryID(StoryID.SR_ID_003)]
        [Priority(PriorityLevel.Medium)]
        [TestCaseSource(typeof(DataProvider), "PrepareTestCases", new object[] { TestID.TC_ID_0006 })]
        public void TC_MOVE_AUTHO_TO_CORE_VerifyAbleToAccessTranscriptionByAccountSignedUpFromCore(
            Dictionary<string, string> Data)
        {
            TestContext.Out.WriteLine("Start Test Case - {0}", TestID.TC_ID_0006);

            LoginPage loginPage = new LoginPage(Driver, PlatformBaseURL);
            IEmailHomePage emailHomePage = new YopMailHomePage(Driver);
            IEmailEmailPage emailEmailPage = new YopMailEmailPage(Driver);

            TestContext.Out.WriteLine("Go to Login Page");
            loginPage.Navigate();

            // Create a random email address
            string randomEmailAddr = RandomUtils.GetRandomEmailAddress(Data["username_head"], emailEmailPage.EmailDomainName);

            TestContext.Out.WriteLine("Register a new user activate it from email {0}", randomEmailAddr);
            SignUpNewAccountAndActivate(loginPage, emailHomePage, emailEmailPage,
                randomEmailAddr, Data["password"]);

            TestContext.Out.WriteLine("Verify email confirm message displayed");
            VerifySignUpEmailConfirmMessage();

            // just need to login to verify 
            ApplySingleUserAuthTest.TryToAccessTranscriptionLandingPageFromCorePage(
                loginPage, randomEmailAddr, Data["password"]);

            TestContext.Out.WriteLine("End Test Case - {0}", TestID.TC_ID_0006);
        }

        [Test]
        [Ignore("Transcription page not displayed when start sign in from Platform page")]
        [TestID(TestID.TC_ID_0007), StoryID(StoryID.SR_ID_003)]
        [Priority(PriorityLevel.Medium)]
        [TestCaseSource(typeof(DataProvider), "PrepareTestCases", new object[] { TestID.TC_ID_0007 })]
        public void TC_MOVE_AUTHO_TO_CORE_VerifyTranscriptionLandingPageDisplayedWhenLoggedFromCore(
            Dictionary<string, string> Data)
        {
            TestContext.Out.WriteLine("Start Test Case - {0}", TestID.TC_ID_0007);

            LoginPage loginPage = new LoginPage(Driver, PlatformBaseURL);

            ApplySingleUserAuthTest.TryToAccessTranscriptionLandingPageFromCorePage(
                loginPage, Data["username"], Data["password"]);

            TestContext.Out.WriteLine("End Test Case - {0}", TestID.TC_ID_0007);
        }

        [Test]
        [TestID(TestID.TC_ID_0008), StoryID(StoryID.SR_ID_003)]
        [Priority(PriorityLevel.Medium)]
        [TestCaseSource(typeof(DataProvider), "PrepareTestCases", new object[] { TestID.TC_ID_0008 })]
        public void TC_MOVE_AUTHO_TO_CORE_VerifyTranscriptionRequestLogInAfterLogOutFromCore(
            Dictionary<string, string> Data)
        {
            TestContext.Out.WriteLine("Start Test Case - {0}", TestID.TC_ID_0008);

            LoginPage loginPage = new LoginPage(Driver, PlatformBaseURL);
            HeaderSubPage headerSubPage = new HeaderSubPage(Driver, PlatformBaseURL);
            PortalPage portalPage = new PortalPage(Driver, PlatformBaseURL);

            TestContext.Out.WriteLine("Sign In to Platform Portal page");
            headerSubPage.LogIn(Data["username"], Data["password"]);

            TestContext.Out.WriteLine("Verify Portal page visible");
            Assert.IsTrue(portalPage.IsPageVisible(), "Portal Page not visible");

            TestContext.Out.WriteLine("Log Out from Portal page");
            headerSubPage.LogOut();

            TestContext.Out.WriteLine("Try to navigate to the Transcription Landing page");
            LoginPage transcriptionLoginPage = new LoginPage(Driver, TranscriptionBaseURL);

            transcriptionLoginPage.Navigate();
            ThreadUtils.SleepShortTime();

            TestContext.Out.WriteLine("Verify Log In page displayed instead");
            Assert.IsTrue(loginPage.IsPageVisible(), "Log In page not displayed");

            TestContext.Out.WriteLine("End Test Case - {0}", TestID.TC_ID_0008);
        }

        [Test]
        [TestID(TestID.TC_ID_0009), StoryID(StoryID.SR_ID_003)]
        [Priority(PriorityLevel.Medium)]
        [TestCaseSource(typeof(DataProvider), "PrepareTestCases", new object[] { TestID.TC_ID_0009 })]
        public void TC_MOVE_AUTHO_TO_CORE_VerifyCoreRequestLogInAfterLogOutFromTranscription(
            Dictionary<string, string> Data)
        {
            TestContext.Out.WriteLine("Start Test Case - {0}", TestID.TC_ID_0009);

            LoginPage loginPage = new LoginPage(Driver, PlatformBaseURL);
            ViewAllFilesPage viewAllFilesPage = new ViewAllFilesPage(Driver, TranscriptionBaseURL);
            PortalPage portalPage = new PortalPage(Driver, PlatformBaseURL);

            TestContext.Out.WriteLine("Sign In to Portal page");
            (new HeaderSubPage(Driver, PlatformBaseURL)).LogIn(Data["username"], Data["password"]);

            TestContext.Out.WriteLine("Go to Transcription page");
            viewAllFilesPage.Navigate();
            ThreadUtils.SleepShortTime();

            Assert.IsTrue(viewAllFilesPage.IsTabActive(), "Transcription All page not displayed");

            TestContext.Out.WriteLine("Log Out from the Transcription page");
            (new ViewAllFilesPage(Driver, TranscriptionBaseURL)).LogOut();

            TestContext.Out.WriteLine("Go to Portal page by URL");
            portalPage.Navigate();
            ThreadUtils.SleepShortTime();

            TestContext.Out.WriteLine("Verify Log In page displayed instead");
            Assert.IsTrue(loginPage.IsPageVisible(), "Log In page not displayed");

            TestContext.Out.WriteLine("End Test Case - {0}", TestID.TC_ID_0009);
        }
        #endregion

        #region Methods
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
