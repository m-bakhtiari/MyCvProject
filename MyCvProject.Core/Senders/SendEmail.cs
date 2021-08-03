using System.Net.Mail;
using System.Threading.Tasks;

namespace MyCvProject.Core.Senders
{
    public class SendEmail
    {
        public static void Send(string to, string subject, string body)
        {
            MailMessage mail = new MailMessage();
            SmtpClient client = new SmtpClient("smtp.gmail.com");
            mail.From = new MailAddress("customEmail@gmail.com", "کد فعالسازی");
            mail.To.Add(to);
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;

            //System.Net.Mail.Attachment attachment;
            // attachment = new System.Net.Mail.Attachment("c:/textfile.txt");
            // mail.Attachments.Add(attachment);

            client.UseDefaultCredentials = true;
            client.Port = 587;
            client.Credentials = new System.Net.NetworkCredential("customEmail@gmail.com", "******");
            client.EnableSsl = true;

            client.Send(mail);
        }
    }
}