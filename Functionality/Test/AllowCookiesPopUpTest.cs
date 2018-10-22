using Automation.UI.Core.CommonUtilities;
using Automation.UI.Core.DataProcessing;
using Automation.UI.Core.Selenium.PageObjects.Interpris.Platform;
using Automation.UI.Core.Selenium.PageObjects.Interpris.Transcription;
using Automation.UI.Core.TestAttributes;
using Automation.UI.Core.TestBase;
using Automation.UI.Core.TestLibraries;
using Automation.UI.Functionality.TestAttributes;
using NUnit.Framework;
using System.Collections.Generic;

namespace Automation.UI.Functionality.Test
{
    /// <summary>
    /// Test Suite for Allow Cookies Pop-Up feature
    /// </summary>
    [TestFixture]
    public class AllowCookiesPopUpTest : InterprisTestBase
    {
        #region Test Cases
        [Test]
        [TestID(TestID.TC_ID_0199), StoryID(StoryID.SR_ID_035)]
        [Priority(PriorityLevel.Highest)]
        [TestCaseSource(typeof(DataProvider), "PrepareTestCases", new object[] { TestID.TC_ID_0199 })]
        public void TC_ALLOW_COOKIES_VerifyAllowCookiesPopUpDisappearsWhenUserAccepts(Dictionary<string, string> Data)
        {
            TestContext.Out.WriteLine("Start Test Case - {0}", TestID.TC_ID_0199);

            // try to delete all cookies
            Driver.Manage().Cookies.DeleteAllCookies();

            // Verify sign in successfully with the activated account
            LoginPage loginPage = new LoginPage(Driver, InterprisBaseURL);
            HeaderSubPage headerSubPage = new HeaderSubPage(Driver, InterprisBaseURL);
            ViewAllFilesPage viewAllFilesPage = new ViewAllFilesPage(Driver, InterprisBaseURL);
            PortalPage portalPage = new PortalPage(Driver, PlatformBaseURL);

            // wait for the Allow Cookies Pop-Up in some seconds
            int maxWaitAllowCookiesPopUp = 50;

            // open landing page
            loginPage.Navigate();
            ThreadUtils.SleepShortTime();

            if (headerSubPage.WaitForHeaderText(HeaderSubPage.HOME_TEXT))
            {
                headerSubPage.LogOut();
                ThreadUtils.SleepShortTime();
            }

            // input sign in
            loginPage.InputLoginInfo(Data["username"], Data["password"]);
            ThreadUtils.SleepShortTime();

            TestContext.Out.WriteLine("Verify Allow Cookies Pop-up displayed");
            Assert.IsTrue(headerSubPage.WaitForAllowCookiesPanel(loginPage));

            TestContext.Out.WriteLine("Turn off Allow Cookies Pop-up");
            headerSubPage.WaitToCloseAllowCookiesPanel(loginPage);

            TestContext.Out.WriteLine("Verify Allow Cookies Pop-up NOT displayed");
            Assert.IsFalse(headerSubPage.WaitForAllowCookiesPanel(loginPage, maxWaitAllowCookiesPopUp));

            TestContext.Out.WriteLine("Try to logout and login again");
            headerSubPage.LogOut();
            ThreadUtils.SleepShortTime();

            // input sign in
            loginPage.InputLoginInfo(Data["username"], Data["password"]);
            ThreadUtils.SleepShortTime();

            TestContext.Out.WriteLine("Verify Allow Cookies Pop-up NOT displayed");
            Assert.IsFalse(headerSubPage.WaitForAllowCookiesPanel(loginPage, maxWaitAllowCookiesPopUp));

            TestContext.Out.WriteLine("Try to open a Transcript File or create a new one");
            ViewAllFilesUtils.OpenTranscriptFile(viewAllFilesPage,
                Data["test_file_folder"], Data["test_audio_file_3"], Data["test_audio_lang_3"]);

            TestContext.Out.WriteLine("Verify Allow Cookies Pop-up NOT displayed");
            Assert.IsFalse(headerSubPage.WaitForAllowCookiesPanel(loginPage, maxWaitAllowCookiesPopUp));

            // check credits
            TestContext.Out.WriteLine("Go to Portal page");
            portalPage.Navigate();
            ThreadUtils.SleepShortTime();

            TestContext.Out.WriteLine("Verify Portal Page visible");
            Assert.IsTrue(portalPage.IsPageVisible(), "Portal Page not visible");

            TestContext.Out.WriteLine("Verify Allow Cookies Pop-up NOT displayed");
            Assert.IsFalse(headerSubPage.WaitForAllowCookiesPanel(loginPage, maxWaitAllowCookiesPopUp));

            TestContext.Out.WriteLine("End Test Case - {0}", TestID.TC_ID_0199);
        }
        #endregion
    }
}
