using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using Automation.UI.Core.CommonUtilities;
using System.Text.RegularExpressions;
using System.Linq;
using OpenQA.Selenium.Remote;
using NUnit.Framework;

namespace Automation.UI.Core.Selenium.PageObjects.Interpris.Transcription
{
    /// <summary>
    /// contains all elements and actions for Upload Page
    /// </summary>
    public class UploadPage : LandingPage
    {
        public const string TAB_TEXT = "Upload";
        public const string INFOR_MSG_UPLOAD_BIG_FILE = "Maximum file size allowed is 4GB";

        public const int MAX_WAIT_FOR_IN_PROGRESS_FILE = 900; // wait uploading file for 15 minutes
        public const int MAX_UPLOADING_CONCURRENT = 3; // max concurent files uploading

        #region UI Objects
        private const string BtnBrowseFile = "//*[@aria-hidden=\"false\"]//button[.='Browse files']";

        private readonly string divUploadedFilePanel = "//*[@aria-hidden=\"false\"]//div[starts-with(@class,\"styles__StyledListFile-\")]";

        private readonly string divUploadedItem = "//*[@aria-hidden=\"false\"]//div[starts-with(@class,\"styles__StyledListFile-\")]" +
            "//div[@role=\"presentation\"]";

        private readonly string spanUploadingText = "//*[@aria-hidden=\"false\"]//div[starts-with(@class,\"styles__StyledListFile-\")]" +
            "//div[@role=\"presentation\"]//span[text()=\"Uploading\"]";

        private readonly string spanItemStatus = "//*[@aria-hidden=\"false\"]//div[@role=\"presentation\"]//span[contains(@class,\"file-info__state\")]";

        private readonly string divUploadingProgressBar = "//*[@aria-hidden=\"false\"]//div[starts-with(@class,\"styles__StyledListFile-\")]" +
            "//div[@role=\"presentation\"]//div[@class=\"ant-progress-bg\"]";

        private readonly string inputUploadFile = "//input[@name=\"file\"]";
        #endregion

        public UploadPage(IWebDriver driver, string baseURL) : base(driver, baseURL) { }

        #region Properties
        public BaseWebObject ButtonBrowseFile => FindWebElement(BtnBrowseFile, true);
        public BaseWebObject DivUploadedFilePanel => FindWebElement(divUploadedFilePanel);
        public BaseWebObject SpanUploadingText => FindWebElement(spanUploadingText, true);
        public BaseWebObject DivUploadingProgressBar => FindWebElement(divUploadingProgressBar, true);
        public BaseWebObject InputUploadFile => FindWebElement(inputUploadFile);
        public IList<BaseWebObject> SpanUploadedItems => FindWebElements(
            "//*[@aria-hidden='false' and @role='tabpanel']//div[starts-with(@class,'styles__LandingPage')]" +
            "//div[@role='presentation']//span[contains(@class,\"qsr-icon\")]");
        public BaseWebObject SpanCancelUploadingFileItem(string filename) => FindWebElement(
            $"//*[@aria-hidden=\"false\"]//span[.=\"{filename}\"]/ancestor::*[@role=\"presentation\"]" +
            $"//button[contains(@class,\"ActionButton\")]/span[text()=\"Cancel\"]", true);
        #endregion

        #region Methods
        /// <summary>
        /// Check if the Upload tab is active & displayed
        /// </summary>
        /// <returns>True if the tab displayed; otherwise, False</returns>
        public bool IsTabActive()
        {
            return SpanActiveTabText.Text.Equals(TAB_TEXT) &&
                ButtonBrowseFile.IsVisible;
        }

        /// <summary>
        /// Check if uploaded file already existed on web
        /// </summary>
        /// <param name="filename">file name want to check</param>
        /// <returns>true or false</returns>
        public bool IsFileNameExistedOnUploadView(string filename)
        {
            return DivUploadedFilePanel.Text.Trim().Contains(filename);
        }

        /// <summary>
        /// Try to push file to input tag
        /// </summary>
        /// <param name="filePath"></param>
        public void PushFileToInputTag(IList<string> filePaths)
        {
            TestContext.Out.WriteLine("Push File to Input tag");

            try
            {
                // add file path to file detector for remote access
                LocalFileDetector detector = new LocalFileDetector();
                ((RemoteWebDriver)Driver).FileDetector = detector;

                // change the input file to visible
                ((IJavaScriptExecutor)Driver).ExecuteScript(
                    "arguments[0].setAttribute('style', 'display: visible;');",
                    InputUploadFile.WebElement);
                ThreadUtils.SleepShortTime();

                foreach (string filePath in filePaths)
                {
                    InputUploadFile.WebElement.SendKeys(filePath);
                    ThreadUtils.SleepVeryShortTime();
                }
            }
            finally
            {
                // change the input file to visible
                ((IJavaScriptExecutor)Driver).ExecuteScript(
                    "arguments[0].setAttribute('style', 'display: none;');",
                    InputUploadFile.WebElement);
                ThreadUtils.SleepShortTime();
            }
        }

