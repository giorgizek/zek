using Microsoft.EntityFrameworkCore;
using Zek.Data.Entity;
using Zek.Persistence.Configurations;

namespace Zek.Model.Identity
{
    public class Schema
    {
        public bool Identity { get; set; }
    }

    public class User : User<int>
    {
    }

    public class User<TKey> where TKey : IEquatable<TKey>
    {
        public User()
        {
        }
        public User(string userName) : this()
        {
            UserName = userName;
        }


        /// <summary>
        /// Gets or sets the primary key for this user.
        /// </summary>
        public virtual TKey Id { get; set; } = default!;

        /// <summary>
        /// Gets or sets the user name for this user.
        /// </summary>
        public virtual string UserName { get; set; }

        /// <summary>
        /// Gets or sets the normalized user name for this user.
        /// </summary>
        public virtual string NormalizedUserName { get; set; }

        /// <summary>
        /// Gets or sets the email address for this user.
        /// </summary>
        public virtual string Email { get; set; }

        /// <summary>
        /// Gets or sets the normalized email address for this user.
        /// </summary>
        public virtual string NormalizedEmail { get; set; }

        /// <summary>
        /// Gets or sets a flag indicating if a user has confirmed their email address.
        /// </summary>
        /// <value>True if the email address has been confirmed, otherwise false.</value>
        public virtual bool EmailConfirmed { get; set; }

        /// <summary>
        /// Gets or sets a salted and hashed representation of the password for this user.
        /// </summary>
        public virtual string PasswordHash { get; set; }

        /// <summary>
        /// A random value that must change whenever a users credentials change (password changed, login removed)
        /// </summary>
        public virtual string SecurityStamp { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// A random value that must change whenever a user is persisted to the store
        /// </summary>
        public virtual string ConcurrencyStamp { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// Gets or sets a telephone number for the user.
        /// </summary>
        public virtual string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets a flag indicating if a user has confirmed their telephone address.
        /// </summary>
        /// <value>True if the telephone number has been confirmed, otherwise false.</value>
        public virtual bool PhoneNumberConfirmed { get; set; }

        /// <summary>
        /// Gets or sets a flag indicating if two factor authentication is enabled for this user.
        /// </summary>
        /// <value>True if 2fa is enabled, otherwise false.</value>
        public virtual bool TwoFactorEnabled { get; set; }

        /// <summary>
        /// Gets or sets the date and time, in UTC, when any user lockout ends.
        /// </summary>
        /// <remarks>
        /// A value in the past means the user is not locked out.
        /// </remarks>
        //public virtual DateTimeOffset? LockoutEnd { get; set; }
        public virtual DateTime? LockoutEnd { get; set; }

        /// <summary>
        /// Gets or sets a flag indicating if the user could be locked out.
        /// </summary>
        /// <value>True if the user could be locked out, otherwise false.</value>
        public virtual bool LockoutEnabled { get; set; }

        /// <summary>
        /// Gets or sets the number of failed login attempts for the current user.
        /// </summary>
        public virtual int AccessFailedCount { get; set; }

        /// <summary>
        /// User active status
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Returns the username for this user.
        /// </summary>
        public override string ToString() => UserName ?? string.Empty;

        public int? CreatorId { get; set; }
        public DateTime CreateDate { get; set; }

        public int? ModifierId { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }



    public class UserMap : UserMap<User, int>
    {
        public UserMap(ModelBuilder builder, bool unique = true) : base(builder, unique)
        {
        }
    }

    public class UserMap<TEntity> : UserMap<TEntity, int>
        where TEntity : User
    {
        public UserMap(ModelBuilder builder, bool unique = true) : base(builder, unique)
        {
        }
    }

    public class UserMap<TEntity, TKey> : EntityTypeMap<TEntity>
        where TEntity : User<TKey>
        where TKey : IEquatable<TKey>
    {
        public UserMap(ModelBuilder builder, bool unique = true) : this(builder, unique, unique)
        {
        }
        public UserMap(ModelBuilder builder, bool uniqueUserName, bool uniqueEmail) : base(builder)
        {
            ToTable("Users", nameof(Schema.Identity));

            Property(t => t.Id).ValueGeneratedOnAdd();
            Property(u => u.UserName).HasMaxLength(256);
            Property(u => u.NormalizedUserName).HasMaxLength(256);
            Property(u => u.Email).HasMaxLength(256);
            Property(u => u.NormalizedEmail).HasMaxLength(256);
            Property(u => u.ConcurrencyStamp).HasMaxLength(50).IsConcurrencyToken();
            Property(u => u.SecurityStamp).HasMaxLength(50).IsConcurrencyToken();
            Property(u => u.PhoneNumber).HasMaxLength(50);
            Property(u => u.LockoutEnd).HasColumnTypeDateTime();
            Property(t => t.CreateDate).HasColumnTypeDateTime();
            Property(t => t.ModifiedDate).HasColumnTypeDateTime();

            HasKey(u => u.Id);
            if (uniqueUserName)
                HasIndex(u => u.NormalizedUserName).IsUnique();
            if (uniqueEmail)
                HasIndex(u => u.NormalizedEmail).IsUnique();

            HasIndex(x => x.IsActive);
            HasIndex(t => t.CreatorId);
            HasIndex(t => t.ModifierId);
        }
    }
}