using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace RestaurantApp.Core
{
    public class User
    {
        public User()
        {

        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(250)]
        public string Name { get; set; }
        public string Password { get; set; }
        public int Permission { get; set; }
        public int Age { get; set; }
        public int Gender { get; set; }
        public string image { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
    public enum UserRole
    {
        admin,
        normal_user
    }
    public enum Gender
    {
        male,
        Female,
        Other
    }
}
