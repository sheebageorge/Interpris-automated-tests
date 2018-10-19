using Automation.UI.Core.CommonUtilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Drawing;
using System.Threading;

namespace Automation.UI.Core.Selenium
{
    /// <summary>
    /// Wrapper class of Selenium web element class
    /// </summary>
    public class BaseWebObject
    {
        public const string OBJECT_TEXT_TRUNCATED_CSS_VAL = "ellipsis";
        public const string OBJECT_TEXT_CENTER_ALIGNED_CSS_VAL = "center";
        public const string OBJECT_TEXT_MOUSE_POINTER_TYPE_CSS_VAL = "text";

        public BaseWebObject(IWebElement webElement)
        {
            WebElement = webElement;
        }

        public IWebElement WebElement { get; }

        /// <summary>
        /// Get Text attribute of the web element
        /// </summary>
        public string Text
        {
            get { return WebElement.Text; }
        }

        /// <summary>
        /// Get Value attribute of the web element
        /// </summary>
        public string Value
        {
            get { return WebElement.GetAttribute("value"); }
        }

        /// <summary>
        /// Check if the web element visible or not.
        /// Return True if the element visible; otherwise, return False
        /// </summary>
        public bool IsVisible
        {
            get
            {
                try
                {
                    return WebElement.Displayed;
                } catch (StaleElementReferenceException) { }
                catch (NotFoundException) { }

                return false;
            }
        }

        /// <summary>
        /// Check if the web element text truncated or not
        /// </summary>
        public bool IsTextTruncated
        {
            get
            {
                string textOverflowCSSVal = GetCssValue("text-overflow");

                return (!string.IsNullOrEmpty(textOverflowCSSVal)) &&
                    textOverflowCSSVal.Equals(OBJECT_TEXT_TRUNCATED_CSS_VAL);
            }
        }

        /// <summary>
        /// Check if the web element text center aligned or not
        /// </summary>
        public bool IsTextCenterAligned
        {
            get
            {
                string textCenterAlignedCSSVal = GetCssValue("text-align");

                return (!string.IsNullOrEmpty(textCenterAlignedCSSVal)) &&
                    textCenterAlignedCSSVal.Equals(OBJECT_TEXT_CENTER_ALIGNED_CSS_VAL);
            }
        }
        
        /// Get the retangle of the element
        /// </summary>
        public Rectangle ObjRectangle
        {
            get
            {
                return new Rectangle(WebElement.Location, WebElement.Size);
            }
        }

        /// <summary>
        /// Check if the mouse pointer type is text
        /// </summary>
        public bool IsMousePointerTypeText
        {
            get
            {
                string mousePointerTypeTextCSSVal = GetCssValue("cursor");

                return (!string.IsNullOrEmpty(mousePointerTypeTextCSSVal)) &&
                    mousePointerTypeTextCSSVal.Equals(OBJECT_TEXT_MOUSE_POINTER_TYPE_CSS_VAL);
            }
        }

        /// <summary>
        /// Check if the web element is enabled or not.
        /// Return True if the element is enabled; otherwise, return False
        /// </summary>
        public bool IsEnabled
        {
            get { return WebElement.Enabled; }
        }

        /// <summary>
        /// Check if the web element is selected or not.
        /// Return True if the element is selected; otherwise, return False
        /// </summary>
        public bool IsSelected
        {
            get { return WebElement.Selected; }
        }

        /// <summary>
        /// Do Click action on the web element
        /// </summary>
        public void Click()
        {
            WebElement.Click();
        }

        /// <summary>
        /// Wait and click on the element
        /// </summary>
        /// <param name="MAX_WAIT"></param>
        public void WaitAndClick(int MAX_WAIT = 30)
        {
            int i = 0;
            while(i < MAX_WAIT)
            {
                i++;

                try
                {
                    if (WebElement.Displayed && WebElement.Enabled)
                    {
                        WebElement.Click();
                        return;
                    }
                }
                catch (StaleElementReferenceException) { }
                catch (WebDriverException) { }

                ThreadUtils.SleepShortTime();
            }
        }

        /// <summary>
        /// Clear the text box and type keys into the text box
        /// </summary>
        /// <param name="inputText">String to type</param>
        /// <param name="submit">Flag to type Enter after typing the full inputText</param>
        public void SendKeys(string inputText, bool submit = false)
        {
            WebElement.Clear();

            WebElement.SendKeys(inputText);

            if (submit)
            {
                WebElement.Submit();
            }
        }

        /// <summary>
        /// Get Attribute of Element
        /// </summary>
        /// <param name="attributeName"></param>
        public string GetAttribute(string attributeName) => WebElement.GetAttribute(attributeName);

        /// <summary>
        /// Get CSS value of Element
        /// </summary>
        /// <param name="propertyName">Name of the property</param>
        public string GetCssValue(string propertyName) => WebElement.GetCssValue(propertyName);

        /// <summary>
        /// Select a dropdown list by text
        /// </summary>
        /// <param name="text">item in list</param>
        public void SelectDropDownListItemByText(string text)
        {
            SelectElement objSel = new SelectElement(WebElement);
            objSel.SelectByText(text);
        }
    }
}
