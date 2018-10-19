using Automation.UI.Core.CommonUtilities;
using Automation.UI.Core.TestBase;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Collections.Generic;

namespace Automation.UI.Core.Selenium.PageObjects.Interpris.Transcription
{
    /// <summary>
    /// contains all elements and actions for View All Files Page
    /// </summary>
    public class ViewAllFilesPage : LandingPage
    {
        public const string TAB_TEXT = "All files";

        public const string TRANSCRIBED_FILE_ICON_CLASS_VALUE = "qsr-icon-file-alt";
        public const string UPLOADED_FILE_ICON_CLASS_VALUE = "qsr-icon-file-audio";

        public const string FILE_ITEM_BOX_EXPECTED_BORDER_COLOR_RGB = "rgb(74, 144, 226)";
        public const string FILE_ITEM_BOX_EXPECTED_BORDER_COLOR_HEX = "#4a90e2";
        public const string FILE_ITEM_BOX_EXPECTED_BORDER_STYLE = "solid";
        public const string FILE_ITEM_BOX_EXPECTED_BORDER_WIDTH = "1px";

        public const string BTN_LABEL_OPEN_TRANSCRIPT = "Open";

        public const int MAX_WAIT_FOR_TRANSCRIBING = 60;

        #region UI objects
        private readonly string divFileItemContainer =
            "//div[contains(@class,\"ant-tabs-tabpane-active\")]" +
            "//div[starts-with(@class,\"styles__LandingPageContentWrapper-\")]";
        private readonly string divFileItemActionButton =
            "//div[@id=\"app\"]//div[contains(@class,\"ant-tabs-tabpane-active\")]" +
            "//div[starts-with(@class,\"styles__LandingPageContentWrapper-\")]" +
            "//div[contains(@class,\"action-btn\")]";
        #endregion

        public ViewAllFilesPage(IWebDriver driver, string baseURL) : base(driver, baseURL) { }

        #region Properties
        public BaseWebObject DivFileItemContainer => FindWebElement(divFileItemContainer);
        public BaseWebObject SpanFileItem(string filename) => FindWebElement(
            $"//*[@aria-hidden=\"false\"]//span[@class=\"file-info__name\" and .=\"{filename}\"]", true);
        public BaseWebObject DivFileItemBox(string filename) => FindWebElement(
            $"//*[@aria-hidden=\"false\"]//span[@class=\"file-info__name\" and .=\"{filename}\"]" +
            $"/ancestor::*[@role=\"presentation\"]/div[contains(@class,\"commonstyle__StyledTranscriptionFile-\")]", true);
        public IList<BaseWebObject> SpanFileItemIcons(string iconClassType) => FindWebElements(
            $"//*[@aria-hidden=\"false\"]//span[@class=\"file-info__name\"]" +
            $"/ancestor::*[@role=\"presentation\"]//div[@class=\"icon\"]" +
            $"/span[contains(@class,\"qsr-icon\") and contains(@class,\"{iconClassType}\")]");
        public IList<BaseWebObject> DivFileItemActionButtons => FindWebElements(divFileItemActionButton);
        #endregion

        #region Methods
        /// <summary>
        /// Check if the Upload tab is active & displayed
        /// </summary>
        /// <returns>True if the tab displayed; otherwise, False</returns>
        public bool IsTabActive()
        {
            return SpanActiveTabText.Text.Equals(TAB_TEXT) && TabAll.IsVisible;
        }

        /// <summary>
        /// Wait for the page loaded all the items
        /// </summary>
        /// <param name="MAX_WAIT">Max time to wait</param>
        public void WaitForItemListLoaded(int MAX_WAIT = 50)
        {
            ThreadUtils.SleepShortTime();

            int iCount = 0;
            while (string.IsNullOrEmpty(DivFileItemContainer.Text.Trim()) && iCount < MAX_WAIT)
            {
                iCount++;
                ThreadUtils.SleepVeryShortTime();
            }
        }

        /// <summary>
        /// Wait for the file disappears from the UI
        /// </summary>
        /// <param name="fileName">File name to wait for</param>
        /// <param name="MAX_WAIT">Max time to wait</param>
        public void WaitForTheFileDisappear(string fileName, int MAX_WAIT = 50)
        {
            ThreadUtils.SleepShortTime();

            int iCount = 0;
            while (DivFileItemContainer.Text.Trim().Contains(fileName) && iCount < MAX_WAIT)
            {
                iCount++;
                ThreadUtils.SleepVeryShortTime();

                Refresh();
                ThreadUtils.SleepShortTime();
            }
        }

