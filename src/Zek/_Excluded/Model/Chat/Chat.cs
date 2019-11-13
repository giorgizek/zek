using System;
using Microsoft.EntityFrameworkCore;
using Zek.Data.Entity;

namespace Zek.Model.Chat
{
    public class Chat<TType, TUser> : ChatPoco
        where TType : class
        where TUser : class
    {
        public TType Type { get; set; }
        public TUser Creator { get; set; }

    }
    public class ChatPoco
    {
        public int Id { get; set; }

        public int ApplicationId { get; set; }
        public int? TypeId { get; set; }

        public string Name { get; set; }

        //public string Token { get; set; }50

        //public int Flags { get; set; }

        //public DateTime CreateDate { get; set; }

        //public string IconId { get; set; }255
        //public string BackgroundId { get; set; }255

        //LastReadMessageToken int
        public int? LastMessageId { get; set; }
        //public int PGType
        //public string PGUri 255
        //public int PGRevision
        //public int PGLongtitude
        //public int PGLatitude
        //public string PGCountry 255
        //public string PGTabLine 255
        //public string PGTags 255
        //public int PGRole
        //public int PGLastMessageID
        //public int PGWatchersCount
        //public int PGSearchFlags
        //public string MetaData 255


        public bool IsDeleted { get; set; }

        public int CreatorId { get; set; }
        public DateTime CreateDate { get; set; }

    }



    public class ChatMap<TChat, TType, TUser> : ChatPocoMap<TChat>
        where TChat : Chat<TType, TUser>
        where TType : class
        where TUser : class
    {
        public ChatMap(ModelBuilder builder) : base(builder)
        {
            HasOne(t => t.Type).WithMany().HasForeignKey(t => t.TypeId).OnDelete(DeleteBehavior.Restrict);
            HasOne(t => t.Creator).WithMany().HasForeignKey(t => t.CreatorId).OnDelete(DeleteBehavior.Restrict);
        }
    }
    public class ChatPocoMap<TChat> : EntityTypeMap<TChat>
        where TChat : ChatPoco
    {
        public ChatPocoMap(ModelBuilder builder) : base(builder)
        {
            ToTable("Chats", "Chat");
            HasKey(t => t.Id);
            Property(t => t.Id).ValueGeneratedOnAdd();

            HasIndex(t => t.ApplicationId);

            HasIndex(t => t.TypeId);

            Property(t => t.Name).HasMaxLength(200);

            HasIndex(t => t.LastMessageId);
            HasIndex(t => t.IsDeleted);
            HasIndex(t => t.CreatorId);
            Property(t => t.CreateDate).HasColumnType("datetime2(0)");
        }
    }

   
}
