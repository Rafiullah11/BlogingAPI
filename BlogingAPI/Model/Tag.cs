using System.ComponentModel.DataAnnotations;

namespace BlogingAPI.Model
{
        public class Tag
        {
            [Key]
            public int TagId { get; set; }

            [Required]
            [MaxLength(50)]
            public string Name { get; set; }

            public ICollection<PostTag> PostTags { get; set; }
        }
    

}
