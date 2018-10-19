using Automation.UI.Core.Selenium.PageObjects.Interpris.Platform;
using NUnit.Framework;

namespace Automation.UI.Core.TestLibraries
{
    public class PlatformUtils
    {
        /// <summary>
        /// Check if Sign Up Step Two Sub Page is visible   
        /// </summary>
        public static void VerifyPageDisplayed(SignUpStepTwoSubPage signUpStepTwoSubPage)
        {
            signUpStepTwoSubPage.WaitForElementVisible(signUpStepTwoSubPage.DivTitleWithText);

            Assert.IsTrue(signUpStepTwoSubPage.DivTitle.IsVisible, "Step 2 Provide personal details not displayed.");
            Assert.AreEqual(SignUpStepTwoSubPage.PAGE_TITLE, signUpStepTwoSubPage.DivTitle.Text, "Step 2 Provide personal details not correct.");

            signUpStepTwoSubPage.WaitForElementVisible(signUpStepTwoSubPage.ButtonNext);
        }

        /// <summary>
        /// Check if Sign Up Step Three Sub Page is visible  
        /// </summary>
        public static void VerifyPageDisplayed(SignUpStepThreeSubPage signUpStepThreeSubPage)
        {
            signUpStepThreeSubPage.WaitForElementVisible(signUpStepThreeSubPage.DivTitleWithText);

            Assert.IsTrue(signUpStepThreeSubPage.DivTitle.IsVisible, "Step 3 Provide research area related info not displayed.");
            Assert.AreEqual(SignUpStepThreeSubPage.PAGE_TITLE, signUpStepThreeSubPage.DivTitle.Text, "Step 3 Provide research area related info not displayed.");

            signUpStepThreeSubPage.WaitForElementVisible(signUpStepThreeSubPage.ButtonNext);
        }

        /// <summary>
        /// Check if Sign Up Step Four Sub Page is visible  
        /// </summary>
        public static void VerifyPageDisplayed(SignUpStepFourSubPage signUpStepFourSubPage)
        {
            signUpStepFourSubPage.WaitForElementVisible(signUpStepFourSubPage.DivTitleWithText);

            Assert.IsTrue(signUpStepFourSubPage.DivTitle.IsVisible, "Step 4 Create a payment method not displayed.");
            Assert.AreEqual(SignUpStepFourSubPage.PAGE_TITLE, signUpStepFourSubPage.DivTitle.Text, "Step 4 Create a payment method not displayed.");
        }

        /// <summary>
        /// Check if Sign Up Step Five Sub Page is visible  
        /// </summary>
        public static void VerifyPageDisplayed(SignUpStepFiveSubPage signUpStepFiveSubPage)
        {
            signUpStepFiveSubPage.WaitForElementVisible(signUpStepFiveSubPage.DivTitle);

            Assert.IsTrue(signUpStepFiveSubPage.DivTitle.IsVisible, "Step 5 Verify email not displayed.");
            Assert.AreEqual(SignUpStepFiveSubPage.PAGE_TITLE, signUpStepFiveSubPage.DivTitle.Text, "Step 5 Verify email not displayed.");

            signUpStepFiveSubPage.WaitForElementVisible(signUpStepFiveSubPage.ButtonBackToLogIn);
        }

        /// <summary>
        /// Check if Sign Up Step Six Sub Page is visible 
        /// </summary>
        public static void VerifyPageDisplayed(SignUpStepSixSubPage signUpStepSixSubPage)
        {
            signUpStepSixSubPage.WaitForElementVisible(signUpStepSixSubPage.DivTitle);

            Assert.IsTrue(signUpStepSixSubPage.DivTitle.IsVisible, "Step 6 Verify email success not displayed.");
            Assert.AreEqual(SignUpStepSixSubPage.PAGE_TITLE, signUpStepSixSubPage.DivTitle.Text, "Step 6 Verify email success not displayed.");

            signUpStepSixSubPage.WaitForElementVisible(signUpStepSixSubPage.ButtonBackToLogIn);
        }

        /// <summary>
        /// Check if Order summary Sub Page is visible 
        /// </summary>
        /*public static void VerifyPageDisplayed(OrderSumnmarySubPage orderSumnmarySubPage)
        {
            orderSumnmarySubPage.WaitForElementVisible(orderSumnmarySubPage.DivTitle);

            Assert.IsTrue(orderSumnmarySubPage.DivTitle.IsVisible, "Order summary Page not displayed.");
            Assert.AreEqual(OrderSumnmarySubPage.PAGE_TITLE, orderSumnmarySubPage.DivTitle.Text, " Order summary Page not displayed.");

            orderSumnmarySubPage.WaitForElementVisible(orderSumnmarySubPage.ButtonPaynow);
        }*/

        /// <summary>
        /// Check if Success Order Sub Page is visible 
        /// </summary>
        public static void VerifyPageDisplayed(SuccessOrderSubPage successOrderSubPage)
        {
            successOrderSubPage.WaitForElementVisible(successOrderSubPage.DivTitle);

            Assert.IsTrue(successOrderSubPage.DivTitle.IsVisible, "Success Order Sub Page not displayed.");
            Assert.AreEqual(SuccessOrderSubPage.PAGE_TITLE, successOrderSubPage.DivTitle.Text, "Success Order Sub Page not displayed.");

            successOrderSubPage.WaitForElementVisible(successOrderSubPage.ButtonOK);
        }
    }
}
