using OpenQA.Selenium;
using System.Collections.Generic;
using NUnit.Framework;
using System;
using Automation.UI.Core.CommonUtilities;
using Automation.UI.Core.Selenium.PageObjects.Interpris.Platform;
using Automation.UI.Core.TestBase;

namespace Automation.UI.Core.Selenium.PageObjects.Interpris.Transcription
{
    /// <summary>
    /// Wrapper class of Landing (Home) Page
    /// contains all elements and actions for Landing Page
    /// </summary>
    public class LandingPage: BasePage
    {
        public const string PAGE_TITLE = "NVivo Transcription";

        public const int TAB_COUNT = 2;
        public static readonly string[] TAB_NAMES = new string[] { "All files", "Upload" };

        public const string MSG_DELETE_CONFIRMATION = "Are you sure you want to delete the file(s)?";
        public const string MSG_ERROR = "Sorry!\r\nThis operation failed.";
        public const string MSG_NOT_ENOUGH_CREDITS = "You have insufficient funds to do this. Would you like to buy more time?";

        #region UI Objects
        private readonly string tabList = "//div[@role=\"tablist\"]//div[@role=\"tab\"]";
        private readonly string tabAll = "xpath=//div[@role='tab'and .='All files']";
        private readonly string tabUpload = "xpath=//div[@role='tab'and .='Upload']";
        private const string BtnDelete = "//*[@aria-hidden='false']//*[@class='right__delete-btn']/button";
        private readonly string spanActiveTabText = "//div[@id=\"app\"]//div[@role=\"tablist\"]" +
            "//div[@role=\"tab\" and @class=\"ant-tabs-tab-active ant-tabs-tab\"]/span";
        #endregion

        public LandingPage(IWebDriver driver, string baseURL) : base(driver, baseURL, PageConstants.TRANSCRIPTION_PAGE_LANDING_URL) { }

        #region Properties
        public IList<BaseWebObject> TabList => FindWebElements(tabList);
        public BaseWebObject TabAll => FindWebElement(tabAll, true);
        public BaseWebObject TabUpload => FindWebElement(tabUpload, true);
        public BaseWebObject ButtonDelete => FindWebElement(BtnDelete, true);
        public BaseWebObject SpanActiveTabText => FindWebElement(spanActiveTabText, true);

