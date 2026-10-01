using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using myStore.DataBase;
//using myStore.DataBase;
using myStore.DTO;
using myStore.Models;
using myStore.ServiceLayer;
namespace myStore.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public  class productController : ControllerBase
    {
        private readonly IproductServices _iproductServices;
        public productController(IproductServices iproductServices)
        {
            _iproductServices = iproductServices;
        }
        [HttpGet]
        public IActionResult GetAllProduct()
        {
           var result1= _iproductServices.GetAllProducts();
            return Ok(result1);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var result2 = _iproductServices.GetProductById(id);
            if (result2==null)
            {
                return NotFound("Not Found");
            }
            return Ok(result2);
        }
        [HttpPost]
        public IActionResult ADD(ProductDTO model) { 
        _iproductServices.Add(model);
            return Ok("ADD Sucessufully");
        }
        [HttpPut("{id}")]
        public IActionResult Update(ProductDTO productDTO,int id) {
            var result2 = _iproductServices.GetProductById(id);
            if (result2 == null)
            {
                return NotFound("Not Found");
            }
            _iproductServices.Update(productDTO, id);
            return Ok( "product uodated");

        }
        [HttpDelete("{id}")]
        public IActionResult DeleteById(ProductDTO productDTO,int id) {
            var result2 = _iproductServices.GetProductById(id);
            if (result2 == null)
            {
                return NotFound("Not Found");
            }
            _iproductServices.Delete(productDTO, id);
            return Ok( );
        }

    }
 
    }

