using Microsoft.EntityFrameworkCore;
using Zek.Model.Identity;

namespace Zek.Model.Base
{
    public class BaseModel<TId> : BaseModel<TId, User>
    {
    }

    public class BaseModel<TId, TUser> : PocoModel<TId>
        where TUser : User
    {
        public TUser Creator { get; set; }

        public TUser Modifier { get; set; }
    }

    public class BaseModelMap<TEntity, TId> : BaseModelMap<TEntity, TId, User>
        where TEntity : BaseModel<TId>
    {
        public BaseModelMap(ModelBuilder builder, bool? valueGeneratedOnAdd = null) : base(builder, valueGeneratedOnAdd)
        {
        }
    }

    public class BaseModelMap<TEntity, TId, TUser> : PocoModelMap<TEntity, TId>
        where TEntity : BaseModel<TId, TUser>
        where TUser : User
    {
        public BaseModelMap(ModelBuilder builder, bool? valueGeneratedOnAdd = null) : base(builder, valueGeneratedOnAdd)
        {
            HasOne(t => t.Creator).WithMany().HasForeignKey(t => t.CreatorId).OnDelete(DeleteBehavior.Restrict);
            HasOne(t => t.Modifier).WithMany().HasForeignKey(t => t.ModifierId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
