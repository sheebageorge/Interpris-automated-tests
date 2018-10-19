using Automation.UI.Core.CommonUtilities;
using UIAutomationClient;

namespace Automation.UI.Core.DesktopAutomation.DownloadFileConfirmPanel
{
    public class WebDownloadFileConfirmPanelEdge : WebDownloadFileConfirmPanel
    {
        public const string WINDOW_TITLE = "NVivo Transcription - Microsoft Edge";

        public WebDownloadFileConfirmPanelEdge() : base()
        {
            // initilize the open dialog instance
            edgeObj = GetWindowElement(GetUIAutomation().CreatePropertyCondition(propertyIdName, WINDOW_TITLE));

            saveSplitButtonCondition = GetUIAutomation().CreateAndCondition(
                GetUIAutomation().CreatePropertyCondition(propertyIdName, "Split button, more options"),
                GetUIAutomation().CreatePropertyCondition(propertyIdClassName, "Button"));

            popUpPanelCondition = GetUIAutomation().CreateAndCondition(
                GetUIAutomation().CreatePropertyCondition(propertyIdName, "Microsoft Edge"),
                GetUIAutomation().CreatePropertyCondition(propertyIdClassName, "Windows.UI.Core.CoreWindow"));

            saveAsButtonCondition = GetUIAutomation().CreateAndCondition(
                GetUIAutomation().CreatePropertyCondition(propertyIdName, "Save as"),
                GetUIAutomation().CreatePropertyCondition(propertyIdClassName, "Button"));
        }

        #region UI objects
        private IUIAutomationElement edgeObj;

        private IUIAutomationCondition saveSplitButtonCondition;
        private IUIAutomationCondition popUpPanelCondition;
        private IUIAutomationCondition saveAsButtonCondition;
        #endregion

        #region Properties
        public IUIAutomationElement SaveSplitButton
        {
            get
            {
                IUIAutomationElement downloadFileConfirmPanel = GetChildNodeElement(edgeObj,
                    TreeScope.TreeScope_Descendants,
                    GetUIAutomation().CreatePropertyCondition(propertyIdName, "Notification"));

                return GetChildNodeElement(downloadFileConfirmPanel, TreeScope.TreeScope_Descendants,
                    saveSplitButtonCondition, cacheRequestInvokePattern);
            }
        }

        public IUIAutomationElement SaveAsButton
        {
            get
            {
                IUIAutomationElement popUpPanel = GetChildNodeElement(edgeObj,
                    TreeScope.TreeScope_Descendants, popUpPanelCondition);

                return GetChildNodeElement(popUpPanel, TreeScope.TreeScope_Descendants,
                    saveAsButtonCondition, cacheRequestInvokePattern);
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Open Save As dialog to save the download file
        /// </summary>
        public override void OpenSaveAsDialog()
        {
            // click open button
            InvokeAutomationElement(SaveSplitButton);
            ThreadUtils.SleepShortTime();

            // click open button
            InvokeAutomationElement(SaveAsButton);
            ThreadUtils.SleepShortTime();
        }
        #endregion
    }
}
