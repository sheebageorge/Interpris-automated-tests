using Automation.UI.Core.CommonUtilities;
using System.Windows.Forms;
using UIAutomationClient;

namespace Automation.UI.Core.DesktopAutomation.DownloadFileDialog
{
    public class WebSaveAsFileDialogEdge : WebSaveAsFileDialog
    {
        public const string WINDOW_TITLE = "NVivo Transcription - Microsoft Edge";

        #region UI objects
        private IUIAutomationElement saveAsDialog;

        private IUIAutomationCondition fileInputTextCondition;

        private IUIAutomationElement _FileInputText = null;
        #endregion

        public WebSaveAsFileDialogEdge() : base()
        {
            // initilize the open dialog instance
            IUIAutomationElement edgeObj = GetWindowElement(
                GetUIAutomation().CreatePropertyCondition(propertyIdName, WINDOW_TITLE));

            fileInputTextCondition = GetUIAutomation().CreateAndCondition(
                GetUIAutomation().CreatePropertyCondition(propertyIdName, "File name:"),
                GetUIAutomation().CreatePropertyCondition(propertyIdClassName, "Edit"));

            saveAsDialog = GetChildNodeElement(edgeObj, TreeScope.TreeScope_Children,
                GetUIAutomation().CreatePropertyCondition(propertyIdName, "Save As"));
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
        #endregion

        #region Methods
        /// <summary>
        /// Select a file to upload
        /// </summary>
        /// <param name="folderPath">Folder path of the file</param>
        /// <param name="fileName">Name of file to upload</param>
        public override void SaveAFile(string folderPath, string fileName)
        {
            InsertTextUsingUIAutomation(FileInputText, "\"" + folderPath + "\"");
            ThreadUtils.SleepShortTime();

            // click open button
            SendKeys.SendWait("{ENTER}");
            ThreadUtils.SleepShortTime();
        }
        #endregion
    }
}
