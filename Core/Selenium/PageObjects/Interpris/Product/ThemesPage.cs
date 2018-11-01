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
    /// Wrapper class of Theme Page
    /// contains all elements and actions for the Theme Page
    /// </summary>
    public class ThemesPage: LandingPage
    {
        public override string PageName { get; protected set; } = "Theme";

        #region UI Objects
        #endregion

        public ThemesPage(IWebDriver driver, string baseURL) : base(driver, baseURL) { }

        #region Properties
        #endregion

        #region Methods
        #endregion
    }
}
