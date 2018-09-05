using System.ComponentModel.DataAnnotations;
using TestFullStack.Domain.Base;

namespace TestFullStack.Domain.Entities
{
    public class User : EntityBase
    {

        [Required]
        [StringLength(250)]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        public string Username {get; set;}

        [Required]
        [StringLength(32)] //maxsize HashMD5
        public string Password { get; set; }

        [Required]
        [StringLength(500)]
        public string Email { get; set; }

    }
}
