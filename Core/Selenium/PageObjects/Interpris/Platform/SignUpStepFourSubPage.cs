using NUnit.Framework;
using OpenQA.Selenium;

namespace Automation.UI.Core.Selenium.PageObjects.Interpris.Platform
{
    /// <summary>
    /// Wrapper class of Sign Up Step Four Sub Page
    /// contains all elements and actions for Sign Up Step Four Sub Page
    /// </summary>
    public class SignUpStepFourSubPage : BasePage
    {
        #region Page Default
        public const string PAGE_TITLE = "Please enter your card payment details below.";
        public const string ERR_MSG_FIELD_IS_REQUIRED = "Required field not completed";
        #endregion


        #region Page Objects    
        private readonly string divTitle = "//div[contains(@class,\"wrapper-heading-signup-form\")]";
        private readonly string divTitleWithText = $"//div[contains(@class,\"wrapper-heading-signup-form\") and .=\"{PAGE_TITLE}\"]";

        private readonly string spanInfoButton = "//span[contains(@class,\"qsr-icon-info\")]";
        private readonly string divToolTip = "//div[contains(@class,\"ant-tooltip-inner\")]";

        private readonly string frameZouraPayment = "id=z_hppm_iframe";

        private readonly string divIconVisa = "id=card-image-container-Visa";
        private readonly string divIconMasterCard = "id=card-image-container-MasterCard";
        private readonly string divIconAmericanExpress = "id=card-image-container-AmericanExpress";
        private readonly string divIconDiscover = "id=card-image-container-Discover";

        private readonly string inputName = "id=input-creditCardHolderName";
        private readonly string inputCCNumber = "id=input-creditCardNumber";
        private readonly string selectExpirationMonth = "id=input-creditCardExpirationMonth";
        private readonly string selectExpirationYear = "id=input-creditCardExpirationYear";
        private readonly string inputSecurityCode = "id=input-cardSecurityCode";

        private readonly string inputNameError = "id=error-creditCardHolderName";
        private readonly string inputCCNumberError = "id=error-creditCardNumber";
        private readonly string selectExpirationMonthError = "id=error-creditCardExpirationMonth";
        private readonly string inputSecurityCodeError = "id=error-cardSecurityCode";

        private readonly string inputAddress1 = "id=input-creditCardAddress1";
        private readonly string inputAddress2 = "id=input-creditCardAddress2";
        private readonly string inputCity = "id=input-creditCardCity";
        private readonly string inputPostalCode = "id=input-creditCardPostalCode";
        private readonly string selectCountry = "id=input-creditCardCountry";
        private readonly string inputState = "id=input-creditCardState";

        private readonly string inputAddress1Error = "id=error-creditCardAddress1";
        private readonly string inputCityError = "id=error-creditCardCity";
        private readonly string inputPostalCodeError = "id=error-creditCardPostalCode";
        private readonly string selectCountryError = "id=error-creditCardCountry";
        private readonly string inputStateError = "id=error-creditCardState";

        private readonly string buttonSubmit = "id=submitButton";
        private readonly string buttonGoBack = "//button[.=\"Go back\"]";
        #endregion

        public SignUpStepFourSubPage(IWebDriver driver, string baseURL = "") : base(driver, baseURL, "") { }

        #region Properties
        public BaseWebObject DivTitle => FindWebElement(divTitle, true);
        public BaseWebObject DivTitleWithText => FindWebElement(divTitleWithText, true);

        public BaseWebObject SpanInfoButton => FindWebElement(spanInfoButton, true);
        public BaseWebObject DivToolTip => FindWebElement(divToolTip, true);

        public BaseWebObject FrameZouraPayment => FindWebElement(frameZouraPayment, true);

        private BaseWebObject DivIconVisa => FindWebElement(divIconVisa, true);
        private BaseWebObject DivIconMasterCard => FindWebElement(divIconMasterCard, true);
        private BaseWebObject DivIconAmericanExpress => FindWebElement(divIconAmericanExpress, true);
        private BaseWebObject DivIconDiscover => FindWebElement(divIconDiscover, true);

        private BaseWebObject InputName => FindWebElement(inputName, true);
        private BaseWebObject InputCCNumber => FindWebElement(inputCCNumber, true);
        private BaseWebObject SelectExpirationMonth => FindWebElement(selectExpirationMonth, true);
        private BaseWebObject SelectExpirationYear => FindWebElement(selectExpirationYear, true);
        private BaseWebObject InputSecurityCode => FindWebElement(inputSecurityCode, true);

        private BaseWebObject InputNameError => FindWebElement(inputNameError, true);
        private BaseWebObject InputCCNumberError => FindWebElement(inputCCNumberError, true);
        private BaseWebObject SelectExpirationMonthError => FindWebElement(selectExpirationMonthError, true);
        private BaseWebObject InputSecurityCodeError => FindWebElement(inputSecurityCodeError, true);

        private BaseWebObject InputAddress1 => FindWebElement(inputAddress1, true);
        private BaseWebObject InputAddress2 => FindWebElement(inputAddress2, true);
        private BaseWebObject InputCity => FindWebElement(inputCity, true);
        private BaseWebObject InputPostalCode => FindWebElement(inputPostalCode, true);
        private BaseWebObject SelectCountry => FindWebElement(selectCountry, true);
        private BaseWebObject InputState => FindWebElement(inputState, true);

        private BaseWebObject InputAddress1Error => FindWebElement(inputAddress1Error, true);
        private BaseWebObject InputCityError => FindWebElement(inputCityError, true);
        private BaseWebObject InputPostalCodeError => FindWebElement(inputPostalCodeError, true);
        private BaseWebObject SelectCountryError => FindWebElement(selectCountryError, true);
        private BaseWebObject InputStateError => FindWebElement(inputStateError, true);

