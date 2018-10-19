using OpenQA.Selenium;

namespace Automation.UI.Core.Selenium.PageObjects.Interpris.Transcription
{
    public class TranscriptionHomePage : BasePage
    {
        #region UI Objects
        private readonly string spanPageTitle = "//div[@id=\"app\"]//span[text()=\"This is HomePage component!\"]";
        #endregion

        public TranscriptionHomePage(IWebDriver driver, string baseURL) : base(driver, baseURL, PageConstants.TRANSCRIPTION_PAGE_HOME_URL) { }

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
