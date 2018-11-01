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
    public class DashboardPage: LandingPage
    {
        public override string PageName { get; protected set; } = "Dashboard";

        #region UI Objects
        #endregion

        public DashboardPage(IWebDriver driver, string baseURL) : base(driver, baseURL) { }

        #region Properties
        #endregion

        #region Methods
        #endregion
    }
}
