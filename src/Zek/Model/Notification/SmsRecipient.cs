using Microsoft.EntityFrameworkCore;
using Zek.Data.Entity;
using Zek.Persistence.Configurations;

namespace Zek.Model.Notification
{
    public class SmsRecipient
    {
        public int Id { get; set; }
        public int SmsId { get; set; }
        public int? UserId { get; set; }
        public string PhoneNumber { get; set; }
        public int TryCount { get; set; }
        public int StatusId { get; set; }
        public DateTime? SentDate { get; set; }
    }

    public class SmsRecipientMap<TSmsRecipient> : EntityTypeMap<TSmsRecipient>
        where TSmsRecipient : SmsRecipient
    {
        public SmsRecipientMap(ModelBuilder builder) : base(builder)
        {
            ToTable("SmsRecipients", "Notification");
            HasKey(x => x.Id);
            Property(x => x.Id).ValueGeneratedOnAdd();
            Property(x => x.PhoneNumber).HasColumnTypePhoneNumber();
            Property(x => x.SentDate).HasColumnTypeDateTime();

            HasIndex(x => x.SmsId);
            HasIndex(x => x.UserId);

            HasIndex(x => x.PhoneNumber);

        }
    }
}