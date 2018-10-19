using Automation.UI.Core.CommonUtilities;
using OpenQA.Selenium;
using System.Collections.Generic;

namespace Automation.UI.Core.Selenium.PageObjects.Interpris.Platform
{
    /// <summary>
    /// Wrapper class of Login (Sign In/Sign Up) Page
    /// contains all elements and actions for Login Page
    /// </summary>
    public class LoginPage : BasePage
    {
        public const string PAGE_TITLE = "Interpris 2";

        public const string TAB_SIGNIN = "Log In";

        public const string ALLOW_COOKIES_TEXT = "Allow cookies";
        public const int MAIN_LAYOUT_DIV_TAG_COUNT = 3;

        public const string ERR_MSG_VERIFY_EMAIL_BEFORE_LOGGING_IN = "PLEASE VERIFY YOUR EMAIL BEFORE LOGGING IN.";
        public const string ERR_MSG_SIGN_IN_INVALID_EMAIL_OR_PASSWORD = "Wrong email or password.";
        public const string ERR_MSG_FF_SIGN_IN_INVALID_EMAIL_OR_PASSWORD =
            "We're sorry, something went wrong when attempting to log in.";
        public const string ERR_MSG_USERNAME_EMPTY = "Can't be blank";
        public const string ERR_MSG_PASSWORD_EMPTY = "Can't be blank";
        public const string ERR_MSG_ACCOUNT_MULTI_LOGIN_BLOCKED = "Your account has been blocked after multiple consecutive login attempts.";
        public const string ERR_MSG_USER_EXISTING = "The user already exists.";

        public const string ERR_MSG_GC_INVALID_EMAIL_TYPE_1 = "A part followed by '@' should not contain the symbol";
        public const string ERR_MSG_GC_INVALID_EMAIL_TYPE_2 = "Please include an '@' in the email address.";
        public const string ERR_MSG_FF_INVALID_EMAIL = "Please enter an email address.";
        public const string ERR_MSG_IE_INVALID_EMAIL = "You must enter a valid email address";

        public const string INFOR_MSG_RESET_PASSWORD_SENT_MAIL = "We've just sent you an email to reset your password.";

        #region UI Objects
        private readonly string divTabContainer = 
            "//form[@class=\"auth0-lock-widget\"]//ul[@class=\"auth0-lock-tabs\"]";
        #region Used remove later
        private readonly string aLogInTab =
            "//form[@class=\"auth0-lock-widget\"]//ul[@class=\"auth0-lock-tabs\"]//*[text()=\"Log In\"]";
        private readonly string aSignUpTab =
            "//form[@class=\"auth0-lock-widget\"]//ul[@class=\"auth0-lock-tabs\"]//*[text()=\"Sign Up\"]";
        private readonly string inputEmail =
            "//form[@class=\"auth0-lock-widget\"]//input[@name=\"email\"]";
        private readonly string inputPassword =
            "//form[@class=\"auth0-lock-widget\"]//input[@name=\"password\"]";
        private readonly string divShowPassword =
            "//form[@class=\"auth0-lock-widget\"]//div[@class=\"auth0-lock-show-password\"]";
        private readonly string spanLogIn =
            "//form[@class=\"auth0-lock-widget\"]//button[@class=\"auth0-lock-submit\"]/span[text()=\"Log In\"]";
        #endregion
        private readonly string buttonSignUp =
            "//form[@class=\"auth0-lock-widget\"]//button[@class=\"auth0-lock-submit\" and ./span[text()=\"Create my Account\"]]";

        private readonly string spanErrorMessage =
            "//form[@class=\"auth0-lock-widget\"]//div[@class=\"auth0-global-message auth0-global-message-error\"]" +
            "/span[@class=\"animated fadeInUp\"]/span";

        private readonly string spanInforMessage =
            "//form[@class=\"auth0-lock-widget\"]//div[@class=\"auth0-global-message auth0-global-message-success\"]" +
            "/span[@class=\"animated fadeInUp\"]/span";

