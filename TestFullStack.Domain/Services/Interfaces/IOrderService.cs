using System.Collections.Generic;
using TestFullStack.Domain.DTOs;
using TestFullStack.Domain.Entities;


namespace TestFullStack.Domain.Services.Interfaces
{
    public interface IOrderService
    {

        void Save(OrderRequest orderRequest);
        List<Order> Get(FilterOrderRequest filterOrderRequest, Token token);
        Order Get(long id);
    }
}