        /// <summary>
        /// Wait for the file appears from the UI
        /// </summary>
        /// <param name="fileName">File name to wait for</param>
        /// <param name="MAX_WAIT">Max time to wait</param>
        public void WaitForTheFileAppear(string fileName, int MAX_WAIT = 50)
        {
            ThreadUtils.SleepShortTime();

            int iCount = 0;
            while (!SpanFileItem(fileName).IsVisible &&
                iCount < MAX_WAIT)
            {
                iCount++;
                ThreadUtils.SleepVeryShortTime();

                Refresh();
                ThreadUtils.SleepShortTime();

                if (DivFileItemContainer.Text.Trim().Contains(fileName.Substring(0, fileName.LastIndexOf('.'))))
                    return;
            }
        }

        /// <summary>
        /// Check if the file item selected & highlighted
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public bool IsSelectedItemHighlighted(string fileName)
        {
            BaseWebObject fileItemBox = DivFileItemBox(fileName);

            TestContext.Out.WriteLine("Browser: {0}", WebUITestBaseClass.Browser);

            switch (WebUITestBaseClass.Browser)
            {
                case BrowserConstants.BROWSER_CHROME:
                    TestContext.Out.WriteLine("IsSelectedItemHighlighted: {0}/{1}/{2}",
                        fileItemBox.GetCssValue("border-color"),
                        fileItemBox.GetCssValue("border-style"),
                        fileItemBox.GetCssValue("border-width"));

                    return (fileItemBox.GetCssValue("border-color").Equals(FILE_ITEM_BOX_EXPECTED_BORDER_COLOR_RGB) &&
                        fileItemBox.GetCssValue("border-style").Equals(FILE_ITEM_BOX_EXPECTED_BORDER_STYLE) &&
                        fileItemBox.GetCssValue("border-width").Equals(FILE_ITEM_BOX_EXPECTED_BORDER_WIDTH));
                case BrowserConstants.BROWSER_FIREFOX:
                case BrowserConstants.BROWSER_EDGE:
                    TestContext.Out.WriteLine("IsSelectedItemHighlighted: {0}/{1}/{2} {3}/{4}/{5} {6}/{7}/{8} {9}/{10}/{11}",
                        fileItemBox.GetCssValue("border-bottom-color"),
                        fileItemBox.GetCssValue("border-bottom-style"),
                        fileItemBox.GetCssValue("border-bottom-width"),
                        fileItemBox.GetCssValue("border-top-color"),
                        fileItemBox.GetCssValue("border-top-style"),
                        fileItemBox.GetCssValue("border-top-width"),
                        fileItemBox.GetCssValue("border-left-color"),
                        fileItemBox.GetCssValue("border-left-style"),
                        fileItemBox.GetCssValue("border-left-width"),
                        fileItemBox.GetCssValue("border-right-color"),
                        fileItemBox.GetCssValue("border-right-style"),
                        fileItemBox.GetCssValue("border-right-width"));

                    return (fileItemBox.GetCssValue("border-bottom-color").Equals(FILE_ITEM_BOX_EXPECTED_BORDER_COLOR_RGB) &&
                        fileItemBox.GetCssValue("border-bottom-style").Equals(FILE_ITEM_BOX_EXPECTED_BORDER_STYLE) &&
                        fileItemBox.GetCssValue("border-bottom-width").Equals(FILE_ITEM_BOX_EXPECTED_BORDER_WIDTH) &&
                        fileItemBox.GetCssValue("border-top-color").Equals(FILE_ITEM_BOX_EXPECTED_BORDER_COLOR_RGB) &&
                        fileItemBox.GetCssValue("border-top-style").Equals(FILE_ITEM_BOX_EXPECTED_BORDER_STYLE) &&
                        fileItemBox.GetCssValue("border-top-width").Equals(FILE_ITEM_BOX_EXPECTED_BORDER_WIDTH) &&
                        fileItemBox.GetCssValue("border-left-color").Equals(FILE_ITEM_BOX_EXPECTED_BORDER_COLOR_RGB) &&
                        fileItemBox.GetCssValue("border-left-style").Equals(FILE_ITEM_BOX_EXPECTED_BORDER_STYLE) &&
                        fileItemBox.GetCssValue("border-left-width").Equals(FILE_ITEM_BOX_EXPECTED_BORDER_WIDTH) &&
                        fileItemBox.GetCssValue("border-right-color").Equals(FILE_ITEM_BOX_EXPECTED_BORDER_COLOR_RGB) &&
                        fileItemBox.GetCssValue("border-right-style").Equals(FILE_ITEM_BOX_EXPECTED_BORDER_STYLE) &&
                        fileItemBox.GetCssValue("border-right-width").Equals(FILE_ITEM_BOX_EXPECTED_BORDER_WIDTH));
                case BrowserConstants.BROWSER_IE:
                    TestContext.Out.WriteLine("IsSelectedItemHighlighted: {0}/{1}/{2}",
                        fileItemBox.GetCssValue("border-color"),
                        fileItemBox.GetCssValue("border-style"),
                        fileItemBox.GetCssValue("border-width"));

                    return (fileItemBox.GetCssValue("border-color").Equals(FILE_ITEM_BOX_EXPECTED_BORDER_COLOR_HEX) &&
                        fileItemBox.GetCssValue("border-style").Equals(FILE_ITEM_BOX_EXPECTED_BORDER_STYLE) &&
                        fileItemBox.GetCssValue("border-width").Equals(FILE_ITEM_BOX_EXPECTED_BORDER_WIDTH));
                default:
                    return false;
            }
        }

