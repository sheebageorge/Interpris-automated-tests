using Automation.UI.Core.CommonUtilities;
using Automation.UI.Core.DataProcessing;
using Automation.UI.Core.Selenium.PageObjects.Interpris.Platform;
using Automation.UI.Core.Selenium.PageObjects.Interpris.Product;
using Automation.UI.Core.TestAttributes;
using Automation.UI.Core.TestBase;
using Automation.UI.Platform.TestAttributes;
using NUnit.Framework;
using System.Collections.Generic;

namespace Automation.UI.Platform.Test
{
    /// <summary>
    /// Test Suite for Access Portal Page feature
    /// </summary>
    [TestFixture]
    public class AccessPortalPageTest : PlatformTestBase
    {
        #region Test Cases
        [Test]
        [TestID(TestID.TC_ID_0003), StoryID(StoryID.SR_ID_002)]
        [Priority(PriorityLevel.Medium)]
        [TestCaseSource(typeof(DataProvider), "PrepareTestCases", new object[] { TestID.TC_ID_0003 })]
        public void TC_ACCESS_PORTAL_PAGE_VerifyAbleToAccessPortalPageWithCorrectURL(Dictionary<string, string> Data)
        {
            TestContext.Out.WriteLine("Start Test Case - {0}", TestID.TC_ID_0003);

            LoginPage loginPage = new LoginPage(Driver, PlatformBaseURL);
            HeaderSubPage headerSubPage = new HeaderSubPage(Driver, PlatformBaseURL);
            PortalPage portalPage = new PortalPage(Driver, PlatformBaseURL);

            headerSubPage.LogIn(Data["username"], Data["password"]);

            TestContext.Out.WriteLine("Go to Portal Page by URL");
            portalPage.Navigate();
            ThreadUtils.SleepShortTime();

            TestContext.Out.WriteLine("Verify Portal Page visible");
            Assert.IsTrue(portalPage.IsPageVisible(), "Portal Page not visible");

            TestContext.Out.WriteLine("End Test Case - {0}", TestID.TC_ID_0003);
        }

        [Test]
        [TestID(TestID.TC_ID_0004), StoryID(StoryID.SR_ID_002)]
        [Priority(PriorityLevel.Medium)]
        [TestCaseSource(typeof(DataProvider), "PrepareTestCases", new object[] { TestID.TC_ID_0004 })]
        public void TC_ACCESS_PORTAL_PAGE_VerifyAbleToAccessPortalPageByClickingOnTheTopMenuItem(
            Dictionary<string, string> Data)
        {
            TestContext.Out.WriteLine("Start Test Case - {0}", TestID.TC_ID_0004);

            LoginPage loginPage = new LoginPage(Driver, PlatformBaseURL);
            HeaderSubPage headerSubPage = new HeaderSubPage(Driver, PlatformBaseURL);
            PortalPage portalPage = new PortalPage(Driver, PlatformBaseURL);

            headerSubPage.LogIn(Data["username"], Data["password"]);

            TestContext.Out.WriteLine("Click on the top menu to go to Portal Page");
            headerSubPage.OpenPortalPageByTopMenuItem();
            ThreadUtils.SleepShortTime();

            TestContext.Out.WriteLine("Verify Portal Page visible");
            Assert.IsTrue(portalPage.IsPageVisible(), "Portal Page not visible");

            TestContext.Out.WriteLine("End Test Case - {0}", TestID.TC_ID_0004);
        }

        [Test]
        [TestID(TestID.TC_ID_0005), StoryID(StoryID.SR_ID_002)]
        [Priority(PriorityLevel.Medium)]
        [TestCaseSource(typeof(DataProvider), "PrepareTestCases", new object[] { TestID.TC_ID_0005 })]
        public void TC_ACCESS_PORTAL_PAGE_VerifyUnAbleToAccessPortalPageIfNotLogIn(Dictionary<string, string> Data)
        {
            TestContext.Out.WriteLine("Start Test Case - {0}", TestID.TC_ID_0005);

            LoginPage loginPage = new LoginPage(Driver, PlatformBaseURL);
            PortalPage portalPage = new PortalPage(Driver, PlatformBaseURL);

            TestContext.Out.WriteLine("Go to Portal Page by URL");
            portalPage.Navigate();
            ThreadUtils.SleepShortTime();

            TestContext.Out.WriteLine("Verify still staying in Login Page");
            Assert.IsTrue(loginPage.IsPageVisible(), "Login Page not visible");

            TestContext.Out.WriteLine("End Test Case - {0}", TestID.TC_ID_0005);
        }
        #endregion
    }
}