        public BaseWebObject DivSelectLanguage(string filename) => FindWebElement(
            $"//*[@aria-hidden='false']//span[text()='{filename}']" +
            $"/ancestor::*[@role='presentation']//*[@role='combobox']", true);
        public BaseWebObject DivAudioLength(string filename) => FindWebElement(
            $"//*[@aria-hidden='false']//span[text()='{filename}']" +
            $"/ancestor::*[@role='presentation']//*[contains(@class, 'DurationLabel')]", true);
        public BaseWebObject DivTranscribeLink(string filename) => FindWebElement(
            $"//*[@aria-hidden=\"false\"]//span[.=\"{filename}\"]/ancestor::*[@role=\"presentation\"]" +
            $"//button[contains(@class,\"ActionButton\") and not(contains(@class,\"disabled\"))]" +
            $"/span[text()=\"Transcribe\"]", true);
        public BaseWebObject BtnTranscribeFile(string filename) => FindWebElement(
            $"//*[@aria-hidden=\"false\"]//span[.=\"{filename}\"]/ancestor::*[@role=\"presentation\"]" +
            $"//button[contains(@class,\"ActionButton\") and ./span[text()=\"Transcribe\"]]", true);
        public BaseWebObject DivTranscribeButtonTitle(string filename) => FindWebElement(
            $"//*[@aria-hidden=\"false\"]//span[.=\"{filename}\"]/ancestor::*[@role=\"presentation\"]//div[4]", true);
        public IList<BaseWebObject> LanguageOptions => FindWebElements(
            "//div[contains(@class,\"ant-select-dropdown dropdown-language-item\") and not(contains(@class,\"ant-select-dropdown-hidden\"))]" +
            "//ul[@role='listbox']/li");
        public BaseWebObject LanguageOptionsListBox => FindWebElement(
            "//div[contains(@class,\"ant-select-dropdown dropdown-language-item\") and not(contains(@class,\"ant-select-dropdown-hidden\"))]" +
            "//ul[@role='listbox']");
        public BaseWebObject CbxSelectLanguageItem(string language) => FindWebElement(
            $"//div[contains(@class,\"ant-select-dropdown dropdown-language-item\") and not(contains(@class,\"ant-select-dropdown-hidden\"))]" +
            $"//ul[@role='listbox']/li[.='{language}']");
        public BaseWebObject InputLanguageSearchBox(string filename) => FindWebElement(
            $"//*[@aria-hidden=\"false\"]//span[@class=\"file-info__name\" and .=\"{filename}\"]" +
            $"/ancestor::*[@role=\"presentation\"]//div[starts-with(@class,\"LanguageDropdown-\")]//input[@class=\"ant-select-search__field\"]", true);
        public BaseWebObject SpanFileItemIcon(string filename) => FindWebElement($"//*[@aria-hidden=\"false\"]//span[@class=\"file-info__name\" and .=\"{filename}\"]" +
            $"/ancestor::*[@role=\"presentation\"]//div[@class=\"icon\"]/span[contains(@class,\"qsr-icon\")]", true);
        public BaseWebObject SpanUploadedFileItem(string filename) => FindWebElement($"//*[@aria-hidden='false']" +
            $"//span[@class=\"file-info__name\" and .='{filename}']/ancestor::*[@role='presentation']//span[contains(@class,\"qsr-icon\")]");
        public BaseWebObject ATranscribedFileItem(string filename) => FindWebElement($"//*[@aria-hidden='false']" +
            $"//span[@class=\"file-info__name\" and .='{filename}']/a", true);
        public BaseWebObject ATranscribedFileOpenLink(string filename) => FindWebElement(
            $"//*[@aria-hidden=\"false\"]//span[@class=\"file-info__name\" and .=\"{filename}\"]" +
            $"/ancestor::*[@role=\"presentation\"]//a[./span[text()=\"Open\"]]", true);
        public BaseWebObject DivTranscribedFileItem(string filename) => FindWebElement($"//*[@aria-hidden='false']" +
            $"//span[@class=\"file-info__name\" and .='{filename}']/ancestor::*[@role='presentation']");
        public BaseWebObject DivDeletingFileNotifyMsg(string filename) => FindWebElement(
            $"//body//div[@class=\"CustomTopLeftPosition\"]//div[starts-with(@class,\"Notification__item__message\") " +
            $"and contains(.,\"{filename}\") and contains(.,\"has been deleted\")]", true);
        public IList<BaseWebObject> DivUploadedItems => FindWebElements(
            "//*[@aria-hidden=\"false\"]//div[@role=\"presentation\"]");
        #endregion

        #region Methods
        /// <summary>
        /// Log out of system
        /// </summary>
        public void LogOut()
        {
            (new HeaderSubPage(Driver, InterprisTestBase.InterprisBaseURL)).LogOut();
        }

        /// <summary>
        /// Log in the system
        /// </summary>
        /// <param name="username">Username to login</param>
        /// <param name="password">Password to login</param>
        public void LogIn(string username, string password)
        {
            (new HeaderSubPage(Driver, InterprisTestBase.InterprisBaseURL)).LogIn(username, password);
        }

        /// <summary>
        /// Check if the Delete button enabled/disabled
        /// </summary>
        /// <returns>True/False</returns>
        public bool IsDeleteButtonEnabled()
        {
            return (ButtonDelete.GetAttribute("disabled") == null);
        }

        /// <summary>
        /// Get confirm message and close
        /// </summary>
        /// <returns>Confirm message</returns>
        public string GetConfirmMessageAndClose()
        {
            return (new ConfirmMessageSubPage(Driver, WebUITestBaseClass.Browser)).GetConfirmMessageAndClose();
        }

