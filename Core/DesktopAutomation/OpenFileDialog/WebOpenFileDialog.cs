using Automation.UI.Core.CommonUtilities;
using Automation.UI.Core.Selenium;
using System;
using System.Collections.Generic;
using System.IO;
using UIAutomationClient;

namespace Automation.UI.Core.DesktopAutomation.OpenFileDialog
{
    public abstract class WebOpenFileDialog: DesktopWindowObject
    {
        #region Static method to get real WebOpenFileDialog object
        public static WebOpenFileDialog GetOpenDialog(string browserType)
        {
            switch(browserType)
            {
                case BrowserConstants.BROWSER_CHROME:
                    return new WebOpenFileDialogChrome();
                case BrowserConstants.BROWSER_FIREFOX:
                    return new WebOpenFileDialogFirefox();
                case BrowserConstants.BROWSER_IE:
                    return new WebOpenFileDialogIE();
                case BrowserConstants.BROWSER_EDGE:
                    return new WebOpenFileDialogEdge();
                case BrowserConstants.BROWSER_SAFARI:
                    break;
                default:
                    break;
            }

            return null;
        }
        #endregion

        #region UI objects
        protected IUIAutomationElement openDialog;

        protected IUIAutomationCondition fileInputTextCondition;
        protected IUIAutomationCondition openButtonCondition;
        protected IUIAutomationCondition cancelButtonCondition;
        protected IUIAutomationCondition fileListViewCondition;
        protected IUIAutomationCondition fileItemCondition;

        private IUIAutomationElement _FileInputText = null;
        private IUIAutomationElement _OpenButton = null;
        private IUIAutomationElement _CancelButton = null;
        private IUIAutomationElementArray _FileItems = null;
        #endregion

        public WebOpenFileDialog()
        {
            fileInputTextCondition = GetUIAutomation().CreateAndCondition(
                GetUIAutomation().CreatePropertyCondition(propertyIdName, "File name:"),
                GetUIAutomation().CreatePropertyCondition(propertyIdClassName, "Edit"));

            openButtonCondition = GetUIAutomation().CreateAndCondition(
                GetUIAutomation().CreatePropertyCondition(propertyIdName, "Open"),
                GetUIAutomation().CreatePropertyCondition(propertyIdClassName, "Button"));

            cancelButtonCondition = GetUIAutomation().CreateAndCondition(
                GetUIAutomation().CreatePropertyCondition(propertyIdName, "Cancel"),
                GetUIAutomation().CreatePropertyCondition(propertyIdClassName, "Button"));

            fileListViewCondition = GetUIAutomation().CreatePropertyCondition(propertyIdClassName, "UIItemsView");

            fileItemCondition = GetUIAutomation().CreatePropertyCondition(propertyIdClassName, "UIItem");
        }

        #region Properties
        public IUIAutomationElement FileInputText
        {
            get {
                if (_FileInputText == null)
                {
                    _FileInputText = GetChildNodeElement(openDialog, TreeScope.TreeScope_Descendants,
                        fileInputTextCondition, cacheRequestValueItemPattern);
                }

                return _FileInputText;
            }
        }

        public IUIAutomationElement OpenButton
        {
            get {
                if (_OpenButton == null)
                {
                    _OpenButton = GetChildNodeElement(openDialog, TreeScope.TreeScope_Children,
                        openButtonCondition, cacheRequestInvokePattern);
                }
                return _OpenButton; }
        }

        public IUIAutomationElement CancelButton
        {
            get
            {
                if (_CancelButton == null)
                {
                    _CancelButton = GetChildNodeElement(openDialog, TreeScope.TreeScope_Children,
                        cancelButtonCondition, cacheRequestInvokePattern);
                }
                return _CancelButton;
            }
        }

        public IUIAutomationElementArray FileItems
        {
            get
            {
                if (_FileItems == null)
                {
                    _FileItems = GetAllChildrenNodeElements(
                        GetChildNodeElement(openDialog, TreeScope.TreeScope_Descendants, fileListViewCondition),
                        TreeScope.TreeScope_Children, fileItemCondition, cacheRequestInvokeSelectPattern);
                }
                return _FileItems;
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Get list of names of files in folder
        /// </summary>
        /// <returns>List of file names</returns>
        public IList<String> GetFileNameList()
        {
            IList<string> fileNameList = new List<string>();

            IUIAutomationElementArray fileItems = FileItems;

            for(int i = 0; i < fileItems.Length; i++)
            {
                fileNameList.Add(fileItems.GetElement(i).CurrentName);
            }

            InvokeAutomationElement(CancelButton);

            return fileNameList;
        }

        /// <summary>
        /// Select a file to upload
        /// </summary>
        /// <param name="folderPath">Folder path of the file</param>
        /// <param name="fileName">Name of file to upload</param>
        public void SelectAFileByName(string folderPath, string fileName)
        {
            string filePath = FileSystemUtils.GetFullFilePath(folderPath, fileName);

            // input the file list to open
            InsertTextUsingUIAutomation(FileInputText, "");
            ThreadUtils.SleepShortTime();
            InsertTextUsingUIAutomation(FileInputText, "\"" + filePath + "\"");
            ThreadUtils.SleepShortTime();

            // click open button
            InvokeAutomationElement(OpenButton);
        }

        /// <summary>
        /// Select multiple files from Open dialog
        /// </summary>
        /// <param name="folderPath">Folder path of the file</param>
        /// <param name="fileNameList">List of file names to select</param>
        public void SelectMultipleFilesInOpenDialog(string folderPath, IList<string> fileNameList)
        {
            IList<string> fileFullPathList = new List<string>();
            foreach(string fileName in fileNameList)
            {
                fileFullPathList.Add(FileSystemUtils.GetFullFilePath(folderPath, fileName));
            }

            // input the file list to open
            InsertTextUsingUIAutomation(FileInputText, "");
            ThreadUtils.SleepShortTime();
            InsertTextUsingUIAutomation(FileInputText, "\"" + string.Join("\" \"", fileFullPathList) + "\"");
            ThreadUtils.SleepShortTime();

            // click open button
            InvokeAutomationElement(OpenButton);
        }

        /// <summary>
        /// Fill in the file/folder name location and Enter to open it
        /// </summary>
        public void OpenFileFolder(string filePath)
        {
            if (!Path.IsPathRooted(filePath))
            {
                filePath = Path.GetFullPath(filePath);
            }

            // input file to file name box
            InsertTextUsingUIAutomation(FileInputText, filePath);

            ThreadUtils.SleepShortTime();

            // click open button
            InvokeAutomationElement(OpenButton);
        }
        #endregion
    }
}
