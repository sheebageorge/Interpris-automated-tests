using OpenQA.Selenium;
using System.Collections.Generic;
using NUnit.Framework;
using System;
using Automation.UI.Core.CommonUtilities;
using Automation.UI.Core.Selenium.PageObjects.Interpris.Platform;
using Automation.UI.Core.TestBase;
using Automation.UI.Core.DesktopAutomation.OpenFileDialog;

namespace Automation.UI.Core.Selenium.PageObjects.Interpris.Product
{
    /// <summary>
    /// Wrapper class of Views Page
    /// contains all elements and actions for the Views Page
    /// </summary>
    public class DataSourcesPage : BasePage
    {

        public const string IMPORT_TYPE_CSV = "csv";
        public const string IMPORT_TYPE_EXCEL = "excel";
        public const int MAX_WAIT_FOR_IN_PROGRESS_FILE = 900; // wait uploading file for 15 minutes

        #region UI Objects
        private readonly string divPageName = "//div/div[@class=\"isoLeft\"]/span[text()=\"Data Sources\"]";
        private readonly string divLabelCSV = "//div[@class=\"btn-upload-area\"]//label[text()=\"CSV\"]";
        private readonly string divLabelExcel = "//div[@class=\"btn-upload-area\"]//label[text()=\"Excel\"]";
        private readonly string btnUpload = "//button[@data-tut=\"upload_button\"]";
        private readonly string btnCancel = "//button[@data-tut=\"import-click-tour\"]/../button";
        private readonly string btnImport = "//button[@data-tut=\"import-click-tour\"]";
        private readonly string divImportedData = "//div[@data-tut=\"dropdown-tour\"]";

        #endregion

        //descendant::

        public DataSourcesPage(IWebDriver driver, string baseURL) : base(driver, baseURL, PageConstants.INTERPRIS_PAGE_VIEW_URL) { }

        #region Properties
        public BaseWebObject DivPageName => FindWebElement(divPageName, true);
        public BaseWebObject DivLabelCSV => FindWebElement(divLabelCSV, true);
        public BaseWebObject DivLabelExcel => FindWebElement(divLabelExcel, true);
        public BaseWebObject DivImportedData => FindWebElement(divImportedData, true);
        public BaseWebObject BtnUpload => FindWebElement(btnUpload, true);
        public BaseWebObject ButtonCancel => FindWebElement(btnCancel, true);
        public BaseWebObject ButtonImport => FindWebElement(btnImport, true);
        #endregion

        #region Methods
        /// <summary>
        /// Check if the DataSources page is active & displayed
        /// </summary>
        /// <returns>True if the page displayed; otherwise, False</returns>
        public bool IsPageVisible()
        {
            return DivPageName.IsVisible;
        }

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
        /// Upload a file in Upload File Tab
        /// </summary>
        /// <param name="browserType">Type of browser</param>
        /// <param name="folderPath">Folder of files to upload</param>
        /// <param name="fileName">File Name</param>
        public void UploadOneFile(string browserType, string folderPath, string fileName)
        {
            TestContext.Out.WriteLine("Importing file {0}", fileName);

            if (!WebUITestBaseClass.BrowserStackEnabled)
            {
                TestContext.Out.WriteLine("Click Import");
                BtnUpload.WaitAndClick();

                ThreadUtils.SleepShortTime();

                TestContext.Out.WriteLine("Select type csv");               
                DivLabelCSV.WaitAndClick();

                WebOpenFileDialog openFileDialog = WebOpenFileDialog.GetOpenDialog(WebUITestBaseClass.Browser);

                TestContext.Out.WriteLine("Select file {0} to upload", fileName);
                openFileDialog.SelectAFileByName(folderPath, fileName);
            }
            else
            {
                //dsPage.UploadFile(folderPath, fileName);
            }

            ThreadUtils.SleepShortTime();
        }
        #endregion
    }
}
