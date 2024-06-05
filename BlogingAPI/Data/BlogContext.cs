using BlogApp.Models;
using BlogingAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Data
{
    public class BlogContext : DbContext
    {
        public BlogContext(DbContextOptions<BlogContext> options) : base(options)
        {
        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<CommentOnPost> CommentOnPosts { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Author>()
        //        .HasMany(a => a.BlogPosts)
        //        .WithOne(b => b.Author)
        //        .HasForeignKey(b => b.AuthorId)
        //        .OnDelete(DeleteBehavior.Cascade); // Cascade delete BlogPosts when an Author is deleted

        //    modelBuilder.Entity<BlogPost>()
        //        .HasMany(b => b.CommentOnPosts)
        //        .WithOne(c => c.BlogPost)
        //        .HasForeignKey(c => c.BlogPostId)
        //        .OnDelete(DeleteBehavior.Cascade); // Cascade delete CommentOnPosts when a BlogPost is deleted

        //    modelBuilder.Entity<CommentOnPost>()
        //        .HasOne(c => c.Author)
        //        .WithMany(a => a.CommentOnPosts)
        //        .HasForeignKey(c => c.AuthorId)
        //        .OnDelete(DeleteBehavior.Restrict); // Prevent cascading delete on CommentOnPosts when an Author is deleted
        //}
    }
}
