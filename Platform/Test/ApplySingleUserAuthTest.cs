using Automation.UI.Core.CommonUtilities;
using Automation.UI.Core.DataProcessing;
using Automation.UI.Core.Selenium.PageObjects.Interpris.Platform;
using Automation.UI.Core.Selenium.PageObjects.Interpris.Transcription;
using Automation.UI.Core.TestAttributes;
using Automation.UI.Core.TestBase;
using Automation.UI.Platform.TestAttributes;
using Automation.UI.Core.TestLibraries;
using NUnit.Framework;
using System.Collections.Generic;

namespace Automation.UI.Platform.Test
{
    /// <summary>
    /// Test Suite for Apply Single User feature
    /// </summary>
    [TestFixture]
    public class ApplySingleUserAuthTest : PlatformTestBase
    {
        #region Test Cases
        [Test]
        [TestID(TestID.TC_ID_0010), StoryID(StoryID.SR_ID_004)]
        [Priority(PriorityLevel.Medium)]
        [TestCaseSource(typeof(DataProvider), "PrepareTestCases", new object[] { TestID.TC_ID_0010 })]
        public void TC_APPLY_SINGLE_USER_VerifySingleUserCanSeeTheirUploadedFiles(Dictionary<string, string> Data)
        {
            TestContext.Out.WriteLine("Start Test Case - {0}", TestID.TC_ID_0010);

            // create data
            List<string> user1UploadFileList = new List<string>() { Data["test_upload_file_1"] };

            FileSystemUtils.CreateFilesNotExistedInFolder(
                Data["test_uploaded_file_folder"], user1UploadFileList, Data["file_size_small"]);

            // create pages
            LoginPage loginPage = new LoginPage(Driver, PlatformBaseURL);
            ViewAllFilesPage viewAllFilesPage = new ViewAllFilesPage(Driver, TranscriptionBaseURL);

            // user can see their uploaded file
            VerifyUserCanSignInAndUploadAFile(loginPage, Data["username_1"], Data["password_1"],
                viewAllFilesPage, Data["test_uploaded_file_folder"], user1UploadFileList);

            TestContext.Out.WriteLine("End Test Case - {0}", TestID.TC_ID_0010);
        }

        [Test]
        [TestID(TestID.TC_ID_0011), StoryID(StoryID.SR_ID_004)]
        [Priority(PriorityLevel.Medium)]
        [TestCaseSource(typeof(DataProvider), "PrepareTestCases", new object[] { TestID.TC_ID_0011 })]
        public void TC_APPLY_SINGLE_USER_VerifySingleUserCannotSeeFilesUploadedByOtherUsers(Dictionary<string, string> Data)
        {
            TestContext.Out.WriteLine("Start Test Case - {0}", TestID.TC_ID_0011);

            // create data
            List<string> user1UploadFileList = new List<string>() { Data["test_upload_file_1"] };
            List<string> user2UploadFileList = new List<string>() { Data["test_upload_file_2"] };

            FileSystemUtils.CreateFilesNotExistedInFolder(
                Data["test_uploaded_file_folder"], user1UploadFileList, Data["file_size_small"]);
            FileSystemUtils.CreateFilesNotExistedInFolder(
                Data["test_uploaded_file_folder"], user2UploadFileList, Data["file_size_small"]);

            // create pages
            LoginPage loginPage = new LoginPage(Driver, PlatformBaseURL);
            ViewAllFilesPage viewAllFilesPage =
                new ViewAllFilesPage(Driver, TranscriptionBaseURL);

            // user can see their uploaded file
            VerifyUserCanSignInAndUploadAFile(loginPage, Data["username_1"], Data["password_1"],
                viewAllFilesPage, Data["test_uploaded_file_folder"], user1UploadFileList);

            TestContext.Out.WriteLine("Switch to another user");

            // log out from the Transcription page
            viewAllFilesPage.LogOut();

            // user can see their uploaded file
            VerifyUserCanSignInAndUploadAFile(loginPage, Data["username_2"], Data["password_2"],
                viewAllFilesPage, Data["test_uploaded_file_folder"], user2UploadFileList);

            // verify the file uploaded by user 1 cannot be viewed by user 2
            TestContext.Out.WriteLine("Verify user cannot see the files they don't upload");
            ViewAllFilesUtils.VerifyListOfFilesOnViewAllFilesTab(viewAllFilesPage, user1UploadFileList, false);

            TestContext.Out.WriteLine("End Test Case - {0}", TestID.TC_ID_0011);
        }

