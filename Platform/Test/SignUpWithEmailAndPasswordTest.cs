using Automation.UI.Core.CommonUtilities;
using Automation.UI.Core.DataProcessing;
using Automation.UI.Core.Selenium.PageObjects.Interpris.Platform;
using Automation.UI.Core.TestAttributes;
using Automation.UI.Core.TestBase;
using Automation.UI.Core.TestLibraries;
using Automation.UI.Platform.TestAttributes;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Automation.UI.Platform.Test
{
    /// <summary>
    /// Test Suite for Sign up with email and password feature
    /// </summary>
    [TestFixture]
    public class SignUpWithEmailAndPasswordTest : PlatformTestBase
    {
        #region Test Cases
        [Test]
        [TestID(TestID.TC_ID_0018), StoryID(StoryID.SR_ID_005)]
        [Priority(PriorityLevel.Highest)]
        [TestCaseSource(typeof(DataProvider), "PrepareTestCases", new object[] { TestID.TC_ID_0018 })]
        public void TC_SIGN_UP_WITH_EMAIL_AND_PASS_VerifyLockScreenDisplayCorrectlyLogInAndSignUp(Dictionary<string, string> Data)
        {
            TestContext.Out.WriteLine($"Start Test Case - {TestID.TC_ID_0018}");

            LoginPage loginPage = new LoginPage(Driver, PlatformBaseURL);
            loginPage.Navigate();
            loginPage.ALogInTab.WaitAndClick(30);

            Assert.IsTrue(loginPage.DivTabContainer.IsVisible, "Log in tab and Sign up tab not displayed");
            Assert.IsTrue(loginPage.ALogInTab.IsVisible, "Log in tab not displayed");
            Assert.IsTrue(loginPage.ASignUpTab.IsVisible, "Sign up tab not displayed");

            loginPage.ASignUpTab.Click();
            Assert.IsTrue(loginPage.DivShowPassword.IsVisible, "Show password button not displayed");

            VerifyPrivacyPolicyAndTermsConditions(loginPage, Data["privacy_policy_url"], Data["terms_and_conditions_url"]);

            TestContext.Out.WriteLine($"End Test Case - {TestID.TC_ID_0018}");
        }

        [Test]
        [TestID(TestID.TC_ID_0019), StoryID(StoryID.SR_ID_005)]
        [Priority(PriorityLevel.Highest)]
        [TestCaseSource(typeof(DataProvider), "PrepareTestCases", new object[] { TestID.TC_ID_0019 })]
        public void TC_SIGN_UP_WITH_EMAIL_AND_PASS_VerifyLockScreenDisplayCorrectlySignUp(Dictionary<string, string> Data)
        {
            TestContext.Out.WriteLine($"Start Test Case - {TestID.TC_ID_0019}");

            LoginPage loginPage = new LoginPage(Driver, PlatformBaseURL + Data["sign_up_only_url"]);
            loginPage.Navigate();
            loginPage.InputEmail.WaitAndClick(30);

            Assert.IsTrue(loginPage.DivTabContainer == null, "Log in tab and Sign up tab not displayed");

            Assert.IsTrue(loginPage.DivShowPassword.IsVisible, "Show password button not displayed");

            VerifyPrivacyPolicyAndTermsConditions(loginPage, Data["privacy_policy_url"], Data["terms_and_conditions_url"]);

            TestContext.Out.WriteLine($"End Test Case - {TestID.TC_ID_0019}");
        }


        [Test]
        [TestID(TestID.TC_ID_0020), StoryID(StoryID.SR_ID_005)]
        [Priority(PriorityLevel.Highest)]
        [TestCaseSource(typeof(DataProvider), "PrepareTestCases", new object[] { TestID.TC_ID_0020 })]
        public void TC_SIGN_UP_WITH_EMAIL_AND_PASS_VerifyProvidePersonalDetailsDisplayWhenClickSignUp(Dictionary<string, string> Data)
        {
            TestContext.Out.WriteLine($"Start Test Case - {TestID.TC_ID_0020}");

            string email = RandomUtils.GetRandomEmailAddress(Data["email_prefix"], Data["email_domain"]);
            string password = email;

            SignUpStepTwoSubPage signUpStepTwoSubPage = new SignUpStepTwoSubPage(Driver);
            SignUpStepThreeSubPage signUpStepThreeSubPage = new SignUpStepThreeSubPage(Driver);

            TestContext.Out.WriteLine("Go to core page and Sign up with valid fields.");
            LoginPage loginPage = new LoginPage(Driver, PlatformBaseURL);
            loginPage.Navigate();
            loginPage.InputSignUpInfo(email, password);

            TestContext.Out.WriteLine("Vefiry Step 2 Provide personal details is displayed.");
            PlatformUtils.VerifyPageDisplayed(signUpStepTwoSubPage);

            TestContext.Out.WriteLine($"End Test Case - {TestID.TC_ID_0020}");
        }


        [Test]
        [TestID(TestID.TC_ID_0021), StoryID(StoryID.SR_ID_005)]
        [Priority(PriorityLevel.Highest)]
        [TestCaseSource(typeof(DataProvider), "PrepareTestCases", new object[] { TestID.TC_ID_0021 })]
        public void TC_SIGN_UP_WITH_EMAIL_AND_PASS_VerifyProvidePersonalDetailsDisplayWhenUserJustCompletedStep1LogIn(Dictionary<string, string> Data)
        {
            TestContext.Out.WriteLine($"Start Test Case - {TestID.TC_ID_0021}");

            string email = RandomUtils.GetRandomEmailAddress(Data["email_prefix"], Data["email_domain"]);
            string password = email;

            SignUpStepTwoSubPage signUpStepTwoSubPage = new SignUpStepTwoSubPage(Driver);

            TestContext.Out.WriteLine("Go to core page and Sign up with valid fields.");
            LoginPage loginPage = new LoginPage(Driver, PlatformBaseURL);
            loginPage.Navigate();
            loginPage.InputSignUpInfo(email, password);

            TestContext.Out.WriteLine("Vefiry Step 2 Provide personal details is displayed.");
            PlatformUtils.VerifyPageDisplayed(signUpStepTwoSubPage);

            TestContext.Out.WriteLine("Clear all cookies and Refresh page");
            Driver.Manage().Cookies.DeleteAllCookies();
            Driver.Navigate().Refresh();

            TestContext.Out.WriteLine("Go to core page and Log in.");
            loginPage.Navigate();
            loginPage.InputLoginInfo(email, password);

            TestContext.Out.WriteLine("Vefiry Step 2 Provide personal details is displayed.");
            PlatformUtils.VerifyPageDisplayed(signUpStepTwoSubPage);

            TestContext.Out.WriteLine($"End Test Case - {TestID.TC_ID_0021}");
        }
        #endregion

        #region Methods
        /// <summary>
        /// Verify Privacy Policy and Terms & Conditions URL at sign up page
        /// check if 
        /// </summary>
        /// <param name="loginPage"></param>
        /// <param name="privacyPolicyUrl">Privacy Policy URL</param>
        /// <param name="termsAndConditionsUrl">Terms & Conditions URL</param>
        public void VerifyPrivacyPolicyAndTermsConditions(LoginPage loginPage, string privacyPolicyUrl, string termsAndConditionsUrl)
        {
            int tabsCount = Driver.WindowHandles.Count;
            string currentTab = Driver.WindowHandles[0];

            TestContext.Out.WriteLine("Click Privacy Policy link");
            loginPage.APolicyAgreement.Click();
            tabsCount++;

            TestContext.Out.WriteLine("Verify new tab is added");
            Assert.AreEqual(tabsCount, Driver.WindowHandles.Count, "No new tab is added");

            TestContext.Out.WriteLine("Switch to new tab");
            Driver.SwitchTo().Window(Driver.WindowHandles.Last());

            TestContext.Out.WriteLine("Verify new tab url is {0}", privacyPolicyUrl);
            ThreadUtils.SleepMediumTime();

            Assert.IsTrue(Driver.Url.Contains(privacyPolicyUrl), "Privacy Policy url is not correct, it was {0}", Driver.Url);

            TestContext.Out.WriteLine("Switch to sign up tab");
            Driver.SwitchTo().Window(currentTab);

            TestContext.Out.WriteLine("Click Terms and Conditions link");
            loginPage.ATermsAndConditionsAgreement.Click();
            tabsCount++;

            TestContext.Out.WriteLine("Verify new tab is added");
            Assert.AreEqual(tabsCount, Driver.WindowHandles.Count, "No new tab is added");

            TestContext.Out.WriteLine("Switch to new tab");
            Driver.SwitchTo().Window(Driver.WindowHandles.Last());

            TestContext.Out.WriteLine("Verify new tab url is {0}", termsAndConditionsUrl);
            ThreadUtils.SleepMediumTime();

            Assert.IsTrue(Driver.Url.Contains(termsAndConditionsUrl), "Terms and Conditions url is not correct, it was {0}", Driver.Url);
        }
        #endregion
    }
}
