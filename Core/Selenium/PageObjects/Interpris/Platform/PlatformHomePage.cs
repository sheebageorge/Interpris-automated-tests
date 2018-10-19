using OpenQA.Selenium;

namespace Automation.UI.Core.Selenium.PageObjects.Interpris.Platform
{
    /// <summary>
    /// Wrapper class of Portal Home Page
    /// contains all elements and actions for Portal Home Page
    /// </summary>
    public class PlatformHomePage : BasePage
    {
        #region UI Objects
        private readonly string spanPageTitle = "//div[@id=\"app\"]//span[text()=\"Try me!\"]";
        #endregion

        public PlatformHomePage(IWebDriver driver, string baseURL) : base(driver, baseURL, PageConstants.PLATFORM_PAGE_HOME_URL) { }

        #region Properties
        public BaseWebObject SpanPageTitle => FindWebElement(spanPageTitle, true);
        #endregion

        #region Methods
        /// <summary>
        /// Check if the home page is active & displayed
        /// </summary>
        /// <returns>True if the page displayed; otherwise, False</returns>
        public bool IsPageVisible()
        {
            return SpanPageTitle.IsVisible;
        }
        #endregion
    }
}
