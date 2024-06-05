using BlogApp.Models;
using BlogingAPI.Model;

namespace BlogingAPI.DTO.CommentsDto
{
     public class CommentOnPostDto
    {
        public int Id { get; set; }
        public string? Content { get; set; }
        public int BlogPostId { get; set; }
        public int AuthorId { get; set; }
    }
    
}
