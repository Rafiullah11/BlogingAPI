using BlogApp.Models;
using BlogingAPI.DTO.AuthorsDtos;
using BlogingAPI.DTO.BlogPostDtos;
using BlogingAPI.Model;

namespace BlogingAPI.DTO.CommentsDto
{
     public class CommentOnPostDto
    {
        public int Id { get; set; }
        public string? Content { get; set; }
        public int BlogPostId { get; set; }
        public AuthorsDto? Author { get; set; }
        public List<BlogPostDto> BlogPost { get; set; }

    }

}
