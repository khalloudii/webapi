using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Entities.OrderAggregate
{
    public class ProductItemOrdered
    {
        public ProductItemOrdered() { }
        public ProductItemOrdered(int productItemId, string priductName, string pictureUrl)
        {
            ProductItemId = productItemId;
            PriductName = priductName;
            PictureUrl = pictureUrl;
        }

        public int ProductItemId { get; set; }
        public string PriductName { get; set; }
        public string PictureUrl { get; set; }
    }
}
