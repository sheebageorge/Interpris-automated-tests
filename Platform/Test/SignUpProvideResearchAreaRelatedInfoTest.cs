using Automation.UI.Core.CommonUtilities;
using Automation.UI.Core.DataProcessing;
using Automation.UI.Core.Selenium.PageObjects.Interpris.Platform;
using Automation.UI.Core.TestAttributes;
using Automation.UI.Core.TestBase;
using Automation.UI.Core.TestLibraries;
using Automation.UI.Platform.TestAttributes;
using NUnit.Framework;
using System.Collections.Generic;

namespace Automation.UI.Platform.Test
{
    /// <summary>
    /// Test Suite for Sign up provide research area related info feature
    /// </summary>
    [TestFixture]
    public class SignUpProvideResearchAreaRelatedInfoTest : PlatformTestBase
    {
        #region Test Cases
        [Test]
        [TestID(TestID.TC_ID_0024), StoryID(StoryID.SR_ID_007)]
        [Priority(PriorityLevel.Highest)]
        [TestCaseSource(typeof(DataProvider), "PrepareTestCases", new object[] { TestID.TC_ID_0024 })]
        public void TC_SIGN_UP_PROVIDE_RESEARCH_AREA_VerifyProvideResearchAreaRelatedInfoDisplayCorrectly(Dictionary<string, string> Data)
        {
            TestContext.Out.WriteLine($"Start Test Case - {TestID.TC_ID_0024}");

            string email = RandomUtils.GetRandomEmailAddress(Data["email_prefix"], Data["email_domain"]);
            string password = email;
            
            string SECTOR = Data["sector"];
            string ORGANIZATION = Data["organization"];

            SignUpStepTwoSubPage signUpStepTwoSubPage = new SignUpStepTwoSubPage(Driver);
            SignUpStepThreeSubPage signUpStepThreeSubPage = new SignUpStepThreeSubPage(Driver);

            TestContext.Out.WriteLine("Go to core page and Sign up with valid fields");
            GotoCoreAndSignUp(email, password);

            TestContext.Out.WriteLine("Input data for Step 2 Provide personal details");
            PlatformUtils.VerifyPageDisplayed(signUpStepTwoSubPage);
            signUpStepTwoSubPage.SubmitFormData(Data["first_name"], Data["last_name"], Data["country"]);

            TestContext.Out.WriteLine("Vefiry Step 3 Provide research area related info displayed");
            PlatformUtils.VerifyPageDisplayed(signUpStepThreeSubPage);

            TestContext.Out.WriteLine("Click 'Next' button");
            signUpStepThreeSubPage.ButtonNext.Click();

            TestContext.Out.WriteLine("Verify error messages displayed correctly");
            Assert.AreEqual(SignUpStepThreeSubPage.ERR_MSG_SECTOR_IS_REQUIRED, signUpStepThreeSubPage.DivSectorError.Text,
                $"Error message '{SignUpStepThreeSubPage.ERR_MSG_SECTOR_IS_REQUIRED}' is not correct");
            Assert.AreEqual(SignUpStepThreeSubPage.ERR_MSG_ORGANIZATION_IS_REQUIRED, signUpStepThreeSubPage.DivOrganizationError.Text,
                $"Error message '{SignUpStepThreeSubPage.ERR_MSG_ORGANIZATION_IS_REQUIRED}' is not correct");

            TestContext.Out.WriteLine($"Click to select '{SECTOR}' in Sector field");
            signUpStepThreeSubPage.DivSector.Click();
            signUpStepThreeSubPage.LiSectorItem(SECTOR).Click();
            Assert.IsFalse(signUpStepThreeSubPage.IsFormMainContainsMessage(SignUpStepThreeSubPage.ERR_MSG_SECTOR_IS_REQUIRED),
                $"Error message '{SignUpStepThreeSubPage.ERR_MSG_SECTOR_IS_REQUIRED}' is still appear");

            TestContext.Out.WriteLine($"Enter '{ORGANIZATION}' into the Organization field");
            signUpStepThreeSubPage.InputOrganization.SendKeys(ORGANIZATION);
            Assert.IsFalse(signUpStepThreeSubPage.IsFormMainContainsMessage(SignUpStepThreeSubPage.ERR_MSG_ORGANIZATION_IS_REQUIRED),
                $"Error message '{SignUpStepThreeSubPage.ERR_MSG_ORGANIZATION_IS_REQUIRED}' is still appear");

            TestContext.Out.WriteLine($"End Test Case - {TestID.TC_ID_0024}");
        }

