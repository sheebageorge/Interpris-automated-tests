using Automation.UI.Core.CommonUtilities;
using Automation.UI.Core.Selenium;
using OpenQA.Selenium;

namespace Automation.UI.Core.Selenium.QsrUluruAccountAuthenPages
{
    public class ChangePasswordPage: BasePage
    {
        #region UI objects
        private readonly string inputNewPassword = "//div[@id=\"change-password-widget-container\"]" +
            "//form[@class=\"auth0-lock-widget\"]//input[@name=\"password\" and @placeholder=\"your new password\"]";
        private readonly string inputConfirmNewPassword = "//div[@id=\"change-password-widget-container\"]" +
            "//form[@class=\"auth0-lock-widget\"]//input[@name=\"password\" and @placeholder=\"confirm your new password\"]";
        private readonly string btnSubmit = "//div[@id=\"change-password-widget-container\"]" +
            "//form[@class=\"auth0-lock-widget\"]//button[@class=\"auth0-lock-submit\"]";
        private readonly string pChangePasswordConfirmMsg = "//div[@id=\"change-password-widget-container\"]" +
            "//form[@class=\"auth0-lock-widget\"]//p[text()=\"Your password has been reset successfully.\"]";
        #endregion

        public ChangePasswordPage(IWebDriver driver) : base(driver, "https://qsrulurudev.eu.auth0.com", null) { }

        #region Properties
        public BaseWebObject InputNewPassword => FindWebElement(inputNewPassword, true);
        public BaseWebObject InputConfirmNewPassword => FindWebElement(inputConfirmNewPassword, true);
        public BaseWebObject PChangePasswordConfirmMsg => FindWebElement(pChangePasswordConfirmMsg, true);
        public BaseWebObject BtnSubmit => FindWebElement(btnSubmit, true);
        #endregion

        #region Methods
        /// <summary>
        /// Fill new password and confirm new password
        /// Click submit
        /// </summary>
        /// <param name="newPassword">New password</param>
        public bool FillInChangePasswordInfor(string newPassword)
        {
            InputNewPassword.SendKeys(newPassword);
            InputConfirmNewPassword.SendKeys(newPassword);
            BtnSubmit.Click();

            ThreadUtils.SleepShortTime();

            return PChangePasswordConfirmMsg.IsVisible;
        }
        #endregion
    }
}
