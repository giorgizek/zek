using Microsoft.EntityFrameworkCore;
using Zek.Data.Entity;
using Zek.OTP;
using Zek.Persistence.Configurations;

namespace Zek.Model.OTP
{
    [Obsolete]
    public class Otp
    {
        public int Id { get; set; }
        public int ApplicationId { get; set; }
        public int? UserId { get; set; }
        public string? Code { get; set; }
        public OtpStatus Status { get; set; }
        public int AccessFailedCount { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ExpireDate { get; set; }
        public string? IpAddress { get; set; }
    }

    [Obsolete]
    public class OtpMap : EntityTypeMap<Otp>
    {
        public OtpMap(ModelBuilder builder) : base(builder)
        {
            ToTable("Otps", "Identity");
            HasKey(x => x.Id);
            
            Property(x => x.Id).ValueGeneratedOnAdd();
            Property(x => x.CreateDate).HasColumnTypeDateTime();
            Property(x => x.ExpireDate).HasColumnTypeDateTime();
            Property(x => x.IpAddress).HasMaxLength(50);

            HasIndex(x => new { x.ApplicationId, x.UserId });
            HasIndex(x => new { x.ApplicationId, x.ExpireDate });
        }
    }
}
