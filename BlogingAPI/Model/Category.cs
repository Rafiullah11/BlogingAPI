
using System.ComponentModel.DataAnnotations;

namespace BlogingAPI.Model
{

        public class Category
        {
            [Key]
            public int CategoryId { get; set; }

            [Required]
            [MaxLength(100)]
            public string Name { get; set; }

            public ICollection<BlogPost> BlogPosts { get; set; }
        }
    

}
