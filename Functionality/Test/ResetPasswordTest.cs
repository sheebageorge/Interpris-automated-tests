using Automation.UI.Core.Selenium.ExternalMail;
using Automation.UI.Core.Selenium.QsrUluruAccountAuthenPages;
using Automation.UI.Core.CommonUtilities;
using Automation.UI.Core.DataProcessing;
using Automation.UI.Core.Selenium;
using Automation.UI.Core.Selenium.PageObjects.Interpris.Platform;
using Automation.UI.Core.Selenium.PageObjects.Interpris.Product;
using Automation.UI.Core.TestAttributes;
using Automation.UI.Core.TestBase;
using Automation.UI.Functionality.TestAttributes;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Automation.UI.Functionality.Test
{
    /// <summary>
    /// Test Suite for reset password feature
    /// </summary>
    [TestFixture]
    public class ResetPasswordTest : InterprisTestBase
    {
        public const string NVIVO_RESET_PASSWORD_EMAIL_TITLE_1 = "NVivo Transcription | Change Password";
        public const string NVIVO_RESET_PASSWORD_EMAIL_TITLE_2 = "Reset your password";

        public static readonly List<string> resetPWEmailTitles = new List<string>()
            { NVIVO_RESET_PASSWORD_EMAIL_TITLE_1, NVIVO_RESET_PASSWORD_EMAIL_TITLE_2};

        #region Test Cases
        [Test]
        [TestID(TestID.TC_ID_0031), StoryID(StoryID.SR_ID_004)]
        [Priority(PriorityLevel.Highest)]
        [TestCaseSource(typeof(DataProvider), "PrepareTestCases", new object[] { TestID.TC_ID_0031 })]
        public void TC_RESET_PASSWORD_VerifyUserCanResetPasswordOfAnExistingAccount(Dictionary<string, string> Data)
        {
            TestContext.Out.WriteLine("Start Test Case - {0}", TestID.TC_ID_0031);

            LoginPage loginPage = new LoginPage(Driver, InterprisBaseURL);
            HeaderSubPage headerSubPage = new HeaderSubPage(Driver, InterprisBaseURL);

            TestContext.Out.WriteLine("Go to Login Page");
            loginPage.Navigate();

            // fill in reset password infor and submit
            FillInResetPasswordInfor(loginPage, Data["reset_pass_username"], LoginPage.INFOR_MSG_RESET_PASSWORD_SENT_MAIL);

            // open email and click reset password link
            string resetPassLink = OpenEmailAndClickResetPasswordLinkInEmail(
                Data["reset_pass_username"], Data["reset_pass_password"]);

            Assert.IsTrue(!string.IsNullOrEmpty(resetPassLink), "Reset Password link empty.");

            // change password
            string newPassword = Data["reset_pass_password"] + RandomUtils.GetMilisecondString();
            FillInNewPassword(resetPassLink, newPassword);

            TestContext.Out.WriteLine("Go to Login Page");
            loginPage.Navigate();

            // Verify sign in successfully with the activated account
            SignInTest.SignInSuccess(loginPage, headerSubPage, Data["reset_pass_username"], newPassword);

            TestContext.Out.WriteLine("End Test Case - {0}", TestID.TC_ID_0031);
        }

        [Test]
        [Ignore("Account blocking feature not available yet")]
        [TestID(TestID.TC_ID_0032), StoryID(StoryID.SR_ID_004)]
        [Priority(PriorityLevel.Highest)]
        [TestCaseSource(typeof(DataProvider), "PrepareTestCases", new object[] { TestID.TC_ID_0032 })]
        public void TC_RESET_PASSWORD_VerifyUserCannotResetBlockedAccountPassword(Dictionary<string, string> Data)
        {
            TestContext.Out.WriteLine("Start Test Case - {0}", TestID.TC_ID_0032);

            LoginPage loginPage = new LoginPage(Driver, InterprisBaseURL);

            TestContext.Out.WriteLine("Go to Login Page");
            loginPage.Navigate();

            // call verify method
            VerifyResetPasswordEmailNotComeForInvalidAccount(loginPage,
                Data["blocked_username"], LoginPage.INFOR_MSG_RESET_PASSWORD_SENT_MAIL,
                Data["blocked_username"], Data["blocked_password"]);

            TestContext.Out.WriteLine("End Test Case - {0}", TestID.TC_ID_0032);
        }

        [Test]
        [TestID(TestID.TC_ID_0033), StoryID(StoryID.SR_ID_004)]
        [Priority(PriorityLevel.Medium)]
        [TestCaseSource(typeof(DataProvider), "PrepareTestCases", new object[] { TestID.TC_ID_0033 })]
        public void TC_RESET_PASSWORD_VerifyUserCannotResetPasswordForNotRegisteredAccount(Dictionary<string, string> Data)
        {
            TestContext.Out.WriteLine("Start Test Case - {0}", TestID.TC_ID_0033);

            LoginPage loginPage = new LoginPage(Driver, InterprisBaseURL);

            TestContext.Out.WriteLine("Go to Login Page");
            loginPage.Navigate();

            // call verify method
            VerifyResetPasswordEmailNotComeForInvalidAccount(loginPage, Data["unregistered_username"],
                LoginPage.INFOR_MSG_RESET_PASSWORD_SENT_MAIL,
                Data["unregistered_username"], Data["unregistered_password"]);

            TestContext.Out.WriteLine("End Test Case - {0}", TestID.TC_ID_0033);
        }

        [Test]
        [TestID(TestID.TC_ID_0034), StoryID(StoryID.SR_ID_004)]
        [Priority(PriorityLevel.Medium)]
        [TestCaseSource(typeof(DataProvider), "PrepareTestCases", new object[] { TestID.TC_ID_0034 })]
        public void TC_RESET_PASSWORD_ValidateEmailForResetPassword(Dictionary<string, string> Data)
        {
            TestContext.Out.WriteLine("Start Test Case - {0}", TestID.TC_ID_0034);

            LoginPage loginPage = new LoginPage(Driver, InterprisBaseURL);

            TestContext.Out.WriteLine("Go to Login Page");
            loginPage.Navigate();

            TestContext.Out.WriteLine("Fill in empty email");
            loginPage.InputResetPasswordInfo("");

            TestContext.Out.WriteLine("Get error messages from username");
            string errorMsg = loginPage.GetUsernameErrorMessage();
            Assert.IsTrue(errorMsg.Equals(LoginPage.ERR_MSG_USERNAME_EMPTY, StringComparison.OrdinalIgnoreCase),
                "Invalid Username error message {0} not {1}", errorMsg, LoginPage.ERR_MSG_USERNAME_EMPTY);

            string errorMessage = "";

            switch (Browser)
            {
                case BrowserConstants.BROWSER_CHROME:
                    TestContext.Out.WriteLine("Fill in invalid email 1");
                    loginPage.InputEmail.SendKeys(Data["invalid_email_1"]);
                    loginPage.SpanSendMail.Click();

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
            loginPage.InputEmail.SendKeys(Data["invalid_email_2"]);
            loginPage.SpanSendMail.Click();

            ThreadUtils.SleepShortTime();

            TestContext.Out.WriteLine("Verify if the message displayed correctly");
            Assert.IsTrue(loginPage.IsValidateMessageInvalidEmaiVisible(errorMessage),
                "Validate Message not visible.");

            TestContext.Out.WriteLine("End Test Case - {0}", TestID.TC_ID_0034);
        }

        [Test]
        [Ignore("Known Issue - Not active account still receiving reset password email")]
        [TestID(TestID.TC_ID_0035), StoryID(StoryID.SR_ID_004)]
        [Priority(PriorityLevel.Medium)]
        [TestCaseSource(typeof(DataProvider), "PrepareTestCases", new object[] { TestID.TC_ID_0035 })]
        public void TC_RESET_PASSWORD_VerifyUserCannotResetPasswordForNotActivatedAccount(Dictionary<string, string> Data)
        {
            TestContext.Out.WriteLine("Start Test Case - {0}", TestID.TC_ID_0035);

            LoginPage loginPage = new LoginPage(Driver, InterprisBaseURL);

            TestContext.Out.WriteLine("Go to Login Page");
            loginPage.Navigate();

            // call verify method
            VerifyResetPasswordEmailNotComeForInvalidAccount(loginPage, Data["noactive_username"],
                LoginPage.INFOR_MSG_RESET_PASSWORD_SENT_MAIL,
                Data["noactive_username"], Data["noactive_password"]);

            TestContext.Out.WriteLine("End Test Case - {0}", TestID.TC_ID_0035);
        }
        #endregion

        #region Public methods
        /// <summary>
        /// Fill in new password and submit
        /// </summary>
        /// <param name="newPassword">New password</param>
        public void FillInNewPassword(string resetPassLink, string newPassword)
        {
            Driver.Navigate().GoToUrl(resetPassLink);

            ThreadUtils.SleepShortTime();

            ChangePasswordPage changePasswordPage = new ChangePasswordPage(Driver);

            TestContext.Out.WriteLine("Fill in new password and submit");
            Assert.IsTrue(changePasswordPage.FillInChangePasswordInfor(newPassword), "Change password not successfully.");
        }

        /// <summary>
        /// Verify an invalid account cannot receive Reset Password email
        /// </summary>
        /// <param name="loginPage">Login Page</param>
        /// <param name="username">Email address of the user</param>
        /// <param name="inforMsg">Inform message to check</param>
        /// <param name="emailHomePage">Page object to email home page</param>
        /// <param name="emailEmailPage">Page object to email items page</param>
        /// <param name="emailAddress">Email address to login</param>
        /// <param name="emailPassword">Email password to login</param>
        public static void VerifyResetPasswordEmailNotComeForInvalidAccount(LoginPage loginPage, string username, string inforMsg,
            string emailAddress, string emailPassword)
        {
            // fill in reset password infor and submit
            FillInResetPasswordInfor(loginPage, username, inforMsg);

            // should waiting for a while before checking new email
            ThreadUtils.SleepLongTime();

            // verify email
            OpenMailAndVerifyNotResetPasswordEmail(emailAddress, emailPassword);
        }

        /// <summary>
        /// Open reset password box
        /// Fill in email addresss
        /// Click Send Mail button
        /// </summary>
        /// <param name="loginPage">Login Page</param>
        /// <param name="username">Email address of the user</param>
        /// <param name="inforMsg">Inform message to check</param>
        public static void FillInResetPasswordInfor(LoginPage loginPage, string username, string inforMsg)
        {
            TestContext.Out.WriteLine("Fill in reset password email and submit");
            loginPage.InputResetPasswordInfo(username);

            // thread some seconds for error message to displayed
            ThreadUtils.SleepMediumTime();

            string informMsgUI = loginPage.GetInforMessage().Trim();

            TestContext.Out.WriteLine("Verify Inform message from Reset password page");
            Assert.IsTrue(informMsgUI.Equals(inforMsg, StringComparison.OrdinalIgnoreCase),
                "Inform message not correct {0} not {1}.", informMsgUI, inforMsg);
        }

        /// <summary>
        /// Open email to click Reset password link
        /// </summary>
        /// <param name="emailHomePage">Page object to email home page</param>
        /// <param name="emailEmailPage">Page object to email items page</param>
        /// <param name="emailAddress">Email address to login</param>
        /// <param name="emailPassword">Email password to login</param>
        public static string OpenEmailAndClickResetPasswordLinkInEmail(
            string emailAddress, string emailPassword, int MAX_WAIT = 10)
        {
            TestContext.Out.WriteLine("Open email to reset password");

            int i = 0;
            while(i < MAX_WAIT)
            {
                i++;

                // get the message from email server
                string msgBody = GmailPop3Client.GetUluruAccountInfoEmailByTitle(emailAddress, emailPassword,
                    resetPWEmailTitles);

                TestContext.Out.WriteLine("OpenEmailAndClickResetPasswordLinkInEmail: {0}", msgBody);

                if (string.IsNullOrEmpty(msgBody))
                {
                    ThreadUtils.SleepMediumTime();

                    continue;
                }

                // analyze the email message to get the reset password url
                string resetPassLink = GetResetPasswordLinkFromEmailBody(msgBody);

                TestContext.Out.WriteLine("OpenEmailAndClickResetPasswordLinkInEmail: {0}", resetPassLink);

                return resetPassLink;
            }

            return string.Empty;
        }

        /// <summary>
        /// Open email to check if the Password Reset email comes
        /// </summary>
        /// <param name="emailHomePage">Page object to email home page</param>
        /// <param name="emailEmailPage">Page object to email items page</param>
        /// <param name="emailAddress">Email address to login</param>
        /// <param name="emailPassword">Email password to login</param>
        public static void OpenMailAndVerifyNotResetPasswordEmail(string emailAddress, string emailPassword)
        {
            TestContext.Out.WriteLine("Open email to reset password");
            string msgBody = GmailPop3Client.GetUluruAccountInfoEmailByTitle(emailAddress, emailPassword,
                resetPWEmailTitles);

            TestContext.Out.WriteLine("OpenMailAndVerifyNotResetPasswordEmail: {0}", msgBody);

            Assert.IsTrue(msgBody == null, "Reset Password email still received!");
        }

        public static string GetResetPasswordLinkFromEmailBody(string emailBody)
        {
            string resetPassText1Start = "If it was you, then confirm the password change by opening this url:";
            string resetPass1NextWord = "Thanks,";

            string resetPassText2Start = "If it was you, then to confirm the password change click here (";
            string resetPass2NextWord = ")";

            int posStart = emailBody.IndexOf(resetPassText1Start);
            int posNext = -1;

            if (posStart >= 0)
            {
                posNext = emailBody.IndexOf(resetPass1NextWord, posStart);

                if (posStart > 0 && posNext > 0)
                {
                    return emailBody.Substring(posStart + resetPassText1Start.Length,
                        posNext - (posStart + resetPassText1Start.Length)).Trim();
                }
            }
            else
            {
                posStart = emailBody.IndexOf(resetPassText2Start);
                posNext = emailBody.IndexOf(resetPass2NextWord, posStart);

                if (posStart > 0 && posNext > 0)
                {
                    return emailBody.Substring(posStart + resetPassText2Start.Length,
                        posNext - (posStart + resetPassText2Start.Length)).Trim();
                }
            }

            return "";
        }
        #endregion
    }
}
