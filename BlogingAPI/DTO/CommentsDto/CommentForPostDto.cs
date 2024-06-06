using BlogingAPI.DTO.AuthorsDtos;
using BlogingAPI.DTO.BlogPostDtos;

namespace BlogingAPI.DTO.CommentsDto
{
    public class CommentForPostDto
    {
        public int Id { get; set; }
        public string? Content { get; set; }
        //public int BlogPostId { get; set; }
    }
}
