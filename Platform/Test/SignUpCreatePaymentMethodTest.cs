using Automation.UI.Core.Selenium.ExternalMail;
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
    /// Test Suite for Sign up create a payment method feature
    /// </summary>
    [TestFixture]
    public class SignUpCreatePaymentMethodTest : PlatformTestBase
    {
        #region Test Cases

        [Test]
        [TestID(TestID.TC_ID_0026), StoryID(StoryID.SR_ID_008)]
        [Priority(PriorityLevel.Highest)]
        [TestCaseSource(typeof(DataProvider), "PrepareTestCases", new object[] { TestID.TC_ID_0026 })]
        public void TC_SIGN_UP_CREATE_PAYMENT_METHOD_AbleToShowCybersourcePaymentMethodCorrectlyWithMexicoCountrySelected(Dictionary<string, string> Data)
        {
            TestContext.Out.WriteLine($"Start Test Case - {TestID.TC_ID_0026}");

            Data["country"] = "Mexico";
            VerifyShowCyberSourceMethodCorrectly(Data);

            TestContext.Out.WriteLine($"End Test Case - {TestID.TC_ID_0026}");
        }

        [Test]
        [TestID(TestID.TC_ID_0027), StoryID(StoryID.SR_ID_008)]
        [Priority(PriorityLevel.Highest)]
        [TestCaseSource(typeof(DataProvider), "PrepareTestCases", new object[] { TestID.TC_ID_0027 })]
        public void TC_SIGN_UP_CREATE_PAYMENT_METHOD_AbleToShowCybersourcePaymentMethodCorrectlyWithDominicaCountrySelected(Dictionary<string, string> Data)
        {
            TestContext.Out.WriteLine($"Start Test Case - {TestID.TC_ID_0027}");

            Data["country"] = "Dominica";
            VerifyShowCyberSourceMethodCorrectly(Data);

            TestContext.Out.WriteLine($"End Test Case - {TestID.TC_ID_0027}");
        }

        [Test]
        [TestID(TestID.TC_ID_0028), StoryID(StoryID.SR_ID_008)]
        [Priority(PriorityLevel.Highest)]
        [TestCaseSource(typeof(DataProvider), "PrepareTestCases", new object[] { TestID.TC_ID_0028 })]
        public void TC_SIGN_UP_CREATE_PAYMENT_METHOD_AbleToShowIpPaymentsPaymentMethodCorrectlyWithAustraliaCountrySelected(Dictionary<string, string> Data)
        {
            TestContext.Out.WriteLine($"Start Test Case - {TestID.TC_ID_0028}");

            Data["country"] = "Australia";
            VerifyShowIPPaymentsMethodCorrectly(Data);

            TestContext.Out.WriteLine($"End Test Case - {TestID.TC_ID_0028}");
        }

        [Test]
        [TestID(TestID.TC_ID_0029), StoryID(StoryID.SR_ID_008)]
        [Priority(PriorityLevel.Highest)]
        [TestCaseSource(typeof(DataProvider), "PrepareTestCases", new object[] { TestID.TC_ID_0029 })]
        public void TC_SIGN_UP_CREATE_PAYMENT_METHOD_AbleToShowIpPaymentsPaymentMethodCorrectlyWithVietNamCountrySelected(Dictionary<string, string> Data)
        {
            TestContext.Out.WriteLine($"Start Test Case - {TestID.TC_ID_0029}");

            Data["country"] = "Viet Nam";
            VerifyShowIPPaymentsMethodCorrectly(Data);

            TestContext.Out.WriteLine($"End Test Case - {TestID.TC_ID_0029}");
        }

        [Test]
        [TestID(TestID.TC_ID_0030), StoryID(StoryID.SR_ID_008)]
        [Priority(PriorityLevel.Highest)]
        [TestCaseSource(typeof(DataProvider), "PrepareTestCases", new object[] { TestID.TC_ID_0030 })]
        public void TC_SIGN_UP_CREATE_PAYMENT_METHOD_AbleToShowIpPaymentsPaymentMethodCorrectlyWithCroatiaCountrySelected(Dictionary<string, string> Data)
        {
            TestContext.Out.WriteLine($"Start Test Case - {TestID.TC_ID_0030}");

            Data["country"] = "Croatia";
            VerifyShowIPPaymentsMethodCorrectly(Data);

            TestContext.Out.WriteLine($"End Test Case - {TestID.TC_ID_0030}");
        }

        [Test]
        [TestID(TestID.TC_ID_0031), StoryID(StoryID.SR_ID_008)]
        [Priority(PriorityLevel.Highest)]
        [TestCaseSource(typeof(DataProvider), "PrepareTestCases", new object[] { TestID.TC_ID_0031 })]
        public void TC_SIGN_UP_CREATE_PAYMENT_METHOD_AbleToShowIpPaymentsPaymentMethodCorrectlyWithHungaryCountrySelected(Dictionary<string, string> Data)
        {
            TestContext.Out.WriteLine($"Start Test Case - {TestID.TC_ID_0031}");

            Data["country"] = "Hungary";
            VerifyShowIPPaymentsMethodCorrectly(Data);

            TestContext.Out.WriteLine($"End Test Case - {TestID.TC_ID_0031}");
        }

        [Test]
        [TestID(TestID.TC_ID_0032), StoryID(StoryID.SR_ID_008)]
        [Priority(PriorityLevel.Highest)]
        [TestCaseSource(typeof(DataProvider), "PrepareTestCases", new object[] { TestID.TC_ID_0032 })]
        public void TC_SIGN_UP_CREATE_PAYMENT_METHOD_PaymentMethodFormIsStyledAccordingToNvivoStyleGuide(Dictionary<string, string> Data)
        {
            TestContext.Out.WriteLine($"Start Test Case - {TestID.TC_ID_0032}");

            string email = RandomUtils.GetRandomEmailAddress(Data["email_prefix"], Data["email_domain"]);
            string password = email;

            string COUNTRY_1 = "Aruba";
            string COUNTRY_2 = "Turkey";

            SignUpStepTwoSubPage signUpStepTwoSubPage = new SignUpStepTwoSubPage(Driver);
            SignUpStepThreeSubPage signUpStepThreeSubPage = new SignUpStepThreeSubPage(Driver);
            SignUpStepFourSubPage signUpStepFourSubPage = new SignUpStepFourSubPage(Driver);

            TestContext.Out.WriteLine("Go to core page and Sign up with valid fields");
            LoginPage loginPage = new LoginPage(Driver, PlatformBaseURL);
            loginPage.Navigate();
            loginPage.InputSignUpInfo(email, password);

            TestContext.Out.WriteLine("Input data for Step 2 Provide personal details");
            PlatformUtils.VerifyPageDisplayed(signUpStepTwoSubPage);
            signUpStepTwoSubPage.SubmitFormData(Data["first_name"], Data["last_name"], COUNTRY_1);

            TestContext.Out.WriteLine("Input data for Step 3 Provide research area related info");
            PlatformUtils.VerifyPageDisplayed(signUpStepThreeSubPage);
            signUpStepThreeSubPage.SubmitFormData(Data["sector"], Data["organization"]);

            TestContext.Out.WriteLine("Vefiry Payment method fields of CyberSource are displayed");
            PlatformUtils.VerifyPageDisplayed(signUpStepFourSubPage);
            Assert.IsTrue(signUpStepFourSubPage.IsCyberSourceFieldsDisplayed(),
                "Style of Payment method form is not styled according to NVivo style guide (CyberSource)");

            TestContext.Out.WriteLine("Refresh to go back to step 2");
            Driver.Navigate().Refresh();

            TestContext.Out.WriteLine("Input data for Step 2 Provide personal details");
            PlatformUtils.VerifyPageDisplayed(signUpStepTwoSubPage);
            signUpStepTwoSubPage.SubmitFormData(Data["first_name"], Data["last_name"], COUNTRY_2);

            TestContext.Out.WriteLine("Input data for Step 3 Provide research area related info");
            PlatformUtils.VerifyPageDisplayed(signUpStepThreeSubPage);
            signUpStepThreeSubPage.SubmitFormData(Data["sector"], Data["organization"]);

            TestContext.Out.WriteLine("Vefiry Payment method fields of IP Payments are displayed");
            PlatformUtils.VerifyPageDisplayed(signUpStepFourSubPage);
            Assert.IsTrue(signUpStepFourSubPage.IsIPPaymentsFieldsDisplayed(),
                "Style of Payment method form is not styled according to NVivo style guide (IP Payments)");

            TestContext.Out.WriteLine($"End Test Case - {TestID.TC_ID_0032}");
        }

        [Test]
        [TestID(TestID.TC_ID_0033), StoryID(StoryID.SR_ID_008)]
        [Priority(PriorityLevel.Highest)]
        [TestCaseSource(typeof(DataProvider), "PrepareTestCases", new object[] { TestID.TC_ID_0033 })]
        public void TC_SIGN_UP_CREATE_PAYMENT_METHOD_AbleToProvision15MinutesOfFreeTrial(Dictionary<string, string> Data)
        {
            TestContext.Out.WriteLine($"Start Test Case - {TestID.TC_ID_0033}");

            string email = RandomUtils.GetRandomEmailAddress(Data["email_prefix"], Data["email_domain"]);
            string password = email;

            SignUpStepTwoSubPage signUpStepTwoSubPage = new SignUpStepTwoSubPage(Driver);
            SignUpStepThreeSubPage signUpStepThreeSubPage = new SignUpStepThreeSubPage(Driver);
            SignUpStepFourSubPage signUpStepFourSubPage = new SignUpStepFourSubPage(Driver);
            SignUpStepFiveSubPage signUpStepFiveSubPage = new SignUpStepFiveSubPage(Driver);
            SignUpStepSixSubPage signUpStepSixSubPage = new SignUpStepSixSubPage(Driver);

            TestContext.Out.WriteLine("Go to core page and Sign up with valid fields");
            LoginPage loginPage = new LoginPage(Driver, PlatformBaseURL);
            loginPage.Navigate();
            loginPage.InputSignUpInfo(email, password);

            TestContext.Out.WriteLine("Input data for Step 2 Provide personal details");
            PlatformUtils.VerifyPageDisplayed(signUpStepTwoSubPage);
            signUpStepTwoSubPage.SubmitFormData(Data["first_name"], Data["last_name"], Data["country"]);

            TestContext.Out.WriteLine("Input data for Step 3 Provide research area related info");
            PlatformUtils.VerifyPageDisplayed(signUpStepThreeSubPage);
            signUpStepThreeSubPage.SubmitFormData(Data["sector"], Data["organization"]);

            TestContext.Out.WriteLine("Input data for Step 4 Sign up create a payment method");
            PlatformUtils.VerifyPageDisplayed(signUpStepFourSubPage);
            signUpStepFourSubPage.InputCardInfo(Data["card_name"], Data["card_ccnumber"], Data["card_month"], Data["card_year"], Data["card_security_code"]);
            signUpStepFourSubPage.InputCardAddress(Data["card_address_1"], Data["card_address_2"], Data["card_city"], Data["card_postal_code"], Data["card_country"], Data["card_state"]);
            signUpStepFourSubPage.ClickSubmitButton();

            TestContext.Out.WriteLine("Click go back to log in");
            PlatformUtils.VerifyPageDisplayed(signUpStepFiveSubPage);
            signUpStepFiveSubPage.GoBackToLogin();

            TestContext.Out.WriteLine("Open spambox home page and view emails");
            IEmailHomePage spamboxHomePage = new SpamboxHomePage(Driver);
            spamboxHomePage.OpenEmail(email, "");

            TestContext.Out.WriteLine("Open first email and click verify account");
            IEmailEmailPage spamboxEmailPage = new SpamboxEmailPage(Driver);
            spamboxEmailPage.ClickOpenFirstEmailItem();
            spamboxEmailPage.ClickActivateAcountByEmail();

            TestContext.Out.WriteLine("Switch to verify email tab and log in");
            Driver.SwitchTo().Window(Driver.WindowHandles.Last());
            PlatformUtils.VerifyPageDisplayed(signUpStepSixSubPage);
            signUpStepSixSubPage.GoBackToLogin();
            loginPage.InputLoginInfo(email, password);

            TestContext.Out.WriteLine("Verify Transcription Minutes Remaining are 15mins");
            HeaderSubPage headerSubPage = new HeaderSubPage(Driver, PlatformBaseURL);
            Assert.AreEqual("15", headerSubPage.GetRemainingMinutes(), "Transcription Minutes Remaining are not 15mins");

            TestContext.Out.WriteLine($"End Test Case - {TestID.TC_ID_0033}");
        }
        #endregion

        #region Methods
        /// <summary>
        /// Sign up with new account
        /// Go to payment and verify IPPayments payment method displayed correctly
        /// </summary>
        /// <param name="Data">Dictionary contains data for sign up step</param>
        public void VerifyShowIPPaymentsMethodCorrectly(Dictionary<string, string> Data)
        {
            string email = RandomUtils.GetRandomEmailAddress(Data["email_prefix"], Data["email_domain"]);
            string password = email;

            SignUpStepTwoSubPage signUpStepTwoSubPage = new SignUpStepTwoSubPage(Driver);
            SignUpStepThreeSubPage signUpStepThreeSubPage = new SignUpStepThreeSubPage(Driver);
            SignUpStepFourSubPage signUpStepFourSubPage = new SignUpStepFourSubPage(Driver);

            TestContext.Out.WriteLine("Go to core page and Sign up with valid fields");
            LoginPage loginPage = new LoginPage(Driver, PlatformBaseURL);
            loginPage.Navigate();
            loginPage.InputSignUpInfo(email, password);

            TestContext.Out.WriteLine("Input data for Step 2 Provide personal details");
            PlatformUtils.VerifyPageDisplayed(signUpStepTwoSubPage);
            signUpStepTwoSubPage.SubmitFormData(Data["first_name"], Data["last_name"], Data["country"]);

            TestContext.Out.WriteLine("Input data for Step 3 Provide research area related info");
            PlatformUtils.VerifyPageDisplayed(signUpStepThreeSubPage);
            signUpStepThreeSubPage.SubmitFormData(Data["sector"], Data["organization"]);

            TestContext.Out.WriteLine("Vefiry Step 4 Create a payment method is displayed");
            PlatformUtils.VerifyPageDisplayed(signUpStepFourSubPage);

            Assert.IsTrue(signUpStepFourSubPage.IsIPPaymentsFieldsDisplayed(), "Payment method fields of IP Payments are not displayed");

            TestContext.Out.WriteLine("Click 'Next' button");
            signUpStepFourSubPage.ClickSubmitButton();

            Assert.IsTrue(signUpStepFourSubPage.IsIPPaymentsErrorFieldsDisplayed(), "Require fields of IP Payments are not displayed");
        }


        /// <summary>
        /// Sign up with new account
        /// Go to payment and verify CyberSource payment method displayed correctly
        /// </summary>
        /// <param name="Data">Dictionary contains data for sign up step</param>
        public void VerifyShowCyberSourceMethodCorrectly(Dictionary<string, string> Data)
        {
            string email = RandomUtils.GetRandomEmailAddress(Data["email_prefix"], Data["email_domain"]);
            string password = email;

            SignUpStepTwoSubPage signUpStepTwoSubPage = new SignUpStepTwoSubPage(Driver);
            SignUpStepThreeSubPage signUpStepThreeSubPage = new SignUpStepThreeSubPage(Driver);
            SignUpStepFourSubPage signUpStepFourSubPage = new SignUpStepFourSubPage(Driver);

            TestContext.Out.WriteLine("Go to core page and Sign up with valid fields");
            LoginPage loginPage = new LoginPage(Driver, PlatformBaseURL);
            loginPage.Navigate();
            loginPage.InputSignUpInfo(email, password);

            TestContext.Out.WriteLine("Input data for Step 2 Provide personal details");
            PlatformUtils.VerifyPageDisplayed(signUpStepTwoSubPage);
            signUpStepTwoSubPage.SubmitFormData(Data["first_name"], Data["last_name"], Data["country"]);

            TestContext.Out.WriteLine("Input data for Step 3 Provide research area related info");
            PlatformUtils.VerifyPageDisplayed(signUpStepThreeSubPage);
            signUpStepThreeSubPage.SubmitFormData(Data["sector"], Data["organization"]);

            TestContext.Out.WriteLine("Vefiry Step 4 Create a payment method is displayed");
            PlatformUtils.VerifyPageDisplayed(signUpStepFourSubPage);

            Assert.IsTrue(signUpStepFourSubPage.IsCyberSourceFieldsDisplayed(), "Payment method fields of CyberSource are not displayed");

            TestContext.Out.WriteLine("Click 'Next' button");
            signUpStepFourSubPage.ClickSubmitButton();

            Assert.IsTrue(signUpStepFourSubPage.IsCyberSourceErrorFieldsDisplayed(), "Require fields of CyberSource are not displayed");
        }
        #endregion
    }
}
