using System.Collections.Generic;
using Zek.Model.DTO.Attachment;

namespace Zek.Model.DTO.Email
{
    public class EmailDTO
    {
        public EmailAddressDTO From { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public bool IsHtml { get; set; }

        public List<EmailAttachmentDTO> Attachments { get; set; }

        public List<EmailAddressDTO> To { get; set; }
        public List<EmailAddressDTO> Cc { get; set; }
        public List<EmailAddressDTO> Bcc { get; set; }
    }
}
