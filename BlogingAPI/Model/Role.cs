using System.ComponentModel.DataAnnotations;

namespace BlogingAPI.Model
{
        public class Role
        {
            [Key]
            public int RoleId { get; set; }

            [Required]
            [MaxLength(50)]
            public string RoleName { get; set; }

            public ICollection<User> Users { get; set; }
        }
    

}
