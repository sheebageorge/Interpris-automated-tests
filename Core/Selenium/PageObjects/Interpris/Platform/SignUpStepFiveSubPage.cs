using Automation.UI.Core.CommonUtilities;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Automation.UI.Core.Selenium.PageObjects.Interpris.Platform
{
    /// <summary>
    /// Wrapper class of Sign Up Step Five Sub Page
    /// contains all elements and actions for Sign Up Step Five Sub Page
    /// this page is to alert user to verify email before log in
    /// </summary>
    public class SignUpStepFiveSubPage : BasePage
    {
        #region Page Default
        public const string PAGE_TITLE = "Verify it's you.";
        public const string PAGE_TEXT_NOTIFICATION = "Just to make sure  it's really you, we've send you an email with a verification link.";
        public const string PAGE_TEXT_THANKS = "Thank you for helping us keep NVivo Transcription secure.";
        public const string PAGE_TEXT_BACK_TO_LOG_IN = "Back to Log In";
        public const string PAGE_TEXT_RESEND_EMAIL = "Click here to resend";
        public const string PAGE_TEXT_RESEND_EMAIL_ALERT = "Email verification has been sent again.";
        #endregion

        #region Page Objects
        private readonly string spanVerifyEmailIcon = "//span[contains(@class,\"qsr-icon-verify-email\"]";
        private readonly string divTitle = "//div[@class=\"title\"]";
        private readonly string divNotification = "//div[@class=\"notification\"]";
        private readonly string divThanks = "//div[@class=\"thanks\"]";
        private readonly string buttonBackToLogIn = "//button[@id=\"verify-email-go-to-login\"]";
        private readonly string buttonResendEmail = "//button[@id=\"verify-email-resend-email\"]";
        #endregion

        public SignUpStepFiveSubPage(IWebDriver driver, string baseURL = "") : base(driver, baseURL, "") { }

        #region Properties
        public BaseWebObject SpanVerifyEmailIcon => FindWebElement(spanVerifyEmailIcon, true);
        public BaseWebObject DivTitle => FindWebElement(divTitle, true);
        public BaseWebObject DivNotification => FindWebElement(divNotification, true);
        public BaseWebObject DivThanks => FindWebElement(divThanks, true);
        public BaseWebObject ButtonBackToLogIn => FindWebElement(buttonBackToLogIn, true);
        public BaseWebObject ButtonResendEmail => FindWebElement(buttonResendEmail, true);
        #endregion

        #region Methods
        /// <summary>
        /// Click go back to log in
        /// </summary>
        public void GoBackToLogin()
        {
            ButtonBackToLogIn.Click();
        }

        /// <summary>
        /// Click resend email
        /// Accept alert dialog
        /// </summary>
        public void ResendEmail()
        {
            ButtonResendEmail.Click();
            ThreadUtils.SleepMediumTime();
            IAlert alert = Driver.SwitchTo().Alert();
            alert.Accept();
        }
        #endregion
    }
}