        [Test]
        [TestID(TestID.TC_ID_0025), StoryID(StoryID.SR_ID_007)]
        [Priority(PriorityLevel.Highest)]
        [TestCaseSource(typeof(DataProvider), "PrepareTestCases", new object[] { TestID.TC_ID_0025 })]
        public void TC_SIGN_UP_PROVIDE_RESEARCH_AREA_VerifyDataIsSavedWhenUserProvidedResearchAreaRelatedInfo(Dictionary<string, string> Data)
        {
            TestContext.Out.WriteLine($"Start Test Case - {TestID.TC_ID_0025}");

            string email = RandomUtils.GetRandomEmailAddress(Data["email_prefix"], Data["email_domain"]);
            string password = email;
            
            string SECTOR = Data["sector"];
            string ORGANIZATION = Data["organization"];

            SignUpStepTwoSubPage signUpStepTwoSubPage = new SignUpStepTwoSubPage(Driver);
            SignUpStepThreeSubPage signUpStepThreeSubPage = new SignUpStepThreeSubPage(Driver);
            SignUpStepFourSubPage signUpStepFourSubPage = new SignUpStepFourSubPage(Driver);

            TestContext.Out.WriteLine("Go to core page and Sign up with valid fields");
            GotoCoreAndSignUp(email, password);

            TestContext.Out.WriteLine("Input data for Step 2 Provide personal details");
            PlatformUtils.VerifyPageDisplayed(signUpStepTwoSubPage);
            signUpStepTwoSubPage.SubmitFormData(Data["first_name"], Data["last_name"], Data["country"]);

            TestContext.Out.WriteLine("Input data for Step 3 Provide research area related info");
            PlatformUtils.VerifyPageDisplayed(signUpStepThreeSubPage);
            signUpStepThreeSubPage.SubmitFormData(Data["sector"], Data["organization"]);

            TestContext.Out.WriteLine("Vefiry Step 4 Create a payment method is displayed");
            PlatformUtils.VerifyPageDisplayed(signUpStepFourSubPage);

            TestContext.Out.WriteLine("Clear all cookies and Refresh page");
            Driver.Manage().Cookies.DeleteAllCookies();
            Driver.Navigate().Refresh();

            TestContext.Out.WriteLine("Go to core page and Log in");
            GotoCoreAndLogin(email, password);

            TestContext.Out.WriteLine("Vefiry Step 2 Provide personal details is displayed");
            PlatformUtils.VerifyPageDisplayed(signUpStepTwoSubPage);

            TestContext.Out.WriteLine("Click 'Next' button");
            ThreadUtils.SleepShortTime();
            signUpStepTwoSubPage.ButtonNext.Click();

            TestContext.Out.WriteLine("Vefiry Step 3 Provide research area related info displayed");
            PlatformUtils.VerifyPageDisplayed(signUpStepThreeSubPage);

            Assert.AreEqual(SECTOR, signUpStepThreeSubPage.DivSector.Text, "Sector are not saved");
            Assert.AreEqual(ORGANIZATION, signUpStepThreeSubPage.InputOrganization.Value, "Organization are not saved");

            TestContext.Out.WriteLine($"End Test Case - {TestID.TC_ID_0025}");
        }
        #endregion

        #region Methods
        /// <summary>
        /// Try to Access Core page and Sign up for new account
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public void GotoCoreAndSignUp(string username, string password)
        {
            LoginPage loginPage = new LoginPage(Driver, PlatformBaseURL);
            loginPage.Navigate();
            loginPage.InputSignUpInfo(username, password);

        }

        /// <summary>
        /// Try to Access Core page and Log in 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public void GotoCoreAndLogin(string username, string password)
        {
            LoginPage loginPage = new LoginPage(Driver, PlatformBaseURL);
            loginPage.Navigate();
            loginPage.InputLoginInfo(username, password);
        }
        #endregion
    }
}