        private readonly string spanUsernameErrorMessage =
            "//form[@class=\"auth0-lock-widget\"]//div[@class=\"auth0-lock-input-block auth0-lock-input-email auth0-lock-error\"]" +
            "//div[@class=\"auth0-lock-error-msg\"]/span";
        private readonly string spanPasswordErrorMessage =
            "//form[@class=\"auth0-lock-widget\"]//div[@class=\"auth0-lock-input-block auth0-lock-input-password auth0-lock-error\"]" +
            "//div[@class=\"auth0-lock-error-msg\"]/span";

        private readonly string aResetPassword =
            "//form[@class=\"auth0-lock-widget\"]//a[text()=\"Don't remember your password?\"]";
        private readonly string divResetPasswordBoxTitle =
            "//form[@class=\"auth0-lock-widget\"]//div[text()=\"Reset your password\"]";
        private readonly string spanSendMail =
            "//form[@class=\"auth0-lock-widget\"]//span[text()=\"Send email\"]";

        private readonly string inputPolicyAgreement = "//span[contains(@class,\"sign-up-terms-agreement\")]//input[@type=\"checkbox\"]";
        private readonly string aPolicyAgreement = "//span[contains(@class,\"sign-up-terms-agreement\")]//a[text()=\"Privacy Policy\"]";
        private readonly string aTermsAndConditionsAgreement = "//span[contains(@class,\"sign-up-terms-agreement\")]//a[text()=\"Terms & Conditions\"]";

        private readonly string btnBuyCredits = "//div[starts-with(@class,\"header-content\")]//span[text()=\"Buy Credits\"]";

        private readonly string cssValidateMessageInvalidEmail = "CssSelector=input:invalid";
        private readonly string loginPageCurrentTab = "//*[@class='auth0-lock-tabs-current']/a";

        private readonly string divWrapperLayoutChildren = "//div[starts-with(@class,\"layoutstyle-\") and " +
            "contains(@class,\"ant-layout\")]/div";
        private readonly string divWrapperLayoutFooter = "//div[starts-with(@class,\"layoutstyle-\") and " +
            "contains(@class,\"ant-layout\")]/div[starts-with(@class,\"Wrapper-\")]";
        private readonly string btnAllowCookies = "//div[starts-with(@class,\"layoutstyle-\") and contains(@class,\"ant-layout\")]" +
            "/div[starts-with(@class,\"Wrapper-\")]//button[./span[text()=\"Allow cookies\"]]";
        #endregion

        #region Password input remind dialog
        private readonly string divPasswordRemindDialog = "//div[@class=\"auth0-lock-password-strength animated fadeIn\"]";
        private readonly string spanPasswordRemindLength = "//div[@class=\"auth0-lock-password-strength animated fadeIn\"]" +
            "/ul/li/span[text()=\"At least 8 characters in length\"]";
        private readonly string spanPasswordRemindContains = "//div[@class=\"auth0-lock-password-strength animated fadeIn\"]" +
            "/ul/li/span[text()=\"Contain at least 3 of the following 4 types of characters:\"]";
        private readonly string spanPasswordRemindLowerCase = "//div[@class=\"auth0-lock-password-strength animated fadeIn\"]" +
            "/ul/li/ul/li/span[text()=\"Lower case letters (a-z)\"]";
        private readonly string spanPasswordRemindUpperCase = "//div[@class=\"auth0-lock-password-strength animated fadeIn\"]" +
            "/ul/li/ul/li/span[text()=\"Upper case letters (A-Z)\"]";
        private readonly string spanPasswordRemindNumber = "//div[@class=\"auth0-lock-password-strength animated fadeIn\"]" +
            "/ul/li/ul/li/span[text()=\"Numbers (i.e. 0-9)\"]";
        private readonly string spanSpecialChars = "//div[@class=\"auth0-lock-password-strength animated fadeIn\"]" +
            "/ul/li/ul/li/span[text()=\"Special characters (e.g. !@#$%^&*)\"]";
        #endregion

