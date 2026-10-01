using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using myStore.ServiceLayer;

namespace myStore.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CartItemController : ControllerBase
    {
        private readonly ICartItemService _cartItemService;

        public CartItemController(ICartItemService cartItemService)
        {
            _cartItemService = cartItemService;
        }

        // GET: api/CartItem/cart/1
        [HttpGet("cart/{cartId}")]
        public IActionResult GetCartItems(int cartId)
        {
            var items = _cartItemService.GetCartItems(cartId);

            return Ok(items);
        }

        // POST: api/CartItem
        [HttpPost]
        public IActionResult AddToCart(
            int cartId,
            int productId,
            int quantity)
        {
            _cartItemService.AddToCart(
                cartId,
                productId,
                quantity);

            return Ok("Item Added To Cart");
        }

        // PUT: api/CartItem/5
        [HttpPut("{cartItemId}")]
        public IActionResult UpdateQuantity(
            int cartItemId,
            int quantity)
        {
            try
            {
                _cartItemService.UpdateQuantity(
                    cartItemId,
                    quantity);

                return Ok("Quantity Updated");
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        // DELETE: api/CartItem/5
        [HttpDelete("{cartItemId}")]
        public IActionResult RemoveFromCart(int cartItemId)
        {
            try
            {
                _cartItemService.RemoveFromCart(cartItemId);

                return Ok("Item Removed From Cart");
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}