using Automation.UI.Core.CommonUtilities;
using Automation.UI.Core.TestBase;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;

namespace Automation.UI.Core.Selenium
{
    /// <summary>
    /// SearchBy enum for Selenium web element object type
    /// </summary>
    public enum SearchBy
    {
        ID,
        XPath,
        Name,
        ClassName,
        TagName,
        CssSelector
    }

    /// <summary>
    /// Base class of all web pages
    /// </summary>
    public class BasePage
    {
        private const int MAX_WAIT_ELEMENT = 30;

        public BasePage(IWebDriver driver, string baseURL, string pageURL)
        {
            Driver = driver;
            BaseURL = baseURL;
            PageURL = pageURL;
        }

        public IWebDriver Driver { get; }
        public string BaseURL { get; }
        public string PageURL { get; }

        /// <summary>
        /// Find web element by properties
        /// </summary>
        /// <param name="locator">Locator string to find the element</param>
        /// <param name="waitForElementVisible">Flag to verify the found element visible or not</param>
        /// <returns>Wrapper object of Selenium web element</returns>
        public BaseWebObject FindWebElement(string locator, bool waitForElementVisible = false)
        {
            By byObj = GetWebElementByObject(locator);
            BaseWebObject findObject = null;

            try
            {
                if (byObj != null)
                {
                    if (!waitForElementVisible)
                    {
                        findObject = new BaseWebObject(Driver.FindElement(byObj));
                    }
                    else
                    {
                        findObject = new BaseWebObject(Driver.FindElement(byObj));

                        WaitForElementVisible(findObject);
                    }
                }
            } catch (StaleElementReferenceException ex)
            {
                TestContext.Out.WriteLine("StaleElementReferenceException: {0}", ex.Message);
            } catch (NotFoundException nex)
            {
                TestContext.Out.WriteLine("NotFoundException: {0}", nex.Message);
            }

            return findObject;
        }

        /// <summary>
        /// Wait until the element visible
        /// </summary>
        /// <param name="element"></param>
        /// <param name="MAX_WAIT"></param>
        public void WaitForElementVisible(BaseWebObject element, int MAX_WAIT = 100)
        {
            int iCount = 0;
            while(iCount < MAX_WAIT)
            {
                iCount++;

                try
                {
                    if (element.IsVisible)
                    {
                        return;
                    }
                    else
                    {
                        ThreadUtils.SleepVeryShortTime();
                    }
                }
                catch (StaleElementReferenceException)
                {
                    continue;
                }
            }
        }

        /// <summary>
        /// Find all web elements by properties
        /// </summary>
        /// <param name="locator">Locator string to find the element</param>
        /// <returns>List of Wrapper objects of Selenium web elements</returns>
        public IList<BaseWebObject> FindWebElements(string locator)
        {
            By byObj = GetWebElementByObject(locator);
            List<BaseWebObject> findObjects = new List<BaseWebObject>();

            try
            {
                if (byObj != null)
                {
                    var foundObjs = Driver.FindElements(byObj);

                    if (foundObjs != null)
                    {
                        foreach(var foundObj in foundObjs)
                        {
                            findObjects.Add(new BaseWebObject(foundObj));
                        }
                    }
                }
            }
            catch (StaleElementReferenceException ex)
            {
                TestContext.Out.WriteLine("StaleElementReferenceException: {0}", ex.Message);
            }
            catch (NotFoundException nex)
            {
                TestContext.Out.WriteLine("NotFoundException: {0}", nex.Message);
            }

            return findObjects;
        }

        /// <summary>
        /// Get Selenium By object from locator string
        /// </summary>
        /// <param name="locator">Locator string to find the element</param>
        /// <returns>Selenium By object</returns>
        public By GetWebElementByObject(string locator)
        {
            By byObj = null;

            SearchBy searchType = SearchBy.ID;
            string searchStr = locator;

            if (locator.StartsWith("//"))
            {
                searchType = SearchBy.XPath;
            }
            else
            {
                int idx = locator.IndexOf('=');

                if (idx > 0)
                {
                    searchType = (SearchBy)Enum.Parse(typeof(SearchBy), locator.Substring(0, idx), true);
                    searchStr = locator.Substring(idx + 1);
                }
            }

            switch (searchType)
            {
                case SearchBy.ID:
                    byObj = By.Id(searchStr);
                    break;
                case SearchBy.XPath:
                    byObj = By.XPath(searchStr);
                    break;
                case SearchBy.Name:
                    byObj = By.Name(searchStr);
                    break;
                case SearchBy.ClassName:
                    byObj = By.ClassName(searchStr);
                    break;
                case SearchBy.TagName:
                    byObj = By.TagName(searchStr);
                    break;
                case SearchBy.CssSelector:
                    byObj = By.CssSelector(searchStr);
                    break;
                default:
                    break;
            }

            return byObj;
        }

