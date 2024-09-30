using CodePulse.API.Models.Domain;
using CodePulse.API.Models.DTO;
using CodePulse.API.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodePulse.API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase {
        private readonly IBasketRepository _basketRepository;

        public BasketController(IBasketRepository basketRepository) {
            this._basketRepository = basketRepository;
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> AddToBasket([FromBody] AddToBasketDto productDto) {

            var response = new ProductLike {
                ProductId = productDto.ProductId,
                UserId = productDto.UserId
            };

            await _basketRepository.AddProductToBasket(response);

            return Ok(response);
        }
        [HttpPost]
        [Route("Remove")]
        public async Task<IActionResult> RemoveFromBasket([FromBody] RemoveFromBasketDto removeFromBasketDto) {

            var response = new ProductLike {
                ProductId = removeFromBasketDto.CosmeticId,
                UserId = removeFromBasketDto.UserId
            };

            await _basketRepository.RemoveProductFromBasket(response);

            return Ok(response);
        }
    }
}
