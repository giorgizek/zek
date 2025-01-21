using Zek.Model.DTO.Attachment;

namespace Zek.Office
{
    public class Email
    {
        public EmailAddress From { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public bool IsHtml { get; set; }

        public IEnumerable<EmailAttachmentDTO> Attachments { get; set; }

        public IEnumerable<EmailAddress> To { get; set; }
        public IEnumerable<EmailAddress> Cc { get; set; }
        public IEnumerable<EmailAddress> Bcc { get; set; }

        public iCalendar Calendar { get; set; }
        public IEnumerable<iCalendar> Calendars { get; set; }
    }
}