        /// <summary>
        /// Get absolute URL link string
        /// </summary>
        /// <param name="baseURL">Base URL</param>
        /// <returns>Absolute URL link string</returns>
        public string GetURL()
        {
            if (PageURL != null)
            {
                return new Uri(new Uri(BaseURL), PageURL).ToString();
            }

            return BaseURL;
        }

        /// <summary>
        /// Navigate to the page URL
        /// </summary>
        public void Navigate()
        {
            Driver.Navigate().GoToUrl(GetURL());
        }

        /// <summary>
        /// Get page title
        /// </summary>
        /// <returns></returns>
        public string GetPageTitle()
        {
            return Driver.Title;
        }

        /// <summary>
        /// Navigate to the given URL
        /// </summary>
        /// <param name="strURL"></param>
        public void NavigateTo(string strURL)
        {
            Driver.Navigate().GoToUrl(strURL);
        }

        /// <summary>
        /// Verify text of title of page if contains the search string
        /// </summary>
        /// <param name="searchStr">String to verify</param>
        public void VerifyPageTitleContains(string searchStr)
        {
            StringAssert.Contains(searchStr, Driver.Title);
        }

        /// <summary>
        /// Search if the given text appears in the page contents
        /// </summary>
        /// <param name="searchText"></param>
        /// <returns>True if the text found; otherwise, False</returns>
        public bool IsPageContainsText(string searchText)
        {
            try
            {
                if (Driver != null)
                {
                    return Driver.FindElement(By.TagName("body")).Text.Contains(searchText);
                }
            } catch (Exception ex)
            {
                TestContext.Error.WriteLine("IsPageContainsText ERR: {0}", ex.Message);
            }

            return false;
        }

        /// <summary>
        /// Get page text
        /// </summary>
        /// <returns>Page text</returns>
        public string GetPageText()
        {
            try
            {
                if (Driver != null)
                {
                    return Driver.FindElement(By.TagName("body")).Text.Trim();
                }
            }
            catch (Exception ex)
            {
                TestContext.Error.WriteLine("IsPageContainsText ERR: {0}", ex.Message);
            }

            return "";
        }

        /// <summary>
        /// Refresh the current page
        /// </summary>
        public void Refresh()
        {
            Driver.Navigate().Refresh();
        }

        /// <summary>
        /// Back to the last page
        /// </summary>
        public void Back()
        {
            Driver.Navigate().Back();
        }

        /// <summary>
        /// Count web element with the same locator
        /// </summary>
        /// <param name="locator">element locator</param>
        /// <returns></returns>
        public int CountWebElement(string locator)
        {
            try
            {
                return FindWebElements(locator).Count;
            }
            catch (Exception)
            {
                return 0;
            }
        }
       
        /// <summary>
        /// Verify if the element in the visible screen
        /// </summary>
        /// <param name="element">Element to check</param>
        /// <returns>True if the element visible; otherwise, False</returns>
        public bool IsVisibleInViewport(BaseWebObject element)
        {
            try
            {
                return (bool)((IJavaScriptExecutor)Driver).ExecuteScript(
                    "var elem = arguments[0],                 " +
                    "  box = elem.getBoundingClientRect(),    " +
                    "  cx = box.left + box.width / 2,         " +
                    "  cy = box.top + box.height / 2,         " +
                    "  e = document.elementFromPoint(cx, cy); " +
                    "for (; e; e = e.parentElement) {         " +
                    "  if (e === elem)                        " +
                    "    return true;                         " +
                    "}                                        " +
                    "return false;                            "
                    , element.WebElement);
            }
            catch (InvalidCastException) { }
            catch (StaleElementReferenceException) { }

            return element.IsVisible;
        }

        /// <summary>
        /// Check if the element has scrollbar
        /// </summary>
        /// <param name="element">Element to check</param>
        /// <returns>True if the element has scrollbar; otherwise, False</returns>
        public bool HasScrollbar(BaseWebObject element)
        {
            return (bool)((IJavaScriptExecutor)Driver).ExecuteScript(
                    "return arguments[0].scrollHeight > arguments[0].offsetHeight;", element.WebElement);
        }

