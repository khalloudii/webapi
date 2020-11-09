using System;
using System.Collections.Generic;

namespace Data.Entities
{
    // here we will not relate customer basket with the user
    // anonemous users can use basket
    // But when make order here we should relate the order
    // if user has basket already, but he did not logged in and use basket anonemously
    // then he logged in >> so as i told he already has basket, and there is another basket
    public class CustomerBasket
    {
        public CustomerBasket() { }

        public CustomerBasket(string id)
        {
            Id = id;
        }

        public string Id { get; set; }

        public List<BasketItem> Items { get; set; } = new List<BasketItem>();
        public int? DeliveryMethodId { get; set; }
        public string ClientSecret { get; set; }

        // to be able to update payment intent if the client make chenges to delivery for ex, after make order
        public string PaymentIntentId { get; set; }
        public decimal ShippingPrice { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
