using UIAutomationClient;

namespace Automation.UI.Core.DesktopAutomation.OpenFileDialog
{
    public class WebOpenFileDialogChrome : WebOpenFileDialog
    {
        public const string WINDOW_TITLE = "Interpris 2 - Google Chrome";

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
