using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.ViewModels
{
    public class BasketItemViewModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        [Range(0.1, double.MaxValue, ErrorMessage = "price must greater than 0")]
        public decimal Price { get; set; }
        [Required]
        [Range(0.1, double.MaxValue, ErrorMessage = "qty at least 1")]
        public int Quantity { get; set; }
        public string PicturUrl { get; set; }
        public string Brand { get; set; }
        public string Type { get; set; }
    }
}
