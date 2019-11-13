using System;
using Microsoft.EntityFrameworkCore;
using Zek.Model.Base;
using Zek.Model.Identity;

namespace Zek.Model.Document
{
    public class ApplicationBase<TUser> : BaseModel<int, TUser>
        where TUser : User
    {
        public DateTime Date { get; set; }

        public string Comment { get; set; }

        public bool IsApproved { get; set; }

        public int? ApproverId { get; set; }
        public TUser Approver { get; set; }
        public DateTime? ApproveDate { get; set; }
    }



    public class ApplicationBaseMap<TApplication, TUser> : BaseModelMap<TApplication, int, TUser>
        where TApplication : ApplicationBase<TUser>
        where TUser : User
    {
        public ApplicationBaseMap(ModelBuilder builder) : base(builder)
        {
            Property(t => t.Date).HasColumnType("date");
            HasIndex(t => t.Date);

            HasIndex(c => c.IsApproved);

            HasOne(t => t.Approver).WithMany().HasForeignKey(t => t.ModifierId).OnDelete(DeleteBehavior.Restrict);
            Property(t => t.ApproveDate).HasColumnType("datetime2(0)");
        }
    }
}
