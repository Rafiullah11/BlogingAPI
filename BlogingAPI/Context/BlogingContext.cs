using BlogingAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace BlogingAPI.Context
{
        public class BloggingContext : DbContext
        {
            public BloggingContext(DbContextOptions<BloggingContext> options)
            : base(options)
            {
            }

            public DbSet<User> Users { get; set; }
            public DbSet<Role> Roles { get; set; }
            public DbSet<BlogPost> BlogPosts { get; set; }
            public DbSet<Category> Categories { get; set; }
            public DbSet<Comment> Comments { get; set; }
            public DbSet<Tag> Tags { get; set; }
            public DbSet<PostTag> PostTags { get; set; }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.Entity<PostTag>()
                    .HasKey(pt => new { pt.PostId, pt.TagId });

                modelBuilder.Entity<PostTag>()
                    .HasOne(pt => pt.BlogPost)
                    .WithMany(p => p.PostTags)
                    .HasForeignKey(pt => pt.PostId);

                modelBuilder.Entity<PostTag>()
                    .HasOne(pt => pt.Tag)
                    .WithMany(t => t.PostTags)
                    .HasForeignKey(pt => pt.TagId);
            }
        }
    

}
