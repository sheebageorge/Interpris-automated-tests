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
    /// Test Suite for Sign up provide personal details
    /// </summary>
    [TestFixture]
    public class SignUpProvidePersonalDetailsTest : PlatformTestBase
    {
        #region Test Cases
        [Test]
        [TestID(TestID.TC_ID_0022), StoryID(StoryID.SR_ID_006)]
        [Priority(PriorityLevel.Highest)]
        [TestCaseSource(typeof(DataProvider), "PrepareTestCases", new object[] { TestID.TC_ID_0022 })]
        public void TC_SIGN_UP_PROVIDE_PERSONAL_DETAILS_VerifyProvidePersonalDetailsDisplayCorrectly(Dictionary<string, string> Data)
        {
            TestContext.Out.WriteLine($"Start Test Case - {TestID.TC_ID_0022}");

            string email = RandomUtils.GetRandomEmailAddress(Data["email_prefix"], Data["email_domain"]);
            string password = email;

            string FIRST_NAME = Data["first_name"];
            string LAST_NAME = Data["last_name"];
            string COUNTRY_SELECT = Data["country_select"];
            string COUNTRY_INPUT = Data["country_input"];
            string COUNTRY_SUGGESTION_1 = Data["country_suggestion_1"];
            string COUNTRY_SUGGESTION_2 = Data["country_suggestion_2"];

            SignUpStepTwoSubPage signUpStepTwoSubPage = new SignUpStepTwoSubPage(Driver);

            TestContext.Out.WriteLine("Go to core page and Sign up with valid fields");
            GotoCoreAndSignUp(email, password);

            TestContext.Out.WriteLine("Vefiry Step 2 Provide personal details is displayed");
            PlatformUtils.VerifyPageDisplayed(signUpStepTwoSubPage);

            TestContext.Out.WriteLine("Click 'Next' button");
            signUpStepTwoSubPage.ButtonNext.Click();

            TestContext.Out.WriteLine("Verify error messages displayed correctly");
            Assert.AreEqual(SignUpStepTwoSubPage.ERR_MSG_FIRST_NAME_IS_REQUIRED, signUpStepTwoSubPage.DivFirstNameError.Text,
                $"Error message '{SignUpStepTwoSubPage.ERR_MSG_FIRST_NAME_IS_REQUIRED}' is not correct");
            Assert.AreEqual(SignUpStepTwoSubPage.ERR_MSG_LAST_NAME_IS_REQUIRED, signUpStepTwoSubPage.DivLastNameError.Text,
                $"Error message '{SignUpStepTwoSubPage.ERR_MSG_LAST_NAME_IS_REQUIRED} is not correct");
            Assert.AreEqual(SignUpStepTwoSubPage.ERR_MSG_COUNTRY_IS_REQUIRED, signUpStepTwoSubPage.DivCountryError.Text,
                $"Error message '{SignUpStepTwoSubPage.ERR_MSG_COUNTRY_IS_REQUIRED}' is not correct");

            TestContext.Out.WriteLine($"Enter '{FIRST_NAME}' into the First name field");
            signUpStepTwoSubPage.InputFirstName.SendKeys(FIRST_NAME);
            Assert.IsFalse(signUpStepTwoSubPage.IsFormMainContainsMessage(SignUpStepTwoSubPage.ERR_MSG_FIRST_NAME_IS_REQUIRED),
                $"Error message '{SignUpStepTwoSubPage.ERR_MSG_FIRST_NAME_IS_REQUIRED}' is still appear");

            TestContext.Out.WriteLine($"Enter '{LAST_NAME}' into the Last name field");
            signUpStepTwoSubPage.InputLastName.SendKeys(LAST_NAME);
            Assert.IsFalse(signUpStepTwoSubPage.IsFormMainContainsMessage(SignUpStepTwoSubPage.ERR_MSG_LAST_NAME_IS_REQUIRED),
                $"Error message '{SignUpStepTwoSubPage.ERR_MSG_LAST_NAME_IS_REQUIRED}' is still appear");

            TestContext.Out.WriteLine($"Enter '{COUNTRY_INPUT}' into the Country field");
            signUpStepTwoSubPage.DivCountry.Click();
            signUpStepTwoSubPage.InputCountry.SendKeys(COUNTRY_INPUT);

            TestContext.Out.WriteLine($"Verify '{COUNTRY_SUGGESTION_1}', '{COUNTRY_SUGGESTION_2}' displayed in the dropdown list");
            Assert.IsTrue(signUpStepTwoSubPage.LiCountryItem(COUNTRY_SUGGESTION_1).IsVisible, $"{COUNTRY_SUGGESTION_1} not displayed in the dropdown list");
            Assert.IsTrue(signUpStepTwoSubPage.LiCountryItem(COUNTRY_SUGGESTION_2).IsVisible, $"{COUNTRY_SUGGESTION_2} not displayed in the dropdown list");

            TestContext.Out.WriteLine($"Click to select '{COUNTRY_SELECT}'");
            signUpStepTwoSubPage.LiCountryItem(COUNTRY_SELECT).Click();
            Assert.AreEqual(COUNTRY_SELECT, signUpStepTwoSubPage.DivCountry.Text,
                $"Country field did not select value '{COUNTRY_SELECT}'");
            Assert.IsFalse(signUpStepTwoSubPage.IsFormMainContainsMessage(SignUpStepTwoSubPage.ERR_MSG_COUNTRY_IS_REQUIRED),
                $"Error message '{SignUpStepTwoSubPage.ERR_MSG_COUNTRY_IS_REQUIRED}' is still appear");

            TestContext.Out.WriteLine($"End Test Case - {TestID.TC_ID_0022}");
        }

        [Test]
        [TestID(TestID.TC_ID_0023), StoryID(StoryID.SR_ID_006)]
        [Priority(PriorityLevel.Highest)]
        [TestCaseSource(typeof(DataProvider), "PrepareTestCases", new object[] { TestID.TC_ID_0023 })]
        public void TC_SIGN_UP_PROVIDE_PERSONAL_DETAILS_VerifyDataIsSavedWhenUserProvidedPersonalDetails(Dictionary<string, string> Data)
        {
            TestContext.Out.WriteLine($"Start Test Case - {TestID.TC_ID_0023}");

            string email = RandomUtils.GetRandomEmailAddress(Data["email_prefix"], Data["email_domain"]);
            string password = email;

            string FIRST_NAME = Data["first_name"];
            string LAST_NAME = Data["last_name"];
            string COUNTRY = Data["country_select"];

            SignUpStepTwoSubPage signUpStepTwoSubPage = new SignUpStepTwoSubPage(Driver);
            SignUpStepThreeSubPage signUpStepThreeSubPage = new SignUpStepThreeSubPage(Driver);

            TestContext.Out.WriteLine("Go to core page and Sign up with valid fields");
            GotoCoreAndSignUp(email, password);

            TestContext.Out.WriteLine("Input data for Step 2 Provide personal details");
            PlatformUtils.VerifyPageDisplayed(signUpStepTwoSubPage);
            signUpStepTwoSubPage.SubmitFormData(FIRST_NAME, LAST_NAME, COUNTRY);

            TestContext.Out.WriteLine("Vefiry Step 3 Provide research area related info displayed");
            PlatformUtils.VerifyPageDisplayed(signUpStepThreeSubPage);

            TestContext.Out.WriteLine("Clear all cookies and Refresh page");
            Driver.Manage().Cookies.DeleteAllCookies();
            Driver.Navigate().Refresh();

            TestContext.Out.WriteLine("Go to core page and Log in");
            GotoCoreAndLogin(email, password);

            TestContext.Out.WriteLine("Vefiry Step 2 Provide personal details is displayed");
            PlatformUtils.VerifyPageDisplayed(signUpStepTwoSubPage);

            Assert.AreEqual(FIRST_NAME, signUpStepTwoSubPage.InputFirstName.Value, "First name are not saved");
            Assert.AreEqual(LAST_NAME, signUpStepTwoSubPage.InputLastName.Value, "Last name are not saved");
            Assert.AreEqual(COUNTRY, signUpStepTwoSubPage.DivSelectedCountry.Text, "Country are not saved");

            TestContext.Out.WriteLine($"End Test Case - {TestID.TC_ID_0023}");
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
