using System.Collections.Generic;

namespace Automation.UI.Core.Selenium
{
    public class BrowserConstants
    {
        public const string BROWSER_IE = "ie";
        public const string BROWSER_CHROME = "chrome";
        public const string BROWSER_FIREFOX = "firefox";
        public const string BROWSER_EDGE = "edge";
        public const string BROWSER_SAFARI = "safari";

        public static readonly Dictionary<string, string> BROWSERSTACK_BROWSER_NAMES =
            new Dictionary<string, string>() {
                { BROWSER_IE, "IE" },
                { BROWSER_CHROME, "Chrome" },
                { BROWSER_FIREFOX, "Firefox" },
                { BROWSER_EDGE, "Edge" }
            };
    }
}
