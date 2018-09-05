using System.Collections.Generic;

namespace TestFullStack.Domain.DTOs
{
    public class OrderRequest
    {
        public long IdUser { get; set; }
        public List<long> IdsProducts { get; set; }
    }
}
