using BlogApp.Models;

namespace BlogingAPI.Model
{
    public class Author
    {
        // One author can post many blogs
        public int Id { get; set; }
        public string? Name { get; set; }
        public List<BlogPost> BlogPosts { get; } = new List<BlogPost>();

    }

}
