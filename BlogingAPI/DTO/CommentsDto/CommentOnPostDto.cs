using BlogApp.Models;
using BlogingAPI.DTO.BlogPostDtos;
using BlogingAPI.Model;

namespace BlogingAPI.DTO.CommentsDto
{
     public class CommentOnPostDto
    {
        public int Id { get; set; }
        public string? Content { get; set; }
        public int BlogPostId { get; set; }
        public int AuthorId { get; set; }
        public List<BlogPostDto> BlogPost { get; set; }
        public List<CommentOnPost> CommentOnPosts { get; }
        //public BlogPost? BlogPost { get; set; } = new BlogPost();

    }

}
