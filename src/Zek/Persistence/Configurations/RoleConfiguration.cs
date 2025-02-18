using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zek.Domain.Entities.Identity;

namespace Zek.Persistence.Configurations
{
    internal class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public virtual void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Roles", "Identity");

            builder.Property(t => t.Id).ValueGeneratedOnAdd();
            builder.Property(r => r.ConcurrencyStamp).HasMaxLength(50).IsConcurrencyToken();
            builder.Property(u => u.Name).HasMaxLength(256);
            builder.Property(u => u.NormalizedName).HasMaxLength(256);

            builder.HasKey(r => r.Id);

            builder.HasIndex(r => r.NormalizedName).IsUnique();
        }
    }
}
