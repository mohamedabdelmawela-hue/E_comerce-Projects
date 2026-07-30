using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using myStore.DataBase;
//using myStore.DataBase;
using myStore.DTO;
using myStore.Models;
namespace myStore.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]

    public class productController : ControllerBase
    {
        private readonly E_comerceContext _context;
        public productController(E_comerceContext context)
        {
            _context = context;
        }
        [HttpPost]
        public IActionResult AddProduct(ProductDTO productDTO)
        {
            var result = _context.Products.FirstOrDefault(x => x.productName == productDTO.productName);
            if (result != null)
            {
                return NotFound("Product is exist");
            }
            var categ = _context.Category.Find(productDTO.categoryId);
            if (categ == null)
            {
                return NotFound("category Not found");
            }
            productModel productModel = new productModel
            {
                productName = productDTO.productName,
                productDescription = productDTO.productDescription,
                productprice = productDTO.productprice,
                productQuantity = productDTO.productQuantity,
                categoryId = productDTO.categoryId,
            };
            _context.Products.Add(productModel);
            _context.SaveChanges();
            return Ok();
        }
        [HttpGet]
        public IActionResult GetAllProduct()
        {
            var result = _context.Products.ToList();
            if (result.Count > 0)
            {
                return Ok(result);
            }
            return NotFound("NotFound");
        }
        [HttpGet("{productId}")]
        public IActionResult GetProduct(int productId)
        {
            var resultProductID = _context.Products.Find(productId);
            if (resultProductID == null)
            {
                return NotFound("Not Found");
            }
            return Ok(resultProductID);
        }
        [HttpPut("{productId}")]
        public IActionResult EditProduct(int productId, ProductDTO productDTO)
        {
            var resultEdite = _context.Products.Find(productId);
            if (resultEdite == null)
            {
                return NotFound("Not Found");

            }

            resultEdite.productName = productDTO.productName;
            resultEdite.productDescription = productDTO.productDescription;
            resultEdite.productprice = productDTO.productprice;
            resultEdite.productQuantity = productDTO.productQuantity;
            resultEdite.categoryId = productDTO.categoryId;

            _context.SaveChanges();
            return Ok(resultEdite);
            // productName= productDTO.productName;
        }
        [HttpDelete("{productId}")]
        public IActionResult DeleteProduct(int productId)
        {
            var resultDelete = _context.Products.Find(productId);
            if (resultDelete == null)
            {
                return NotFound("NotFound");
            }
            _context.Products.Remove(resultDelete);
            _context.SaveChanges();
            return Ok(resultDelete);
        }

        [HttpGet("search")]
        public IActionResult SearchProduct(string name)
        {
            var result = _context.Products.Where(x => x.productName.Contains(name)).ToList();
            if (result.Count() > 0)
            {
                return Ok(result);
            }
            return NotFound();

        }
        [HttpGet("filter")]
        public IActionResult Filter(string? name, int? categoryId)
        {
            //هات كل المنتجات بس استني  متنفذش حاجه
            var query = _context.Products.AsQueryable();
            //لو بحث بالاسمرنقذ ده 
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(x => x.productName.Contains(name));
            }
            //g
            if (categoryId.HasValue)
            {
                query = query.Where(x => x.categoryId == categoryId);
            }
            return Ok(query);
        }

    }
}
