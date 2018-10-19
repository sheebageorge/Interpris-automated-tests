using OpenQA.Selenium;

namespace Automation.UI.Core.Selenium.ExternalMail
{
    /// <summary>
    /// Wrapper class of Yop Mail homepage
    /// </summary>
    public class SpamboxHomePage : BasePage, IEmailHomePage
    {
        public const string SPAMBOX_HOMEPAGE_URL = "http://spambox.xyz/";
        public const string SPAMBOX_XYZ_EMAIL_DOMAIN = "@spambox.xyz";
        public const string DIRTMAIL_GA_EMAIL_DOMAIN = "@dirtmail.ga";
        public const string TEMPMAIL_WIN_EMAIL_DOMAIN = "@tempmail.win";

        #region UI Objects
        private readonly string inputEmail = "//input[@name=\"email\"]";
        private readonly string selectDoamin = "//select[@name=\"domain\"]";
        private readonly string aCreate = "//a[contains(@onclick, \"setNewID()\")]";
        private readonly string aRandom = "//a[contains(@onclick, \"createUser\")]";
        #endregion

        public SpamboxHomePage(IWebDriver driver) : base(driver, SPAMBOX_HOMEPAGE_URL, null) { }

        #region Properties
        public BaseWebObject InputEmail => FindWebElement(inputEmail, true);
        public BaseWebObject SelectDomain => FindWebElement(selectDoamin, true);
        public BaseWebObject ACreate => FindWebElement(aCreate, true);
        public BaseWebObject ARandom => FindWebElement(aRandom, true);
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
            InputEmail.SendKeys(username);
            ACreate.Click();
        }
        #endregion
    }
}
