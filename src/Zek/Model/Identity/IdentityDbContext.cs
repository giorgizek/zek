using Microsoft.EntityFrameworkCore;
using System;

namespace Zek.Model.Identity
{

    public class IdentityDbContext : IdentityDbContext<User, Role, int, UserRole>
    {
        public IdentityDbContext()
        {
        }

        public IdentityDbContext(DbContextOptions options)
            : base(options)
        {
        }
    }

    public class IdentityDbContext<TUser, TRole> : IdentityDbContext<TUser, TRole, int, UserRole>
        where TUser : User
        where TRole : Role
    {
        public IdentityDbContext()
        {
        }

        public IdentityDbContext(DbContextOptions options)
            : base(options)
        {
        }
    }



    /// <summary>
    /// Base class for the Entity Framework database context used for identity.
    /// </summary>
    /// <typeparam name="TUser">The type of user objects.</typeparam>
    /// <typeparam name="TRole">The type of role objects.</typeparam>
    /// <typeparam name="TKey">The type of the primary key for users and roles.</typeparam>
    /// <typeparam name="TUserRole">The type of the user role object.</typeparam>
    public class IdentityDbContext<TUser, TRole, TKey, TUserRole> : IdentityUserContext<TUser, TKey>
        where TUser : User<TKey>
        where TRole : Role<TKey>
        where TKey : IEquatable<TKey>
        where TUserRole : UserRole<TKey>
    {
        /// <summary>
        /// Initializes a new instance of the db context.
        /// </summary>
        /// <param name="options">The options to be used by a <see cref="DbContext"/>.</param>
        public IdentityDbContext(DbContextOptions options) : base(options) { }

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        protected IdentityDbContext() { }

        /// <summary>
        /// Gets or sets the <see cref="DbSet{TEntity}"/> of User roles.
        /// </summary>
        public virtual DbSet<TUserRole> UserRoles { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="DbSet{TEntity}"/> of roles.
        /// </summary>
        public virtual DbSet<TRole> Roles { get; set; }


        /// <summary>
        /// Configures the schema needed for the identity framework.
        /// </summary>
        /// <param name="builder">
        /// The builder being used to construct the model for this context.
        /// </param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            new RoleMap<TRole, TKey>(builder);

            builder.Entity<TUser>(b =>
            {
                b.HasMany<TUserRole>().WithOne().HasForeignKey(ur => ur.UserId).IsRequired();
            });
            builder.Entity<TRole>(b =>
            {
                b.HasMany<TUserRole>().WithOne().HasForeignKey(ur => ur.RoleId).IsRequired();
            });
            builder.Entity<TUserRole>(b =>
            {
                b.HasKey(r => new { r.UserId, r.RoleId });
                b.ToTable("UserRoles", nameof(Schema.Identity));
            });
        }
    }
}