using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TestFullStack.Domain.Base;

namespace TestFullStack.Domain.Entities
{
    public class Product : EntityBase
    {

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(250)]
        public string Description {get; set;}

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

    }
}
