using Data.Entities;
using Data.Entities.OrderAggregate;
using Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Services
{
    public class OrderService : IOrderService
    {
        //private readonly IGenericRepository<Order> _orderRepo;
        //private readonly IGenericRepository<DeliveryMethod> _dmRepo;
        //private readonly IGenericRepository<Product> _productRepo;
        private readonly IBasketRepository _basketRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPaymentService _paymentService;

        public OrderService(IBasketRepository basketRepo, IUnitOfWork unitOfWork,
            IPaymentService paymentService)
        {
            this._basketRepo = basketRepo;
            this._unitOfWork = unitOfWork;
            this._paymentService = paymentService;
        }

        //public OrderService(IGenericRepository<Order> orderRepo, IGenericRepository<DeliveryMethod> dmRepo,
        //    IGenericRepository<Product> productRepo, IBasketRepository basketRepo)
        //{
        //    this._orderRepo = orderRepo;
        //    this._dmRepo = dmRepo;
        //    this._productRepo = productRepo;
        //    this._basketRepo = basketRepo;
        //}

        public async Task<Order> CreateOrderAsync(string buyerEmail, int deliveryMethodId, string basketId, Address shippingAddress)
        {
            // get basket
            var basket = await _basketRepo.GetBasketAsync(basketId);

            // get items from product repo
            var items = new List<OrderItem>();
            foreach(var item in basket.Items)
            {
                //var productItem = await _productRepo.GetByIdAsync(item.Id);
                var productItem = await _unitOfWork.Repository<Product>().GetByIdAsync(item.Id);
                var itemOrdered = new ProductItemOrdered(productItem.Id, productItem.NameAR,
                    productItem.PictureUrl);
                var orderItem = new OrderItem(itemOrdered, productItem.Price, item.Quantity);
                items.Add(orderItem);
            }

            // get delivery method repo
            var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(deliveryMethodId);

            // get subtotal
            var subtotal = items.Sum(i => i.Price * i.Quantity);

            // check to see if order exist:
            var existingOrder = await _unitOfWork.Repository<Order>().GetWhereInclude(o => o.PaymentIntentId == basket.PaymentIntentId, "OrderItems");

            if(existingOrder != null)
            {
                _unitOfWork.Repository<Order>().Delete(existingOrder);
                await _paymentService.CreateOrUpdatePaymentIntent(basket.PaymentIntentId);
            }

            // create order
            var order = new Order(items, buyerEmail, shippingAddress, deliveryMethod, subtotal, basket.PaymentIntentId);

            _unitOfWork.Repository<Order>().Add(order);

            // TODO: save to db
            var result = await _unitOfWork.Complete();

            if (result <= 0) return null;

            //do not delete basket here because maybe the payment faild
            //// delete basket
            //await _basketRepo.DeleteBasketAsync(basketId);

            // return order
            return order;
        }

        public async Task<List<DeliveryMethod>> GetDeliveryMethodsAsync()
        {
            return await _unitOfWork.Repository<DeliveryMethod>().ListAllAsync();

        }

        public async Task<Order> GetOrderByIdAsync(int id, string buyerEmail)
        {
            //return await _unitOfWork.Repository<Order>().Get(o => o.Id == id && o.BuyerEmail == buyerEmail);
            return await _unitOfWork.Repository<Order>()
                .GetWhereInclude(o => o.Id == id && o.BuyerEmail == buyerEmail, "DeliveryMethod,OrderItems");
        }

        public async Task<List<Order>> GetOrdersForUserAsync(string buyerEmail)
        {
            return await _unitOfWork.Repository<Order>()
                .GetListWhereInclude(o => o.BuyerEmail == buyerEmail, "DeliveryMethod,OrderItems");
        }
    }
}
