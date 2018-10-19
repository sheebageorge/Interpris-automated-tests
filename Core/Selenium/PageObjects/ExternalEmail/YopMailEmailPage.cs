using Automation.UI.Core.CommonUtilities;
using NUnit.Framework;
using OpenQA.Selenium;
using System;

namespace Automation.UI.Core.Selenium.ExternalMail
{
    /// <summary>
    /// Wrapper class of Yop Mail email page
    /// </summary>
    public class YopMailEmailPage : BasePage, IEmailEmailPage
    {
        public const string YOPMAIL_EMAILPAGE_URL = "http://www.yopmail.com/en/";
        public const string YOPMAIL_EMAIL_DOMAIN_NAME = "@yopmail.com";

        public const string RESET_PASSWORD_EMAIL_TITLE = "You have submitted a password change request!";
        public const string NO_MAIL_FOR_MSG_TITLE = "No mail for";

        #region UI Objects
        private readonly string divEmailTitle = "xpath=//div[@id=\"webmailhaut\"]" +
            "/table[@class=\"wc\"]/tbody//div[contains(@class,\"bname\")]";
        #endregion

        #region Inbox iframe - UI Objects
        private readonly string iframeInbox = "id=ifinbox";
        private readonly string divMessageInbox = "id=msginbox";
        private readonly string spanFirstEmailItemTitle = "xpath=//div[@class=\"m\"]" +
            "//span[@class=\"lmf\"]";
        #endregion

        #region Email content iframe - UI Objects
        private readonly string iframeEmail = "id=ifmail";
        private readonly string aActivateByEmail = "xpath=//div[@id=\"mailmillieu\"]" +
            "//a[starts-with(@href,\"https://qsrulurudev.eu.auth0.com/lo/verify_email?ticket=\")]";
        private readonly string aResetPasswordByEmail = "//div[@id=\"mailmillieu\"]" +
            "//a[starts-with(@href,\"https://qsrulurudev.eu.auth0.com/lo/reset?ticket=\")]";
        private readonly string divResetPasswordMailTitle = "//div[@id=\"mailmillieu\"]" +
            "//div[contains(text(),\"" + RESET_PASSWORD_EMAIL_TITLE + "\")]";
        #endregion

        public YopMailEmailPage(IWebDriver driver) : base(driver, YOPMAIL_EMAILPAGE_URL, null) { }

        #region interface properties
        public string EmailDomainName => YOPMAIL_EMAIL_DOMAIN_NAME;
        #endregion

        #region Properties
        public BaseWebObject DivEmailTitle => FindWebElement(divEmailTitle, true);
        public BaseWebObject IframeInbox => FindWebElement(iframeInbox, true);
        public BaseWebObject DivMessageInbox => FindWebElement(divMessageInbox, true);
        public BaseWebObject SpanFirstEmailItemTitle => FindWebElement(spanFirstEmailItemTitle, true);
        public BaseWebObject IframeEmail => FindWebElement(iframeEmail, true);
        public BaseWebObject AActivateByEmail => FindWebElement(aActivateByEmail, true);
        public BaseWebObject AResetPasswordByEmail => FindWebElement(aResetPasswordByEmail, true);
        public BaseWebObject DivResetPasswordMailTitle => FindWebElement(divResetPasswordMailTitle, true);
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

            // switch to iframe Inbox first
            Driver.SwitchTo().Frame(IframeInbox.WebElement);

            try
            {
                fistEmailTitle = SpanFirstEmailItemTitle.Text;
            }
            catch (Exception ex)
            {
                TestContext.Error.WriteLine("GetFirstEmailItemTitle ERR: {0}", ex.Message);
            }

            // switch back to the main page
            Driver.SwitchTo().DefaultContent();

            return fistEmailTitle;
        }

        /// <summary>
        /// Get time of the latest (first) email from the inbox frame
        /// </summary>
        /// <returns>First email item time</returns>
        public string GetFirstEmailItemTime()
        {
            return "";
        }

        /// <summary>
        /// Click the first email item to open its content
        /// </summary>
        public void ClickOpenFirstEmailItem()
        {
            // switch to iframe Inbox first
            Driver.SwitchTo().Frame(IframeInbox.WebElement);

            try
            {
                if (!IsPageContainsText(NO_MAIL_FOR_MSG_TITLE))
                {
                    SpanFirstEmailItemTitle.Click();

                    ThreadUtils.SleepShortTime();
                }
            }
            catch (Exception ex)
            {
                TestContext.Error.WriteLine("ClickOpenFirstEmailItem ERR: {0}", ex.Message);
            }

            // switch back to the main page
            Driver.SwitchTo().DefaultContent();
        }

        /// <summary>
        /// Click the activate link from the email to activate user
        /// </summary>
        public void ClickActivateAcountByEmail()
        {
            // switch to iframe Inbox first
            Driver.SwitchTo().Frame(IframeEmail.WebElement);

            try
            {
                AActivateByEmail.Click();
            }
            catch (Exception ex)
            {
                TestContext.Error.WriteLine("ClickActivateAcountByEmail ERR: {0}", ex.Message);
            }

            // switch back to the main page
            Driver.SwitchTo().DefaultContent();
        }

        /// <summary>
        /// Click reset password link to reset password for the account
        /// </summary>
        public void ClickResetPasswordLink()
        {
            // switch to iframe email first
            Driver.SwitchTo().Frame(IframeEmail.WebElement);

            try
            {
                if (DivResetPasswordMailTitle.IsVisible)
                {
                    AResetPasswordByEmail.Click();
                }
            }
            catch (Exception ex)
            {
                TestContext.Error.WriteLine("ClickResetPasswordLink ERR: {0}", ex.Message);
            }

            // switch back to the main page
            Driver.SwitchTo().DefaultContent();
        }

        /// <summary>
        /// Check if the reset password email just comes
        /// </summary>
        /// <returns>True if the email comes; otherwise, False</returns>
        public bool IsResetPasswordEmailCome()
        {
            bool isResetPasswordEmailFound = true;

            // switch to iframe email first
            Driver.SwitchTo().Frame(IframeEmail.WebElement);

            try
            {
                isResetPasswordEmailFound = IsPageContainsText(RESET_PASSWORD_EMAIL_TITLE);
            }
            catch (Exception ex)
            {
                TestContext.Error.WriteLine("IsResetPasswordEmailCome ERR: {0}", ex.Message);
            }

            // switch back to the main page
            Driver.SwitchTo().DefaultContent();

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
