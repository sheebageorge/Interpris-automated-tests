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
    /// Wrapper class of Views Page
    /// contains all elements and actions for the Views Page
    /// </summary>
    public class ViewsPage: BasePage
    {
        #region UI Objects
        private readonly string divPageName = "//div/div[@class=\"isoLeft\"]/span[text()=\"Views\"]";
        #endregion

        public ViewsPage(IWebDriver driver, string baseURL) : base(driver, baseURL, PageConstants.INTERPRIS_PAGE_VIEW_URL) { }

        #region Properties
        public BaseWebObject DivPageName => FindWebElement(divPageName, true);
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
        #endregion
    }
}
