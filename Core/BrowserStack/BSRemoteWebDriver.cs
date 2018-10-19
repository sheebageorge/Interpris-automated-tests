using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;

namespace Automation.UI.Core.BrowserStack
{
    public class BSRemoteWebDriver : RemoteWebDriver, ITakesScreenshot
    {
        public BSRemoteWebDriver(ICapabilities dc)
            : base(new Uri("http://hub-cloud.browserstack.com/wd/hub/"), dc)
        {
        }

        public new Screenshot GetScreenshot()
        {
            Response screenshotResponse = this.Execute(DriverCommand.Screenshot, null);
            string base64 = screenshotResponse.Value.ToString();
            return new Screenshot(base64);
        }
    }
}
