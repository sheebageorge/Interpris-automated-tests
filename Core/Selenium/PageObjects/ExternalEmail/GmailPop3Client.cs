using MailKit.Net.Pop3;
using System;
using System.Collections.Generic;

namespace Automation.UI.Core.Selenium.ExternalMail
{
    /// <summary>
    /// Gmail Pop3 client
    /// </summary>
    public class GmailPop3Client
    {
        public const string GMAIL_POP3_ADDRESS = "pop.gmail.com";
        public const int POP3_PORT = 995;

        /// <summary>
        /// Get the message text content by the email title and latest date
        /// </summary>
        /// <param name="username">Username of Gmail account</param>
        /// <param name="password">Password of Gmail account</param>
        /// <param name="msgTitle">Email title to filter the message</param>
        /// <returns></returns>
        public static string GetUluruAccountInfoEmailByTitle(string username, string password, List<string> msgTitles)
        {
            MimeKit.MimeMessage firstMsg = null;

            using (var client = new Pop3Client())
            {
                try
                {
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                    client.Connect(GMAIL_POP3_ADDRESS, POP3_PORT, true);

                    client.Authenticate(username, password);

                    for (int i = 0; i < client.Count; i++)
                    {
                        var message = client.GetMessage(i);

                        string findMsg;

                        try
                        {
                            findMsg = msgTitles.Find(x => x.Equals(message.Subject));
                        }
                        catch(ArgumentNullException)
                        {
                            findMsg = null;
                        }

                        if (findMsg != null)
                        {
                            if (firstMsg == null)
                            {
                                firstMsg = message;
                            }
                            else
                            {
                                if (System.DateTimeOffset.Compare(firstMsg.Date, message.Date) < 0)
                                {
                                    firstMsg = message;
                                }
                            }
                        }
                    }
                }
                finally
                {
                    client.Disconnect(true);
                }
            }

            if (firstMsg != null)
            {
                return firstMsg.TextBody;
            }

            return null;
        }
    }
}
