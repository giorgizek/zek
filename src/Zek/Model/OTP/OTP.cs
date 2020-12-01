using System;
using Microsoft.EntityFrameworkCore;
using Zek.Data.Entity;

namespace Zek.Model.OTP
{
    public class OTP
    {
        public int Id { get; set; }
        public int ApplicationId { get; set; }
        public int UserId { get; set; }
        public string Code { get; set; }
        public int StatusId { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ExpireDate { get; set; }
    }

    public class OTPMap : EntityTypeMap<OTP>
    {
        public OTPMap(ModelBuilder builder) : base(builder)
        {
            ToTable("OTPs", "Identity");
            HasKey(x => x.Id);
            
            Property(x => x.Id).ValueGeneratedOnAdd();
            Property(x => x.CreateDate).HasColumnTypeDateTime();
            Property(x => x.ExpireDate).HasColumnTypeDateTime();

            HasIndex(x => new { x.ApplicationId, x.UserId });
            HasIndex(x => new { x.ApplicationId, x.ExpireDate });
        }
    }
}
