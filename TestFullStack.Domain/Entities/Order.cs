using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TestFullStack.Domain.Base;

namespace TestFullStack.Domain.Entities
{
    public class Order : EntityBase
    {

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [Required]
        public User User { get; set; }
                
        public virtual IList<ItemOrder> Items { get; set; }

    }
}