        /// <summary>
        /// Get the top Y position of the file list
        /// </summary>
        /// <param name="iconClassType">Type of icon (transcribed or uploaded)</param>
        /// <returns>Top Y position</returns>
        public int GetTopPosYFileItems(string iconClassType)
        {
            int posStart = int.MaxValue;

            foreach (BaseWebObject item in SpanFileItemIcons(iconClassType))
            {
                if (item.WebElement.Location.Y < posStart)
                {
                    posStart = item.WebElement.Location.Y;
                }
            }

            return posStart;
        }

        /// <summary>
        /// Get the bottom Y position of the file list
        /// </summary>
        /// <param name="iconClassType">Type of icon (transcribed or uploaded)</param>
        /// <returns>Bottom Y position</returns>
        public int GetBottomPosYFileItems(string iconClassType)
        {
            int posStart = 0;

            foreach (BaseWebObject item in SpanFileItemIcons(iconClassType))
            {
                if (item.WebElement.Location.Y > posStart)
                {
                    posStart = item.WebElement.Location.Y;
                }
            }

            return posStart;
        }

        /// <summary>
        /// Open the transcribed file when the text ready
        /// </summary>
        /// <param name="filename">File name to open</param>
        public void OpenTranscribedFile(string filename)
        {
            SpanFileItemIcon(filename).Click();
        }

        /// <summary>
        /// Open the transcribed file when the text ready
        /// </summary>
        /// <param name="filename">File name to open</param>
        public void OpenTranscribedFileByOpenLink(string filename)
        {
            ATranscribedFileOpenLink(filename).Click();
        }

        /// <summary>
        /// Get link of the transcribed file
        /// </summary>
        /// <param name="filename">File name to open</param>
        public string GetTranscribedFileHref(string filename)
        {
            return ATranscribedFileItem(filename).GetAttribute("href");
        }

        /// <summary>
        /// Get the id of the transcribed file
        /// </summary>
        /// <param name="filename"></param>
        /// <returns>Id of the transcribed file</returns>
        public string GetTranscribedFileID(string filename)
        {
            string transcribedFileHref = GetTranscribedFileHref(filename);
            string editorTagVal = "/editor/";
            int editorTagPos = transcribedFileHref.LastIndexOf(editorTagVal);

            if (editorTagPos >= 0)
            {
                return transcribedFileHref.Substring(editorTagPos + editorTagVal.Length);
            }

            return transcribedFileHref;
        }

        /// <summary>
        /// Open the transcribed file when the text ready
        /// </summary>
        /// <param name="filename">File name to open</param>
        public void OpenTranscribedFileCtrlClick(string filename)
        {
            ActionCtrlMouseClick(ATranscribedFileItem(filename));
        }

        /// <summary>
        /// Open the transcribed file when the text ready
        /// </summary>
        /// <param name="filename">File name to open</param>
        public void OpenTranscribedFileShiftClick(string filename)
        {
            ActionShiftMouseClick(ATranscribedFileItem(filename));
        }
        #endregion
    }
}