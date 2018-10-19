using UIAutomationClient;

namespace Automation.UI.Core.DesktopAutomation.OpenFileDialog
{
    public class WebOpenFileDialogChrome : WebOpenFileDialog
    {
        public const string WINDOW_TITLE = "NVivo Transcription - Google Chrome";

        public WebOpenFileDialogChrome() : base()
        {
            // initilize the open dialog instance
            IUIAutomationElement chromeObj = GetWindowElement(
                GetUIAutomation().CreatePropertyCondition(propertyIdName, WINDOW_TITLE));

            openDialog = GetChildNodeElement(chromeObj, TreeScope.TreeScope_Children,
                GetUIAutomation().CreatePropertyCondition(propertyIdName, "Open"));
        }
    }
}
