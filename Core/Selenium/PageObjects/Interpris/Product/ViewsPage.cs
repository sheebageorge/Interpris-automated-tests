using OpenQA.Selenium;
using System.Collections.Generic;
using NUnit.Framework;
using System;
using Automation.UI.Core.CommonUtilities;
using Automation.UI.Core.Selenium.PageObjects.Interpris.Platform;
using Automation.UI.Core.TestBase;

namespace Automation.UI.Core.Selenium.PageObjects.Interpris.Product
{
    /// <summary>
    /// Wrapper class of Views Page
    /// contains all elements and actions for the Views Page
    /// </summary>
    public class ViewsPage: LandingPage
    {
        public override string PageName { get; protected set; } = "Views";
        #region UI Objects
        private readonly string btnNewView = "//*/div[@class=\"btn-component\"]/button";
        //private readonly string iRenameIcon = "//div[@class=\"isoEditData\"]";
        private readonly string iRenameIcon = "//div[@class=\"isoEditData\"]//i[contains(@class, \"anticon-edit\")]";
        private readonly string deleteIcon = "//td[@class=\"align-text-right\"]";
        // private readonly string listThemes = "//body//div//ul[@role=\"listbox\"]/li[@role=\"option\"]";
        //private readonly string divThemeDropDown = "//div[contains(@class, \"ant-select-selection__rendered\")]";
        private readonly string trViews = "//table//tbody//tr";

        #endregion

        public ViewsPage(IWebDriver driver, string baseURL) : base(driver, baseURL) { }

        #region Properties
        //public BaseWebObject DivThemeDropDown => FindWebElement(divThemeDropDown, true);
        public BaseWebObject BtnNewView => FindWebElement(btnNewView, true);
        public BaseWebObject IRenameIcon => FindWebElement(iRenameIcon, true);
        public BaseWebObject DeleteIcon => FindWebElement(deleteIcon, true);
        
        public IList<BaseWebObject> TRViews => FindWebElements(trViews);       

        #endregion

        #region Methods
        
        /// <summary>
        /// Check if the Views page is active & displayed
        /// </summary>
        /// <returns>True if the page displayed; otherwise, False</returns>
        public void CreateView()
        {
            BtnNewView.Click();

            

            //open and close to trigger the population of existing themes
            // DivThemeDropDown.Click();//open dropDown
            //DivThemeDropDown.Click();//close dropDown

            // foreach (BaseWebObject item in ListThemes)
            // {
            //    TestContext.Out.WriteLine(item.Text.Trim());   
            //}
        }

        /// <summary>
        /// Check if the Views page is active & displayed
        /// </summary>
        /// <returns>True if the page displayed; otherwise, False</returns>
        public void RenameView()
        {
            //IRenameIcon.Click();
            DeleteIcon.Click();
            ThreadUtils.SleepMediumTime();

            //open and close to trigger the population of existing themes
            // DivThemeDropDown.Click();//open dropDown
            //DivThemeDropDown.Click();//close dropDown

            // foreach (BaseWebObject item in ListThemes)
            // {
            //    TestContext.Out.WriteLine(item.Text.Trim());   
            //}
        }

        public void FindView(String viewName, Boolean exactMatch)
        {
            Boolean found = false;
             foreach (BaseWebObject row in TRViews)
             {
                //row 
            }
        }

        #endregion
    }
}
