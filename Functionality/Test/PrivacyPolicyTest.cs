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
    /// Test Suite for Privacy Policy feature
    /// </summary>
    [TestFixture]
    public class PrivacyPolicyTest : InterprisTestBase
    {
        #region Test Cases
        [Test]
        [TestID(TestID.TC_ID_0100), StoryID(StoryID.SR_ID_012)]
        [Priority(PriorityLevel.Highest)]
        public void TC_PRIVACY_POLICY_VerifyUserAgreesWithThePrivacyPolicy()
        {
            TestContext.Out.WriteLine("Start Test Case - {0}", TestID.TC_ID_0100);

            LoginPage loginPage = new LoginPage(Driver, InterprisBaseURL);

            TestContext.Out.WriteLine("Go to Login Page");
            loginPage.Navigate();

            TestContext.Out.WriteLine("Click Sign Up tab");
            loginPage.ASignUpTab.WaitAndClick();

            if (loginPage.InputPolicyAgreement.IsSelected == false)
            {
                TestContext.Out.WriteLine("Click to enable Privacy Policy checkbox");
                loginPage.InputPolicyAgreement.Click();
            }

            TestContext.Out.WriteLine("Verify Sign Up button is able to click");
            Assert.IsTrue(loginPage.ButtonSignUp.IsEnabled, "Sign Up button is not able to click.");

            TestContext.Out.WriteLine("End Test Case - {0}", TestID.TC_ID_0100);
        }

        [Test]
        [TestID(TestID.TC_ID_0101), StoryID(StoryID.SR_ID_012)]
        [Priority(PriorityLevel.Highest)]
        public void TC_PRIVACY_POLICY_VerifyUserDisagreesWithThePrivacyPolicy()
        {
            TestContext.Out.WriteLine("Start Test Case - {0}", TestID.TC_ID_0101);

            LoginPage loginPage = new LoginPage(Driver, InterprisBaseURL);

            TestContext.Out.WriteLine("Go to Login Page");
            loginPage.Navigate();

            TestContext.Out.WriteLine("Click Sign Up tab");
            loginPage.ASignUpTab.WaitAndClick();

            if (loginPage.InputPolicyAgreement.IsSelected == true)
            {
                TestContext.Out.WriteLine("Click to disable Privacy Policy checkbox");
                loginPage.InputPolicyAgreement.Click();
            }

            TestContext.Out.WriteLine("Verify Sign Up button is not able to click");
            Assert.IsFalse(loginPage.ButtonSignUp.IsEnabled, "Sign Up button is able to click.");

            TestContext.Out.WriteLine("End Test Case - {0}", TestID.TC_ID_0101);
        }


        [Test]
        [Ignore("Currently disabled due to covered by test case UL-1652 and UL-1653")]
        [TestID(TestID.TC_ID_0102), StoryID(StoryID.SR_ID_012)]
        [Priority(PriorityLevel.Highest)]
        [TestCaseSource(typeof(DataProvider), "PrepareTestCases", new object[] { TestID.TC_ID_0102 })]
        public void TC_PRIVACY_POLICY_VerifyUserSeesThePrivacyPolicy(Dictionary<string, string> Data)
        {
            int tabsCount = 0;

            TestContext.Out.WriteLine("Start Test Case - {0}", TestID.TC_ID_0102);

            LoginPage loginPage = new LoginPage(Driver, InterprisBaseURL);

            TestContext.Out.WriteLine("Go to Login Page");
            loginPage.Navigate();

            TestContext.Out.WriteLine("Click Sign Up tab");
            loginPage.ASignUpTab.WaitAndClick();

            tabsCount = Driver.WindowHandles.Count;

            TestContext.Out.WriteLine("Click Privacy Policy link");
            loginPage.APolicyAgreement.Click();
            tabsCount++;

            TestContext.Out.WriteLine("Verify new tab is added");
            Assert.AreEqual(tabsCount, Driver.WindowHandles.Count, "No new tab is added");

            TestContext.Out.WriteLine("Switch to new tab");
            Driver.SwitchTo().Window(Driver.WindowHandles.Last());

            TestContext.Out.WriteLine("Verify new tab url is {0}", Data["privacy_policy_url"]);
            ThreadUtils.SleepMediumTime();

            Assert.IsTrue(Driver.Url.Contains(Data["privacy_policy_url"]), "New tab url is not correct");

            TestContext.Out.WriteLine("End Test Case - {0}", TestID.TC_ID_0102);
        }
        #endregion Test Cases
    }
}
