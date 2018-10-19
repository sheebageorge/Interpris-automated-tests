using NUnit.Framework;
using OpenQA.Selenium;

namespace Automation.UI.Core.Selenium.PageObjects.Interpris.Platform
{
    /// <summary>
    /// Wrapper class of Sign Up Step Six Sub Page
    /// contains all elements and actions for Sign Up Step Six Sub Page
    /// this page is displayed after user click verify link in email
    /// </summary>
    public class SignUpStepSixSubPage : BasePage
    {
        #region Page Default
        public const string PAGE_TITLE = "Your email address has been verified.";
        public const string PAGE_TEXT_NOTIFICATION = "If you're using NVivo, you can go back to NVivo and log in there.";
        public const string PAGE_TEXT_THANKS = "Thank you for helping us keep NVivo Transcription secure.";
        public const string PAGE_TEXT_BACK_TO_LOG_IN = "Back to Log In";
        #endregion

        #region Page Objects
        private readonly string spanVerifyEmailIcon = "//span[contains(@class,\"qsr-icon-verify-email\"]";
        private readonly string divTitle = "//div[@class=\"title\"]";
        private readonly string divNotification = "//div[@class=\"notification\"]";
        private readonly string divThanks = "//div[@class=\"thanks\"]";
        private readonly string buttonBackToLogIn = "//button[@id=\"back-to-login-id\"]";
        #endregion

        public SignUpStepSixSubPage(IWebDriver driver, string baseURL = "") : base(driver, baseURL, "") { }

        #region Properties
        public BaseWebObject SpanVerifyEmailIcon => FindWebElement(spanVerifyEmailIcon, true);
        public BaseWebObject DivTitle => FindWebElement(divTitle, true);
        public BaseWebObject DivNotification => FindWebElement(divNotification, true);
        public BaseWebObject DivThanks => FindWebElement(divThanks, true);
        public BaseWebObject ButtonBackToLogIn => FindWebElement(buttonBackToLogIn, true);
        #endregion

        #region Methods
        /// <summary>
        /// Click go back to log in
        /// </summary>
        public void GoBackToLogin()
        {
            ButtonBackToLogIn.Click();
        }
        #endregion
    }
}
