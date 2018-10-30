using UIAutomationClient;

namespace Automation.UI.Core.DesktopAutomation.OpenFileDialog
{
    public class WebOpenFileDialogFirefox: WebOpenFileDialog
    {
        public const string WINDOW_TITLE = "Interpris 2 - Mozilla Firefox";

        public WebOpenFileDialogFirefox() : base()
        {
            // initilize the open dialog instance
            IUIAutomationElement ffObj = GetWindowElement(
                GetUIAutomation().CreatePropertyCondition(propertyIdName, WINDOW_TITLE));

            openDialog = GetChildNodeElement(ffObj, TreeScope.TreeScope_Children,
                GetUIAutomation().CreatePropertyCondition(propertyIdName, "File Upload"));
        }
    }
}
