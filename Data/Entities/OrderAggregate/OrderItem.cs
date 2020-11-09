using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Entities.OrderAggregate
{
    public class OrderItem
    {
        public OrderItem() { }
        public OrderItem(ProductItemOrdered itemOrderd, decimal price, int quantity)
        {
            ItemOrderd = itemOrderd;
            Price = price;
            Quantity = quantity;
        }

        public int Id { get; set; }
        public ProductItemOrdered ItemOrderd { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
