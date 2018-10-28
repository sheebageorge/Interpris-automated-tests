using Automation.UI.Core.CommonUtilities;
using Automation.UI.Core.DataProcessing;
using Automation.UI.Core.Selenium;
using Automation.UI.Core.Selenium.PageObjects.Interpris.Platform;
using Automation.UI.Core.Selenium.PageObjects.Interpris.Product;
using Automation.UI.Core.TestAttributes;
using Automation.UI.Core.TestBase;
using Automation.UI.Functionality.TestAttributes;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Automation.UI.Functionality.Test
{
    /// <summary>
    /// Test Suite for views
    /// </summary>
    [TestFixture]
    public class ViewsTest : InterprisTestBase
    {
        #region Test Cases
        [Test]
        [TestID(TestID.TC_ID_0050), StoryID(StoryID.SR_ID_003)]
        [Priority(PriorityLevel.High)]
        [TestCaseSource(typeof(DataProvider), "PrepareTestCases", new object[] { TestID.TC_ID_0001 })]
        public void TC_VIEW_DeleteDefaultView(Dictionary<string, string> Data)
        {
            TestContext.Out.WriteLine("Start Test Case - {0}", TestID.TC_ID_0050);

            LoginPage loginPage = new LoginPage(Driver, InterprisBaseURL);
            HeaderSubPage headerSubPage = new HeaderSubPage(Driver, InterprisBaseURL);
            NavigatorPage navigatorPage = new NavigatorPage(Driver, InterprisBaseURL);

            // Verify sign in successfully with the activated account
            SignInSuccess(loginPage, headerSubPage, Data["username"], Data["password"]);



            navigatorPage.ActivateMenu(NavigatorPage.MENU_VIEWS);
            navigatorPage.ActivateMenu(NavigatorPage.MENU_THEMES);
            TestContext.Out.WriteLine("End Test Case - {0}", TestID.TC_ID_0050);
        }
        #endregion

        #region Test Cases
        [Test]
        [TestID(TestID.TC_ID_0051), StoryID(StoryID.SR_ID_003)]
        [Priority(PriorityLevel.High)]
        [TestCaseSource(typeof(DataProvider), "PrepareTestCases", new object[] { TestID.TC_ID_0001 })]
        public void TC_VIEW_CreateViewManually(Dictionary<string, string> Data)
        {
            TestContext.Out.WriteLine("Start Test Case - {0}", TestID.TC_ID_0051);

            LoginPage loginPage = new LoginPage(Driver, InterprisBaseURL);
            HeaderSubPage headerSubPage = new HeaderSubPage(Driver, InterprisBaseURL);
            NavigatorPage navigatorPage = new NavigatorPage(Driver, InterprisBaseURL);

            SignInSuccess(loginPage, headerSubPage, Data["username"], Data["password"]);
            TestContext.Out.WriteLine("Click on the menu - view");

            navigatorPage.ActivateMenu(NavigatorPage.MENU_VIEWS);

            TestContext.Out.WriteLine("Click on the menu - themes");
            navigatorPage.ActivateMenu(NavigatorPage.MENU_THEMES);
            TestContext.Out.WriteLine("End Test Case - {0}", TestID.TC_ID_0051);
        }
        #endregion
        #region Public methods
        /// <summary>
        /// Try to log in with valid username and password
        /// Expect to get in the Landing page
        /// </summary>
        /// <param name="loginPage">Login page</param>
        /// <param name="landingPage">Landing page</param>
        /// <param name="username">Valid username</param>
        /// <param name="password">Valid password</param>
        public static void SignInSuccess(LoginPage loginPage, HeaderSubPage headerSubPage,
            string username, string password)
        {
            TestContext.Out.WriteLine("Go to Login Page");
            loginPage.Navigate();

            TestContext.Out.WriteLine("Click Log In tab and Fill required information");
            loginPage.InputLoginInfo(username, password);

            TestContext.Out.WriteLine("Verify landed to Home Page");
            Assert.IsTrue(headerSubPage.TextHomeScreen != null, "Home text is not visible");
        }
        #endregion
    }
}
