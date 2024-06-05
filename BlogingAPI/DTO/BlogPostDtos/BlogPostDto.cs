using BlogApp.Models;
using BlogingAPI.Model;

namespace BlogingAPI.DTO.BlogPostDtos
{
    public class BlogPostDto
    {
        public int Id { get; set; }
        public string? BlogTitle { get; set; }
        public string? BlogContent { get; set; }
        public int AuthorId { get; set; }
        public List<CommentOnPost> CommentOnPosts { get; } 
    }
}
