using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.ViewModels
{
    public class CustomerBasketViewModel
    {
        [Required]
        public string Id { get; set; }

        public List<BasketItemViewModel> Items { get; set; }

        public int? DeliveryMethodId { get; set; }
        public string ClientSecret { get; set; }

        // to be able to update payment intent if the client make chenges to delivery for ex, after make order
        public string PaymentIntentId { get; set; }
        public decimal ShippingPrice { get; set; }
    }
}
