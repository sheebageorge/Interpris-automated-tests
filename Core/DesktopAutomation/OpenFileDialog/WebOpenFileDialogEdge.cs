using UIAutomationClient;

namespace Automation.UI.Core.DesktopAutomation.OpenFileDialog
{
    public class WebOpenFileDialogEdge: WebOpenFileDialog
    {
        public const string WINDOW_TITLE = "Interpris 2 - Microsoft Edge";

        public WebOpenFileDialogEdge(): base()
        {
            // initilize the open dialog instance
            IUIAutomationElement edgeObj = GetWindowElement(
                GetUIAutomation().CreatePropertyCondition(propertyIdName, WINDOW_TITLE));

            openDialog = GetChildNodeElement(edgeObj, TreeScope.TreeScope_Children,
                GetUIAutomation().CreatePropertyCondition(propertyIdName, "Open"));
        }
    }
}
