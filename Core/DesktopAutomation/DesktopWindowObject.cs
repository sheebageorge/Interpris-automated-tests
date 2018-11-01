using Automation.UI.Core.Selenium;
using System;
using System.Threading;
using UIAutomationClient;

namespace Automation.UI.Core.DesktopAutomation
{
    /// <summary>
    /// Utility class to interact with the desktop window
    /// </summary>
    public class DesktopWindowObject
    {
        public const int MAX_WAIT_ELEMENT_SEARCH = 50;
        public const int MAX_SLEEP_TIME = 100;

        private static IUIAutomation uiAutomation;

        // pattern ids
        protected readonly int patternIdInvoke = 10000; // UIA_InvokePatternId
        protected readonly int patternIdSelectionItem = 10010; // UIA_SelectionItemPatternId
        protected readonly int patternIdSelection = 10001; // UIA_SelectionPatternId
        protected readonly int patternIdValueItem = 10002; // UIA_ValuePatternId

        // request pattern objects
        protected IUIAutomationCacheRequest cacheRequestInvokePattern = null;
        protected IUIAutomationCacheRequest cacheRequestSelectionItemPattern = null;
        protected IUIAutomationCacheRequest cacheRequestSelectionPattern = null;
        protected IUIAutomationCacheRequest cacheRequestInvokeSelectPattern = null;
        protected IUIAutomationCacheRequest cacheRequestValueItemPattern = null;

        // property ids
        protected readonly int propertyIdClassName = 30012; // UIA_ClassNamePropertyId
        protected readonly int propertyIdName = 30005; // UIA_NamePropertyId

        public DesktopWindowObject()
        {
            SetUpActionPattern();
        }

        #region Methods
        /// <summary>
        /// Set Up the action pattern for UI Automation Element
        /// </summary>
        private void SetUpActionPattern()
        {
            cacheRequestInvokePattern = GetUIAutomation().CreateCacheRequest();
            cacheRequestInvokePattern.AddPattern(patternIdInvoke);

            cacheRequestSelectionItemPattern = GetUIAutomation().CreateCacheRequest();
            cacheRequestSelectionItemPattern.AddPattern(patternIdSelectionItem);

            cacheRequestSelectionPattern = GetUIAutomation().CreateCacheRequest();
            cacheRequestSelectionPattern.AddPattern(patternIdSelection);

            cacheRequestInvokeSelectPattern = GetUIAutomation().CreateCacheRequest();
            cacheRequestInvokeSelectPattern.AddPattern(patternIdInvoke);
            cacheRequestInvokeSelectPattern.AddPattern(patternIdSelectionItem);

            cacheRequestValueItemPattern = GetUIAutomation().CreateCacheRequest();
            cacheRequestValueItemPattern.AddPattern(patternIdValueItem);
        }
        #endregion

        #region Properties
        public static IUIAutomation GetUIAutomation()
        {
            if (uiAutomation == null)
            {
                uiAutomation = new CUIAutomation8();
            }

            return uiAutomation;
        }
        #endregion

        #region Static methods
        /// <summary>
        /// Count all windows on root by browser type
        /// </summary>
        /// <param name="browserType"></param>
        /// <returns></returns>
        public static int CountAllWindowsFromDesktop(string browserType)
        {
            IUIAutomationElement elementRoot = GetUIAutomation().GetRootElement();
            int propertyIdClassName = 30012;
            string countClassName = "";

            switch (browserType)
            {
                case BrowserConstants.BROWSER_CHROME:
                    countClassName = "Chrome_WidgetWin_1";
                    break;
                case BrowserConstants.BROWSER_FIREFOX:
                    countClassName = "MozillaWindowClass";
                    break;
                case BrowserConstants.BROWSER_IE:
                    countClassName = "IEFrame";
                    break;
                case BrowserConstants.BROWSER_EDGE:
                    countClassName = "ApplicationFrameWindow";
                    break;
                case BrowserConstants.BROWSER_SAFARI:
                    break;
                default:
                    break;
            }

            return elementRoot.FindAll(TreeScope.TreeScope_Children,
                GetUIAutomation().CreatePropertyCondition(propertyIdClassName, countClassName)).Length;
        }
        #endregion

        #region Search Nodes
        /// <summary>
        /// Get Window element
        /// </summary>
        /// <param name="searchCondition">search condition</param>
        /// <returns>The UI Automation Element</returns>
        public IUIAutomationElement GetWindowElement(IUIAutomationCondition searchCondition)
        {
            return GetUIAutomation().GetRootElement().FindFirst(TreeScope.TreeScope_Children, searchCondition);
        }

