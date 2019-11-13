using System;
using Microsoft.EntityFrameworkCore;
using Zek.Data.Entity;

namespace Zek.Model.Chat
{
    //public class Message : Message<Chat>
    //{
    //    public Message Parent { get; set; }
    //}
    public class Message<TChat, TType, TUser> : MessageBase<TChat>
        where TChat : Chat<TType, TUser>
        where TType : class
        where TUser : class
    {
        public TUser Creator { get; set; }
    }
    public class MessageBase<TChat> : MessagePoco
        where TChat : ChatPoco
    {
        public TChat Chat { get; set; }
    }
    public class MessagePoco
    {
        public int Id { get; set; }

        public int ChatId { get; set; }
        public int? ParentId { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }


        public bool IsDeleted { get; set; }

        public int CreatorId { get; set; }
        public DateTime CreateDate { get; set; }
    }



    //public class MessageMap : MessageMap<Message>
    //{
    //    public MessageMap(ModelBuilder builder) : base(builder)
    //    {
    //        HasOne(t => t.Parent).WithMany().HasForeignKey(t => t.ParentId).OnDelete(DeleteBehavior.Restrict);
    //    }
    //}
    //public class MessageMap<TMessage, TUser> : MessageMap<TMessage, Chat>
    //     where TMessage : Message<Chat>
    //    where TUser : class
    //{
    //    public MessageMap(ModelBuilder builder) : base(builder)
    //    {
    //    }
    //}
    public class MessageMap<TMessage, TChat, TType, TUser> : MessageBaseMap<TMessage, TChat>
        where TMessage : Message<TChat, TType, TUser>
        where TChat : Chat<TType, TUser>
        where TType : class
        where TUser : class
    {
        public MessageMap(ModelBuilder builder) : base(builder)
        {
            HasOne(t => t.Chat).WithMany().HasForeignKey(t => t.ChatId).OnDelete(DeleteBehavior.Cascade);
            HasOne(t => t.Creator).WithMany().HasForeignKey(t => t.CreatorId).OnDelete(DeleteBehavior.Cascade);
        }
    }
    public class MessageBaseMap<TMessage, TChat> : MessagePocoMap<TMessage>
        where TMessage : MessageBase<TChat>
        where TChat : ChatPoco
    {
        public MessageBaseMap(ModelBuilder builder) : base(builder)
        {
            HasOne(t => t.Chat).WithMany().HasForeignKey(t => t.ChatId).OnDelete(DeleteBehavior.Cascade);
        }
    }
    public class MessagePocoMap<TMessage> : EntityTypeMap<TMessage>
        where TMessage : MessagePoco
    {
        public MessagePocoMap(ModelBuilder builder) : base(builder)
        {
            ToTable("Messages", "Chat");
            HasKey(t => t.Id);
            Property(t => t.Id).ValueGeneratedOnAdd();

            HasIndex(t => t.ChatId);
            HasIndex(t => t.ParentId);

            Property(t => t.Subject).HasMaxLength(500);
            Property(t => t.Body).HasMaxLength(5000);


            HasIndex(t => t.IsDeleted);

            HasIndex(t => t.CreatorId);
            Property(t => t.CreateDate).HasColumnType("datetime2(0)");
        }
    }
}
