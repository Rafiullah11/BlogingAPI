namespace BlogingAPI.DTO.BlogPostDtos
{
     public class BlogPostCreateDto
    {
        public string? BlogTitle { get; set; }
        public string? BlogContent { get; set; }
        public int AuthorId { get; set; }
    }
}