        /// <summary>
        /// Get confirm message and click Yes
        /// </summary>
        /// <returns>Confirm message</returns>
        public string GetConfirmMessageAndClickYes()
        {
            return (new ConfirmMessageSubPage(Driver, WebUITestBaseClass.Browser)).GetConfirmMessageAndClickYes();
        }

        /// <summary>
        /// Get confirm message and click No
        /// </summary>
        /// <returns>Confirm message</returns>
        public string GetConfirmMessageAndClickNo()
        {
            return (new ConfirmMessageSubPage(Driver, WebUITestBaseClass.Browser)).GetConfirmMessageAndClickNo();
        }

        /// <summary>
        /// Get List of language in Select Transcription Language in combox
        /// </summary>
        /// <param name="filename">file name</param>
        /// <returns>List of language</returns>
        public List<string> GetAllItemsOfSelectTranscriptionLangugeCombobox(string filename)
        {
            TestContext.Out.WriteLine("Get all languages in combobox");
            ClickSelectLanguageComboboxByFileName(filename);

            return GetAllItemsOfLangList();
        }

        /// <summary>
        /// Get all items in the list
        /// </summary>
        /// <returns></returns>
        public List<string> GetAllItemsOfLangList()
        {
            List<string> languages = new List<string>();
            foreach (BaseWebObject option in LanguageOptions)
            {
                languages.Add(option.GetAttribute("innerText"));
            }

            languages.Sort();
            return languages;
        }

        /// <summary>
        /// Fill the search text box in the language item search
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="searchText"></param>
        public void FillLanguageSearchText(string fileName, string searchText)
        {
            InputLanguageSearchBox(fileName).SendKeys(searchText);
        }

        /// <summary>
        /// Wait for the file icon class contains the expected value
        /// </summary>
        /// <param name="fileName">File name to wait for the icon</param>
        /// <param name="classExpectedContent">Class content to wait</param>
        /// <param name="maxWaitTime">Max wait times (5 seconds each waiting row)</param>
        public void WaitForFileIconClassContains(string fileName, string classExpectedContent, int maxWaitTime)
        {
            int iCount = 0;

            while (iCount < maxWaitTime)
            {
                iCount++;

                try
                {
                    // verify the audio and text icon
                    if (GetFileItemIconClassValue(fileName).Contains(classExpectedContent))
                    {
                        return;
                    }

                    // try to click the transcribe button if it is visible
                    ClickTranscribeLinkByFileName(fileName);
                }
                catch (StaleElementReferenceException)
                {
                    continue;
                }

                ThreadUtils.SleepMediumTime();

                Refresh();

                ThreadUtils.SleepShortTime();
            }
        }

        /// <summary>
        /// Wait for the audio file length visible
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="MAX_WAIT"></param>
        /// <returns>True if the audio length visible on time; otherwise, return False</returns>
        public bool WaitForAudioFileLengthVisible(string fileName, int MAX_WAIT = 50)
        {
            int iCount = 0;

            while (iCount < MAX_WAIT)
            {
                iCount++;

                try
                {
                    if (DivAudioLength(fileName).IsVisible)
                    {
                        return true;
                    }
                }
                catch (StaleElementReferenceException)
                {
                    continue;
                }

                ThreadUtils.SleepShortTime();

                Refresh();

                ThreadUtils.SleepShortTime();
            }

            return false;
        }

        /// <summary>
        /// Get the icon class value of the file item
        /// </summary>
        /// <param name="fileName">Name of the transcribe file</param>
        /// <returns>Value of class attribute</returns>
        public string GetFileItemIconClassValue(string fileName)
        {
            return SpanFileItemIcon(fileName).GetAttribute("class");
        }

