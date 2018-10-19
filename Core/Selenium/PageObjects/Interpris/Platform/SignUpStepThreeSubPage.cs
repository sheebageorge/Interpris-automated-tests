using Automation.UI.Core.CommonUtilities;
using OpenQA.Selenium;

namespace Automation.UI.Core.Selenium.PageObjects.Interpris.Platform
{
    /// <summary>
    /// Wrapper class of Sign Up Step Three Sub Page
    /// contains all elements and actions for Sign Up Step Three Sub Page
    /// </summary>
    public class SignUpStepThreeSubPage : BasePage
    {
        #region Page Default
        public const string PAGE_TITLE = "What is your area of research?";

        public const string ERR_MSG_SECTOR_IS_REQUIRED = "Sector is required";
        public const string ERR_MSG_ORGANIZATION_IS_REQUIRED = "Organization is required";
        #endregion

        #region Page Objects
        private readonly string formMain = "//form";
        private readonly string divTitle = "//div[@class=\"title\"]";
        private readonly string divTitleWithText = $"//div[@class=\"title\" and .=\"{PAGE_TITLE}\"]";
        private readonly string divSector = "id=sector";
        private readonly string divSectorError = "//span[descendant::div[@id=\"sector\"]]/following-sibling::div[@class=\"ant-form-explain\"]";
        private readonly string inputOrganization = "//input[@id=\"organisation\"]";
        private readonly string divOrganizationError = "//span[descendant::input[@id=\"organisation\"]]/following-sibling::div[@class=\"ant-form-explain\"]";
        private readonly string buttonNext = "//button[.=\"Next\"]";
        private readonly string buttonGoBack = "//button[.=\"Go back\"]";
        #endregion

        public SignUpStepThreeSubPage(IWebDriver driver, string baseURL = "") : base(driver, baseURL, "") { }

        #region Properties
        public BaseWebObject FormMain => FindWebElement(formMain, true);
        public BaseWebObject DivTitle => FindWebElement(divTitle, true);
        public BaseWebObject DivTitleWithText => FindWebElement(divTitleWithText, true);
        public BaseWebObject DivSector => FindWebElement(divSector, true);
        public BaseWebObject DivSectorError => FindWebElement(divSectorError, true);
        public BaseWebObject InputOrganization => FindWebElement(inputOrganization, true);
        public BaseWebObject DivOrganizationError => FindWebElement(divOrganizationError, true);
        public BaseWebObject ButtonNext => FindWebElement(buttonNext, true);
        public BaseWebObject ButtonGoBack => FindWebElement(buttonGoBack, true);

        public BaseWebObject LiSectorItem(string sectorName) => FindWebElement($"//li[@role=\"option\" and text()=\"{sectorName}\"]", true);
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
        /// Input sector and country
        /// Click next button
        /// </summary>
        public void SubmitFormData(string sector, string organization)
        {
            WaitForElementVisible(DivTitleWithText);
            DivSector.Click();
            LiSectorItem(sector).Click();
            InputOrganization.SendKeys(organization);
            ThreadUtils.SleepShortTime();
            ButtonNext.Click();
        }
        #endregion
    }
}
