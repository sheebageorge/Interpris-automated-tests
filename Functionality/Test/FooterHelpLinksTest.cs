using Automation.UI.Core.CommonUtilities;
using Automation.UI.Core.DataProcessing;
using Automation.UI.Core.Selenium.PageObjects.Interpris.Platform;
using Automation.UI.Core.TestAttributes;
using Automation.UI.Core.TestBase;
using Automation.UI.Functionality.TestAttributes;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Automation.UI.Functionality.Test
{
    /// <summary>
    /// Test Suite for Footer Help link
    /// </summary>
    [TestFixture]
    public class FooterHelpLinksTest : TranscriptionTestBase
    {
        #region Test Cases
        [Test]
        [TestID(TestID.TC_ID_0180), StoryID(StoryID.SR_ID_026)]
        [Priority(PriorityLevel.Highest)]
        [TestCaseSource(typeof(DataProvider), "PrepareTestCases", new object[] { TestID.TC_ID_0180 })]
        public void TC_FOOTER_HELP_LINKS_VerifyTargetPageCorrectlyWhenClickOnHelpLinksTranscriptionPage(Dictionary<string, string> Data)
        {
            TestContext.Out.WriteLine("Start Test Case - {0}", TestID.TC_ID_0180);

            (new HeaderSubPage(Driver, TranscriptionTestBase.TranscriptionBaseURL)).LogIn(Data["username"], Data["password"]);

            Dictionary<string, string> helpLinks =
                new Dictionary<string, string>() {
                    { FooterSubPage.FOOTER_HELPFULS_LINK_LABEL_PRIVACY, Data["privacy_link"] },
                    { FooterSubPage.FOOTER_HELPFULS_LINK_LABEL_TERMS_CONDITIONS, Data["term_condition_link"] },
                    { FooterSubPage.FOOTER_HELPFULS_LINK_LABEL_LEGAL, Data["legal_link"] },
                    { FooterSubPage.FOOTER_HELPFULS_LINK_LABEL_CONTACT, Data["contact_us_link"] },
                    { FooterSubPage.FOOTER_HELPFULS_LINK_LABEL_COPYRIGHT, Data["copy_right_link"] }
                };

            TestContext.Out.WriteLine("Verify the current open page");
            Assert.IsTrue(Driver.Url.Contains(TranscriptionBaseURL));

            OpenAndVerifyHelpLinks(TranscriptionBaseURL, helpLinks);

            TestContext.Out.WriteLine("End Test Case - {0}", TestID.TC_ID_0180);
        }

        [Test]
        [TestID(TestID.TC_ID_0181), StoryID(StoryID.SR_ID_026)]
        [Priority(PriorityLevel.Highest)]
        [TestCaseSource(typeof(DataProvider), "PrepareTestCases", new object[] { TestID.TC_ID_0181 })]
        public void TC_FOOTER_HELP_LINKS_VerifyTargetPageCorrectlyWhenClickOnHelpLinksPortalPage(Dictionary<string, string> Data)
        {
            TestContext.Out.WriteLine("Start Test Case - {0}", TestID.TC_ID_0181);

            (new HeaderSubPage(Driver, TranscriptionTestBase.PlatformBaseURL)).LogIn(Data["username"], Data["password"]);

            Dictionary<string, string> helpLinks =
                new Dictionary<string, string>() {
                    { FooterSubPage.FOOTER_HELPFULS_LINK_LABEL_PRIVACY, Data["privacy_link"] },
                    { FooterSubPage.FOOTER_HELPFULS_LINK_LABEL_TERMS_CONDITIONS, Data["term_condition_link"] },
                    { FooterSubPage.FOOTER_HELPFULS_LINK_LABEL_LEGAL, Data["legal_link"] },
                    { FooterSubPage.FOOTER_HELPFULS_LINK_LABEL_CONTACT, Data["contact_us_link"] },
                    { FooterSubPage.FOOTER_HELPFULS_LINK_LABEL_COPYRIGHT, Data["copy_right_link"] }
                };

            TestContext.Out.WriteLine("Verify the current open page");
            Assert.IsTrue(Driver.Url.Contains(PlatformBaseURL));

            OpenAndVerifyHelpLinks(PlatformBaseURL, helpLinks);

            TestContext.Out.WriteLine("End Test Case - {0}", TestID.TC_ID_0181);
        }
        #endregion

        #region Methods
        /// <summary>
        /// Open the Transcription or Portal page to verify help links
        /// </summary>
        /// <param name="baseURL"></param>
        /// <param name="helpLinks"></param>
        public void OpenAndVerifyHelpLinks(string baseURL, Dictionary<string, string> helpLinks)
        {
            FooterSubPage footerSubPage = new FooterSubPage(Driver, baseURL);

            foreach (string key in helpLinks.Keys)
            {
                TestContext.Out.WriteLine("Test {0} link", key);

                int tabsCount = Driver.WindowHandles.Count;

                footerSubPage.AFooterHelpfulsLinkByLabel(key).Click();
                ThreadUtils.SleepShortTime();

                tabsCount++;

                TestContext.Out.WriteLine("Verify new tab is added");
                Assert.AreEqual(tabsCount, Driver.WindowHandles.Count, "No new tab is added");

                TestContext.Out.WriteLine("Switch to new tab");
                Driver.SwitchTo().Window(Driver.WindowHandles.Last());
                ThreadUtils.SleepShortTime();

                TestContext.Out.WriteLine("Verify new tab url is {0}", helpLinks[key]);

                Assert.IsTrue(Driver.Url.Contains(helpLinks[key]),
                    "New tab url is not correct:\n{0}\n{1}", Driver.Url, helpLinks[key]);

                Driver.Close();
                ThreadUtils.SleepShortTime();

                Driver.SwitchTo().Window(Driver.WindowHandles.First());
                ThreadUtils.SleepShortTime();
            }
        }
        #endregion
    }
}
