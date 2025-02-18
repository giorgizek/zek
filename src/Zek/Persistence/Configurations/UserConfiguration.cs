using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zek.Domain.Entities.Identity;

namespace Zek.Persistence.Configurations
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public virtual void Configure(EntityTypeBuilder<User> builder)
        {
            Configure(builder, true, true);
        }
        public virtual void Configure(EntityTypeBuilder<User> builder, bool unique)
        {
            Configure(builder, unique, unique);
        }
        public virtual void Configure(EntityTypeBuilder<User> builder, bool uniqueUserName, bool uniqueEmail)
        {
            builder.ToTable("Users", "Identity");

            builder.Property(t => t.Id).ValueGeneratedOnAdd();
            builder.Property(u => u.UserName).HasMaxLength(256);
            builder.Property(u => u.NormalizedUserName).HasMaxLength(256);
            builder.Property(u => u.Email).HasMaxLength(256);
            builder.Property(u => u.NormalizedEmail).HasMaxLength(256);
            builder.Property(u => u.ConcurrencyStamp).HasMaxLength(50).IsConcurrencyToken();
            builder.Property(u => u.SecurityStamp).HasMaxLength(50).IsConcurrencyToken();
            builder.Property(u => u.PhoneNumber).HasMaxLength(50);
            builder.Property(u => u.LockoutEnd).HasColumnTypeDateTime();
            builder.Property(t => t.CreateDate).HasColumnTypeDateTime();
            builder.Property(t => t.ModifiedDate).HasColumnTypeDateTime();

            builder.HasKey(u => u.Id);
            if (uniqueUserName)
                builder.HasIndex(u => u.NormalizedUserName).IsUnique();
            if (uniqueEmail)
                builder.HasIndex(u => u.NormalizedEmail).IsUnique();

            builder.HasIndex(x => x.IsActive);
            builder.HasIndex(t => t.CreatorId);
            builder.HasIndex(t => t.ModifierId);
        }
    }
}