        /// <summary>
        /// Scroll the element into view
        /// </summary>
        /// <param name="element"></param>
        public void ScrollElementToView(BaseWebObject element)
        {
            if (!IsVisibleInViewport(element))
            {
                //((IJavaScriptExecutor)Driver).ExecuteScript(
                //    "window.scrollBy(0,{0})", element.WebElement.Location.Y);
                ((IJavaScriptExecutor)Driver).ExecuteScript(
                    "arguments[0].scrollIntoView();", element.WebElement);
            }
        }

        /// <summary>
        /// Set caret at the given position in the next field
        /// </summary>
        /// <param name="element">Web Element</param>
        /// <param name="caretPos">Position of the caret</param>
        public void SetCaretAtPositionInTextContent(BaseWebObject element, int caretPos)
        {
            ((IJavaScriptExecutor)Driver).ExecuteScript(string.Format(
               "var el = arguments[0];" +
               "var range = document.createRange();" +
               "var sel = window.getSelection();" +
               "range.setStart(el.childNodes[0], {0});" +
               "range.collapse(true);" +
               "sel.removeAllRanges();" +
               "sel.addRange(range);" +
               "el.focus(); ", caretPos), element.WebElement);
        }

        /// <summary>
        /// Select a text by the number of words
        /// </summary>
        /// <param name="charCountNumber">number of chars to select</param>
        public void SelectTextByCharCountJS(BaseWebObject element, int startPos, int charCountNumber)
        {
            ((IJavaScriptExecutor)Driver).ExecuteScript(string.Format(
               "var el = arguments[0];" +
               "var range = document.createRange();" +
               "var sel = window.getSelection();" +
               "range.setStart(el.childNodes[0], {0});" +
               "range.setEnd(el.childNodes[0], {1});" +
               "sel.removeAllRanges();" +
               "sel.addRange(range);", startPos, startPos + charCountNumber), element.WebElement);
        }

        /// <summary>
        /// Select text from multi elements
        /// </summary>
        public void SelectTextMultiElementByCharCountJS(BaseWebObject element1, BaseWebObject element2,
            int startPos, int endPos)
        {
            ((IJavaScriptExecutor)Driver).ExecuteScript(string.Format(
               "var el1 = arguments[0]; var el2 = arguments[1];" +
               "var range = document.createRange();" +
               "var sel = window.getSelection();" +
               "range.setStart(el1.childNodes[0], {0});" +
               "range.setEnd(el2.childNodes[0], {1});" +
               "sel.removeAllRanges();" +
               "sel.addRange(range);", startPos, endPos), element1.WebElement, element2.WebElement);
        }

        /// <summary>
        /// Type text on keyboard
        /// </summary>
        /// <param name="key">text</param>
        public void TypeOnKeyboard(string key)
        {
            Actions builder = new Actions(Driver);
            builder.SendKeys(key).Build().Perform();

            if (WebUITestBaseClass.Browser.Equals(BrowserConstants.BROWSER_IE))
            {
                // try to type delete button
                if (key == Keys.Delete)
                {
                    keybd_event(VK_DELETE, 0x9d, 0, 0); // press
                    keybd_event(VK_DELETE, 0x9d, KEYEVENTF_KEYUP, 0); // release
                }
            }
        }
        
        /// <summary>
        /// Try to delete all text in text box
        /// </summary>
        /// <param name="element"></param>
        public void TryToCleanUpTextboxValue(BaseWebObject element, int MAX_COUNT = 250)
        {
            int iCount = 0;
            while (!string.IsNullOrEmpty(element.Value.Trim()) && iCount < MAX_COUNT)
            {
                ActionMouseDoubleClick(element);
                TypeOnKeyboard(Keys.Delete);
            }
        }

        /// <summary>
        /// Do double click on the element
        /// </summary>
        /// <param name="element">Element to move to</param>
        public void ActionMouseDoubleClick(BaseWebObject element)
        {
            Actions builder = new Actions(Driver);
            builder.MoveToElement(element.WebElement).DoubleClick(element.WebElement).Build().Perform();
        }

        /// <summary>
        /// Fire the real keyboard event
        /// </summary>
        /// <param name="bVk"></param>
        /// <param name="bScan"></param>
        /// <param name="dwFlags"></param>
        /// <param name="dwExtraInfo"></param>
        [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
        static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);
        public const int KEYEVENTF_KEYUP = 0x0002; // Key up flag
        public const int VK_CONTROL = 0x11; // Control key code
        public const int VK_SHIFT = 0x10; // Shift key code
        public const int VK_DELETE = 0x2E; // Delete key code

