namespace Automation.UI.Core.Selenium.ExternalMail
{
    public interface IEmailEmailPage
    {
        string EmailDomainName { get; }

        void VerifyEmailLogInSuccess(string emailAddress);
        string GetFirstEmailItemTitle();
        string GetFirstEmailItemTime();
        void ClickOpenFirstEmailItem();
        void ClickActivateAcountByEmail();
        void ClickResetPasswordLink();
        bool IsResetPasswordEmailCome();
        void ActivateAccountByEmail(string expectedEmailTitle);
    }
}
