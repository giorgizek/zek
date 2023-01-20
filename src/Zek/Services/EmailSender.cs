using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Zek.Extensions.Collections;
using Zek.Model.Config;
using Zek.Model.DTO.Email;
using Zek.Office;

namespace Zek.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
        Task SendEmailAsync(Email model);
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

        public virtual Task SendEmailAsync(string email, string subject, string message)
        {
            var model = new Email
            {
                Subject = subject,
                Body = message,
                To = new List<EmailAddress>
                {
                    new() { Address = email }
                },
            };
            return SendEmailAsync(model);
        }

        public virtual async Task SendEmailAsync(Email model)
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

            await client.SendMailAsync(message);
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

            model.To?.ForEach(x => message.To.Add(new MailAddress(x.Address, x.Name, Encoding.UTF8)));
            model.Cc?.ForEach(x => message.CC.Add(new MailAddress(x.Address, x.Name, Encoding.UTF8)));
            model.Bcc?.ForEach(x => message.Bcc.Add(new MailAddress(x.Address, x.Name, Encoding.UTF8)));

            model.Attachments?.ForEach(x =>
            {
                var attachment = new Attachment(new MemoryStream(x.FileData), x.FileName) { ContentId = x.ContentId };
                message.Attachments.Add(attachment);
            });

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