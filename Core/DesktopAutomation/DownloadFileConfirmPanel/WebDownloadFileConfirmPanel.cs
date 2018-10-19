using Automation.UI.Core.Selenium;

namespace Automation.UI.Core.DesktopAutomation.DownloadFileConfirmPanel
{
    public abstract class WebDownloadFileConfirmPanel : DesktopWindowObject
    {
        #region Static method to get real WebSaveAsFileDialog object
        public static WebDownloadFileConfirmPanel GetDownloadFileConfirmPanel(string browserType)
        {
            switch (browserType)
            {
                case BrowserConstants.BROWSER_IE:
                    return new WebDownloadFileConfirmPanelIE();
                case BrowserConstants.BROWSER_EDGE:
                    return new WebDownloadFileConfirmPanelEdge();
                case BrowserConstants.BROWSER_CHROME:
                case BrowserConstants.BROWSER_FIREFOX:
                case BrowserConstants.BROWSER_SAFARI:
                default:
                    break;
            }

            return null;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Open Save As dialog to save the download file
        /// </summary>
        public abstract void OpenSaveAsDialog();
        #endregion
    }
}
