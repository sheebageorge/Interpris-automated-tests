using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation.UI.Core.Selenium.PageObjects.Interpris.Product
{
    /// <summary>
    /// Wrapper class of Get Started Guide Sub Page
    /// contains all elements and actions for Get Started Guide Sub Page
    /// </summary>
    public class GetStartedGuideSubPage : BasePage
    {
        #region UI objects
        private readonly string divContainerDialog = "//div[contains(@class,\"ant-modal-wrap\") and @role=\"dialog\"]";

        private readonly string pHeaderTitle =
            "//div[@class=\"ant-modal-body\"]/p[contains(@class,\"Heading\") " +
            "and .=\"Welcome to NVivo Transcription.\"]";
        private readonly string pDescriptionText =
            "//div[@class=\"ant-modal-body\"]/p[contains(@class,\"Description\") " +
            "and .=\"Here are 3 quick steps to transcribing your files:\"]";

        private readonly string divUploadStepNo =
            "//div[@class=\"ant-modal-body\"]/div[contains(@class,\"StepUpload\")]" +
            "//div[@class=\"no\" and .=\"1\"]";
        private readonly string divUploadStepTitle =
            "//div[@class=\"ant-modal-body\"]/div[contains(@class,\"StepUpload\")]" +
            "//div[@class=\"title\" and .=\"Upload\"]";
        private readonly string divUploadStepContent =
            "//div[@class=\"ant-modal-body\"]/div[contains(@class,\"StepUpload\")]" +
            "//div[@class=\"content\" and .=\"Drag and drop your files here, or browse " +
            "for them on your computer, tablet or mobile.\"]";
        private readonly string divUploadStepImg =
            "//div[@class=\"ant-modal-body\"]/div[contains(@class,\"StepUpload\")]" +
            "/div[contains(@class,\"ImageCol\")]/img";

        private readonly string divTranscribeStepNo =
            "//div[@class=\"ant-modal-body\"]/div[contains(@class,\"StepTranscribe\")]" +
            "//div[@class=\"no\" and .=\"2\"]";
        private readonly string divTranscribeStepTitle =
            "//div[@class=\"ant-modal-body\"]/div[contains(@class,\"StepTranscribe\")]" +
            "//div[@class=\"title\" and .=\"Transcribe\"]";
        private readonly string divTranscribeStepContent =
            "//div[@class=\"ant-modal-body\"]/div[contains(@class,\"StepTranscribe\")]" +
            "//div[@class=\"content\" and .=\"Specify the language spoken on your file and click 'Transcribe'\"]";
        private readonly string divTranscribeStepImg =
            "//div[@class=\"ant-modal-body\"]/div[contains(@class,\"StepTranscribe\")]" +
            "/div[contains(@class,\"ImageCol\")]/img";

        private readonly string divEditStepNo =
            "//div[@class=\"ant-modal-body\"]/div[contains(@class,\"StepEdit\")]" +
            "//div[@class=\"no\" and .=\"3\"]";
        private readonly string divEditStepTitle =
            "//div[@class=\"ant-modal-body\"]/div[contains(@class,\"StepEdit\")]" +
            "//div[@class=\"title\" and .=\"Edit\"]";
        private readonly string divEditStepContent =
            "//div[@class=\"ant-modal-body\"]/div[contains(@class,\"StepEdit\")]" +
            "//div[@class=\"content\" and .=\"Click on your transcribed file " +
            "to open it in the editor Here, you can edit the text and the speaker names. " +
            "When you are done, you can export your transcript for analysis in NVivo.\"]";
        private readonly string divEditStepImg =
            "//div[@class=\"ant-modal-body\"]/div[contains(@class,\"StepEdit\")]" +
            "/div[contains(@class,\"ImageCol\")]/img";

        private readonly string btnOK = "//div[@class=\"ant-modal-body\"]//button[span[text()=\"Ok\"]]";
        private readonly string pFooterText =
            "//div[@class=\"ant-modal-body\"]/p[contains(@class,\"Tip\") and " +
            ".=\"To see this guide again, click on the top right menu and select 'Get started guide'.\"]";
        #endregion

        public GetStartedGuideSubPage(IWebDriver driver, string baseURL) : base(driver, baseURL, "") { }

        #region Properties
        public BaseWebObject DivContainerDialog => FindWebElement(divContainerDialog);

        public BaseWebObject PHeaderTitle => FindWebElement(pHeaderTitle, true);
        public BaseWebObject PDescriptionText => FindWebElement(pDescriptionText, true);

        public BaseWebObject DivUploadStepNo => FindWebElement(divUploadStepNo, true);
        public BaseWebObject DivUploadStepTitle => FindWebElement(divUploadStepTitle, true);
        public BaseWebObject DivUploadStepContent => FindWebElement(divUploadStepContent, true);
        public BaseWebObject DivUploadStepImg => FindWebElement(divUploadStepImg, true);

        public BaseWebObject DivTranscribeStepNo => FindWebElement(divTranscribeStepNo, true);
        public BaseWebObject DivTranscribeStepTitle => FindWebElement(divTranscribeStepTitle, true);
        public BaseWebObject DivTranscribeStepContent => FindWebElement(divTranscribeStepContent, true);
        public BaseWebObject DivTranscribeStepImg => FindWebElement(divTranscribeStepImg, true);

        public BaseWebObject DivEditStepNo => FindWebElement(divEditStepNo, true);
        public BaseWebObject DivEditStepTitle => FindWebElement(divEditStepTitle, true);
        public BaseWebObject DivEditStepContent => FindWebElement(divEditStepContent, true);
        public BaseWebObject DivEditStepImg => FindWebElement(divEditStepImg, true);

        public BaseWebObject BtnOK => FindWebElement(btnOK, true);
        public BaseWebObject PFooterText => FindWebElement(pFooterText, true);
        #endregion

        #region Methods
        /// <summary>
        /// Check if the Get Started Guide box visible
        /// </summary>
        /// <returns></returns>
        public bool IsGetStartedGuideBoxVisible()
        {
            return PHeaderTitle.IsVisible && PDescriptionText.IsVisible &&
                DivUploadStepNo.IsVisible && DivUploadStepTitle.IsVisible && DivUploadStepContent.IsVisible && DivUploadStepImg.IsVisible &&
                DivTranscribeStepNo.IsVisible && DivTranscribeStepTitle.IsVisible && DivTranscribeStepContent.IsVisible && DivTranscribeStepImg.IsVisible &&
                DivEditStepNo.IsVisible && DivEditStepTitle.IsVisible && DivEditStepContent.IsVisible && DivEditStepImg.IsVisible &&
                BtnOK.IsVisible && PFooterText.IsVisible;
        }
        #endregion
    }
}
