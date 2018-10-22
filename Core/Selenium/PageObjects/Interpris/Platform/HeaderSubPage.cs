using Automation.UI.Core.CommonUtilities;
using NUnit.Framework;
using OpenQA.Selenium;
using System;

namespace Automation.UI.Core.Selenium.PageObjects.Interpris.Platform
{
    /// <summary>
    /// Wrapper class of Header Sub Page
    /// contains all elements and actions for Header Sub Page
    /// </summary>
    public class HeaderSubPage : BasePage
    {
        public const string HOME_TEXT = "Home";

        #region Page Objects
        private readonly string homeScreen = "//div[@id=\"root\"]//div[@class=\"isoLeft\"]//*[text()=\"Home\"]";
        private readonly string spanMinuteRemain = "//div[@id=\"app\"]//div[contains(@class,\"header-content\")]" +
            "//div[contains(@class,\"wrapper-remaining-time\")]//span[@class=\"minutes\"]";
        private readonly string aAccountAvatar = "//*[@class='isoUser']";
        private readonly string btnGoToPortal = "//div[contains(@class,\"Menu-Panel\")]" +
            "//ul[@class=\"Menu-Panel__list-menu\"]/li[1]/a[@href=\"/portal\"]";
        private readonly string btnMyAccount =
            "//div[contains(@class,\"Menu-Panel\")]//li/a[.=\"My account\"]";
        private readonly string btnGetStartedGuide =
            "//div[contains(@class,\"Menu-Panel\")]//li/a[.=\"Get Started Guide\"]";
        private readonly string btnLogOut =
            "//div[contains(@class,\"isoUserDropdown\")]//a[.=\"Logout\"]";
        private readonly string divHeaderPanel = "//div[contains(@class,\"ant-layout-header\")]";
        private readonly string aNVivoLogo = "//div[contains(@class,\"header-content\")]//a[img[@class=\"logo\"]]";
        #endregion

        public HeaderSubPage(IWebDriver driver, string baseURL) : base(driver, baseURL, "") { }

        #region Properties
        public BaseWebObject TextHomeScreen => FindWebElement(homeScreen, true);
        public BaseWebObject SpanMinuteRemain => FindWebElement(spanMinuteRemain, true);
        public BaseWebObject AAccountAvatar => FindWebElement(aAccountAvatar, true);
        public BaseWebObject BtnGoToPortal => FindWebElement(btnGoToPortal, true);
        public BaseWebObject BtnMyAccount => FindWebElement(btnMyAccount, true);
        public BaseWebObject BtnGetStartedGuide => FindWebElement(btnGetStartedGuide, true);
        public BaseWebObject ButtonLogOut => FindWebElement(btnLogOut, true);
        public BaseWebObject DivHeaderPanel => FindWebElement(divHeaderPanel, true);
        public BaseWebObject ANVivoLogo => FindWebElement(aNVivoLogo, true);
        #endregion

        #region Methods
        /// <summary>
        /// Open portal page by top menu item
        /// </summary>
        public void OpenPortalPageByTopMenuItem()
        {
            AAccountAvatar.Click();

            ThreadUtils.SleepShortTime();

            BtnGoToPortal.Click();
        }

        /// <summary>
        /// Open Get Started Guide box
        /// </summary>
        public void OpenGetStartedGuideBox()
        {
            AAccountAvatar.Click();

            ThreadUtils.SleepShortTime();

            BtnGetStartedGuide.Click();
        }

        /// <summary>
        /// Open My Account Portal
        /// </summary>
        public void OpenMyAccount()
        {
            AAccountAvatar.Click();

            ThreadUtils.SleepShortTime();

            BtnMyAccount.Click();
        }

        /// <summary>
        /// Log out of system
        /// </summary>
        public void LogOut()
        {
            try
            {
                if (!string.IsNullOrEmpty(GetPageText()) &&
                    DivHeaderPanel.Text.Contains(HOME_TEXT))
                {
                    int iCount = 0;
                    int MAX_COUNT = 5;

                    while (iCount < MAX_COUNT)
                    {
                        iCount++;

                        AAccountAvatar.Click();
                        ThreadUtils.SleepShortTime();

                        if (ButtonLogOut.IsVisible)
                        {
                            break;
                        }
                    }

                    ButtonLogOut.Click();
                }
            }
            catch (Exception ex)
            {
                TestContext.Out.WriteLine("LogOut ERR: {0}", ex.Message);
            }
        }

        /// <summary>
        /// Wait for a text displayed on Header panel
        /// </summary>
        /// <param name="textValue"></param>
        /// <returns>True if the input text displayed on header</returns>
        public bool WaitForHeaderText(string textValue, int MAX_WAIT = 50)
        {
            int i = 0;

            while(i < MAX_WAIT)
            {
                i++;

                if (DivHeaderPanel.Text.Contains(HOME_TEXT))
                {
                    return true;
                }

                ThreadUtils.SleepVeryShortTime();
            }

            return false;
        }

        /// <summary>
        /// Wait for the Allow Cookies panel appear
        /// </summary>
        /// <param name="loginPage"></param>
        /// <param name="MAX_WAIT"></param>
        /// <returns></returns>
        public bool WaitForAllowCookiesPanel(LoginPage loginPage, int MAX_WAIT = 100)
        {
            int i = 0;

            while (i < MAX_WAIT)
            {
                i++;

                // close Cookies confirm box
                if (loginPage.DivWrapperLayoutChildren.Count > LoginPage.MAIN_LAYOUT_DIV_TAG_COUNT &&
                    loginPage.DivWrapperLayoutFooter.Text.Contains(LoginPage.ALLOW_COOKIES_TEXT))
                {
                    return true;
                }

                ThreadUtils.SleepVeryShortTime();
            }

            return false;
        }

        /// <summary>
        /// Wait and close Allow Cookies panel
        /// </summary>
        /// <param name="loginPage"></param>
        /// <param name="MAX_WAIT"></param>
        public void WaitToCloseAllowCookiesPanel(LoginPage loginPage, int MAX_WAIT = 100)
        {
            int i = 0;

            while (i < MAX_WAIT)
            {
                i++;

                // close Cookies confirm box
                if (loginPage.DivWrapperLayoutChildren.Count > LoginPage.MAIN_LAYOUT_DIV_TAG_COUNT &&
                    loginPage.DivWrapperLayoutFooter.Text.Contains(LoginPage.ALLOW_COOKIES_TEXT))
                {
                    loginPage.BtnAllowCookies.Click();
                    ThreadUtils.SleepShortTime();

                    return;
                }

                ThreadUtils.SleepVeryShortTime();
            }
        }

        /// <summary>
        /// Log in the system
        /// </summary>
        /// <param name="username">Username to login</param>
        /// <param name="password">Password to login</param>
        public void LogIn(string username, string password)
        {
            // open landing page
            Navigate();
            ThreadUtils.SleepShortTime();

            if (WaitForHeaderText(HOME_TEXT))
            {
                LogOut();
                ThreadUtils.SleepShortTime();
            }

            // Verify sign in successfully with the activated account
            LoginPage loginPage = new LoginPage(Driver, BaseURL);

            // input sign in
            loginPage.InputLoginInfo(username, password);
            ThreadUtils.SleepShortTime();
            
            // try to close Allow Cookies message 
            //WaitToCloseAllowCookiesPanel(loginPage);
        }

        /// <summary>
        /// Log in the system
        /// </summary>
        public string GetRemainingMinutes()
        {
            return SpanMinuteRemain.Text;
        }
        #endregion
    }
}
