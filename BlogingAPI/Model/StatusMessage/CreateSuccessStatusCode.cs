using BlogApp.Models;

namespace BlogingAPI.Model.StatusMessage
{
    public class CreateSuccessStatusCode
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public List<BlogPost>? BlogPost { get; set; }
    }
   
}
