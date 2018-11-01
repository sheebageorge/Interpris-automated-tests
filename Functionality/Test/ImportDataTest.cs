using Automation.UI.Core.CommonUtilities;
using Automation.UI.Core.DataProcessing;
using Automation.UI.Core.Selenium;
using Automation.UI.Core.Selenium.PageObjects.Interpris.Platform;
using Automation.UI.Core.Selenium.PageObjects.Interpris.Product;
using Automation.UI.Core.TestAttributes;
using Automation.UI.Core.TestBase;
using Automation.UI.Functionality.TestAttributes;
using NUnit.Framework;
using Automation.UI.Core.TestLibraries;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Automation.UI.Functionality.Test
{
    /// <summary>
    /// Test Suite for views
    /// </summary>
    [TestFixture]
    public class ImportDataTest : InterprisTestBase
    {
        #region Test Cases
        [Test]
        [TestID(TestID.TC_ID_0100), StoryID(StoryID.SR_ID_002)]
        [Priority(PriorityLevel.High)]
        [TestCaseSource(typeof(DataProvider), "PrepareTestCases", new object[] { TestID.TC_ID_0100 })]
        public void TC_IMPORT_cancel(Dictionary<string, string> Data)
        {
            TestContext.Out.WriteLine("Start Test Case - {0}", TestID.TC_ID_0050);

            DataSourcesPage dsPage = new DataSourcesPage(Driver, InterprisBaseURL);

            // Verify sign in successfully with the activated account
            dsPage.LogIn(Data["username"], Data["password"]);
            ActivateView(dsPage);

            TestContext.Out.WriteLine("Click Import and cancel immidiately");
            dsPage.BtnUpload.WaitAndClick();
            dsPage.ButtonCancel.WaitAndClick();

            TestContext.Out.WriteLine("Click Import and cancel");
            dsPage.UploadOneFile(Browser, Data["test_file_folder"], Data["test_file_name"]);
            dsPage.WaitForElementVisible(dsPage.DivImportedData);
            dsPage.ButtonCancel.WaitAndClick();
            
            TestContext.Out.WriteLine("End Test Case - {0}", TestID.TC_ID_0100);
        }

        [Test]
        [TestID(TestID.TC_ID_0101), StoryID(StoryID.SR_ID_002)]
        [Priority(PriorityLevel.High)]
        [TestCaseSource(typeof(DataProvider), "PrepareTestCases", new object[] { TestID.TC_ID_0101 })]
        public void TC_IMPORT_AllowSingleFileSelection(Dictionary<string, string> Data)
        {
            TestContext.Out.WriteLine("Start Test Case - {0}", TestID.TC_ID_0050);

            DataSourcesPage dsPage = new DataSourcesPage(Driver, InterprisBaseURL);
            // Verify sign in successfully with the activated account
            dsPage.LogIn(Data["username"], Data["password"]);
            ActivateView(dsPage);

            TestContext.Out.WriteLine("Click Import and cancel");
            Assert.IsTrue(dsPage.IsMultiSelectEnabled() == 0);
            TestContext.Out.WriteLine("End Test Case - {0}", TestID.TC_ID_0101);
        }
        #endregion

        #region Public methods
        public void ActivateView(DataSourcesPage dsPage)
        {
            NavigatorPage navigatorPage = new NavigatorPage(Driver, InterprisBaseURL);
            TestContext.Out.WriteLine("Click on the menu - view");
            navigatorPage.ActivateMenu(NavigatorPage.MENU_DATA_SOURCES);
            Assert.IsTrue(dsPage.IsPageVisible());
        }
        #endregion
    }
}
