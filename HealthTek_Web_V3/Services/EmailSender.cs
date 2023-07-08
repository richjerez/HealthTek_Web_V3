using HealthTek_Shared_Libraries;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace HealthTek_Web_V3.Services
{
    public class EmailSender
    {

        public async Task SendMessage(Messages emailModel)
        {
            var fromAddress = new MailAddress("healthteksystems@gmail.com", "From HealthTek");
            var toAddress = new MailAddress(emailModel.ToEmail);

            const string fromPassword = "yytzmoxlbmyvhfsk";
            string subject = emailModel.Title;
            string body = emailModel.Message;

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword),
                Timeout = 20000
            };

            var msg = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body,
            };
            msg.IsBodyHtml = true;
            {
                await smtp.SendMailAsync(msg);
            }

        }
    }
}
