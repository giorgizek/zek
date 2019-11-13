using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.AspNetCore.Identity;
using Zek.Data.Entity;

namespace Zek.Model.Identity
{
    /// <summary>
    /// Represents a role in the identity system
    /// </summary>
    /// <typeparam name="TKey">The type used for the primary key for the role.</typeparam>
    public class Role<TKey> where TKey : IEquatable<TKey>
    {
        /// <summary>
        /// Initializes a new instance of <see cref="IdentityRole{TKey}"/>.
        /// </summary>
        public Role()
        {
            ConcurrencyStamp = Guid.NewGuid().ToString();
        }

        /// <summary>
        /// Initializes a new instance of <see cref="IdentityRole{TKey}"/>.
        /// </summary>
        /// <param name="roleName">The role name.</param>
        public Role(string roleName) : this()
        {
            Name = roleName;
        }

        /// <summary>
        /// Gets or sets the primary key for this role.
        /// </summary>
        public virtual TKey Id { get; set; }

        /// <summary>
        /// Gets or sets the name for this role.
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// Gets or sets the normalized name for this role.
        /// </summary>
        public virtual string NormalizedName { get; set; }

        /// <summary>
        /// A random value that should change whenever a role is persisted to the store
        /// </summary>
        public virtual string ConcurrencyStamp { get; set; }

        /// <summary>
        /// Returns the name of the role.
        /// </summary>
        /// <returns>The name of the role.</returns>
        public override string ToString() => Name;
    }

    /// <summary>
    /// The default implementation of <see cref="Role{TKey}"/> which uses a string as the primary key.
    /// </summary>
    public class Role : Role<int>
    {
        public Role()
        {
        }

        public Role(string roleName)
        {
            Name = roleName;
        }
    }


    public class RoleMap : RoleMap<Role, int>
    {
        public RoleMap(ModelBuilder builder) : base(builder)
        {
        }
    }

    public class RoleMap<TEntity, TKey> : EntityTypeMap<TEntity>
        where TEntity : Role<TKey>
        where TKey : IEquatable<TKey>
    {
        public RoleMap(ModelBuilder builder, bool indexNormalizedName = true) : base(builder)
        {
            ToTable("Roles", "Identity");
            
            Property(t => t.Id).ValueGeneratedOnAdd();
            Property(r => r.ConcurrencyStamp).IsConcurrencyToken();
            Property(u => u.Name).HasMaxLength(256);
            Property(u => u.NormalizedName).HasMaxLength(256);

            HasKey(r => r.Id);

            if (indexNormalizedName)
                HasIndex(r => r.NormalizedName).IsUnique();
        }
    }
}
