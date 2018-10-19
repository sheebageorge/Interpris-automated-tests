using Automation.UI.Core.CommonUtilities;
using Automation.UI.Core.DataProcessing;
using Automation.UI.Core.Selenium.PageObjects.Interpris.Platform;
using Automation.UI.Core.Selenium.PageObjects.Interpris.Transcription;
using Automation.UI.Core.TestAttributes;
using Automation.UI.Core.TestBase;
using Automation.UI.Core.TestLibraries;
using Automation.UI.Functionality.TestAttributes;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Automation.UI.Functionality.Test
{
    /// <summary>
    /// Test Suite for Redirect To Correct Page After LogOut feature
    /// </summary>
    [TestFixture]
    public class RedirectToCorrectPageAfterLogOutTest : TranscriptionTestBase
    {
        #region Test Cases
        [Test]
        [TestID(TestID.TC_ID_0200), StoryID(StoryID.SR_ID_036)]
        [Priority(PriorityLevel.Medium)]
        [TestCaseSource(typeof(DataProvider), "PrepareTestCases", new object[] { TestID.TC_ID_0200 })]
        public void TC_REDIRECT_TO_CORRECT_PAGE_VerifyRedirectToPlatformLoginPageAfterLogOutAndAccessAgain(
            Dictionary<string, string> Data)
        {
            TestContext.Out.WriteLine("Start Test Case - {0}", TestID.TC_ID_0200);

            ViewAllFilesPage viewAllFilesPage = new ViewAllFilesPage(Driver, TranscriptionBaseURL);
            LoginPage loginPage = new LoginPage(Driver, TranscriptionBaseURL);
            LandingPage landingPage = new LandingPage(Driver, TranscriptionBaseURL);

            string platformLoginPageURL = new Uri(new Uri(PlatformBaseURL), "login").ToString();

            TestContext.Out.WriteLine("Go to the landing page and sign in");
            ViewAllFilesUtils.LogInAndWaitForAllPageLoad(viewAllFilesPage, Data["username"], Data["password"]);

            TestContext.Out.WriteLine("Try to Log Out");
            viewAllFilesPage.LogOut();
            ThreadUtils.SleepShortTime();

            TestContext.Out.WriteLine("Verify if the Log In page visible");
            Assert.IsTrue(loginPage.IsPageVisible(), "Cannot log out from All page!");

            TestContext.Out.WriteLine("Verify the Platform Login page URL");
            Assert.IsTrue(Driver.Url == platformLoginPageURL,
                "Log in URL not correct {0} not {1}", Driver.Url, platformLoginPageURL);

            TestContext.Out.WriteLine("Navigate to Transcription Login page");
            loginPage.Navigate();
            ThreadUtils.SleepShortTime();

            TestContext.Out.WriteLine("Verify if the Log In page visible");
            Assert.IsTrue(loginPage.IsPageVisible(), "Cannot log out from All page!");

            TestContext.Out.WriteLine("Verify the Platform Login page URL");
            Assert.IsTrue(Driver.Url == platformLoginPageURL,
                "Log in URL not correct {0} not {1}", Driver.Url, platformLoginPageURL);

            TestContext.Out.WriteLine("Navigate to Transcription Landing page");
            landingPage.Navigate();
            ThreadUtils.SleepShortTime();

            TestContext.Out.WriteLine("Verify if the Log In page visible");
            Assert.IsTrue(loginPage.IsPageVisible(), "Cannot log out from All page!");

            TestContext.Out.WriteLine("Verify the Platform Login page URL");
            Assert.IsTrue(Driver.Url == platformLoginPageURL,
                "Log in URL not correct {0} not {1}", Driver.Url, platformLoginPageURL);

            TestContext.Out.WriteLine("End Test Case - {0}", TestID.TC_ID_0200);
        }

        [Test]
        [TestID(TestID.TC_ID_0201), StoryID(StoryID.SR_ID_036)]
        [Priority(PriorityLevel.Medium)]
        [TestCaseSource(typeof(DataProvider), "PrepareTestCases", new object[] { TestID.TC_ID_0201 })]
        public void TC_REDIRECT_TO_CORRECT_PAGE_VerifyRedirectToPlatformLoginPageAfterLogOut(
            Dictionary<string, string> Data)
        {
            TestContext.Out.WriteLine("Start Test Case - {0}", TestID.TC_ID_0201);

            ViewAllFilesPage viewAllFilesPage = new ViewAllFilesPage(Driver, TranscriptionBaseURL);
            LoginPage loginPage = new LoginPage(Driver, TranscriptionBaseURL);
            LandingPage landingPage = new LandingPage(Driver, TranscriptionBaseURL);

            PortalPage portalPage = new PortalPage(Driver, PlatformBaseURL);
            HeaderSubPage headerSubPage = new HeaderSubPage(Driver, PlatformBaseURL);

            string platformLoginPageURL = new Uri(new Uri(PlatformBaseURL), "login").ToString();

            TestContext.Out.WriteLine("Go to the landing page and sign in");
            ViewAllFilesUtils.LogInAndWaitForAllPageLoad(viewAllFilesPage, Data["username"], Data["password"]);

            TestContext.Out.WriteLine("Try to Log Out");
            viewAllFilesPage.LogOut();
            ThreadUtils.SleepShortTime();

            TestContext.Out.WriteLine("Verify if the Log In page visible");
            Assert.IsTrue(loginPage.IsPageVisible(), "Cannot log out from All page!");

            TestContext.Out.WriteLine("Verify the Platform Login page URL");
            Assert.IsTrue(Driver.Url == platformLoginPageURL,
                "Log in URL not correct {0} not {1}", Driver.Url, platformLoginPageURL);

            TestContext.Out.WriteLine("Re-login to the landing page");
            ViewAllFilesUtils.LogInAndWaitForAllPageLoad(viewAllFilesPage, Data["username"], Data["password"]);

            TestContext.Out.WriteLine("Verify the landing page URL");
            Assert.IsTrue(Driver.Url == landingPage.GetURL(),
                "Log in URL not correct {0} not {1}", Driver.Url, landingPage.GetURL());

            TestContext.Out.WriteLine("Try to open a Transcript File or create a new one");
            ViewAllFilesUtils.OpenTranscriptFile(viewAllFilesPage,
                Data["test_file_folder"], Data["test_audio_file_3"], Data["test_audio_lang_3"]);

            TestContext.Out.WriteLine("Try to Log Out");
            viewAllFilesPage.LogOut();
            ThreadUtils.SleepShortTime();

            TestContext.Out.WriteLine("Verify if the Log In page visible");
            Assert.IsTrue(loginPage.IsPageVisible(), "Cannot log out from All page!");

            TestContext.Out.WriteLine("Verify the Platform Login page URL");
            Assert.IsTrue(Driver.Url == platformLoginPageURL,
                "Log in URL not correct {0} not {1}", Driver.Url, platformLoginPageURL);

            TestContext.Out.WriteLine("Re-login to the transcription page");
            landingPage.LogIn(Data["username"], Data["password"]);

            TestContext.Out.WriteLine("Verify the transcription landing page URL");
            Assert.IsTrue(Driver.Url == landingPage.GetURL(),
                "Log in URL not correct {0} not {1}", Driver.Url, landingPage.GetURL());

            // check credits
            TestContext.Out.WriteLine("Go to Portal page to check reserve credits");
            portalPage.Navigate();
            ThreadUtils.SleepShortTime();

            TestContext.Out.WriteLine("Verify Portal Page visible");
            Assert.IsTrue(portalPage.IsPageVisible(), "Portal Page not visible");

            TestContext.Out.WriteLine("Verify the Platform page URL");
            Assert.IsTrue(Driver.Url == portalPage.GetURL(),
                "Log in URL not correct {0} not {1}", Driver.Url, portalPage.GetURL());

            TestContext.Out.WriteLine("Try to Log Out");
            headerSubPage.LogOut();
            ThreadUtils.SleepShortTime();

            TestContext.Out.WriteLine("Verify the Platform Login page URL");
            Assert.IsTrue(Driver.Url == platformLoginPageURL,
                "Log in URL not correct {0} not {1}", Driver.Url, platformLoginPageURL);

            TestContext.Out.WriteLine("End Test Case - {0}", TestID.TC_ID_0201);
        }
        #endregion
    }
}
