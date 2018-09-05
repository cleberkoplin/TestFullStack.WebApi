using System;

namespace TestFullStack.Domain.DTOs
{
    public class FilterOrderRequest
    {
        public decimal StartPrice { get; set; }
        public decimal EndPrice { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        
    }
}