        /// <summary>
        /// Press ctrl while clicking on the elements for multi selection
        /// </summary>
        /// <param name="baseWebElements">List of elements to click</param>
        public void MultiSelectItemsByCtrl(IList<BaseWebObject> baseWebElements)
        {
            try
            {
                TestContext.Out.WriteLine("MultiSelectItemsByCtrl on browser: {0}", WebUITestBaseClass.Browser);

                if (WebUITestBaseClass.Browser.Equals(BrowserConstants.BROWSER_IE))
                {
                    // press ctrl
                    keybd_event(VK_CONTROL, 0x9d, 0, 0);
                }

                Actions builder = new Actions(Driver);

                // press control
                builder = builder.KeyDown(Keys.Control);

                foreach (BaseWebObject element in baseWebElements)
                {
                    ScrollElementToView(element);

                    TestContext.Out.WriteLine("Check element in viewport: {0}", IsVisibleInViewport(element));

                    if (IsVisibleInViewport(element))
                    {
                        builder = builder.MoveToElement(element.WebElement).Click(element.WebElement);
                    }
                    else
                    {
                        break;   
                    }
                }

                // do action
                builder.KeyUp(Keys.Control).Build().Perform();
            }
            catch (InvalidOperationException ex)
            {
                TestContext.Out.WriteLine("MultiSelectItemsByCtrl ERR: {0}", ex.Message);
                baseWebElements[0].Click();
            }
            finally
            {
                if (WebUITestBaseClass.Browser.Equals(BrowserConstants.BROWSER_IE))
                {
                    // Ctrl Release
                    keybd_event(VK_CONTROL, 0x9d, KEYEVENTF_KEYUP, 0);
                }
            }
        }

        /// <summary>
        /// Simulate the mouse move to the web element
        /// </summary>
        /// <param name="element">Element to move to</param>
        public void ActionMouseMoveToElement(BaseWebObject element)
        {
            Actions builder = new Actions(Driver);
            builder.MoveToElement(element.WebElement).Build().Perform();
        }

        /// <summary>
        /// Simulate the mouse click on the web element
        /// </summary>
        /// <param name="element">Element to click</param>
        public void ActionMouseClick(BaseWebObject element)
        {
            Actions builder = new Actions(Driver);
            builder.MoveToElement(element.WebElement).Click(element.WebElement).Build().Perform();
        }

        /// <summary>
        /// Press Ctrl while clicking the element
        /// </summary>
        /// <param name="element">Element to click</param>
        public void ActionCtrlMouseClick(BaseWebObject element)
        {
            if (WebUITestBaseClass.Browser.Equals(BrowserConstants.BROWSER_IE))
            {
                // press ctrl
                keybd_event(VK_CONTROL, 0x9d, 0, 0);
            }

            try
            {
                Actions builder = new Actions(Driver);
                builder.KeyDown(Keys.Control).MoveToElement(element.WebElement)
                    .Click(element.WebElement).KeyUp(Keys.Control).Build().Perform();
            }
            finally
            {
                if (WebUITestBaseClass.Browser.Equals(BrowserConstants.BROWSER_IE))
                {
                    // Ctrl Release
                    keybd_event(VK_CONTROL, 0x9d, KEYEVENTF_KEYUP, 0);
                }
            }
        }

        /// <summary>
        /// Press Shift while clicking the element
        /// </summary>
        /// <param name="element">Element to click</param>
        public void ActionShiftMouseClick(BaseWebObject element)
        {
            // press shift
            keybd_event(VK_SHIFT, 0x9d, 0, 0);

            try
            {
                Actions builder = new Actions(Driver);
                builder.KeyDown(Keys.Shift).MoveToElement(element.WebElement)
                    .Click(element.WebElement).KeyUp(Keys.Shift).Build().Perform();
            }
            finally
            {
                // shift Release
                keybd_event(VK_SHIFT, 0x9d, KEYEVENTF_KEYUP, 0);
            }
        }

        /// <summary>
        /// Click the element at the given offset
        /// </summary>
        /// <param name="element"></param>
        /// <param name="offsetX"></param>
        /// <param name="offsetY"></param>
        public void ActionMouseClickAtOffset(BaseWebObject element, int offsetX, int offsetY)
        {
            Actions builder = new Actions(Driver);
            builder.MoveToElement(element.WebElement, offsetX, offsetY).Click().Build().Perform();
        }

        /// <summary>
        /// Drag and drop element to specified offset
        /// </summary>
        /// <param name="element">Element to click</param>
        public void ActionDragAndDropToOffset(BaseWebObject element, int offsetX, int offsetY)
        {
            Actions actions = new Actions(Driver);
            actions.DragAndDropToOffset(element.WebElement, offsetX, offsetY).Perform();
        }
    }
}
