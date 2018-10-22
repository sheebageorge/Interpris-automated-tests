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
    /// Test Suite for Get Started Guide feature
    /// </summary>
    [TestFixture]
    public class GetStartedGuideTest : InterprisTestBase
    {
        #region Test Cases
        [Test]
        [TestID(TestID.TC_ID_0196), StoryID(StoryID.SR_ID_033)]
        [Priority(PriorityLevel.Highest)]
        [TestCaseSource(typeof(DataProvider), "PrepareTestCases", new object[] { TestID.TC_ID_0196 })]
        public void TC_GET_STARTED_GUIDE_VerifyOpenGettingStartedGuideInTranscriptionLandingPage(
            Dictionary<string, string> Data)
        {
            TestContext.Out.WriteLine("Start Test Case - {0}", TestID.TC_ID_0196);

            ViewAllFilesPage viewAllFilesPage = new ViewAllFilesPage(Driver, InterprisBaseURL);
            HeaderSubPage headerSubPage = new HeaderSubPage(Driver, InterprisBaseURL);

            TestContext.Out.WriteLine("Go to the landing page and sign in");
            viewAllFilesPage.LogIn(Data["username"], Data["password"]);

            TestContext.Out.WriteLine("Verify All tab displayed");
            Assert.IsTrue(viewAllFilesPage.IsTabActive(), "All tab not displayed.");

            TestContext.Out.WriteLine("Open Get Started Guide box");
            headerSubPage.OpenGetStartedGuideBox();
            ThreadUtils.SleepShortTime();

            GetStartedGuideSubPage getStartedGuideSubPage = new GetStartedGuideSubPage(Driver, InterprisBaseURL);

            TestContext.Out.WriteLine("Verify Get Started Guide box visible");
            Assert.IsTrue(getStartedGuideSubPage.IsGetStartedGuideBoxVisible(),
                "Get Started Guide box not visible");

            TestContext.Out.WriteLine("Click OK button the close the Get Started Guide box");
            getStartedGuideSubPage.BtnOK.Click();
            ThreadUtils.SleepShortTime();

            TestContext.Out.WriteLine("Verify Get Started Guide box invisible");
            Assert.IsTrue(!getStartedGuideSubPage.IsGetStartedGuideBoxVisible(),
                "Get Started Guide box not visible");

            TestContext.Out.WriteLine("Open Get Started Guide box");
            headerSubPage.OpenGetStartedGuideBox();
            ThreadUtils.SleepShortTime();

            TestContext.Out.WriteLine("Verify Get Started Guide box visible");
            Assert.IsTrue(getStartedGuideSubPage.IsGetStartedGuideBoxVisible(),
                "Get Started Guide box not visible");

            TestContext.Out.WriteLine("Click outside of the Guide box");
            getStartedGuideSubPage.ActionMouseClickAtOffset(getStartedGuideSubPage.DivContainerDialog, 1, 1);

            ThreadUtils.SleepShortTime();

            TestContext.Out.WriteLine("Verify Get Started Guide box invisible");
            Assert.IsTrue(!getStartedGuideSubPage.IsGetStartedGuideBoxVisible(),
                "Get Started Guide box not visible");

            TestContext.Out.WriteLine("End Test Case - {0}", TestID.TC_ID_0196);
        }

        [Test]
        [TestID(TestID.TC_ID_0197), StoryID(StoryID.SR_ID_033)]
        [Priority(PriorityLevel.Highest)]
        [TestCaseSource(typeof(DataProvider), "PrepareTestCases", new object[] { TestID.TC_ID_0197 })]
        public void TC_GET_STARTED_GUIDE_VerifyOpenGettingStartedGuideInTranscriptionEditorPage(
            Dictionary<string, string> Data)
        {
            TestContext.Out.WriteLine("Start Test Case - {0}", TestID.TC_ID_0197);

            ViewAllFilesPage viewAllFilesPage = new ViewAllFilesPage(Driver, InterprisBaseURL);
            HeaderSubPage headerSubPage = new HeaderSubPage(Driver, InterprisBaseURL);

            // log in the Transcription page and wait some seconds for page loading
            ViewAllFilesUtils.LogInAndWaitForAllPageLoad(viewAllFilesPage, Data["username"], Data["password"]);

            ViewAllFilesUtils.OpenTranscriptFile(viewAllFilesPage,
                Data["test_file_folder"], Data["test_audio_file_3"], Data["test_audio_lang_3"]);

            TestContext.Out.WriteLine("Open Get Started Guide box");
            headerSubPage.OpenGetStartedGuideBox();
            ThreadUtils.SleepShortTime();

            GetStartedGuideSubPage getStartedGuideSubPage = new GetStartedGuideSubPage(Driver, InterprisBaseURL);

            TestContext.Out.WriteLine("Verify Get Started Guide box visible");
            Assert.IsTrue(getStartedGuideSubPage.IsGetStartedGuideBoxVisible(),
                "Get Started Guide box not visible");

            TestContext.Out.WriteLine("Click OK button the close the Get Started Guide box");
            getStartedGuideSubPage.BtnOK.Click();
            ThreadUtils.SleepShortTime();

            TestContext.Out.WriteLine("Verify Get Started Guide box invisible");
            Assert.IsTrue(!getStartedGuideSubPage.IsGetStartedGuideBoxVisible(),
                "Get Started Guide box not visible");

            TestContext.Out.WriteLine("Open Get Started Guide box");
            headerSubPage.OpenGetStartedGuideBox();
            ThreadUtils.SleepShortTime();

            TestContext.Out.WriteLine("Verify Get Started Guide box visible");
            Assert.IsTrue(getStartedGuideSubPage.IsGetStartedGuideBoxVisible(),
                "Get Started Guide box not visible");

            TestContext.Out.WriteLine("Click outside of the Guide box");
            getStartedGuideSubPage.ActionMouseClickAtOffset(getStartedGuideSubPage.DivContainerDialog, 1, 1);

            ThreadUtils.SleepShortTime();

            TestContext.Out.WriteLine("Verify Get Started Guide box invisible");
            Assert.IsTrue(!getStartedGuideSubPage.IsGetStartedGuideBoxVisible(),
                "Get Started Guide box not visible");

            TestContext.Out.WriteLine("End Test Case - {0}", TestID.TC_ID_0197);
        }
        #endregion
    }
}
