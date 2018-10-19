namespace Automation.UI.Core.Selenium.ExternalMail
{
    /// <summary>
    /// Interface for the home page of an email site
    /// </summary>
    public interface IEmailHomePage
    {
        void OpenEmail(string username, string password);
    }
}
