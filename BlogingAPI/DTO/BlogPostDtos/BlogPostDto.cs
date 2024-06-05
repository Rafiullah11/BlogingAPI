using BlogApp.Models;
using BlogingAPI.DTO.CommentsDto;
using BlogingAPI.Model;

namespace BlogingAPI.DTO.BlogPostDtos
{
    public class BlogPostDto
    {
        public int Id { get; set; }
        public string? BlogTitle { get; set; }
        public string? BlogContent { get; set; }
        public int AuthorId { get; set; }
        public List<CommentOnPostDto> CommentOnPosts { get; set; } 
    }
}
