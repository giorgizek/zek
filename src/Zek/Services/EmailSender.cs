using System.Net;
using System.Net.Mail;
using System.Text;
using Microsoft.Extensions.Options;
using Zek.Extensions.Collections;
using Zek.Office;
using Zek.Options;

namespace Zek.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(Email model, CancellationToken cancellationToken = default);
    }

    public class EmailSender : IEmailSender
    {
        protected EmailSenderOptions Options;

        public EmailSender(IOptions<EmailSenderOptions> optionsAccessor)
        {
            if (optionsAccessor != null)
                Config(optionsAccessor.Value);
        }
        public EmailSender(EmailSenderOptions option)
        {
            Config(option);
        }

        public void Config(EmailSenderOptions options)
        {
            Options = options ?? throw new ArgumentNullException(nameof(options));
        }

        public virtual async Task SendEmailAsync(Email model, CancellationToken cancellationToken = default)
        {
            using var client = new SmtpClient();
            if (Options != null)
            {
                if (!string.IsNullOrEmpty(Options.Host))
                    client.Host = Options.Host;
                if (Options.Port != null)
                    client.Port = Options.Port.Value;

                if (Options.EnableSsl != null)
                    client.EnableSsl = Options.EnableSsl.Value;

                if (!string.IsNullOrEmpty(Options.UserName))
                {
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential(Options.UserName, Options.Password);
                    //client.DeliveryMethod = SmtpDeliveryMethod.Network;
                }

                model.From ??= new EmailAddress();

                if (string.IsNullOrEmpty(model.From.Address))
                    model.From.Address = Options.FromEmail;
                if (string.IsNullOrEmpty(model.From.Name))
                    model.From.Name = Options.FromName;
            }


            using var message = ToMailMessage(model);
            //client.SendCompleted += (sender, e) =>
            //{
            //    client.Dispose();
            //    message.Dispose();
            //};

            await client.SendMailAsync(message, cancellationToken);
        }
        public static MailMessage ToMailMessage(Email model)
        {
            var message = new MailMessage
            {
                Subject = model.Subject,
                SubjectEncoding = Encoding.UTF8,
                Body = model.Body,
                BodyEncoding = Encoding.UTF8,
                IsBodyHtml = model.IsHtml,
            };


            if (!string.IsNullOrEmpty(model.From?.Address))
            {
                message.From = new MailAddress(model.From.Address, model.From.Name, Encoding.UTF8);
            }

            foreach (var to in model.To ?? [])
            {
                message.To.Add(new MailAddress(to.Address, to.Name, Encoding.UTF8));
            }

            foreach (var cc in model.Cc ?? [])
            {
                message.CC.Add(new MailAddress(cc.Address, cc.Name, Encoding.UTF8));
            }

            foreach (var bcc in model.Bcc ?? [])
            {
                message.Bcc.Add(new MailAddress(bcc.Address, bcc.Name, Encoding.UTF8));
            }

            foreach (var item in model.Attachments ?? [])
            {
                var attachment = new Attachment(new MemoryStream(item.FileData), item.FileName) { ContentId = item.ContentId };
                message.Attachments.Add(attachment);
            }

            if (model.Calendar != null)
            {
                var ics = model.Calendar.ToString();
                var calendarBytes = Encoding.UTF8.GetBytes(ics);
                var attachment = new Attachment(new MemoryStream(calendarBytes), "event.ics", "text/calendar");
                message.Attachments.Add(attachment);
            }

            if (!model.Calendars.IsNullOrEmpty())
            {
                var i = 1;
                foreach (var cal in model.Calendars)
                {
                    var ics = cal.ToString();
                    var calendarBytes = Encoding.UTF8.GetBytes(ics);
                    var attachment = new Attachment(new MemoryStream(calendarBytes), $"event{i++}.ics", "text/calendar");
                    message.Attachments.Add(attachment);
                }
            }

            return message;
        }
    }
}