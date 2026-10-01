using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using myStore.ServiceLayer;

namespace myStore.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        // GET: api/Order
        [HttpGet]
        public IActionResult GetAll()
        {
            var orders = _orderService.GetAll();

            return Ok(orders);
        }

        // GET: api/Order/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var order = _orderService.GetById(id);

            if (order == null)
            {
                return NotFound("Order Not Found");
            }

            return Ok(order);
        }

        // DELETE: api/Order/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _orderService.Delete(id);

                return Ok("Order Deleted Successfully");
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}