namespace BlogingAPI.DTO.CommentsDto
{
    public class CommentOnPostCreateDto
    {
        public string? Content { get; set; }
        public int BlogPostId { get; set; }
        public int AuthorId { get; set; }
    }
}
