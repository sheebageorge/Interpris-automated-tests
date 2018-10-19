using Automation.UI.Core.CommonUtilities;
using Automation.UI.Core.DesktopAutomation.OpenFileDialog;
using Automation.UI.Core.Selenium.PageObjects.Interpris.Transcription;
using Automation.UI.Core.TestBase;
using NUnit.Framework;
using System.Collections.Generic;

namespace Automation.UI.Core.TestLibraries
{
    public class UploadFileUtils
    {
        /// <summary>
        /// Open the Login page
        /// Log In to Landing page
        /// Open the Upload tab
        /// </summary>
        /// <param name="username">Username to sign in</param>
        /// <param name="password">Password to sign in</param>
        public static void LoginToUploadPage(UploadPage uploadPage, string username, string password)
        {
            // log in and for page loaded
            uploadPage.LogIn(username, password);

            // click to open upload tab
            TestContext.Out.WriteLine("Open Upload file tab");
            uploadPage.TabUpload.Click();
            ThreadUtils.SleepShortTime();

            // wait for the browser file button visible
            Assert.IsTrue(uploadPage.ButtonBrowseFile.IsVisible);

            // wait for files loaded
            uploadPage.WaitForItemListLoaded();
        }

        /// <summary>
        /// Upload multiple files
        /// </summary>
        /// <param name="browserType">Type of browser</param>
        /// <param name="uploadPage">Upload page/Landing page</param>
        /// <param name="folderPath">Folder of files to upload</param>
        /// <param name="fileList">List of uploading files</param>
        public static void UploadMultipleFiles(string browserType, UploadPage uploadPage,
            string folderPath, IList<string> fileList)
        {
            TestContext.Out.WriteLine("Uploading multiple files");

            if (!WebUITestBaseClass.BrowserStackEnabled)
            {
                TestContext.Out.WriteLine("Browse Files");
                uploadPage.ActionMouseClick(uploadPage.ButtonBrowseFile);

                ThreadUtils.SleepShortTime();

                TestContext.Out.WriteLine("Open upload file folder to get the file list");
                WebOpenFileDialog openFileDialog = WebOpenFileDialog.GetOpenDialog(WebUITestBaseClass.Browser);

                openFileDialog.SelectMultipleFilesInOpenDialog(folderPath, fileList);
            }
            else
            {
                uploadPage.UploadFiles(folderPath, fileList);
            }

            ThreadUtils.SleepShortTime();
        }

        /// <summary>
        /// Upload a file in Upload File Tab
        /// </summary>
        /// <param name="browserType">Type of browser</param>
        /// <param name="uploadPage">Upload page/Landing page</param>
        /// <param name="folderPath">Folder of files to upload</param>
        /// <param name="fileName">File Name</param>
        public static void UploadOneFile(string browserType, UploadPage uploadPage,
            string folderPath, string fileName)
        {
            TestContext.Out.WriteLine("Uploading file {0}", fileName);

            if (!WebUITestBaseClass.BrowserStackEnabled)
            {
                TestContext.Out.WriteLine("Browse Files");
                uploadPage.ActionMouseClick(uploadPage.ButtonBrowseFile);

                ThreadUtils.SleepShortTime();

                TestContext.Out.WriteLine("Open upload file folder to get the file list");
                WebOpenFileDialog openFileDialog = WebOpenFileDialog.GetOpenDialog(WebUITestBaseClass.Browser);

                TestContext.Out.WriteLine("Select file {0} to upload", fileName);
                openFileDialog.SelectAFileByName(folderPath, fileName);
            }
            else
            {
                uploadPage.UploadFile(folderPath, fileName);
            }

            ThreadUtils.SleepShortTime();
        }

        /// <summary>
        /// Delete the file item if existing
        /// </summary>
        /// <param name="uploadPage">Upload page</param>
        /// <param name="fileNameList">List of file names to delete</param>
        public static void DeleteItemsInPage(UploadPage uploadPage, IList<string> fileNameList)
        {
            // try to delete all uploaded files
            //uploadPage.TryToDeleteAllUploadedFiles();

            List<string> readyToDeleteItems = new List<string>();

            foreach (string fileName in fileNameList)
            {
                if (uploadPage.DivUploadedFilePanel.Text.Contains(fileName))
                {
                    readyToDeleteItems.Add(fileName);
                }
            }

            if (readyToDeleteItems.Count > 0)
            {
                uploadPage.DeleteFiles(readyToDeleteItems);
            }

            // wait for the deleted files disappear
            WaitForAllDeletedFilesCleanUp(uploadPage, readyToDeleteItems);
        }

        /// <summary>
        /// Wait for all deleted files clean up
        /// </summary>
        /// <param name="uploadPage">Upload page</param>
        /// <param name="fileNameList">List of deleted files</param>
        public static void WaitForAllDeletedFilesCleanUp(UploadPage uploadPage, List<string> fileNameList)
        {
            TestContext.Out.WriteLine("Verify the files deleted");
            foreach (string fileName in fileNameList)
            {
                uploadPage.WaitForTheFileDisappear(fileName);
                Assert.IsTrue(!uploadPage.DivUploadedFilePanel.Text.Trim().Contains(fileName),
                    "File {0} not deleted", fileName);
            }
        }
    }
}