        /// <summary>
        /// Check if the upload file already checked as transcribing (Select Language & Transcibe button disappeared)
        /// </summary>
        /// <param name="fileName">File Name to check</param>
        /// <returns>True is the file transcribing; False if not</returns>
        public bool IsUploadedFileTranscribing(string fileName)
        {
            return string.IsNullOrEmpty(DivTranscribeButtonTitle(fileName).Text.Trim());
        }

        /// <summary>
        /// Select the transcribe language and then click Transcibe link
        /// </summary>
        /// <param name="fileName">File name to transcribe</param>
        /// <param name="language">language want to set</param>
        public void SelectALanguageAndClickTranscribe(string fileName, string language)
        {
            SelectALanguageByText(fileName, language);
            ThreadUtils.SleepShortTime();

            ClickTranscribeLinkByFileName(fileName);
            ThreadUtils.SleepShortTime();
        }

        /// <summary>
        /// Click Transcribe link of a file
        /// </summary>
        /// <param name="filename">name of this file</param>
        public void ClickTranscribeLinkByFileName(string filename)
        {
            if (DivAudioLength(filename).IsVisible)
            {
                if (!IsUploadedFileTranscribing(filename))
                {
                    ScrollElementToView(DivTranscribeLink(filename));

                    DivTranscribeLink(filename).Click();
                }
            }
        }

        /// <summary>
        /// Click Combobox of a file
        /// </summary>
        /// <param name="filename">name of this file</param>
        public void ClickSelectLanguageComboboxByFileName(string filename)
        {
            ThreadUtils.SleepShortTime();
            ActionMouseClick(DivSelectLanguage(filename));
        }

        /// <summary>
        /// Select language for a file 
        /// </summary>
        /// <param name="filename">filename</param>
        /// <param name="language">language want to set</param>
        public void SelectALanguageByText(string filename, string language)
        {
            TestContext.Out.WriteLine($"Select \"{language}\"");
            if (!GetCurrentLanguageOfAFile(filename).Equals(language))
            {
                ClickSelectLanguageComboboxByFileName(filename);
                ActionMouseClick(CbxSelectLanguageItem(language));
                ThreadUtils.SleepShortTime();
            }
        }

        /// <summary>
        /// Count Number of Completely Uploaded File
        /// </summary>
        /// <returns>Number of Completely Uploaded File</returns>
        public int CountCompletelyUploadedFile()
        {
            TestContext.Out.WriteLine("Count ready upload files");
            string readyUploadedFile = "//*[@aria-hidden='false']//*[@role ='presentation' and contains(.,'Ready')]";
            return CountWebElement(readyUploadedFile);
        }

        /// <summary>
        /// Count All Combobox
        /// </summary>
        /// <returns>Number of select transcription language combobox</returns>
        public int CountSelectTranscriptionLanguageCombobox()
        {
            TestContext.Out.WriteLine("Count select language combox for ready upload files");
            string combobox = "//*[@aria-hidden='false']//*[@role ='presentation']//*[@role='combobox']";
            int comboboxNumber = CountWebElement(combobox);
            return comboboxNumber;
        }

        /// <summary>
        /// Verify Select Language combobox is displayed for a file
        /// </summary>
        /// <param name="filename"></param>
        public bool IsSelectLanguageDisplayedForAFile(string filename)
        {
            TestContext.Out.WriteLine($"Verify Select Transcription Language Combobox Is Displayed File {filename}");
            return DivSelectLanguage(filename).IsEnabled;
        }

        /// <summary>
        /// Verify Select Language combobox is displayed for a file
        /// </summary>
        /// <param name="filename">filename of file which user want to check</param>
        /// <returns>Language is selecting</returns>
        public string GetCurrentLanguageOfAFile(string filename)
        {
            TestContext.Out.WriteLine("Get current language");
            ThreadUtils.SleepShortTime();
            string selectingLanguage = DivSelectLanguage(filename).Text;
            return selectingLanguage;
        }