        private BaseWebObject ButtonSubmit => FindWebElement(buttonSubmit, true);
        public BaseWebObject ButtonGoBack => FindWebElement(buttonGoBack, true);
        #endregion

        #region Methods        
        /// <summary>
        /// Input all fields of card info
        /// </summary>
        public void InputCardInfo(string name, string ccNumber, string month, string year, string securityCode)
        {
            Driver.SwitchTo().Frame(FrameZouraPayment.WebElement);
            InputName.SendKeys(name);
            InputCCNumber.SendKeys(ccNumber);
            SelectExpirationMonth.SelectDropDownListItemByText(month);
            SelectExpirationYear.SelectDropDownListItemByText(year);
            InputSecurityCode.SendKeys(securityCode);
            Driver.SwitchTo().DefaultContent();
        }

        /// <summary>
        /// Input all fields of card address
        /// </summary>
        public void InputCardAddress(string address1, string address2, string city, string postalCode, string country, string state)
        {
            Driver.SwitchTo().Frame(FrameZouraPayment.WebElement);
            InputAddress1.SendKeys(address1);
            InputAddress2.SendKeys(address2);
            InputCity.SendKeys(city);
            InputPostalCode.SendKeys(postalCode);
            SelectCountry.SelectDropDownListItemByText(country);

            if (InputState.WebElement.TagName == "select")
            {
                InputState.SelectDropDownListItemByText(state);
            }
            else
            {
                InputState.SendKeys(state);
            }
            Driver.SwitchTo().DefaultContent();
        }
        
        /// <summary>
        /// Click submit button
        /// </summary>
        public void ClickSubmitButton()
        {
            Driver.SwitchTo().Frame(FrameZouraPayment.WebElement);
            ScrollElementToView(ButtonSubmit);
            ButtonSubmit.Click();
            Driver.SwitchTo().DefaultContent();
        }

        /// <summary>
        /// Check if all fields of card info are displayed
        /// </summary>
        /// <returns>true if all fields are visible</returns>
        public bool IsCardInfoFieldsDisplayed()
        {
            Driver.SwitchTo().Frame(FrameZouraPayment.WebElement);
            bool result = DivIconVisa.IsVisible
                && DivIconMasterCard.IsVisible
                && InputName.IsVisible
                && InputCCNumber.IsVisible
                && SelectExpirationMonth.IsVisible
                && SelectExpirationYear.IsVisible
                && InputSecurityCode.IsVisible;
            Driver.SwitchTo().DefaultContent();

            return result;
        }

        /// <summary>
        /// Check if all fields of card address are displayed
        /// </summary>
        /// <returns>true if all fields are visible</returns>
        public bool IsCardAddressFieldsDisplayed()
        {
            Driver.SwitchTo().Frame(FrameZouraPayment.WebElement);
            bool result = InputAddress1.IsVisible
                && InputAddress2.IsVisible
                && InputCity.IsVisible
                && InputPostalCode.IsVisible
                && SelectCountry.IsVisible
                && InputState.IsVisible;
            Driver.SwitchTo().DefaultContent();

            return result;
        }

        /// <summary>
        /// Check if all fields of IP Payments gateway are displayed
        /// </summary>
        /// <returns>true if all fields are displayed</returns>
        public bool IsIPPaymentsFieldsDisplayed()
        {
            return IsCardInfoFieldsDisplayed();
        }

        /// <summary>
        /// Check if all fields of CyberSource gateway are displayed
        /// </summary>
        /// <returns>true if all fields are displayed</returns>
        public bool IsCyberSourceFieldsDisplayed()
        {
            return IsCardInfoFieldsDisplayed() && IsCardAddressFieldsDisplayed();
        }

        /// <summary>
        /// Check if all error fields of card info are displayed
        /// </summary>
        /// <returns>true if all fields are visible</returns>
        public bool IsCardInfoErrorFieldsDisplayed()
        {
            Driver.SwitchTo().Frame(FrameZouraPayment.WebElement);
            bool result = InputNameError.IsVisible
                && InputCCNumberError.IsVisible
                && SelectExpirationMonthError.IsVisible
                && InputSecurityCodeError.IsVisible;
            Driver.SwitchTo().DefaultContent();

            return result;
        }

        /// <summary>
        /// Check if all error fields of card address are displayed
        /// </summary>
        /// <returns>true if all fields are visible</returns>
        public bool IsCardAddressErrorFieldsDisplayed()
        {
            Driver.SwitchTo().Frame(FrameZouraPayment.WebElement);
            bool result = InputAddress1Error.IsVisible
                && InputCityError.IsVisible
                && InputPostalCodeError.IsVisible
                && SelectCountryError.IsVisible
                && InputStateError.IsVisible;
            Driver.SwitchTo().DefaultContent();

            return result;
        }

        /// <summary>
        /// Check if all error fields of IP Payments gateway are displayed
        /// </summary>
        /// <returns>true if all fields are displayed</returns>
        public bool IsIPPaymentsErrorFieldsDisplayed()
        {
            return IsCardInfoErrorFieldsDisplayed() && IsCardAddressErrorFieldsDisplayed();
        }

        /// <summary>
        /// Check if all error fields of CyberSource gateway are displayed
        /// </summary>
        /// <returns>true if all fields are displayed</returns>
        public bool IsCyberSourceErrorFieldsDisplayed()
        {
            return IsCardInfoErrorFieldsDisplayed() && IsCardAddressErrorFieldsDisplayed();
        }
        #endregion
    }
}
