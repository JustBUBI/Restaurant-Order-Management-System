using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Basket.API.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDistributedCache _redisCache;

        public BasketRepository(IDistributedCache redisCache)
        {
            _redisCache = redisCache ?? throw new ArgumentNullException(nameof(redisCache));
        }

        public async Task<Entities.Basket> GetBasket(string userId)
        {
            var basket = await _redisCache.GetStringAsync(userId);

            if (String.IsNullOrEmpty(basket))
                return null;

            return JsonConvert.DeserializeObject<Entities.Basket>(basket);
        }

        public async Task<Entities.Basket> UpdateBasket(Entities.Basket basket)
        {
            await _redisCache.SetStringAsync(basket.UserId, JsonConvert.SerializeObject(basket));

            return await GetBasket(basket.UserId);
        }

        public async Task DeleteBasket(string userId)
        {
            await _redisCache.RemoveAsync(userId);
        }
    }
}