        [Test]
        [TestID(TestID.TC_ID_0012), StoryID(StoryID.SR_ID_004)]
        [Priority(PriorityLevel.Medium)]
        [TestCaseSource(typeof(DataProvider), "PrepareTestCases", new object[] { TestID.TC_ID_0012 })]
        public void TC_APPLY_SINGLE_USER_VerifySingleUserCanSeeTheirTranscribedFiles(Dictionary<string, string> Data)
        {
            TestContext.Out.WriteLine("Start Test Case - {0}", TestID.TC_ID_0012);

            // prepare data
            List<string> user1TranscribingFileNewNames = new List<string>() {
                FileSystemUtils.CloneFileByTailTag(Data["test_data_folder"],
                Data["test_audio_file_1"], StoryID.SR_ID_004) };
            string user1TranscribingAudioLangFileName = Data["test_audio_lang_1"];

            // create pages
            LoginPage loginPage = new LoginPage(Driver, PlatformBaseURL);
            ViewAllFilesPage viewAllFilesPage =
                new ViewAllFilesPage(Driver, TranscriptionBaseURL);

            // verify user can see their transcribed file
            VerifyUserCanSignInAndTranscribeAFile(loginPage, Data["username_1"], Data["password_1"],
                viewAllFilesPage, Data["test_data_folder"], user1TranscribingFileNewNames,
                new List<string>() { user1TranscribingAudioLangFileName });

            TestContext.Out.WriteLine("End Test Case - {0}", TestID.TC_ID_0012);
        }

        [Test]
        [TestID(TestID.TC_ID_0013), StoryID(StoryID.SR_ID_004)]
        [Priority(PriorityLevel.Medium)]
        [TestCaseSource(typeof(DataProvider), "PrepareTestCases", new object[] { TestID.TC_ID_0013 })]
        public void TC_APPLY_SINGLE_USER_VerifySingleUserCannotSeeFilesTranscribedByOtherUsers(Dictionary<string, string> Data)
        {
            TestContext.Out.WriteLine("Start Test Case - {0}", TestID.TC_ID_0013);

            // prepare data
            List<string> user1TranscribingFileNewNames = new List<string>() {
                FileSystemUtils.CloneFileByTailTag(Data["test_data_folder"],
                Data["test_audio_file_1"], StoryID.SR_ID_004) };
            string user1TranscribingAudioLangFileName = Data["test_audio_lang_1"];

            List<string> user2TranscribingFileNewNames = new List<string>() {
                FileSystemUtils.CloneFileByTailTag(Data["test_data_folder"],
                Data["test_audio_file_2"], StoryID.SR_ID_004) };
            string user2TranscribingAudioLangFileName = Data["test_audio_lang_2"];

            // create pages
            LoginPage loginPage = new LoginPage(Driver, PlatformBaseURL);
            ViewAllFilesPage viewAllFilesPage =
                new ViewAllFilesPage(Driver, TranscriptionBaseURL);

            // verify user 1 can see their transcribed file
            VerifyUserCanSignInAndTranscribeAFile(loginPage, Data["username_1"], Data["password_1"],
                viewAllFilesPage, Data["test_data_folder"], user1TranscribingFileNewNames,
                new List<string>() { user1TranscribingAudioLangFileName });

            TestContext.Out.WriteLine("Switch to another user");

            // log out from the Transcription page
            viewAllFilesPage.LogOut();

            // verify user 2 can see their transcribed file
            VerifyUserCanSignInAndTranscribeAFile(loginPage, Data["username_2"], Data["password_2"],
                viewAllFilesPage, Data["test_data_folder"], user2TranscribingFileNewNames,
                new List<string>() { user2TranscribingAudioLangFileName });

            TestContext.Out.WriteLine("User cannot see the transcribed file from other user");
            ViewAllFilesUtils.VerifyListOfFilesOnViewAllFilesTab(viewAllFilesPage, user1TranscribingFileNewNames, false);

            TestContext.Out.WriteLine("End Test Case - {0}", TestID.TC_ID_0013);
        }

