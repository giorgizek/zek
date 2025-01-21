using Microsoft.EntityFrameworkCore;
using Zek.Data.Entity;
using Zek.Persistence.Configurations;

namespace Zek.Model.Identity
{
    public class ForgotPassword
    {
        public int UserId { get; set; }
        public string Code { get; set; }
        public DateTime? ValidTo { get; set; }
    }

    public class ForgotPasswordMap : EntityTypeMap<ForgotPassword>
    {
        public ForgotPasswordMap(ModelBuilder builder) : base(builder)
        {
            ToTable("ForgotPasswords", nameof(Schema.Identity));
            HasKey(x => x.UserId);

            Property(x => x.UserId).ValueGeneratedNever();
            Property(x => x.Code).HasMaxLength(4000);
            Property(x => x.ValidTo).HasColumnTypeDateTime();
        }
    }
}
