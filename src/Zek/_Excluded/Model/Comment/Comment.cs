using Microsoft.EntityFrameworkCore;
using Zek.Model.Base;

namespace Zek.Model.Comment
{
    public class Comment  : BaseModel<int>
    {
        public string Content { get; set; }
        public string Ip { get; set; }
    }

    public class CommentMap : BaseModelMap<Comment, int>
    {
        public CommentMap(ModelBuilder builder) : base(builder)
        {
            ToTable("Posts", "Post");


            Property(t => t.Ip).HasMaxLength(45);
        }
    }
}
