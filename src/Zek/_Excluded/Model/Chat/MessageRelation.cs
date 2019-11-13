using System;
using Microsoft.EntityFrameworkCore;
using Zek.Data.Entity;

namespace Zek.Model.Chat
{

    public class MessageRelation<TMessage, TUser> : MessageRelationPoco
        where TMessage : MessagePoco
        where TUser : class 
    {
        public TMessage Message { get; set; }
        public TUser User { get; set; }
    }
    public class MessageRelationPoco
    {
        public int MessageId { get; set; }
        public int UserId { get; set; }

        public bool IsRead { get; set; }
        public DateTime? ReadDate { get; set; }

        //public bool IsReply { get; set; }
        //public DateTime? ReplyDate { get; set; }

        public bool IsDeleted { get; set; }
        public DateTime? DeleteDate { get; set; }
    }




    //public class MessageRelationMap : EntityTypeMap<MessageRelation<Message>>
    //{
    //    public MessageRelationMap(ModelBuilder builder) : base(builder)
    //    {
    //    }
    //}
    public class MessageRelationMap<TMessageRelation, TMessage, TUser> : EntityTypeMap<TMessageRelation>
        where TMessageRelation : MessageRelation<TMessage, TUser>
        where TMessage : MessagePoco
        where TUser : class 
    {
        public MessageRelationMap(ModelBuilder builder) : base(builder)
        {
            ToTable("MessageRelations", "Chat");
            HasKey(t => new { t.MessageId, t.UserId });

            HasIndex(t => t.UserId);
            HasOne(t => t.User).WithMany().HasForeignKey(t => t.UserId).OnDelete(DeleteBehavior.Restrict);

            HasIndex(t => t.MessageId);
            HasOne(t => t.Message).WithMany().HasForeignKey(t => t.MessageId).OnDelete(DeleteBehavior.Cascade);


            Property(t => t.ReadDate).HasColumnType("datetime2(0)");

            HasIndex(t => t.IsDeleted);

            Property(t => t.DeleteDate).HasColumnType("datetime2(0)");
        }
    }
}