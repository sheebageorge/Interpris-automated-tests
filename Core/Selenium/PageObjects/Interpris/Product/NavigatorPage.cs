using Automation.UI.Core.CommonUtilities;
using OpenQA.Selenium;

namespace Automation.UI.Core.Selenium.PageObjects.Interpris.Product
{
    /// <summary>
    /// Wrapper class for Navigator
    /// contains all elements and actions for the navigator
    /// </summary>
    public class NavigatorPage : BasePage
    {
        public const string MENU_GET_STARTED = "1";
        public const string MENU_DATA_SOURCES = "2";
        public const string MENU_VIEWS = "3";
        public const string MENU_THEMES = "4";
        public const string MENU_DASHBOARDS = "5";

        #region UI objects
        private readonly string menuGetStarted = "//div[contains(@class,\"isomorphicSidebar\")]//a[contains(@href,\"homePage\")]";
        private readonly string menuDataSources = "//div[contains(@class,\"isomorphicSidebar\")]//a[contains(@href,\"dataSources\")]";
        private readonly string menuViews = "//div[contains(@class,\"isomorphicSidebar\")]//a[contains(@href,\"view\")]";
        private readonly string menuThemes = "//div[contains(@class,\"isomorphicSidebar\")]//a[contains(@href,\"themes\")]";
        private readonly string menuDashboards = "//div[contains(@class,\"isomorphicSidebar\")]//a[contains(@href,\"dashboard\")]";
        #endregion

        public NavigatorPage(IWebDriver driver, string baseURL) : base(driver, baseURL, "") { }

        #region Properties
        public BaseWebObject MenuGetStarted => FindWebElement(menuGetStarted);
        public BaseWebObject MenuDataSources => FindWebElement(menuDataSources);
        public BaseWebObject MenuViews => FindWebElement(menuViews);
        public BaseWebObject MenuThemes => FindWebElement(menuThemes);
        public BaseWebObject MenuDashboards => FindWebElement(menuDashboards);
        #endregion

        #region Methods
        /// <summary>
        /// Click Menu items on the navigator bar
        /// </summary>
        /// <param name="menu">Relevant Menu Constant</param>
        public void ActivateMenu(string menu)
        {
            switch (menu)
            {
                case MENU_GET_STARTED:
                    MenuGetStarted.Click();
                    break;

                case MENU_DATA_SOURCES:
                    MenuDataSources.Click();
                    break;

                case MENU_VIEWS:
                    MenuViews.Click();
                    break;

                case MENU_THEMES:
                    MenuThemes.Click();
                    break;

                case MENU_DASHBOARDS:
                    MenuDashboards.Click();
                    break;

                default:
                    break;
            }
            ThreadUtils.SleepShortTime();
        }
        #endregion
    }
}
