using System.ComponentModel.DataAnnotations;

namespace Zek.Model.DTO
{
    public class AreaDTO
    {
        /// <summary>
        /// Application ID / პროგრამის იდენთიფიკატორი
        /// </summary>
        [Required]
        public int? ApplicationId { get; set; }

        /// <summary>
        /// Area name / არეას დასახელება (მაგ Account, Attachment, Faq, Chat....)
        /// </summary>
        [Required]
        public string Area { get; set; }

        /// <summary>
        /// Area ID / არეას იდენთიფიკატორი (მაგ AccountId, AttachmentId, FaqId, ChatId...)
        /// </summary>
        [Required]
        public int? AreaId { get; set; }

        
    }
}
