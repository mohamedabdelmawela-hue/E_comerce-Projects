using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using myStore.DTO;
using myStore.ServiceLayer;

namespace myStore.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        // GET: api/Cart/1
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var cart = _cartService.GetById(id);

            if (cart == null)
            {
                return NotFound("Cart Not Found");
            }

            return Ok(cart);
        }

        // POST: api/Cart
        [HttpPost]
        public IActionResult Add(CartDTO cartDTO)
        {
            _cartService.Add(cartDTO);

            return Ok("Cart Created Successfully");
        }

        // DELETE: api/Cart/1
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _cartService.Delete(id);

                return Ok("Cart Deleted Successfully");
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}