        [Test]
        [TestID(TestID.TC_ID_0014), StoryID(StoryID.SR_ID_004)]
        [Priority(PriorityLevel.Medium)]
        [TestCaseSource(typeof(DataProvider), "PrepareTestCases", new object[] { TestID.TC_ID_0014 })]
        public void TC_APPLY_SINGLE_USER_VerifySingleUserCanReadTheirTranscribedFiles(Dictionary<string, string> Data)
        {
            TestContext.Out.WriteLine("Start Test Case - {0}", TestID.TC_ID_0014);

            // prepare data
            List<string> user1TranscribingFileNewNames = new List<string>() {
                FileSystemUtils.CloneFileByTailTag(Data["test_data_folder"],
                Data["test_audio_file_1"], StoryID.SR_ID_004) };
            string user1TranscribingAudioLangFileName = Data["test_audio_lang_1"];

            // create pages
            LoginPage loginPage = new LoginPage(Driver, PlatformBaseURL);
            ViewAllFilesPage viewAllFilesPage =
                new ViewAllFilesPage(Driver, TranscriptionBaseURL);
            TranscribeEditorPage editorPage =
                new TranscribeEditorPage(Driver, TranscriptionBaseURL);

            // verify user can see their transcribed file
            VerifyUserCanSignInAndTranscribeAFile(loginPage, Data["username_1"], Data["password_1"],
                viewAllFilesPage, Data["test_data_folder"], user1TranscribingFileNewNames,
                new List<string>() { user1TranscribingAudioLangFileName });

            // open the text editor of the transcribed file to get the transcribed text
            TestContext.Out.WriteLine("Open the transcribed file {0}", user1TranscribingFileNewNames[0]);
            viewAllFilesPage.OpenTranscribedFile(user1TranscribingFileNewNames[0]);
            ThreadUtils.SleepShortTime();

            Assert.IsTrue(editorPage.DivTextEditorContent.IsVisible, "Text Editor not visible");
            Assert.IsTrue(editorPage.DivTextEditorContent.Text.Length > 0, "Cannot view transcription file");

            TestContext.Out.WriteLine("End Test Case - {0}", TestID.TC_ID_0014);
        }

        [Test]
        [TestID(TestID.TC_ID_0015), StoryID(StoryID.SR_ID_004)]
        [Priority(PriorityLevel.Medium)]
        [TestCaseSource(typeof(DataProvider), "PrepareTestCases", new object[] { TestID.TC_ID_0015 })]
        public void TC_APPLY_SINGLE_USER_VerifySingleUserCannotReadFilesTranscribedByOtherUsers(
            Dictionary<string, string> Data)
        {
            TestContext.Out.WriteLine("Start Test Case - {0}", TestID.TC_ID_0015);

            // prepare data
            List<string> user1TranscribingFileNewNames = new List<string>() {
                FileSystemUtils.CloneFileByTailTag(Data["test_data_folder"],
                Data["test_audio_file_1"], StoryID.SR_ID_004) };
            string user1TranscribingAudioLangFileName = Data["test_audio_lang_1"];

            List<string> user2TranscribingFileNewNames = new List<string>() {
                FileSystemUtils.CloneFileByTailTag(Data["test_data_folder"],
                Data["test_audio_file_2"], StoryID.SR_ID_004) };
            string user2TranscribingAudioLangFileName = Data["test_audio_lang_2"];

            // create pages
            LoginPage loginPage = new LoginPage(Driver, PlatformBaseURL);
            ViewAllFilesPage viewAllFilesPage =
                new ViewAllFilesPage(Driver, TranscriptionBaseURL);
            TranscribeEditorPage editorPage =
                new TranscribeEditorPage(Driver, TranscriptionBaseURL);

            // verify user 1 can see their transcribed file
            VerifyUserCanSignInAndTranscribeAFile(loginPage, Data["username_1"], Data["password_1"],
                viewAllFilesPage, Data["test_data_folder"], user1TranscribingFileNewNames,
                new List<string>() { user1TranscribingAudioLangFileName });

            string user1TranscribedFileLink = viewAllFilesPage.GetTranscribedFileHref(user1TranscribingFileNewNames[0]);

            TestContext.Out.WriteLine("Switch to another user");

            // log out from the Transcription page
            viewAllFilesPage.LogOut();

            // verify user 2 can see their transcribed file
            VerifyUserCanSignInAndTranscribeAFile(loginPage, Data["username_2"], Data["password_2"],
                viewAllFilesPage, Data["test_data_folder"], user2TranscribingFileNewNames,
                new List<string>() { user2TranscribingAudioLangFileName });

            // open the text editor of the transcribed file to get the transcribed text
            TestContext.Out.WriteLine("Open the transcribed file {0}", user2TranscribingFileNewNames[0]);
            viewAllFilesPage.OpenTranscribedFile(user2TranscribingFileNewNames[0]);
            ThreadUtils.SleepShortTime();

            Assert.IsTrue(editorPage.DivTextEditorContent.IsVisible, "Text Editor not visible");
            Assert.IsTrue(editorPage.DivTextEditorContent.Text.Length > 0, "Cannot view transcription file");

            editorPage.Back();
            ThreadUtils.SleepShortTime();

            viewAllFilesPage.WaitForItemListLoaded();

            TestContext.Out.WriteLine("User cannot read the transcribed file from other user");
            viewAllFilesPage.NavigateTo(user1TranscribedFileLink);
            ThreadUtils.SleepMediumTime();

            TestContext.Out.WriteLine("Transcription file content: {0}", editorPage.DivPageContent.GetAttribute("innerText"));
            Assert.IsTrue(string.IsNullOrEmpty(editorPage.DivPageContent.GetAttribute("innerText").Trim()),
                "File content not empty");

            TestContext.Out.WriteLine("End Test Case - {0}", TestID.TC_ID_0015);
        }

