using System;
using System.Net.Mail;
using System.Net.Mime;
using SolvencyII.Domain.ENumerators;
using SolvencyII.Domain.Interfaces;

namespace SolvencyII.UI.Shared.Loggers
{
    /// <summary>
    /// Logger designed to send an email when the serverity of an error was critical. Not implemented.
    /// </summary>
    public class EMailLogger : ILogger
    {
        public void WriteLog(eSeverity severity, string location, string message)
        {
            if (severity == eSeverity.Critical)
            {
                // Critical error so email someone.
                // SendEmailToAdmin(string.Format("Error T4U - {0}", location), message);
            }
        }

        private static void SendEmailToAdmin(string subject, string bodyText)
        {
            MailAddress eMailAddress = new MailAddress("administration@support.com");

            MailMessage msgMail = new MailMessage(eMailAddress, eMailAddress);

            msgMail.Subject = subject;
            msgMail.IsBodyHtml = false;
            msgMail.BodyEncoding = System.Text.Encoding.UTF8;
            msgMail.HeadersEncoding = System.Text.Encoding.UTF8;
            msgMail.SubjectEncoding = System.Text.Encoding.UTF8;
            msgMail.Body = null;

            AlternateView plainView = AlternateView.CreateAlternateViewFromString(bodyText, msgMail.BodyEncoding, "text/plain");
            plainView.TransferEncoding = TransferEncoding.SevenBit;
            msgMail.AlternateViews.Add(plainView);

            SmtpClient client = new SmtpClient();
            try
            {
                client.Send(msgMail);
            }
            catch (Exception ex)
            {
                FileLogger fileLogger = new FileLogger();
                fileLogger.WriteLog(eSeverity.Error, "Sending Email Error", ex.ToString());
            }


        }

    }
}
