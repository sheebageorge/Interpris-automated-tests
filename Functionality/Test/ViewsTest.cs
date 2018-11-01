using Automation.UI.Core.CommonUtilities;
using Automation.UI.Core.DataProcessing;
using Automation.UI.Core.Selenium;
using Automation.UI.Core.Selenium.PageObjects.Interpris.Platform;
using Automation.UI.Core.Selenium.PageObjects.Interpris.Product;
using Automation.UI.Core.TestAttributes;
using Automation.UI.Core.TestBase;
using Automation.UI.Functionality.TestAttributes;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Automation.UI.Functionality.Test
{
    /// <summary>
    /// Test Suite for views
    /// </summary>
    [TestFixture]
    public class ViewsTest : InterprisTestBase
    {
        #region Test Cases
        [Test]
        [TestID(TestID.TC_ID_0050), StoryID(StoryID.SR_ID_003)]
        [Priority(PriorityLevel.High)]
        [TestCaseSource(typeof(DataProvider), "PrepareTestCases", new object[] { TestID.TC_ID_0001 })]
        public void TC_VIEW_DeleteDefaultView(Dictionary<string, string> Data)
        {
            TestContext.Out.WriteLine("Start Test Case - {0}", TestID.TC_ID_0050);

            ViewsPage viewsPage = new ViewsPage(Driver, InterprisBaseURL);
            viewsPage.LogIn(Data["username"], Data["password"]);
            ActivateView();
            Assert.IsTrue(viewsPage.IsPageVisible());

            TestContext.Out.WriteLine("Click delete icon and cancel delete");

            TestContext.Out.WriteLine("End Test Case - {0}", TestID.TC_ID_0050);
        }
        #endregion

        #region Test Cases
        [Test]
        [TestID(TestID.TC_ID_0051), StoryID(StoryID.SR_ID_003)]
        [Priority(PriorityLevel.High)]
        [TestCaseSource(typeof(DataProvider), "PrepareTestCases", new object[] { TestID.TC_ID_0001 })]
        public void TC_VIEW_CreateViewManually(Dictionary<string, string> Data)
        {
            TestContext.Out.WriteLine("Start Test Case - {0}", TestID.TC_ID_0051);
            ViewsPage viewsPage = new ViewsPage(Driver, InterprisBaseURL);
            viewsPage.LogIn(Data["username"], Data["password"]);
            ActivateView();
            Assert.IsTrue(viewsPage.IsPageVisible());
            viewsPage.CreateView();
            TestContext.Out.WriteLine("End Test Case - {0}", TestID.TC_ID_0051);
        }

        [Test]
        [TestID(TestID.TC_ID_0050), StoryID(StoryID.SR_ID_003)]
        [Priority(PriorityLevel.High)]
        [TestCaseSource(typeof(DataProvider), "PrepareTestCases", new object[] { TestID.TC_ID_0001 })]
        public void TC_VIEW_RenameView(Dictionary<string, string> Data)
        {
            TestContext.Out.WriteLine("Start Test Case - {0}", TestID.TC_ID_0050);
            LoginPage loginPage = new LoginPage(Driver, InterprisBaseURL);
            NavigatorPage navigatorPage = new NavigatorPage(Driver, InterprisBaseURL);
            ViewsPage viewsPage = new ViewsPage(Driver, InterprisBaseURL);
            viewsPage.LogIn(Data["username"], Data["password"]);
            TestContext.Out.WriteLine("Click on the menu - view");
            navigatorPage.ActivateMenu(NavigatorPage.MENU_VIEWS);
            Assert.IsTrue(viewsPage.IsPageVisible());
            viewsPage.RenameView();
            TestContext.Out.WriteLine("End Test Case - {0}", TestID.TC_ID_0050);
        }
        #endregion
        #region Public methods

        public void ActivateView()
        {
            NavigatorPage navigatorPage = new NavigatorPage(Driver, InterprisBaseURL);
            TestContext.Out.WriteLine("Click on the menu - view");
            navigatorPage.ActivateMenu(NavigatorPage.MENU_VIEWS);
            
        }
        #endregion
    }
}
