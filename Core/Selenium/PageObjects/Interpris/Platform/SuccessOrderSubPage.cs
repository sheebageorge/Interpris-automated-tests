using Automation.UI.Core.CommonUtilities;
using OpenQA.Selenium;

namespace Automation.UI.Core.Selenium.PageObjects.Interpris.Platform
{
    /// <summary>
    /// Wrapper class of Success Order Page
    /// contains all elements and actions for Success Order Page
    /// </summary>
    public class SuccessOrderSubPage : BasePage
    {
        #region Page Default
        public const string PAGE_TITLE = "Your purchase has been processed";
        #endregion

        #region Page Objects
        private readonly string divTitle = "//div[contains(@class,\"successOrderPopup\")]//p[@class=\"title\"]";
        private readonly string divPrice = "//div[contains(@class,\"successOrderPopup\")]//div[@class=\"content\"]//p[not(contains(.,'minutes'))]";
        private readonly string divTotalMinutes = "//div[contains(@class,\"successOrderPopup\")]//div[@class=\"content\"]//div[@class='box-item' and contains(.,'minutes')]";
        private readonly string buttonOK = "//div[contains(@class,\"successOrderPopup\")]//button[.=\"Ok\"]";
        #endregion

        public SuccessOrderSubPage(IWebDriver driver, string baseURL = "") : base(driver, baseURL, "") { }

        #region Properties
        public BaseWebObject DivTitle => FindWebElement(divTitle, true);
        public BaseWebObject DivPrice => FindWebElement(divPrice, true);
        public BaseWebObject DivTotalMinutes => FindWebElement(divTotalMinutes, true);
        public BaseWebObject ButtonOK => FindWebElement(buttonOK, true);
        #endregion

        #region Methods

        /// <summary>
        /// Click Ok
        /// </summary>
        public void ClickOk()
        {
            ButtonOK.Click();
        }

        /// <summary>
        /// Get price
        /// </summary>
        public float GetPrice()
        {
            return StringUtils.FilterIntFromString(DivPrice.Text);
        }

        /// <summary>
        /// Get total minutes
        /// </summary>
        public int GetTotalMinutes()
        {
            return StringUtils.FilterIntFromString(DivTotalMinutes.Text);
        }
        #endregion
    }
}
