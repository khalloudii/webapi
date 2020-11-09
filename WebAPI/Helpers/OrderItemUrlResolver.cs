using AutoMapper;
using Data.Entities.OrderAggregate;
using WebAPI.ViewModels;
using Data.Entities;
using Microsoft.Extensions.Configuration;

namespace WebAPI.Helpers
{
    public class OrderItemUrlResolver : IValueResolver<OrderItem, OrderItemViewModel, string>
    {
        private readonly IConfiguration _configuration;

        public OrderItemUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string Resolve(OrderItem source, OrderItemViewModel destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.ItemOrderd.PictureUrl))
            {
                return _configuration["ApiUrl"] + source.ItemOrderd.PictureUrl;
            }

            return null;
        }
    }
}
