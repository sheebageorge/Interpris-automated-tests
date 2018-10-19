using OpenQA.Selenium;

namespace Automation.UI.Core.Selenium.PageObjects.Interpris.Transcription
{
    /// <summary>
    /// Wrapper class of Confirm Message Sub Page
    /// contains all elements and actions for Confirm Message Sub Page
    /// </summary>
    public class ConfirmMessageSubPage : BasePage
    {
        public const string MSG_ERROR_FILE_UPLOAD_NOT_SUPPORTED = "File format is not supported";

        #region UI objects
        private readonly string divConfirmMsgBody = "//div[@class=\"ant-modal-body\"]//div[@class=\"ant-confirm-body\"]";
        private readonly string spanConfirmMsgTitle = "//div[@class=\"ant-modal-body\"]//div[@class=\"ant-confirm-body\"]/span[@class=\"ant-confirm-title\"]";
        private readonly string divConfirmMsgContent = "//div[@class=\"ant-modal-body\"]//div[@class=\"ant-confirm-body\"]/div[@class=\"ant-confirm-content\"]";
        private readonly string btnDialogConfirmYes = "//*[@class='ant-modal-content']//button[@type='button' and .='YES']";
        private readonly string btnDialogConfirmMsgNo = "//*[@class='ant-modal-content']//button[@type='button' and .='NO']";
        private readonly string btnDialogConfirmMsgOK = "//*[@class='ant-modal-content']//button[@type='button' and .='OK']";
        private readonly string btnDialogConfirmMsgNoThanks = "//*[@class='ant-modal-content']//button[@type='button' and .='No thanks.']";
        #endregion

        public ConfirmMessageSubPage(IWebDriver driver, string baseURL) : base(driver, baseURL, "") { }

        #region Properties
        public BaseWebObject DivConfirmMsgBody => FindWebElement(divConfirmMsgBody, true);
        public BaseWebObject SpanConfirmMsgTitle => FindWebElement(spanConfirmMsgTitle, true);
        public BaseWebObject DivConfirmMsgContent => FindWebElement(divConfirmMsgContent, true);
        public BaseWebObject BtnDialogConfirmYes => FindWebElement(btnDialogConfirmYes, true);
        public BaseWebObject BtnDialogConfirmMsgNo => FindWebElement(btnDialogConfirmMsgNo, true);
        public BaseWebObject BtnDialogConfirmMsgOK => FindWebElement(btnDialogConfirmMsgOK, true);
        public BaseWebObject BtnDialogConfirmMsgNoThanks => FindWebElement(btnDialogConfirmMsgNoThanks, true);
        #endregion

        #region Methods
        /// <summary>
        /// Get confirm message and close
        /// </summary>
        /// <returns>Confirm message</returns>
        public string GetConfirmMessageAndClose()
        {
            string confirmMsg = DivConfirmMsgBody.Text.Trim();

            BtnDialogConfirmMsgOK.Click();

            return confirmMsg;
        }

        /// <summary>
        /// Get confirm message and click Yes
        /// </summary>
        /// <returns>Confirm message</returns>
        public string GetConfirmMessageAndClickYes()
        {
            string confirmMsg = DivConfirmMsgBody.Text.Trim();

            BtnDialogConfirmYes.Click();

            return confirmMsg;
        }

        /// <summary>
        /// Get confirm message and click No
        /// </summary>
        /// <returns>Confirm message</returns>
        public string GetConfirmMessageAndClickNo()
        {
            string confirmMsg = DivConfirmMsgBody.Text.Trim();

            BtnDialogConfirmMsgNo.Click();

            return confirmMsg;
        }
        #endregion
    }
}
