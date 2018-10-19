using OpenQA.Selenium;

namespace Automation.UI.Core.Selenium.ExternalMail
{
    /// <summary>
    /// Wrapper class of Yop Mail homepage
    /// </summary>
    public class YopMailHomePage: BasePage, IEmailHomePage
    {
        public const string YOPMAIL_HOMEPAGE_URL = "http://www.yopmail.com/en/";

        #region UI Objects
        private readonly string inputCheckMail = "xpath=//form[@id=\"f\"]//input[@id=\"login\"]";
        #endregion

        public YopMailHomePage(IWebDriver driver) : base(driver, YOPMAIL_HOMEPAGE_URL, null) { }

        #region Properties
        public BaseWebObject InputCheckMail => FindWebElement(inputCheckMail, true);
        #endregion

        #region Methods
        /// <summary>
        /// Open email page of the input username
        /// </summary>
        /// <param name="username">Account username of the email</param>
        /// <param name="password">Account password of the email</param>
        public void OpenEmail(string username, string password)
        {
            Navigate();
            InputCheckMail.SendKeys(username, true);
        }
        #endregion
    }
}
