using Microsoft.EntityFrameworkCore;
using Zek.Data.Entity;

namespace Zek.Model.Identity
{
    public class ForgotPassword
    {
        public int UserId { get; set; }
        public string IdLink { get; set; }
    }

    public class ForgotPasswordMap : EntityTypeMap<ForgotPassword>
    {
        public ForgotPasswordMap(ModelBuilder builder) : base(builder)
        {
            ToTable("ForgotPasswords", "Identity");
            HasKey(x => x.UserId);

            Property(x => x.UserId).ValueGeneratedNever();
        }
    }
}
