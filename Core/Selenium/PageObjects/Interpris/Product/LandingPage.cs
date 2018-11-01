using OpenQA.Selenium;
using System.Collections.Generic;
using NUnit.Framework;
using System;
using Automation.UI.Core.CommonUtilities;
using Automation.UI.Core.Selenium.PageObjects.Interpris.Platform;
using Automation.UI.Core.TestBase;

namespace Automation.UI.Core.Selenium.PageObjects.Interpris.Product
{
    /// <summary>
    /// Wrapper class of Landing (Home) Page
    /// contains all elements and actions for Landing Page
    /// </summary>
    public class LandingPage: BasePage
    {
        public const string PAGE_TITLE = "Interpris 2";

        #region UI Objects
        #endregion

        public LandingPage(IWebDriver driver, string baseURL) : base(driver, baseURL, PageConstants.INTERPRIS_PAGE_HOME_URL) { }

        public virtual string PageName { get; protected set; } = "";

        #region Properties
        public BaseWebObject DivPageName => FindWebElement($"//div/div[@class=\"isoLeft\"]/span[text()=\"{PageName}\"]", true);
        #endregion

        #region Methods
        /// <summary>
        /// Log out of system
        /// </summary>
        public void LogOut()
        {
            (new HeaderSubPage(Driver, InterprisTestBase.InterprisBaseURL)).LogOut();
        }

        /// <summary>
        /// Log in the system
        /// </summary>
        /// <param name="username">Username to login</param>
        /// <param name="password">Password to login</param>
        public void LogIn(string username, string password)
        {
            (new HeaderSubPage(Driver, InterprisTestBase.InterprisBaseURL)).LogIn(username, password);
        }

        public bool IsPageVisible()
        {
            return DivPageName.IsVisible;
        }
        #endregion
    }
}
