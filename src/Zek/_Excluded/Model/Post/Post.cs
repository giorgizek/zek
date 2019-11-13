using System;
using Microsoft.EntityFrameworkCore;
using Zek.Model.Base;

namespace Zek.Model.Post
{
    public class Post : BaseModel<int>
    {
        public DateTime Date { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int? CategoryId { get; set; }

        public string ShortContent { get; set; }
        public string Content { get; set; }

        public bool AllowComment { get; set; }

        public bool IsPinned { get; set; }

        public int StatusId { get; set; }

        public int CommentCount { get; set; }
        public int ViewCount { get; set; }

        public string Ip { get; set; }
    }

    public class PostMap : BaseModelMap<Post, int>
    {
        public PostMap(ModelBuilder builder) : base(builder)
        {
            ToTable("Posts", "Post");

            Property(t => t.Date).HasColumnType("date");
            HasIndex(t => t.Date);

            Property(t => t.Title).HasMaxLength(255);
            Property(t => t.Description).HasMaxLength(200);

            HasIndex(t => t.CategoryId);

            HasIndex(t => t.IsPinned);
            Property(t => t.Ip).HasMaxLength(45);
        }
    }
}
