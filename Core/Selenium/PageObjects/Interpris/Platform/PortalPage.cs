using OpenQA.Selenium;
using System;

namespace Automation.UI.Core.Selenium.PageObjects.Interpris.Platform
{
    public class PortalPage : BasePage
    {
        #region Page Objects
        private readonly string btnBuyCredits = "//div[contains(@class,'ant-layout-content')]//button[*[text()='Buy Credits']]";
        private readonly string spanRemainingMinutes = "//div[contains(@class,'ant-layout-content')]//span[@class='minutes']";

        private readonly string divPlansAndPaymentsHeader = "//div[contains(@class,'styles__StyledHeaderOverlay') and descendant::span[contains(@class, 'icon-plans')]]";
        #endregion

        public PortalPage(IWebDriver driver, string baseURL) : base(driver, baseURL, PageConstants.PLATFORM_PAGE_PORTAL_URL) { }

        #region Properties
        public BaseWebObject ButtonBuyCredits => FindWebElement(btnBuyCredits, true);
        public BaseWebObject SpanRemainingMinutes => FindWebElement(spanRemainingMinutes, true);

        public BaseWebObject DivPlansAndPaymentsHeader => FindWebElement(divPlansAndPaymentsHeader);
        #endregion

        #region Methods
        /// <summary>
        /// Check if the portal page is active & displayed
        /// </summary>
        /// <returns>True if the page displayed; otherwise, False</returns>
        public bool IsPageVisible()
        {
            return ButtonBuyCredits.IsVisible & SpanRemainingMinutes.IsVisible;
        }

        /// <summary>
        /// Get remaining munites on content
        /// </summary>
        public int GetRemainingMinutes()
        {
            Int32.TryParse(SpanRemainingMinutes.Text, out int value);
            return value;
        }
        #endregion
    }
}