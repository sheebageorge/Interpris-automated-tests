using UIAutomationClient;

namespace Automation.UI.Core.DesktopAutomation.OpenFileDialog
{
    public class WebOpenFileDialogIE: WebOpenFileDialog
    {
        public const string WINDOW_TITLE = "Interpris 2 - Internet Explorer";

        public WebOpenFileDialogIE() : base()
        {
            // initilize the open dialog instance
            IUIAutomationElement ieObj = GetWindowElement(
                GetUIAutomation().CreatePropertyCondition(propertyIdName, WINDOW_TITLE));

            openDialog = GetChildNodeElement(ieObj, TreeScope.TreeScope_Children,
                GetUIAutomation().CreatePropertyCondition(propertyIdName, "Choose File to Upload"));
        }
    }
}
