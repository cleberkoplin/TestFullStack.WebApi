using System;
using System.Collections.Generic;
using System.Text;

namespace TestFullStack.Domain.DTOs
{
    public class ItemOrderDetails
    {
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
