using Data.Entities;
using Data.Interfaces;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class BasketRepository : GenericRepository<BasketItem>, IBasketRepository
    {
        private readonly StoreContext _context;
        public BasketRepository(StoreContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> DeleteBasketAsync(string basketId)
        {
            var basket = await GetBasketAsync(basketId);
            if(basket != null)
            {
                _context.CustomerBaskets.Remove(basket);
            }

            return true;
        }

        public async Task<CustomerBasket> GetBasketAsync(string basketId)
        {
            return await _context.CustomerBaskets.FindAsync(basketId);
        }

        public async Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket)
        {
            return null;
            //// check if not exist >> create new:
            //var basket = await GetBasketAsync(basket)
            //var created = await _database.StringSetAsync(basket.Id,
            //    JsonSerializer.Serialize(basket), TimeSpan.FromDays(30));

            //if (!created) return null;

            //return await GetBasketAsync(basket.Id);
        }
        ////private readonly IDatabase _database;
        //public BasketRepository(IConnectionMultiplexer redis)
        //{
        //    _database = redis.GetDatabase();
        //}

        //public async Task<bool> DeleteBasketAsync(string basketId)
        //{
        //    return await _database.KeyDeleteAsync(basketId);
        //}

        //public async Task<CustomerBasket> GetBasketAsync(string basketId)
        //{
        //    var data = await _database.StringGetAsync(basketId);

        //    return data.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomerBasket>(data);
        //}

        //// create update
        //public async Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket)
        //{
        //    var created = await _database.StringSetAsync(basket.Id, 
        //        JsonSerializer.Serialize(basket), TimeSpan.FromDays(30));

        //    if (!created) return null;

        //    return await GetBasketAsync(basket.Id);


        //}
    }
}
