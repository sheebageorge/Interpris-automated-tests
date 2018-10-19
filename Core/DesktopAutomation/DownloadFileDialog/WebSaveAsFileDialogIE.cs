using Automation.UI.Core.CommonUtilities;
using System.Windows.Forms;
using UIAutomationClient;

namespace Automation.UI.Core.DesktopAutomation.DownloadFileDialog
{
    public class WebSaveAsFileDialogIE: WebSaveAsFileDialog
    {
        #region UI objects
        private IUIAutomationElement saveAsDialog;

        private IUIAutomationCondition fileInputTextCondition;
        private IUIAutomationCondition saveButtonCondition;

        private IUIAutomationElement _FileInputText = null;
        private IUIAutomationElement _SaveButton = null;
        #endregion

        public WebSaveAsFileDialogIE() : base()
        {
            IUIAutomationCondition saveAsDialogCondition = GetUIAutomation().CreateAndCondition(
                GetUIAutomation().CreatePropertyCondition(propertyIdName, "Save As"),
                GetUIAutomation().CreatePropertyCondition(propertyIdClassName, "#32770"));

            fileInputTextCondition = GetUIAutomation().CreateAndCondition(
                GetUIAutomation().CreatePropertyCondition(propertyIdName, "File name:"),
                GetUIAutomation().CreatePropertyCondition(propertyIdClassName, "Edit"));

            saveButtonCondition = GetUIAutomation().CreateAndCondition(
                GetUIAutomation().CreatePropertyCondition(propertyIdName, "Save"),
                GetUIAutomation().CreatePropertyCondition(propertyIdClassName, "Button"));

            // initilize the open dialog instance
            saveAsDialog = GetWindowElement(saveAsDialogCondition);
        }

        #region Properties
        public IUIAutomationElement FileInputText
        {
            get
            {
                if (_FileInputText == null)
                {
                    _FileInputText = GetChildNodeElement(saveAsDialog, TreeScope.TreeScope_Descendants,
                        fileInputTextCondition, cacheRequestValueItemPattern);
                }

                return _FileInputText;
            }
        }

        public IUIAutomationElement SaveButton
        {
            get
            {
                if (_SaveButton == null)
                {
                    _SaveButton = GetChildNodeElement(saveAsDialog, TreeScope.TreeScope_Descendants,
                        saveButtonCondition, cacheRequestInvokePattern);
                }

                return _SaveButton;
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Select a file to upload
        /// </summary>
        /// <param name="folderPath">Folder path of the file</param>
        /// <param name="fileName">Name of file to upload</param>
        public override void SaveAFile(string folderPath, string fileName)
        {
            string filePath = FileSystemUtils.GetFullFilePath(folderPath, fileName);

            // input the file list to open
            InsertTextUsingUIAutomation(FileInputText, "");
            ThreadUtils.SleepShortTime();
            InsertTextUsingUIAutomation(FileInputText, filePath);
            ThreadUtils.SleepShortTime();

            // click open button
            InvokeAutomationElement(SaveButton);
        }
        #endregion
    }
}