        public LoginPage(IWebDriver driver, string baseURL) : base(driver, baseURL, PageConstants.PLATFORM_PAGE_LOGIN_URL) { }

        #region Properties
        public BaseWebObject InputPolicyAgreement => FindWebElement(inputPolicyAgreement, true);
        public BaseWebObject APolicyAgreement => FindWebElement(aPolicyAgreement, true);
        public BaseWebObject ATermsAndConditionsAgreement => FindWebElement(aTermsAndConditionsAgreement, true);
        public BaseWebObject BtnBuyCredits => FindWebElement(btnBuyCredits, true);
        public BaseWebObject DivTabContainer => FindWebElement(divTabContainer, true);
        public BaseWebObject ALogInTab => FindWebElement(aLogInTab, true);
        public BaseWebObject ASignUpTab => FindWebElement(aSignUpTab, true);
        public BaseWebObject InputEmail => FindWebElement(inputEmail, true);
        public BaseWebObject DivShowPassword => FindWebElement(divShowPassword, true);
        public BaseWebObject InputPassword => FindWebElement(inputPassword, true);
        public BaseWebObject SpanLogIn => FindWebElement(spanLogIn, true);
        public BaseWebObject ButtonSignUp => FindWebElement(buttonSignUp, true);
        public BaseWebObject SpanErrorMessage => FindWebElement(spanErrorMessage, true);
        public BaseWebObject SpanInforMessage => FindWebElement(spanInforMessage, true);
        public BaseWebObject SpanUsernameErrorMessage => FindWebElement(spanUsernameErrorMessage, true);
        public BaseWebObject SpanPasswordErrorMessage => FindWebElement(spanPasswordErrorMessage, true);
        public BaseWebObject AResetPassword => FindWebElement(aResetPassword, true);
        public BaseWebObject DivResetPasswordBoxTitle => FindWebElement(divResetPasswordBoxTitle, true);
        public BaseWebObject SpanSendMail => FindWebElement(spanSendMail, true);
        public BaseWebObject CssValidateMessageInvalidEmail => FindWebElement(cssValidateMessageInvalidEmail, true);
        public BaseWebObject DivPasswordRemindDialog => FindWebElement(divPasswordRemindDialog, true);
        public BaseWebObject SpanPasswordRemindLength => FindWebElement(spanPasswordRemindLength, true);
        public BaseWebObject SpanPasswordRemindContains => FindWebElement(spanPasswordRemindContains, true);
        public BaseWebObject SpanPasswordRemindLowerCase => FindWebElement(spanPasswordRemindLowerCase, true);
        public BaseWebObject SpanPasswordRemindUpperCase => FindWebElement(spanPasswordRemindUpperCase, true);
        public BaseWebObject SpanPasswordRemindNumber => FindWebElement(spanPasswordRemindNumber, true);
        public BaseWebObject SpanSpecialChars => FindWebElement(spanSpecialChars, true);
        public BaseWebObject CurrentTabLogInPage => FindWebElement(loginPageCurrentTab, true);
        public IList<BaseWebObject> DivWrapperLayoutChildren => FindWebElements(divWrapperLayoutChildren);
        public BaseWebObject DivWrapperLayoutFooter => FindWebElement(divWrapperLayoutFooter, true);
        public BaseWebObject BtnAllowCookies => FindWebElement(btnAllowCookies, true);
        #endregion

        #region Methods
        /// <summary>
        /// Check if the login page is active & displayed
        /// </summary>
        /// <returns>True if the page displayed; otherwise, False</returns>
        public bool IsPageVisible()
        {
            return ALogInTab.IsVisible;
        }

