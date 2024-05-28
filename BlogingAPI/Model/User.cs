using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Xml.Linq;

namespace BlogingAPI.Model
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Username { get; set; }

        [MaxLength(255)]
        [Display(Name = "Email address")]
        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required]
        [MaxLength(255)]
        public string PasswordHash { get; set; }

        public int RoleId { get; set; }

        public Role Role { get; set; }

        public ICollection<BlogPost> BlogPosts { get; set; }

        public ICollection<Comment> Comments { get; set; }
    }
}

