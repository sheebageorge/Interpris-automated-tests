using Automation.UI.Core.CommonUtilities;
using Automation.UI.Core.Selenium;
using Automation.UI.Core.Selenium.PageObjects.Interpris.Product;
using Automation.UI.Core.TestBase;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Automation.UI.Core.TestLibraries
{
    public class ViewAllFilesUtils
    {
        /// <summary>
        /// Log in the system and wait some seconds for the page loading
        /// </summary>
        /// <param name="viewAllFilesPage">View All Files page</param>
        /// <param name="username">User name</param>
        /// <param name="password">Password</param>
        public static void LogInAndWaitForAllPageLoad(ViewAllFilesPage viewAllFilesPage, string username, string password)
        {
            TestContext.Out.WriteLine("Login User");
            viewAllFilesPage.LogIn(username, password);

            TestContext.Out.WriteLine("Verify All tab displayed");
            Assert.IsTrue(viewAllFilesPage.IsTabActive(), "All tab not displayed.");

            TestContext.Out.WriteLine("Sleep for a while for all items displayed");
            viewAllFilesPage.WaitForItemListLoaded();
        }

        /// <summary>
        /// Verify if the expected files displayed in All tab or not
        /// </summary>
        /// <param name="viewAllFilesPage">View All Files tab</param>
        /// <param name="expectedFileList">list of expected files</param>
        public static void VerifyListOfFilesOnViewAllFilesTab(ViewAllFilesPage viewAllFilesPage,
            IList<string> expectedFileList, bool shouldExisting = true)
        {
            // check if the file is not uploaded and/or transcribed yet to upload
            if (shouldExisting)
            {
                foreach (string fileName in expectedFileList)
                {
                    Assert.IsTrue(viewAllFilesPage.DivFileItemContainer.Text.Contains(fileName));
                }
            }
            else
            {
                foreach (string fileName in expectedFileList)
                {
                    Assert.IsFalse(viewAllFilesPage.DivFileItemContainer.Text.Contains(fileName));
                }
            }
        }

        /// <summary>
        /// Upload files to page if not existing
        /// </summary>
        /// <param name="viewAllFilesPage">View All Files page class</param>
        /// <param name="uploadedFileFolder">Upload file folder</param>
        /// <param name="uploadedFileList">List of files to upload</param>
        public static void AddUploadedFiles(ViewAllFilesPage viewAllFilesPage,
            string uploadedFileFolder, IList<string> uploadedFileList)
        {
            IList<string> readyToUploadingFileList = new List<string>();

            viewAllFilesPage.WaitForItemListLoaded();

            // check if the files already uploaded
            foreach (string fileName in uploadedFileList)
            {
                if (!viewAllFilesPage.DivFileItemContainer.Text.Contains(fileName))
                {
                    readyToUploadingFileList.Add(fileName);
                }
            }

            if (readyToUploadingFileList.Count > 0)
            {
                UploadPage uploadPage = new UploadPage(viewAllFilesPage.Driver, viewAllFilesPage.BaseURL);

                TestContext.Out.WriteLine("Go to upload page to upload file");
                uploadPage.TabUpload.Click();

                Assert.IsTrue(uploadPage.IsTabActive(), "Upload page not displayed.");

                // upload new files
                UploadFileUtils.UploadMultipleFiles(WebUITestBaseClass.Browser, uploadPage,
                    uploadedFileFolder, readyToUploadingFileList);

                ThreadUtils.SleepShortTime();
                uploadPage.WaitForFileUploading();
            }

            // back to All page and wait for the transcribing file
            viewAllFilesPage.TabAll.Click();
            Assert.IsTrue(viewAllFilesPage.IsTabActive(), "All tab not displayed.");
        }


        /// <summary>
        /// Try to upload an audio file & transcribed if it is not existing
        /// </summary>
        /// <param name="viewAllFilesPage">View All Files page</param>
        /// <param name="folderPath">Folder path</param>
        /// <param name="audioFileName">Audio file to upload</param>
        /// <param name="transcribeLanguage">Transcribing language</param>
        public static void TryToUploadAndTranscribeAudioFiles(ViewAllFilesPage viewAllFilesPage,
            string folderPath, IList<string> audioFileNames, IList<string> transcribeLanguages)
        {
            TestContext.Out.WriteLine("Try to upload and transcribe the audio files");

            List<string> readyToTranscribeFiles = new List<string>();

            // try to upload the files
            foreach (string audioFileName in audioFileNames)
            {
                // check if the file must be uploaded and transcribed
                if (!viewAllFilesPage.DivFileItemContainer.Text.Contains(audioFileName))
                {
                    readyToTranscribeFiles.Add(audioFileName);
                }
            }

            if (readyToTranscribeFiles.Count > 0)
            {
                // upload the files
                TryToAddUploadAudioFiles(viewAllFilesPage, folderPath, readyToTranscribeFiles);
            }

            // try to transcribe the files
            int i = 0;
            foreach (string audioFileName in audioFileNames)
            {
                if (viewAllFilesPage.DivFileItemContainer.Text.Contains(audioFileName) &&
                    viewAllFilesPage.GetFileItemIconClassValue(audioFileName).Contains(
                        ViewAllFilesPage.UPLOADED_FILE_ICON_CLASS_VALUE))
                {
                    TestContext.Out.WriteLine("Start transcribing the audio file {0}", audioFileName);
                    viewAllFilesPage.SelectALanguageAndClickTranscribe(audioFileName, transcribeLanguages[i]);
                }

                i++;
            }

            // wait for all the transcription completed
            foreach (string audioFileName in audioFileNames)
            {
                TestContext.Out.WriteLine("Wait for the transcription completed {0}", audioFileName);
                viewAllFilesPage.WaitForFileIconClassContains(audioFileName,
                        ViewAllFilesPage.TRANSCRIBED_FILE_ICON_CLASS_VALUE, ViewAllFilesPage.MAX_WAIT_FOR_TRANSCRIBING);
            }
        }


        /// <summary>
        /// Try to upload new audio files to test
        /// </summary>
        /// <param name="viewAllFilesPage">View All Files page</param>
        /// <param name="folderPath">Folder path</param>
        /// <param name="audioFileNames">Audio files to upload</param>
        /// <returns>Ready transcribing file names</returns>
        public static List<string> TryToAddUploadAudioFiles(ViewAllFilesPage viewAllFilesPage,
            string folderPath, List<string> audioFileNames)
        {
            TestContext.Out.WriteLine("Try to upload the new audio files to test");

            List<string> newAudioFileNames = new List<string>();

            // check the audio files and clone to new files if needed
            foreach (string audioFileName in audioFileNames)
            {
                // if the test audio file already transcribed
                // must re-upload the file with another name to test
                if (ShouldCopyToNewAudioFile(viewAllFilesPage, audioFileName))
                {
                    // first, create a new file name
                    string newFileName = FileSystemUtils.GetRandomFileName(audioFileName);

                    // copy the audio file to another one with new file name
                    Assert.IsTrue(FileSystemUtils.CopyFileTo(folderPath, audioFileName,
                        folderPath, newFileName), "Cannot copy the file to new one");

                    newAudioFileNames.Add(newFileName);
                }
                else
                {
                    newAudioFileNames.Add(audioFileName);
                }
            }

            // upload the new files to transcribe
            UploadPage uploadPage = new UploadPage(viewAllFilesPage.Driver, viewAllFilesPage.BaseURL);

            TestContext.Out.WriteLine("Go to upload page to upload file");
            uploadPage.TabUpload.Click();
            ThreadUtils.SleepShortTime();

            // upload the new file
            UploadFileUtils.UploadMultipleFiles(WebUITestBaseClass.Browser, uploadPage, folderPath, newAudioFileNames);

            ThreadUtils.SleepShortTime();
            uploadPage.WaitForFileUploading();

            TestContext.Out.WriteLine("Back to All tab");
            viewAllFilesPage.TabAll.Click();
            ThreadUtils.SleepShortTime();

            TestContext.Out.WriteLine("Verify All tab displayed");
            Assert.IsTrue(viewAllFilesPage.IsTabActive(), "All tab not displayed.");

            return newAudioFileNames;
        }

        /// <summary>
        /// Check if the audio file already uploaded and transcribed or not
        /// </summary>
        /// <param name="viewAllFilesPage">View All Files page</param>
        /// <param name="audioFileName">Audio file to verify</param>
        /// <returns>True if the file already uploaded and transcribed; otherwise, False</returns>
        public static bool ShouldCopyToNewAudioFile(ViewAllFilesPage viewAllFilesPage, string audioFileName)
        {
            return viewAllFilesPage.DivFileItemContainer.Text.Contains(audioFileName) &&
                (viewAllFilesPage.GetFileItemIconClassValue(audioFileName).Contains(
                    ViewAllFilesPage.TRANSCRIBED_FILE_ICON_CLASS_VALUE));
        }

        /// <summary>
        /// Wait for all deleted files clean up
        /// </summary>
        /// <param name="viewAllFilesPage">View All Files page</param>
        /// <param name="newAudioFileNames">List of deleted files</param>
        public static void WaitForAllDeletedFilesCleanUp(ViewAllFilesPage viewAllFilesPage, List<string> newAudioFileNames)
        {
            TestContext.Out.WriteLine("Verify the files deleted");
            foreach (string newAudioFileName in newAudioFileNames)
            {
                viewAllFilesPage.WaitForTheFileDisappear(newAudioFileName);
                Assert.IsTrue(!viewAllFilesPage.DivFileItemContainer.Text.Trim().Contains(newAudioFileName),
                    "File {0} not deleted", newAudioFileName);
            }
        }

        /// <summary>
        /// Verify the Delete File confirm message and click Yes button
        /// </summary>
        /// <param name="viewAllFilesPage">View All Files page</param>
        public static void VerifyDeleteFileConfirmMessageAndAccept(ViewAllFilesPage viewAllFilesPage, bool acceptMessage = true)
        {
            TestContext.Out.WriteLine("Verify Confirm Popup is displayed");

            ConfirmMessageSubPage confirmMsgDialog = new ConfirmMessageSubPage(
                viewAllFilesPage.Driver, WebUITestBaseClass.Browser);

            Assert.IsTrue(confirmMsgDialog.BtnDialogConfirmYes.IsVisible &&
                confirmMsgDialog.BtnDialogConfirmMsgNo.IsVisible, "Yes/No buttons not visible");

            if (acceptMessage)
            {
                Assert.IsTrue(viewAllFilesPage.GetConfirmMessageAndClickYes().Equals(UploadPage.MSG_DELETE_CONFIRMATION));
            }
            else
            {
                Assert.IsTrue(viewAllFilesPage.GetConfirmMessageAndClickNo().Equals(UploadPage.MSG_DELETE_CONFIRMATION));
            }

            ThreadUtils.SleepShortTime();
        }

        /// <summary>
        /// Try to open a transcript file if any
        /// Otherwise, upload and transcribe a new one and open it
        /// </summary>
        /// <param name="viewAllFilesPage"></param>
        /// <param name="testDataFolder"></param>
        /// <param name="transcriptFileName"></param>
        /// <param name="language"></param>
        public static void OpenTranscriptFile(ViewAllFilesPage viewAllFilesPage,
            string testDataFolder, string transcriptFileName, string language)
        {
            bool isTranscriptFileExisting = false;

            TestContext.Out.WriteLine("Search for a Transcript file to open");

            if (viewAllFilesPage.DivFileItemContainer.Text.Length > 0)
            {
                IList<BaseWebObject> DivFileItemActionButtons = viewAllFilesPage.DivFileItemActionButtons;

                foreach (BaseWebObject actionButton in DivFileItemActionButtons)
                {
                    if (actionButton.Text.Equals(ViewAllFilesPage.BTN_LABEL_OPEN_TRANSCRIPT, StringComparison.OrdinalIgnoreCase))
                    {
                        TestContext.Out.WriteLine("Open random Transcript file");
                        actionButton.Click();
                        ThreadUtils.SleepShortTime();

                        isTranscriptFileExisting = true;
                        break;
                    }
                }
            }

            if (!isTranscriptFileExisting)
            {
                // try to upload and transcibe an audio file
                ViewAllFilesUtils.TryToUploadAndTranscribeAudioFiles(viewAllFilesPage, testDataFolder,
                    new List<string>() { transcriptFileName }, new List<string>() { language });

                // open the text editor of the transcribed file to get the transcribed text
                TestContext.Out.WriteLine("Open the just-uploaded-transcribed file {0}", transcriptFileName);
                viewAllFilesPage.OpenTranscribedFile(transcriptFileName);
                ThreadUtils.SleepShortTime();
            }

            TranscribeEditorPage editorPage = new TranscribeEditorPage(viewAllFilesPage.Driver, viewAllFilesPage.BaseURL);

            Assert.IsTrue(editorPage.DivTextEditorContent.IsVisible, "Text Editor not visible");
        }
    }
}
