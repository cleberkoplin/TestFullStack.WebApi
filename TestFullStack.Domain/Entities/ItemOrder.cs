﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TestFullStack.Domain.Base;

namespace TestFullStack.Domain.Entities
{
    public class ItemOrder : EntityBase
    {

        [Required]
        public Order Order { get; set; }

        public virtual Product Product { get; set; }
        [Required]
        public long ProductId { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [Required]
        public int Quantity { get; set; }

    }
}
