using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.API.Repositories
{
    public interface IBasketRepository
    {
        Task<Entities.Basket> GetBasket(string userId);
        Task<Entities.Basket> UpdateBasket(Entities.Basket basket);
        Task DeleteBasket(string userId);
    }
}
