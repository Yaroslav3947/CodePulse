using CodePulse.API.Models.Domain;
using CodePulse.API.Models.DTO;
using CodePulse.API.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodePulse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductLikeController : ControllerBase
    {
        private readonly IProductLikeRepository _productLikeRepository;

        public ProductLikeController(IProductLikeRepository productLikeRepository)
        {
            this._productLikeRepository = productLikeRepository;
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> AddLike([FromBody] ProductLikeDto productLikeDto)
        {

            var response = new ProductLike {
                ProductId = productLikeDto.ProductId,
                UserId = productLikeDto.UserId
            };

            await _productLikeRepository.AddLikeForProduct(response);

            return Ok(response);
        }

        [HttpPost]
        [Route("Remove")]
        public async Task<IActionResult> RemoveLike([FromBody] ProductLikeDto productLikeDto)
        {

            var response = new ProductLike
            {
                ProductId = productLikeDto.ProductId,
                UserId = productLikeDto.UserId
            };

            await _productLikeRepository.RemoveLikeForProduct(response);

            return Ok(response);
        }
    }
}
