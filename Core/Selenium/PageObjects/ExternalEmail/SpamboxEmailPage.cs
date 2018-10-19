using Automation.UI.Core.CommonUtilities;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;

namespace Automation.UI.Core.Selenium.ExternalMail
{
    /// <summary>
    /// Wrapper class of Spambox email page
    /// </summary>
    public class SpamboxEmailPage : BasePage, IEmailEmailPage
    {
        public const string SPAMBOX_EMAILPAGE_URL = "http://www.spambox.xyz/";

        public const string VERIFY_EMAIL_EMAIL_TITLE = "Verify your email";
        public const string RESET_PASSWORD_EMAIL_TITLE = "You have submitted a password change request!";

        #region UI Objects
        private readonly string divEmailTitle = "#current-tmail-id";
        private readonly string ulEmail = "//ul[@id=\"tmail-data\"]/li";
        private readonly string ulEmailFrom = "//ul[@id=\"tmail-data\"]//div[@class=\"name\"]";
        private readonly string ulEmailTitle = "//ul[@id=\"tmail-data\"]//div[@class=\"subject\"]";
        private readonly string ulEmailBody = "//ul[@id=\"tmail-data\"]//div[@class=\"body\"]";
        
        private readonly string ulEmailItemTime = "//div[contains(@id,\"tmail-email-body-content\")]//div[contains(@class,\"tmail-email-time\")]";
        private readonly string ulEmailItemFrom = "//div[contains(@id,\"tmail-email-body-content\")]//div[contains(@class,\"tmail-email-sender\")]";
        private readonly string ulEmailItemTitle = "//div[contains(@id,\"tmail-email-body-content\")]//div[@class=\"tmail-email-title\"]";
        private readonly string ulEmailItemBody = "//div[contains(@id,\"tmail-email-body-content\")]//div[@class=\"tmail-email-body-content\"]";
        
        private readonly string aActiveEmail = "//*[contains(text(),\"Verification link\")]" +
            "//a[@target=\"_blank\" and text()=\"Confirm my account\"]";
        private readonly string aResetPassword = "//*[contains(text(),\"confirm the password change\")]" +
            "//a[@target=\"_blank\" and text()=\"click here\"]";
        #endregion

        public SpamboxEmailPage(IWebDriver driver) : base(driver, SPAMBOX_EMAILPAGE_URL, null) { }

        #region Properties
        public BaseWebObject UlEmail => FindWebElement(ulEmail, true);
        public IList<BaseWebObject> UlEmails => FindWebElements(ulEmail);
        public BaseWebObject UlEmailFrom => FindWebElement(ulEmailFrom, true);
        public BaseWebObject UlEmailTitle => FindWebElement(ulEmailTitle, true);
        public BaseWebObject UlEmailBody => FindWebElement(ulEmailBody, true);

        public BaseWebObject UlEmailItemTime => FindWebElement(ulEmailItemTime, true);
        public BaseWebObject UlEmailItemFrom => FindWebElement(ulEmailItemFrom, true);
        public BaseWebObject UlEmailItemTitle => FindWebElement(ulEmailItemTitle, true);
        public BaseWebObject UlEmailItemBody => FindWebElement(ulEmailItemBody, true);
        
        public BaseWebObject AActiveEmail => FindWebElement(aActiveEmail, true);
        public BaseWebObject AResetPassword => FindWebElement(aResetPassword, true);
        public BaseWebObject DivEmailTitle => FindWebElement(divEmailTitle, true);
        public string EmailDomainName => SpamboxHomePage.SPAMBOX_XYZ_EMAIL_DOMAIN;
        #endregion

        #region Methods
        /// <summary>
        /// Verify that the username is displayed on email title is the exact email address
        /// </summary>
        /// <param name="emailAddress">Email address to check</param>
        public void VerifyEmailLogInSuccess(string emailAddress)
        {
            Assert.IsTrue(DivEmailTitle.Text.Contains(emailAddress));
        }

        /// <summary>
        /// Get title of the latest (first) email from the inbox frame
        /// </summary>
        /// <returns>First email item title</returns>
        public string GetFirstEmailItemTitle()
        {
            string fistEmailTitle = "";

            try
            {
                ThreadUtils.SleepShortTime();
                fistEmailTitle = UlEmailTitle.Text;
            }
            catch (Exception ex)
            {
                TestContext.Error.WriteLine("GetFirstEmailItemTitle ERR: {0}", ex.Message);
            }

            return fistEmailTitle;
        }

        /// <summary>
        /// Get time of the latest (first) email from the inbox frame
        /// </summary>
        /// <returns>First email item time</returns>
        public string GetFirstEmailItemTime()
        {
            string fistEmailTime = "";

            try
            {
                ThreadUtils.SleepShortTime();
                fistEmailTime = UlEmailItemTime.Text;
            }
            catch (Exception ex)
            {
                TestContext.Error.WriteLine("GetFirstEmailItemTime ERR: {0}", ex.Message);
            }

            return fistEmailTime;
        }

        /// <summary>
        /// Click the first email item to open its content
        /// </summary>
        public void ClickOpenFirstEmailItem()
        {
            try
            {
                ThreadUtils.SleepMediumTime();
                UlEmail.Click();
                ThreadUtils.SleepShortTime();
            }
            catch (Exception ex)
            {
                TestContext.Error.WriteLine("ClickOpenFirstEmailItem ERR: {0}", ex.Message);
            }

        }

        /// <summary>
        /// Click the activate link from the email to activate user
        /// </summary>
        public void ClickActivateAcountByEmail()
        {
            try
            {
                ThreadUtils.SleepShortTime();
                AActiveEmail.Click();
            }
            catch (Exception ex)
            {
                TestContext.Error.WriteLine("ClickActivateAcountByEmail ERR: {0}", ex.Message);
            }
        }

        /// <summary>
        /// Click reset password link to reset password for the account
        /// </summary>
        public void ClickResetPasswordLink()
        {
            try
            {
                ThreadUtils.SleepShortTime();
                AResetPassword.Click();
            }
            catch (Exception ex)
            {
                TestContext.Error.WriteLine("ClickResetPasswordLink ERR: {0}", ex.Message);
            }
        }

        /// <summary>
        /// Check if the reset password email just comes
        /// </summary>
        /// <returns>True if the email comes; otherwise, False</returns>
        public bool IsResetPasswordEmailCome()
        {
            bool isResetPasswordEmailFound = true;

            try
            {
                isResetPasswordEmailFound = IsPageContainsText(RESET_PASSWORD_EMAIL_TITLE);
            }
            catch (Exception ex)
            {
                TestContext.Error.WriteLine("IsResetPasswordEmailCome ERR: {0}", ex.Message);
            }

            return isResetPasswordEmailFound;
        }

        /// <summary>
        /// Open activate email and click the Activate link for the account
        /// </summary>
        /// <param name="emailEmailPage">Page object to email items page</param>
        public void ActivateAccountByEmail(string expectedEmailTitle)
        {
            string emailMsgTitle = GetFirstEmailItemTitle();

            Assert.IsTrue(emailMsgTitle.Contains(expectedEmailTitle),
                "New account register message not correct {0} not {1}.", emailMsgTitle, expectedEmailTitle);

            ClickOpenFirstEmailItem();

            ThreadUtils.SleepShortTime();

            ClickActivateAcountByEmail();
        }
        #endregion
    }
}
