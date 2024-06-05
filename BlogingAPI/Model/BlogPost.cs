using BlogingAPI.Model;
using System.ComponentModel.DataAnnotations;

namespace BlogApp.Models
{
    public class BlogPost
    {
        // One blog can have many comments
        public int Id { get; set; }
        public string? BlogTitle { get; set; }
        public string? BlogContent { get; set; }
        public List<CommentOnPost> CommentOnPosts { get; }

        // Foreign key to Author
        public int AuthorId { get; set; }
        public Author? Author { get; set; }
    }


}
