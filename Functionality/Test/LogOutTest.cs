using Automation.UI.Core.CommonUtilities;
using Automation.UI.Core.DataProcessing;
using Automation.UI.Core.Selenium.PageObjects.Interpris.Platform;
using Automation.UI.Core.Selenium.PageObjects.Interpris.Transcription;
using Automation.UI.Core.TestAttributes;
using Automation.UI.Core.TestBase;
using Automation.UI.Functionality.TestAttributes;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Automation.UI.Functionality.Test
{
    /// <summary>
    /// Test Suite for Log Out feature
    /// </summary>
    [TestFixture]
    public class LogOutTest : TranscriptionTestBase
    {
        #region Test Cases
        [Test]
        [Ignore("Transcription homepage disabled currently")]
        [TestID(TestID.TC_ID_0068), StoryID(StoryID.SR_ID_008)]
        [Priority(PriorityLevel.Highest)]
        [TestCaseSource(typeof(DataProvider), "PrepareTestCases", new object[] { TestID.TC_ID_0068 })]
        public void TC_LOGOUT_VerifyLogInPageDisplayedAfterLogOutFromHomePage(Dictionary<string, string> Data)
        {
            TestContext.Out.WriteLine("Start Test Case - {0}", TestID.TC_ID_0068);
            TestContext.Out.WriteLine("End Test Case - {0}", TestID.TC_ID_0068);
        }

        [Test]
        [TestID(TestID.TC_ID_0069), StoryID(StoryID.SR_ID_008)]
        [Priority(PriorityLevel.Highest)]
        [TestCaseSource(typeof(DataProvider), "PrepareTestCases", new object[] { TestID.TC_ID_0069 })]
        public void TC_LOGOUT_VerifyLogInPageDisplayedAfterLogOutFromAllPage(Dictionary<string, string> Data)
        {
            TestContext.Out.WriteLine("Start Test Case - {0}", TestID.TC_ID_0069);

            ViewAllFilesPage viewAllFilesPage = new ViewAllFilesPage(Driver, TranscriptionBaseURL);

            TestContext.Out.WriteLine("Go to the landing page and sign in");
            viewAllFilesPage.LogIn(Data["username"], Data["password"]);

            TestContext.Out.WriteLine("Verify All tab displayed");
            Assert.IsTrue(viewAllFilesPage.IsTabActive(), "All tab not displayed.");

            TestContext.Out.WriteLine("Try to Log Out");
            viewAllFilesPage.LogOut();

            TestContext.Out.WriteLine("Verify if the Log In page visible");
            LoginPage loginPage = new LoginPage(Driver, TranscriptionBaseURL);
            Assert.IsTrue(loginPage.IsPageVisible(), "Cannot log out from All page!");

            TestContext.Out.WriteLine("End Test Case - {0}", TestID.TC_ID_0069);
        }

        [Test]
        [TestID(TestID.TC_ID_0071), StoryID(StoryID.SR_ID_008)]
        [Priority(PriorityLevel.Highest)]
        [TestCaseSource(typeof(DataProvider), "PrepareTestCases", new object[] { TestID.TC_ID_0071 })]
        public void TC_LOGOUT_VerifyLogInPageDisplayedAfterLogOutFromUploadPage(Dictionary<string, string> Data)
        {
            TestContext.Out.WriteLine("Start Test Case - {0}", TestID.TC_ID_0071);

            UploadPage updatePage = new UploadPage(Driver, TranscriptionBaseURL);

            TestContext.Out.WriteLine("Go to the landing page and sign in");
            updatePage.LogIn(Data["username"], Data["password"]);

            TestContext.Out.WriteLine("Navigate to the Upload Page");
            updatePage.TabUpload.Click();

            TestContext.Out.WriteLine("Verify Upload tab displayed");
            Assert.IsTrue(updatePage.IsTabActive(), "Upload tab not displayed.");

            TestContext.Out.WriteLine("Try to Log Out");
            updatePage.LogOut();

            TestContext.Out.WriteLine("Verify if the Log In page visible");
            LoginPage loginPage = new LoginPage(Driver, TranscriptionBaseURL);
            Assert.IsTrue(loginPage.IsPageVisible(), "Cannot log out from Upload page!");

            TestContext.Out.WriteLine("End Test Case - {0}", TestID.TC_ID_0071);
        }

        [Test]
        [TestID(TestID.TC_ID_0072), StoryID(StoryID.SR_ID_008)]
        [Priority(PriorityLevel.High)]
        [TestCaseSource(typeof(DataProvider), "PrepareTestCases", new object[] { TestID.TC_ID_0072 })]
        public void TC_LOGOUT_VerifyUnableToGetBackToLandingPageAfterLogOut(Dictionary<string, string> Data)
        {
            TestContext.Out.WriteLine("Start Test Case - {0}", TestID.TC_ID_0072);

            ViewAllFilesPage viewAllFilesPage = new ViewAllFilesPage(Driver, TranscriptionBaseURL);

            TestContext.Out.WriteLine("Go to the landing page and sign in");
            viewAllFilesPage.LogIn(Data["username"], Data["password"]);

            TestContext.Out.WriteLine("Verify All tab displayed");
            Assert.IsTrue(viewAllFilesPage.IsTabActive(), "All tab not displayed.");

            TestContext.Out.WriteLine("Try to Log Out");
            viewAllFilesPage.LogOut();

            TestContext.Out.WriteLine("Verify if the Log In page visible");
            LoginPage loginPage = new LoginPage(Driver, TranscriptionBaseURL);
            Assert.IsTrue(loginPage.IsPageVisible(), "Cannot log out from All page!");

            // sleep for a while before going back
            viewAllFilesPage.Back();
            ThreadUtils.SleepShortTime();

            TestContext.Out.WriteLine("Verify if the Log In page still visible");
            Assert.IsTrue(loginPage.IsPageVisible(), "The Log in page disappears after going back!");

            TestContext.Out.WriteLine("End Test Case - {0}", TestID.TC_ID_0072);
        }

        [Test]
        [Ignore("Transcription homepage disabled currently")]
        [TestID(TestID.TC_ID_0073), StoryID(StoryID.SR_ID_008)]
        [Priority(PriorityLevel.High)]
        [TestCaseSource(typeof(DataProvider), "PrepareTestCases", new object[] { TestID.TC_ID_0073 })]
        public void TC_LOGOUT_VerifyUnableToGetBackToHomePageAfterLogOut(Dictionary<string, string> Data)
        {
            TestContext.Out.WriteLine("Start Test Case - {0}", TestID.TC_ID_0073);

            TestContext.Out.WriteLine("End Test Case - {0}", TestID.TC_ID_0073);
        }

        [Test]
        [TestID(TestID.TC_ID_0074), StoryID(StoryID.SR_ID_008)]
        [Priority(PriorityLevel.Medium)]
        [TestCaseSource(typeof(DataProvider), "PrepareTestCases", new object[] { TestID.TC_ID_0074 })]
        public void TC_LOGOUT_VerifyLogOutATabWillLogOutAllOtherTabsInTheSameBrowser(Dictionary<string, string> Data)
        {
            TestContext.Out.WriteLine("Start Test Case - {0}", TestID.TC_ID_0074);

            ViewAllFilesPage viewAllFilesPage = new ViewAllFilesPage(Driver, TranscriptionBaseURL);

            TestContext.Out.WriteLine("Go to the landing page and sign in");
            viewAllFilesPage.LogIn(Data["username"], Data["password"]);

            TestContext.Out.WriteLine("Verify All tab displayed");
            Assert.IsTrue(viewAllFilesPage.IsTabActive(), "All tab not displayed.");

            TestContext.Out.WriteLine("Try to Open new tab");
            int currentTabCount = Driver.WindowHandles.Count;
            TryToOpenNewTab(Driver);
            ThreadUtils.SleepShortTime();

            Assert.IsTrue(currentTabCount + 1 == Driver.WindowHandles.Count,
                "Cannot open new tab {0}", Driver.WindowHandles.Count);

            TestContext.Out.WriteLine("Switch to new tab and navigate to Landing page");
            Driver.SwitchTo().Window(Driver.WindowHandles.Last());
            viewAllFilesPage.Navigate();

            TestContext.Out.WriteLine("Verify All tab displayed");
            Assert.IsTrue(viewAllFilesPage.IsTabActive(), "All tab not displayed.");

            TestContext.Out.WriteLine("Try to Log Out");
            viewAllFilesPage.LogOut();

            TestContext.Out.WriteLine("Verify if the Log In page visible");
            LoginPage loginPage = new LoginPage(Driver, TranscriptionBaseURL);
            Assert.IsTrue(loginPage.IsPageVisible(), "Cannot log out from homepage!");

            TestContext.Out.WriteLine("Go back to the first tab");
            Driver.SwitchTo().Window(Driver.WindowHandles.First());

            TestContext.Out.WriteLine("Verify All tab displayed");
            Assert.IsTrue(viewAllFilesPage.IsTabActive(), "All tab not displayed.");

            TestContext.Out.WriteLine("Refresh the page");
            viewAllFilesPage.Refresh();

            TestContext.Out.WriteLine("Verify if the Log In page visible");
            Assert.IsTrue(loginPage.IsPageVisible(), "Cannot log out from homepage!");

            TestContext.Out.WriteLine("End Test Case - {0}", TestID.TC_ID_0074);
        }
        #endregion
    }
}
