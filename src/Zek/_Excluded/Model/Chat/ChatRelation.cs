using System;
using Microsoft.EntityFrameworkCore;
using Zek.Data.Entity;

namespace Zek.Model.Chat
{
    //public class ChatRelation : ChatRelation<Chat, User>
    //{
    //}
    public class ChatRelation<TChat, TType, TUser> : ChatRelationPoco<TChat>
        where TChat : Chat<TType, TUser>
        where TType : class 
        where TUser : class
    {
        public TUser User { get; set; }
    }

    public class ChatRelationPoco<TChat>
        where TChat : ChatPoco
    {
        public int ChatId { get; set; }
        public TChat Chat { get; set; }

        public int UserId { get; set; }

        public int? LastReadMessageId { get; set; }

        public int RoleId { get; set; }

        public bool IsDeleted { get; set; }
        public DateTime? DeleteDate { get; set; }
    }




    public class ChatRelationMap<TChatRelation, TChat, TType, TUser> : ChatRelationPocoMap<TChatRelation, TChat>
        where TChatRelation : ChatRelation<TChat, TType, TUser>
        where TChat : Chat<TType, TUser>
        where TType : class
        where TUser : class
    {
        public ChatRelationMap(ModelBuilder builder) : base(builder)
        {
            HasOne(t => t.User).WithMany().HasForeignKey(t => t.UserId).OnDelete(DeleteBehavior.Cascade);
        }
    }

    public class ChatRelationPocoMap<TChatRelation, TChat> : EntityTypeMap<TChatRelation>
        where TChatRelation : ChatRelationPoco<TChat>
        where TChat : ChatPoco
    {
        public ChatRelationPocoMap(ModelBuilder builder) : base(builder)
        {
            ToTable("ChatRelations", "Chat");
            HasKey(t => new { t.ChatId, t.UserId });

            HasIndex(t => t.ChatId);
            HasOne(t => t.Chat).WithMany().HasForeignKey(t => t.ChatId).OnDelete(DeleteBehavior.Cascade);

            HasIndex(t => t.UserId);

            HasIndex(t => t.IsDeleted);

            Property(t => t.DeleteDate).HasColumnType("datetime2(0)");
        }

    }
}