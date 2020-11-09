using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.ViewModels
{
    public class OrderViewModel
    {
        public string BasketId { get; set; }
        public int DeliveryMethodId { get; set; }
        public AddressViewModel ShipToAddress { get; set; }
    }
}
