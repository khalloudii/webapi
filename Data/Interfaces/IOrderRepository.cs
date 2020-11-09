using Data.Entities.OrderAggregate;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interfaces
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        Task<Order> OrderByPaymentIntentIdWithItems(string paymentIntentId);
    }
}