        /// <summary>
        /// Click Log In tab link
        /// Fill in Username textbox
        /// Fill in Password textbox
        /// Click Log In (Submit) link from the bottom of the Log In dialog
        /// </summary>
        /// <param name="username">Input Username</param>
        /// <param name="password">Input Password</param>
        public void InputLoginInfo(string username, string password)
        {
            (new HeaderSubPage(Driver, BaseURL)).LogOut();
            ALogInTab.WaitAndClick();

            InputEmail.SendKeys(username);
            InputPassword.SendKeys(password);

            SpanLogIn.Click();
            ThreadUtils.SleepShortTime();

            /*if (DivWrapperLayoutChildren.Count > MAIN_LAYOUT_DIV_TAG_COUNT &&
                DivWrapperLayoutFooter.Text.Contains(ALLOW_COOKIES_TEXT))
            {
                BtnAllowCookies.Click();
                ThreadUtils.SleepShortTime();
            }*/
        }

        /// <summary>
        /// Click Sign Up tab link
        /// Fill in Username textbox
        /// Fill in Password textbox
        /// Click Sign Up (Submit) link from the bottom to Sign Up for the new account
        /// </summary>
        /// <param name="username">Input Username</param>
        /// <param name="password">Input Password</param>
        public void InputSignUpInfo(string username, string password)
        {
            (new HeaderSubPage(Driver, BaseURL)).LogOut();
            ASignUpTab.WaitAndClick();

            InputEmail.SendKeys(username);
            InputPassword.SendKeys(password);

            InputPolicyAgreement.Click();

            ButtonSignUp.Click();
        }

        /// <summary>
        /// Open reset password box
        /// Fill in email addresss
        /// Click Send Mail button
        /// </summary>
        /// <param name="username">Email of Account to reset password</param>
        public void InputResetPasswordInfo(string username)
        {
            (new HeaderSubPage(Driver, BaseURL)).LogOut();
            AResetPassword.WaitAndClick();

            // should sleep a little time for the reset password box displayed
            ThreadUtils.SleepShortTime();

            if (DivResetPasswordBoxTitle.IsVisible)
            {
                InputEmail.SendKeys(username);

                SpanSendMail.Click();
            }
        }

        /// <summary>
        /// Get error message from the Sign In/Sign Up dialog
        /// </summary>
        /// <returns>Error message content</returns>
        public string GetErrorMessage()
        {
            return SpanErrorMessage.Text;
        }


        /// <summary>
        /// Get inform message
        /// </summary>
        /// <returns></returns>
        public string GetInforMessage()
        {
            return SpanInforMessage.Text;
        }

        /// <summary>
        /// Get error message from username textbox
        /// </summary>
        /// <returns>Error message</returns>
        public string GetUsernameErrorMessage()
        {
            return SpanUsernameErrorMessage.Text;
        }

        /// <summary>
        /// Get error message from password textbox
        /// </summary>
        /// <returns>Error message</returns>
        public string GetPasswordErrorMessage()
        {
            return SpanPasswordErrorMessage.Text;
        }

        /// <summary>
        /// Get visible property of the Password Reminder dialog
        /// </summary>
        /// <returns></returns>
        public bool IsPasswordReminderDialogVisible()
        {
            return (DivPasswordRemindDialog.IsVisible &&
                SpanPasswordRemindLength.IsVisible &&
                SpanPasswordRemindContains.IsVisible &&
                SpanPasswordRemindLowerCase.IsVisible &&
                SpanPasswordRemindUpperCase.IsVisible &&
                SpanPasswordRemindNumber.IsVisible &&
                SpanSpecialChars.IsVisible);
        }

        /// <summary>
        /// Verify the validate message visible and contains the required message
        /// </summary>
        /// <param name="message">Messag to check</param>
        /// <returns>True if the message visible and displayed correctly</returns>
        public bool IsValidateMessageInvalidEmaiVisible(string message)
        {
            var errorMsgElement = CssValidateMessageInvalidEmail;

            return errorMsgElement.IsVisible &&
                errorMsgElement.WebElement.GetAttribute("validationMessage").Contains(message);
        }
        #endregion
    }
}
