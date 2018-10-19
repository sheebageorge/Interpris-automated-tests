using Automation.UI.Core.Selenium;
using OpenQA.Selenium;

namespace Automation.UI.Core.Selenium.QsrUluruAccountAuthenPages
{
    /// <summary>
    /// Wrapper class of Email Verification Confirm Page
    /// contains all elements and actions for Email Verification Confirm Page
    /// </summary>
    public class EmailVerificationConfirmPage: BasePage
    {
        #region UI objects
        private readonly string spanEmailConfirmSuccess = "xpath=//form[@class=\"auth0-lock-widget\"]" +
            "//div[@class=\"auth0-lock-confirmation-content\"]" +
            "//span[contains(text(),\"Your email was verified.\")]";
        #endregion

        public EmailVerificationConfirmPage(IWebDriver driver) : base(driver, "https://qsrulurudev.eu.auth0.com", null) { }

        #region Properties
        public BaseWebObject SpanEmailConfirmSuccess => FindWebElement(spanEmailConfirmSuccess, true);
        #endregion

        #region Methods
        /// <summary>
        /// Check if email confirm message visible or not
        /// </summary>
        /// <returns></returns>
        public bool IsConfirmSuccessMessageVisible()
        {
            return SpanEmailConfirmSuccess.IsVisible;
        }
        #endregion
    }
}
