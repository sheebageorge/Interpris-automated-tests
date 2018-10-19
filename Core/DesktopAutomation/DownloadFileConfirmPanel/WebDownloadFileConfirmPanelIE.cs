using Automation.UI.Core.CommonUtilities;
using UIAutomationClient;

namespace Automation.UI.Core.DesktopAutomation.DownloadFileConfirmPanel
{
    public class WebDownloadFileConfirmPanelIE : WebDownloadFileConfirmPanel
    {
        public const string WINDOW_TITLE = "NVivo Transcription - Internet Explorer";

        public WebDownloadFileConfirmPanelIE() : base()
        {
            // initilize the open dialog instance
            ieObj = GetWindowElement(
                GetUIAutomation().CreatePropertyCondition(propertyIdName, WINDOW_TITLE));

            notificationToolbarCondition = GetUIAutomation().CreateAndCondition(
                GetUIAutomation().CreatePropertyCondition(propertyIdName, "Notification"),
                GetUIAutomation().CreatePropertyCondition(propertyIdClassName, "DirectUIHWND"));

            saveSplitButtonCondition = GetUIAutomation().CreatePropertyCondition(propertyIdName, "6");
            popUpPanelCondition = GetUIAutomation().CreatePropertyCondition(propertyIdName, "Context");
            saveAsButtonCondition = GetUIAutomation().CreatePropertyCondition(propertyIdName, "Save as");
        }

        #region UI objects
        private IUIAutomationElement ieObj;

        private IUIAutomationCondition notificationToolbarCondition;
        private IUIAutomationCondition saveSplitButtonCondition;
        private IUIAutomationCondition popUpPanelCondition;
        private IUIAutomationCondition saveAsButtonCondition;
        #endregion

        #region Properties
        public IUIAutomationElement SaveSplitButton
        {
            get
            {
                IUIAutomationElement downloadFileConfirmPanel = GetChildNodeElement(ieObj,
                    TreeScope.TreeScope_Descendants, notificationToolbarCondition);

                return GetChildNodeElement(downloadFileConfirmPanel, TreeScope.TreeScope_Descendants,
                    saveSplitButtonCondition, cacheRequestInvokePattern);
            }
        }

        public IUIAutomationElement SaveAsButton
        {
            get
            {
                IUIAutomationElement popUpPanel = GetWindowElement(popUpPanelCondition);

                return GetChildNodeElement(popUpPanel, TreeScope.TreeScope_Children,
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
