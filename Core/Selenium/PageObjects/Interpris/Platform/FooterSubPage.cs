using OpenQA.Selenium;

namespace Automation.UI.Core.Selenium.PageObjects.Interpris.Platform
{
    /// <summary>
    /// Wrapper class of Footer Sub Page
    /// contains all elements and actions for Header Sub Page
    /// </summary>
    public class FooterSubPage : BasePage
    {
        public const string FOOTER_HELPFULS_LINK_LABEL_PRIVACY = "Privacy";
        public const string FOOTER_HELPFULS_LINK_LABEL_TERMS_CONDITIONS = "Terms and Conditions";
        public const string FOOTER_HELPFULS_LINK_LABEL_LEGAL = "Legal";
        public const string FOOTER_HELPFULS_LINK_LABEL_CONTACT = "Contact us";
        public const string FOOTER_HELPFULS_LINK_LABEL_COPYRIGHT = "© NVivo";

        public FooterSubPage(IWebDriver driver, string baseURL) : base(driver, baseURL, "") { }

        #region Properties
        public BaseWebObject AFooterHelpfulsLinkByLabel(string linkLabel) => FindWebElement(
            $"//div[@class=\"footer-item\"]/div[@class=\"links\"]/a[./span[text()=\"{linkLabel}\"]]", true);
        #endregion
    }
}