        /// <summary>
        /// Get file length of a file
        /// </summary>
        /// <param name="filename">name of this file</param>
        public string GetFileLengthForAFile(string filename)
        {
            try
            {
                return DivAudioLength(filename).Text;
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// Navigate to Upload page by click Upload Tab
        /// </summary>
        public void NavigateToUploadPage()
        {
            TestContext.Out.WriteLine("Navigate to Upload page");
            TabUpload.Click();
        }

        /// <summary>
        /// Select uploaded files with a specific file name
        /// </summary>
        /// <param name="filename">file name want to delete</param>
        public void SelectUploadFiles(List<string> fileNames)
        {
            // select the files
            IList<BaseWebObject> baseWebElements = new List<BaseWebObject>();
            foreach (string name in fileNames)
            {
                TestContext.Out.WriteLine($"Select file = {name}");
                baseWebElements.Add(SpanUploadedFileItem(name));
            }

            // delete them
            MultiSelectItemsByCtrl(baseWebElements);

            ThreadUtils.SleepShortTime();
        }

        /// <summary>
        /// Select uploaded files with a specific file name
        /// </summary>
        /// <param name="filename">file name want to delete</param>
        public void SelectTranscriptFiles(List<string> fileNames)
        {
            // select the files
            IList<BaseWebObject> baseWebElements = new List<BaseWebObject>();
            foreach (string name in fileNames)
            {
                TestContext.Out.WriteLine($"Select file = {name}");
                baseWebElements.Add(DivTranscribedFileItem(name));
            }

            // delete them
            MultiSelectItemsByCtrl(baseWebElements);

            ThreadUtils.SleepShortTime();
        }

        /// <summary>
        /// Select uploaded files with a specific file name
        /// </summary>
        /// <param name="filename">file name want to delete</param>
        public void SelectUploadedAndTranscriptFiles(List<string> uploadedFileNames, List<string> transcriptFileNames)
        {
            // select the files
            IList<BaseWebObject> baseWebElements = new List<BaseWebObject>();

            // select uploaded files
            foreach (string name in uploadedFileNames)
            {
                TestContext.Out.WriteLine($"Select file = {name}");
                baseWebElements.Add(SpanUploadedFileItem(name));
            }

            // select transcript files
            foreach (string name in transcriptFileNames)
            {
                TestContext.Out.WriteLine($"Select file = {name}");
                baseWebElements.Add(DivTranscribedFileItem(name));
            }

            // delete them
            MultiSelectItemsByCtrl(baseWebElements);

            ThreadUtils.SleepShortTime();
        }

        /// <summary>
        /// Click Delete button - trash icon
        /// </summary>
        public void ClickDeleteButton()
        {
            TestContext.Out.WriteLine("Click Detele button - Trash icon");
            ScrollElementToView(ButtonDelete);
            ButtonDelete.Click();
            ThreadUtils.SleepShortTime();
        }

        /// <summary>
        /// Delete files
        /// </summary>
        /// <param name="fileNames">list of file name</param>
        public void DeleteFiles(List<string> fileNames)
        {
            try
            {
                SelectUploadFiles(fileNames);
                ClickDeleteButton();

                (new ConfirmMessageSubPage(Driver, TestBase.WebUITestBaseClass.Browser)).BtnDialogConfirmYes.Click();
            }
            catch (Exception ex)
            {
                TestContext.Out.WriteLine("DeleteFiles ERR: {0}", ex.Message);
            }
        }

        /// <summary>
        /// Detect if the delete confirm message popup displayed for all the files
        /// </summary>
        /// <param name="fileNames">List of file names</param>
        /// <returns>True if all expected popups displayed</returns>
        public bool IsDeleteNotificationDisplayedForDeleteFiles(List<string> fileNames)
        {
            foreach (string fileName in fileNames)
            {
                if (!DivDeletingFileNotifyMsg(fileName).IsVisible)
                {
                    return false;
                }
            }

            return true;
        }

        #endregion
    }
}
