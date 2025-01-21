using Microsoft.EntityFrameworkCore;

namespace Zek.Model.Identity
{
    public class IdentityUserContext : IdentityUserContext<User, int>
    {
    }

    public class IdentityUserContext<TUser, TKey> : DbContext
    where TUser : User<TKey>
    where TKey : IEquatable<TKey>
    {
        /// <summary>
        /// Initializes a new instance of the db context.
        /// </summary>
        /// <param name="options">The options to be used by a <see cref="DbContext"/>.</param>
        public IdentityUserContext(DbContextOptions options) : base(options) { }

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        protected IdentityUserContext() { }


        /// <summary>
        /// Gets or sets the <see cref="DbSet{TEntity}"/> of Users.
        /// </summary>
        public virtual DbSet<TUser> Users { get; set; }


        /// <summary>
        /// Configures the schema needed for the identity framework.
        /// </summary>
        /// <param name="builder">
        /// The builder being used to construct the model for this context.
        /// </param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // ReSharper disable once ObjectCreationAsStatement
            new UserMap<TUser, TKey>(builder, false, false);
        }

    }
}