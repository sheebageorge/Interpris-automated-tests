using Automation.UI.Core.Selenium;

namespace Automation.UI.Core.DesktopAutomation.DownloadFileDialog
{
    public abstract class WebSaveAsFileDialog : DesktopWindowObject
    {
        #region Static method to get real WebSaveAsFileDialog object
        public static WebSaveAsFileDialog GetSaveAsDialog(string browserType)
        {
            switch (browserType)
            {
                case BrowserConstants.BROWSER_IE:
                    return new WebSaveAsFileDialogIE();
                case BrowserConstants.BROWSER_EDGE:
                    return new WebSaveAsFileDialogEdge();
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
        /// Select a file to upload
        /// </summary>
        /// <param name="folderPath">Folder path of the file</param>
        /// <param name="fileName">Name of file to upload</param>
        public abstract void SaveAFile(string folderPath, string fileName);
        #endregion
    }
}
