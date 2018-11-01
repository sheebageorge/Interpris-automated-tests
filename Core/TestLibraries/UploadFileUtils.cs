using Automation.UI.Core.CommonUtilities;
using Automation.UI.Core.DesktopAutomation.OpenFileDialog;
using Automation.UI.Core.Selenium.PageObjects.Interpris.Product;
using Automation.UI.Core.TestBase;
using NUnit.Framework;
using System.Collections.Generic;

namespace Automation.UI.Core.TestLibraries
{
    public class UploadFileUtils
    {

        /// <summary>
        /// Upload multiple files
        /// </summary>
        /// <param name="browserType">Type of browser</param>
        /// <param name="uploadPage">Upload page/Landing page</param>
        /// <param name="folderPath">Folder of files to upload</param>
        /// <param name="fileList">List of uploading files</param>
        /*public static void UploadMultipleFiles(string browserType, UploadPage uploadPage,
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
        }*/

       

        /// <summary>
        /// Wait for all deleted files clean up
        /// </summary>
        /// <param name="uploadPage">Upload page</param>
        /// <param name="fileNameList">List of deleted files</param>
        /*public static void WaitForAllDeletedFilesCleanUp(UploadPage uploadPage, List<string> fileNameList)
        {
            TestContext.Out.WriteLine("Verify the files deleted");
            foreach (string fileName in fileNameList)
            {
                uploadPage.WaitForTheFileDisappear(fileName);
                Assert.IsTrue(!uploadPage.DivUploadedFilePanel.Text.Trim().Contains(fileName),
                    "File {0} not deleted", fileName);
            }
        }*/
    }
}