        [Test]
        [TestID(TestID.TC_ID_0016), StoryID(StoryID.SR_ID_004)]
        [Priority(PriorityLevel.Medium)]
        [TestCaseSource(typeof(DataProvider), "PrepareTestCases", new object[] { TestID.TC_ID_0016 })]
        public void TC_APPLY_SINGLE_USER_VerifySingleUserCanEditTheirUploadedFiles(Dictionary<string, string> Data)
        {
            TestContext.Out.WriteLine("Start Test Case - {0}", TestID.TC_ID_0016);

            // create data
            List<string> user1UploadFileList = new List<string>() { Data["test_upload_file_1"] };

            FileSystemUtils.CreateFilesNotExistedInFolder(
                Data["test_uploaded_file_folder"], user1UploadFileList, Data["file_size_small"]);

            // create pages
            LoginPage loginPage = new LoginPage(Driver, PlatformBaseURL);
            ViewAllFilesPage viewAllFilesPage =
                new ViewAllFilesPage(Driver, TranscriptionBaseURL);

            // user can see their uploaded file
            VerifyUserCanSignInAndUploadAFile(loginPage, Data["username_1"], Data["password_1"],
                viewAllFilesPage, Data["test_uploaded_file_folder"], user1UploadFileList);

            string selectLang1 = Data["select_lang_1"];
            string selectLang2 = Data["select_lang_2"];

            TestContext.Out.WriteLine("Change transcribe language 1");
            viewAllFilesPage.SelectALanguageByText(user1UploadFileList[0], selectLang1);

            TestContext.Out.WriteLine("Verify new selected language 1");
            Assert.IsTrue(viewAllFilesPage.GetCurrentLanguageOfAFile(user1UploadFileList[0]).Equals(selectLang1),
                "Lang {0} cannot selected", selectLang1);

            TestContext.Out.WriteLine("Change transcribe language 2");
            viewAllFilesPage.SelectALanguageByText(user1UploadFileList[0], selectLang2);

            TestContext.Out.WriteLine("Verify new selected language 2");
            Assert.IsTrue(viewAllFilesPage.GetCurrentLanguageOfAFile(user1UploadFileList[0]).Equals(selectLang2),
                "Lang {0} cannot selected", selectLang2);

            TestContext.Out.WriteLine("End Test Case - {0}", TestID.TC_ID_0016);
        }

