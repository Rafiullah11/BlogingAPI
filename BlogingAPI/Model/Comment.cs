using System.ComponentModel.DataAnnotations;

namespace BlogingAPI.Model
{
        public class Comment
        {
            [Key]
            public int CommentId { get; set; }

            [Required]
            public string Content { get; set; }

            [Required]
            public DateTime CreatedAt { get; set; }

            public int UserId { get; set; }

            public User User { get; set; }

            public int PostId { get; set; }

            public BlogPost BlogPost { get; set; }
        }
    

}
