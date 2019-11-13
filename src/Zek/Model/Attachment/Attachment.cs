using Microsoft.EntityFrameworkCore;
using Zek.Model.Base;

namespace Zek.Model.Attachment
{
    //public class Attachment : Attachment<User>
    //{
    //}
    //public class AttachmentMap : AttachmentMap<Attachment, User>
    //{
    //    public AttachmentMap(ModelBuilder builder) : base(builder)
    //    {
    //    }
    //}
    //public class Attachment<TUser> : AttachmentPoco
    //  where TUser : User
    //{
    //    public TUser Creator { get; set; }

    //    public TUser Modifier { get; set; }
    //}
    //public class AttachmentMap<TAttachment, TUser> : AttachmentPocoMap<TAttachment>
    //    where TAttachment : Attachment<TUser>
    //    where TUser : User
    //{
    //    public AttachmentMap(ModelBuilder builder) : base(builder)
    //    {
    //        HasOne(t => t.Creator).WithMany().HasForeignKey(t => t.CreatorId).OnDelete(DeleteBehavior.Restrict);
    //        HasOne(t => t.Modifier).WithMany().HasForeignKey(t => t.ModifierId).OnDelete(DeleteBehavior.Restrict);
    //    }
    //}


    public class AttachmentPoco : PocoModel
    {
        public int ApplicationId { get; set; }
        /// <summary>
        /// Area used to categorize (e.g: Loan, Person, Claim...)
        /// </summary>
        public int AreaId { get; set; }

        public int? TypeId { get; set; }//for combobox values (like: passport, contract, signature, photo, avator, icon);
        public string Path { get; set; }
        public string FileName { get; set; }
        //public string FileType { get; set; }
        public int? MimeTypeId { get; set; }
        public int? CompressionTypeId { get; set; }
        public long FileSize { get; set; }
        public string CheckSum { get; set; }

    }

    public class AttachmentPocoMap : AttachmentPocoMap<AttachmentPoco>
    {
        public AttachmentPocoMap(ModelBuilder builder) : base(builder)
        {
        }
    }

    public class AttachmentPocoMap<TAttachment> : PocoModelMap<TAttachment>
        where TAttachment : AttachmentPoco
    {
        public AttachmentPocoMap(ModelBuilder builder) : base(builder)
        {
            ToTable("Attachments", "Attachment");

            HasIndex(t => new { t.ApplicationId, t.AreaId });
            HasIndex(t => t.CheckSum);


            Property(t => t.Path).IsRequired();
            Property(t => t.FileName).IsRequired().HasMaxLength(260);
            Property(t => t.CheckSum).IsRequired().HasMaxLength(255);
        }
    }
}
