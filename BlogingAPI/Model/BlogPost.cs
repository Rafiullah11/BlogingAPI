namespace BlogingAPI.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Xml.Linq;

        public class BlogPost
        {
            [Key]
            public int PostId { get; set; }

            [Required]
            [MaxLength(255)]
            public string Title { get; set; }

            [Required]
            public string Content { get; set; }

            [Required]
            public DateTime CreatedAt { get; set; }

            public DateTime? UpdatedAt { get; set; }

            public int UserId { get; set; }

            public User User { get; set; }

            public int CategoryId { get; set; }

            public Category Category { get; set; }

            public ICollection<Comment> Comments { get; set; }

            public ICollection<PostTag> PostTags { get; set; }
        }
    

}
