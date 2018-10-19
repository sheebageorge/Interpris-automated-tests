using Automation.UI.Core.CommonUtilities;
using NUnit.Framework;
using OpenQA.Selenium;
using System;

namespace Automation.UI.Core.Selenium.PageObjects.Interpris.Platform
{
    /// <summary>
    /// Wrapper class of Sign Up Step Two Sub Page
    /// contains all elements and actions for Sign Up Step Two Sub Page
    /// </summary>
    public class SignUpStepTwoSubPage : BasePage
    {
        #region Page Default
        public const string PAGE_TITLE = "To create your account, we just need some details about you.";

        public const string ERR_MSG_FIRST_NAME_IS_REQUIRED = "First name is required";
        public const string ERR_MSG_LAST_NAME_IS_REQUIRED = "Last name is required";
        public const string ERR_MSG_COUNTRY_IS_REQUIRED = "Country is required";
        #endregion

        #region Page Objects
        private readonly string formMain = "//form";
        private readonly string divTitle = "//div[@class=\"title\"]";
        private readonly string divTitleWithText = $"//div[@class=\"title\" and .=\"{PAGE_TITLE}\"]";
        private readonly string inputFirstName = "id=firstName";
        private readonly string divFirstNameError = "//span[descendant::input[@id=\"firstName\"]]/following-sibling::div[@class=\"ant-form-explain\"]";
        private readonly string inputLastName = "id=lastName";
        private readonly string divLastNameError = "//span[descendant::input[@id=\"lastName\"]]/following-sibling::div[@class=\"ant-form-explain\"]";
        private readonly string divCountry = "//div[@id=\"countryId\"]";
        private readonly string inputCountry = "//input[@id=\"countryId\"]";
        private readonly string divSelectedCountry = "//div[contains(@class,\"ant-select-selection-selected-value\")]";
        private readonly string divCountryError = "//span[descendant::div[@id=\"countryId\"]]/following-sibling::div[@class=\"ant-form-explain\"]";
        private readonly string buttonNext = "//button[@type=\"submit\"]";
        #endregion

        public SignUpStepTwoSubPage(IWebDriver driver, string baseURL = "") : base(driver, baseURL, "") { }

        #region Properties
        public BaseWebObject FormMain => FindWebElement(formMain, true);
        public BaseWebObject DivTitle => FindWebElement(divTitle, true);
        public BaseWebObject DivTitleWithText => FindWebElement(divTitleWithText, true);
        public BaseWebObject InputFirstName => FindWebElement(inputFirstName, true);
        public BaseWebObject DivFirstNameError => FindWebElement(divFirstNameError, true);
        public BaseWebObject InputLastName => FindWebElement(inputLastName, true);
        public BaseWebObject DivLastNameError => FindWebElement(divLastNameError, true);
        public BaseWebObject DivCountry => FindWebElement(divCountry, true);
        public BaseWebObject InputCountry => FindWebElement(inputCountry, true);
        public BaseWebObject DivSelectedCountry => FindWebElement(divSelectedCountry, true);
        public BaseWebObject LiCountryItem(string countryName) => FindWebElement($"//li[@role=\"option\" and text()=\"{countryName}\"]", true);
        public BaseWebObject DivCountryError => FindWebElement(divCountryError, true);
        public BaseWebObject ButtonNext => FindWebElement(buttonNext, true);

        #endregion  

        #region Methods
        /// <summary>
        /// Check if form data contains string
        /// </summary>
        public bool IsFormMainContainsMessage(string message)
        {
            ThreadUtils.SleepShortTime();
            return FormMain.Text.Contains(message);
        }

        /// <summary>
        /// Input first name, last name and country
        /// Click next button
        /// </summary>
        public void SubmitFormData(string firstName, string lastName, string country)
        {
            WaitForElementVisible(DivTitleWithText);
            InputFirstName.SendKeys(firstName);
            InputLastName.SendKeys(lastName);
            DivCountry.Click();
            LiCountryItem(country).Click();
            ThreadUtils.SleepShortTime();
            ButtonNext.Click();
        }
        #endregion
    }
}
