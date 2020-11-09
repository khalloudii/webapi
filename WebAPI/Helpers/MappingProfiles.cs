using WebAPI.ViewModels;
using AutoMapper;
using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Entities.Identity;
using Data.Entities.OrderAggregate;

namespace WebAPI.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductToReturnVM>()
                .ForMember(d => d.Name, o => o.MapFrom(s => s.NameAR))
                .ForMember(d => d.ProductBrand, o => o.MapFrom(s => s.ProductBrand.Name))
                .ForMember(d => d.ProductType, o => o.MapFrom(s => s.ProductType.Name))
                .ForMember(d => d.PictureUrl, o => o.MapFrom<ProductUrlResolver>());
            CreateMap<Data.Entities.Identity.Address, AddressViewModel>().ReverseMap();
            CreateMap<CustomerBasketViewModel, CustomerBasket>();
            CreateMap<BasketItemViewModel, BasketItem>();
            CreateMap<AddressViewModel, Data.Entities.OrderAggregate.Address>();
            CreateMap<Order, OrderToReturnViewModel>()
                .ForMember(d => d.DeliveryMethod, o => o.MapFrom(s => s.DeliveryMethod.ShortName))
                .ForMember(d => d.ShippingPrice, o => o.MapFrom(s => s.DeliveryMethod.Price));
            CreateMap<OrderItem, OrderItemViewModel>()
                .ForMember(d => d.ProductId, o => o.MapFrom(s => s.ItemOrderd.ProductItemId))
                .ForMember(d => d.ProductName, o => o.MapFrom(s => s.ItemOrderd.PriductName))
                .ForMember(d => d.PictureUrl, o => o.MapFrom(s => s.ItemOrderd.PictureUrl))
                .ForMember(d => d.PictureUrl, o => o.MapFrom<OrderItemUrlResolver>());
        }
    }
}
