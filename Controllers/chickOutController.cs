using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using myStore.ServiceLayer;

namespace myStore.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CheckoutController : ControllerBase
    {
        private readonly ICheckoutService _checkoutService;

        public CheckoutController(ICheckoutService checkoutService)
        {
            _checkoutService = checkoutService;
        }

        [HttpPost("{userId}")]
        public IActionResult Checkout(int userId)
        {
             
                _checkoutService.Checkout(userId);

              
            return Ok("ok");
        }
    }
}