        /// <summary>
        /// Try to upload the file to page
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="fileName"></param>
        public void UploadFile(string folderPath, string fileName)
        {
            // push to the file to remote
            PushFileToInputTag(new List<string>() { FileSystemUtils.GetFullFilePath(folderPath, fileName) });
        }

        /// <summary>
        /// Try to upload the list of files
        /// </summary>
        /// <param name="folderPath"></param>
        /// <param name="fileNameList"></param>
        public void UploadFiles(string folderPath, IList<string> fileNameList)
        {
            IList<string> fileFullPathList = new List<string>();
            foreach (string fileName in fileNameList)
            {
                fileFullPathList.Add(FileSystemUtils.GetFullFilePath(folderPath, fileName));
            }

            PushFileToInputTag(fileFullPathList);
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
            while (DivUploadedFilePanel.Text.Trim().Contains(fileName) && iCount < MAX_WAIT)
            {
                iCount++;
                ThreadUtils.SleepVeryShortTime();
            }
        }

        /// <summary>
        /// Wait for the page loaded all the items
        /// </summary>
        /// <param name="MAX_WAIT">Max time to wait</param>
        public void WaitForItemListLoaded(int MAX_WAIT = 50)
        {
            ThreadUtils.SleepShortTime();

            int iCount = 0;
            while (string.IsNullOrEmpty(DivUploadedFilePanel.Text.Trim()) && iCount < MAX_WAIT)
            {
                iCount++;
                ThreadUtils.SleepVeryShortTime();
            }
        }

        /// <summary>
        /// Try to delete all uploaded files
        /// </summary>
        public void TryToDeleteAllUploadedFiles()
        {
            WaitForItemListLoaded();

            if (!string.IsNullOrEmpty(DivUploadedFilePanel.Text.Trim()))
            {
                try
                {
                    // find all uploaded items               
                    IList<BaseWebObject> allUploadedFiles = SpanUploadedItems;

                    if (allUploadedFiles.Count > 0)
                    {
                        MultiSelectItemsByCtrl(allUploadedFiles);

                        // delete the selected items
                        ScrollElementToView(ButtonDelete);
                        ButtonDelete.Click();

                        ThreadUtils.SleepShortTime();

                        // click confirm
                        (new ConfirmMessageSubPage(Driver, TestBase.WebUITestBaseClass.Browser)).BtnDialogConfirmYes.Click();
                    }
                }
                catch (StaleElementReferenceException ex)
                {
                    Console.Out.WriteLine("TryToDeleteAllUploadedFiles ERR: {0}", ex.Message);
                }
            }
        }

        /// <summary>
        /// Get information of all uploaded files
        /// </summary>
        /// <returns>List of file infor</returns>
        public IList<string> GetUploadedFileList()
        {
            IList<string> fileList = new List<string>();
            IList<BaseWebObject> allUploadedFiles = FindWebElements(divUploadedItem);

            foreach (BaseWebObject item in allUploadedFiles)
            {
                fileList.Add(item.Text);
            }

            return fileList;
        }

        /// <summary>
        /// Check if the progress bar visible for in-progress uploading item or not
        /// </summary>
        /// <returns>True if the progress bar visible; otherwise, False</returns>
        public bool IsUploadingTextAndProgressBarVisible()
        {
            return SpanUploadingText.IsVisible && DivUploadingProgressBar.IsVisible;
        }

        /// <summary>
        /// wait for file uploading finishes
        /// </summary>
        public void WaitForFileUploading()
        {
            int iCount = 0;

            while ((string.IsNullOrEmpty(DivUploadedFilePanel.Text.Trim()) ||
                DivUploadedFilePanel.Text.Contains("% Uploading") ||
                DivUploadedFilePanel.Text.Contains("Queued")) &&
                iCount < MAX_WAIT_FOR_IN_PROGRESS_FILE)
            {
                ThreadUtils.SleepShortTime();

                iCount++;
            }
        }

        /// <summary>
        /// Cancel an upload file
        /// </summary>
        /// <param name="filename">file name want to cancel</param>
        public void CancelUploadFile(string filename)
        {
            TestContext.Out.WriteLine("Cancel uploading file: {0}", filename);

            SpanCancelUploadingFileItem(filename).Click();
        }

        /// Count the number of uploading files in-progress
        /// </summary>
        /// <returns>The number of uploading files in-progress</returns>
        public int CountUploadingFileProgressBar()
        {
            return Regex.Matches(DivUploadedFilePanel.Text, "% Uploading").Cast<Match>().Count();
        }

        /// <summary>
        /// Count the number of queued files for uploading
        /// </summary>
        /// <returns>The number of queued files for uploading</returns>
        public int CountQueuedUploadFiles()
        {
            return FindWebElements(spanItemStatus).Count(x => x.Text.Equals("Queued"));
        }
        #endregion
    }
}