        [Test]
        [TestID(TestID.TC_ID_0017), StoryID(StoryID.SR_ID_004)]
        [Priority(PriorityLevel.Medium)]
        [TestCaseSource(typeof(DataProvider), "PrepareTestCases", new object[] { TestID.TC_ID_0017 })]
        public void TC_APPLY_SINGLE_USER_VerifySingleUserCanDeleteTheirUploadedFiles(Dictionary<string, string> Data)
        {
            TestContext.Out.WriteLine("Start Test Case - {0}", TestID.TC_ID_0017);

            // create data
            List<string> user1UploadFileList = new List<string>() { Data["test_upload_file_1"] };

            FileSystemUtils.CreateFilesNotExistedInFolder(
                Data["test_uploaded_file_folder"], user1UploadFileList, Data["file_size_small"]);

            // create pages
            LoginPage loginPage = new LoginPage(Driver, PlatformBaseURL);
            ViewAllFilesPage viewAllFilesPage =
                new ViewAllFilesPage(Driver, TranscriptionBaseURL);

            // user can see their uploaded file
            VerifyUserCanSignInAndUploadAFile(loginPage, Data["username_1"], Data["password_1"],
                viewAllFilesPage, Data["test_uploaded_file_folder"], user1UploadFileList);

            TestContext.Out.WriteLine("Delete the file");
            viewAllFilesPage.SelectUploadFiles(user1UploadFileList);
            viewAllFilesPage.ClickDeleteButton();

            ViewAllFilesUtils.VerifyDeleteFileConfirmMessageAndAccept(viewAllFilesPage);
            ViewAllFilesUtils.WaitForAllDeletedFilesCleanUp(viewAllFilesPage, user1UploadFileList);

            TestContext.Out.WriteLine("End Test Case - {0}", TestID.TC_ID_0017);
        }
        #endregion

        #region Methods

        /// <summary>
        /// Try to Access Transcription Landing page after logged in from Core page
        /// </summary>
        /// <param name="loginPage"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public static void TryToAccessTranscriptionLandingPageFromCorePage(
            LoginPage loginPage, string username, string password)
        {
            TestContext.Out.WriteLine("Go to Login Page");
            loginPage.Navigate();

            TestContext.Out.WriteLine("Click Log In tab and Fill required information");
            loginPage.InputLoginInfo(username, password);
            ThreadUtils.SleepShortTime();

            Assert.IsTrue(loginPage.BtnBuyCredits != null, "Cannot login to the system.");

            ViewAllFilesPage transcriptionAllPage = new ViewAllFilesPage(loginPage.Driver, TranscriptionBaseURL);

            transcriptionAllPage.Navigate();
            ThreadUtils.SleepShortTime();

            TestContext.Out.WriteLine("Verify Transcription Landing page displayed");
            Assert.IsTrue(transcriptionAllPage.IsTabActive(), "Transcription Landing page not visible");
        }

        /// <summary>
        /// Verify if user can log in and upload a file if the file not existing
        /// </summary>
        /// <param name="loginPage"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="viewAllFilesPage"></param>
        /// <param name="folderPath"></param>
        /// <param name="uploadFileList"></param>
        public void VerifyUserCanSignInAndUploadAFile(LoginPage loginPage, string username, string password,
            ViewAllFilesPage viewAllFilesPage, string folderPath,
            List<string> uploadFileList)
        {
            // log in from Platform
            TryToAccessTranscriptionLandingPageFromCorePage(loginPage, username, password);

            // wait for the file list appear
            viewAllFilesPage.WaitForItemListLoaded();

            // upload a file if not existing
            ViewAllFilesUtils.AddUploadedFiles(viewAllFilesPage, folderPath, uploadFileList);

            // verify uploaded files
            ViewAllFilesUtils.VerifyListOfFilesOnViewAllFilesTab(viewAllFilesPage, uploadFileList);
        }

        /// <summary>
        /// Verify user can sign in and transcribe a file if it not existing
        /// </summary>
        /// <param name="loginPage"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="viewAllFilesPage"></param>
        /// <param name="folderPath"></param>
        /// <param name="transribingFileList"></param>
        /// <param name="transcribingLangs"></param>
        public void VerifyUserCanSignInAndTranscribeAFile(LoginPage loginPage, string username, string password,
            ViewAllFilesPage viewAllFilesPage, string folderPath,
            List<string> transribingFileList, List<string> transcribingLangs)
        {
            // log in from Platform
            TryToAccessTranscriptionLandingPageFromCorePage(loginPage, username, password);

            // wait for the file list appear
            viewAllFilesPage.WaitForItemListLoaded();

            // try to upload and transcibe an audio file
            ViewAllFilesUtils.TryToUploadAndTranscribeAudioFiles(viewAllFilesPage, folderPath,
                transribingFileList, transcribingLangs);

            TestContext.Out.WriteLine("User can see the transcribed file in the All page");
            ViewAllFilesUtils.VerifyListOfFilesOnViewAllFilesTab(viewAllFilesPage, transribingFileList);
        }
        #endregion
    }
}
