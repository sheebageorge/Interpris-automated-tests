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
    public class DataSourcesPage: BasePage
    {
      
        public const string IMPORT_TYPE_CSV = "csv";
        public const string IMPORT_TYPE_EXCEL = "excel";

        #region UI Objects
        private readonly string divPageName = "//div/div[@class=\"isoLeft\"]/span[text()=\"Data Sources\"]";
        private readonly string divLabelCSV = "//div[@class=\"btn-upload-area\"]//label[text()=\"CSV\"]";
        private readonly string divLabelExcel = "//div[@class=\"btn-upload-area\"]//label[text()=\"Excel\"]";
        private const string btnImport = "//button[@data-tut=\"upload_button\"]";
        private const string dialogImportReview = "//*[@id=\"rcDialogTitle6\"]";

        #endregion

        //descendant::

        public DataSourcesPage(IWebDriver driver, string baseURL) : base(driver, baseURL, PageConstants.INTERPRIS_PAGE_VIEW_URL) { }

        #region Properties
        public BaseWebObject DivPageName => FindWebElement(divPageName, true);
        public BaseWebObject DivLabelCSV => FindWebElement(divLabelCSV, true);
        public BaseWebObject DivLabelExcel => FindWebElement(divLabelExcel, true);
        public BaseWebObject DialogImportReview => FindWebElement(dialogImportReview, true);
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
        /// Import File
        /// </summary>
        /// <param name="fileNames">list of file name</param>
        public void ImportFile(string type)
        {
            try
            {
                ClickImportButton();
                SelectImportType(type);
                WaitForImportLoaded();
            }
            catch (Exception ex)
            {
                TestContext.Out.WriteLine("ImportFile ERR: {0}", ex.Message);
            }
        }

        public void WaitForImportLoaded(int MAX_WAIT = 50)
        {
            ThreadUtils.SleepShortTime();

            int iCount = 0;
            while (string.IsNullOrEmpty(DialogImportReview.Text.Trim()) && iCount < MAX_WAIT)
            {
                iCount++;
                ThreadUtils.SleepVeryShortTime();
            }
        }
        
        /// <summary>
        /// Click Import button
        /// </summary>
        public void ClickImportButton()
        {
            TestContext.Out.WriteLine("Click Import button");
            ButtonImport.Click();
            ThreadUtils.SleepShortTime();
        }

        /// <summary>
        /// Click Import button
        /// </summary>
        public void SelectImportType(string type)
        {
            TestContext.Out.WriteLine("Select import type : {0}", type);

            switch (type)
            {
                case IMPORT_TYPE_EXCEL:
                    DivLabelExcel.Click();
                    break;
                case IMPORT_TYPE_CSV:
                case "default":                    
                    DivLabelCSV.Click();
                    WebOpenFileDialog openFileDialog = WebOpenFileDialog.GetOpenDialog(WebUITestBaseClass.Browser);
                    string folderPath = "C:\\WorkDir\\source\\Interpris\\Automation.UI\\Functionality\\Data";
                    string fileName = "DataMappings.csv";
                    TestContext.Out.WriteLine("Select file {0} to upload", fileName);
                    openFileDialog.SelectAFileByName(folderPath, fileName);
                    break;
            }
            ThreadUtils.SleepLongTime();
        }

        

        #endregion
    }
}