        /// <summary>
        /// Get the first element of the Parent Node by condition
        /// </summary>
        /// <param name="parent">Parent Automation Element</param>
        /// <param name="searchCondition">search condition</param>
        /// <returns>The UI Automation Element</returns>
        public IUIAutomationElement GetChildNodeElement(IUIAutomationElement parent, TreeScope treeScope,
            IUIAutomationCondition searchCondition, IUIAutomationCacheRequest cacheRequest = null)
        {
            IUIAutomationElement searchNode = null;

            try
            {
                // Finds if automation element is available
                if (parent != null)
                {
                    int ct = 0;
                    do
                    {
                        if (cacheRequest != null)
                        {
                            searchNode = parent.FindFirstBuildCache(treeScope, searchCondition, cacheRequest);
                        }
                        else
                        {
                            searchNode = parent.FindFirst(treeScope, searchCondition);
                        }

                        ++ct;
                        Thread.Sleep(MAX_SLEEP_TIME);
                    }
                    while (searchNode == null && ct < MAX_WAIT_ELEMENT_SEARCH);
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("GetChildNodeElement ERR: {0}", ex.Message);
            }

            return searchNode;
        }

        /// <summary>
        /// Get all child elements in node
        /// </summary>
        /// <param name="parent">Parent Automation Element</param>
        /// <param name="searchCondition">search condition</param>
        /// <returns>The UI Automation Element</returns>
        public IUIAutomationElementArray GetAllChildrenNodeElements(IUIAutomationElement parent, TreeScope treeScope,
            IUIAutomationCondition searchCondition, IUIAutomationCacheRequest cacheRequest = null)
        {
            IUIAutomationElementArray searchNodes = null;

            try
            {
                // Finds if automation element is available
                if (parent != null)
                {
                    int ct = 0;
                    do
                    {
                        if (cacheRequest != null)
                        {
                            searchNodes = parent.FindAllBuildCache(treeScope, searchCondition, cacheRequest);
                        }
                        else
                        {
                            searchNodes = parent.FindAll(treeScope, searchCondition);
                        }

                        ++ct;
                        Thread.Sleep(MAX_SLEEP_TIME);
                    }
                    while (searchNodes == null && ct < MAX_WAIT_ELEMENT_SEARCH);
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("GetAllChildrenNodeElements ERR: {0}", ex.Message);
            }

            return searchNodes;
        }
        #endregion

        #region Common Functions
        /// <summary>
        /// Invoke click on an element
        /// </summary>
        /// <param name="automationElement">UI automation element</param>
        public void InvokeAutomationElement(IUIAutomationElement automationElement)
        {
            IUIAutomationInvokePattern invokePattern = automationElement.GetCachedPattern(patternIdInvoke);
            invokePattern.Invoke();
        }

        /// <summary>
        /// Select an item
        /// </summary>
        /// <param name="automationElement">UI automation element</param>
        public void SelectAutomationElement(IUIAutomationElement automationElement)
        {
            IUIAutomationSelectionItemPattern selectionPattern = automationElement.GetCachedPattern(
                patternIdSelectionItem);
            selectionPattern.Select();

        }

        /// <summary>
        /// Add the item to selection
        /// </summary>
        /// <param name="automationElement">UI automation element</param>
        public void SelectMultiAutomationElement(IUIAutomationElement automationElement)
        {
            IUIAutomationSelectionItemPattern selectionPattern = automationElement.GetCachedPattern(
                patternIdSelectionItem);           
            selectionPattern.AddToSelection();
        }

        /// <summary>
        /// Read the multiple selection property of the automation element
        /// </summary>
        /// <param name="automationElement">UI automation element</param>
        public int IsMultiSelectEnabled(IUIAutomationElement automationElement)
        {
            IUIAutomationSelectionPattern selectionPattern = automationElement.GetCachedPattern(patternIdSelection);
            return selectionPattern.CurrentCanSelectMultiple;
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Inserts a string into each text control of interest.
        /// </summary>
        /// <param name="element">A text control.</param>
        /// <param name="value">The string to be inserted.</param>
        ///--------------------------------------------------------------------
        public void InsertTextUsingUIAutomation(IUIAutomationElement automationElement, string value)
        {
            IUIAutomationValuePattern valuePattern = automationElement.GetCachedPattern(patternIdValueItem);
            valuePattern.SetValue(value);
        }
        #endregion
    }
}
