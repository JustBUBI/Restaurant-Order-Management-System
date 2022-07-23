using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Basket.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Basket.API.Controllers
{
    [ApiController]
    [Route("api/basket")]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _repository;

        public BasketController(IBasketRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [HttpGet("{userId}", Name = "GetBasket")]
        [ProducesResponseType(typeof(Entities.Basket), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Entities.Basket>> GetBasket(string userId)
        {
            var basket = await _repository.GetBasket(userId);
            return Ok(basket ?? new Entities.Basket(userId));
        }

        [HttpPost]
        [ProducesResponseType(typeof(Entities.Basket), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Entities.Basket>> UpdateBasket([FromBody] Entities.Basket basket)
        {
            return Ok(await _repository.UpdateBasket(basket));
        }

        [HttpDelete("{userId}", Name = "DeleteBasket")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteBasket(string userId)
        {
            await _repository.DeleteBasket(userId);
            return Ok();
        }
    }
}
