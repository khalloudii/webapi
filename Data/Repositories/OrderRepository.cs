using Data.Entities.OrderAggregate;
using Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        private readonly StoreContext _context;
        public OrderRepository(StoreContext context) : base(context)
        {
            _context = context;
        }
        //public Task<Order> OrderByPaymentIntentIdWithItems(string paymentIntentId)
        //{
        //    return await _context.Orders.whe
        //}
    }
}
