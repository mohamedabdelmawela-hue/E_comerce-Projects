using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using myStore.DataBase;
using myStore.DTO;
using myStore.Models;
namespace myStore.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class categoryController : ControllerBase
    {
        private readonly E_comerceContext _context;
        public categoryController(E_comerceContext e_ComerceContext) {

            _context = e_ComerceContext;
        }
        [HttpPost("AddCategory")]
        public IActionResult AddCategory(CategoryDTO categoryDTO)
        {
            var res = _context.Category.FirstOrDefault(x => x.categoryName == categoryDTO.CategoryName);
            if (res != null) {
                return BadRequest("res");
            }
            //object from class not from db
            categoryModel ress = new categoryModel
            {
                categoryName = categoryDTO.CategoryName

            };
            _context.Category.Add(ress);
            _context.SaveChanges();

            return Ok("Right");
        }
        [HttpGet("{id}")]
        public IActionResult getCategoryID(int id)
        {
            var result = _context.Category.Find( id);
            if (result == null) {
                return NotFound();
            } 
             return Ok(result);
        }
        [HttpGet]
        public IActionResult GetAllCategory()
        {
            var resultall = _context.Category.ToList();
            if (resultall == null)
            {
                return NotFound();

            }
            return Ok(resultall);


        }
        [HttpPut("{id}")]
        public IActionResult EditCategory(int id,CategoryDTO dTO) { 
        var result = _context.Category.FirstOrDefault(x=>x.categoryId == id);
            if (result == null) 
            { return NotFound(); }

            result.categoryName = dTO.CategoryName;
            
            _context.SaveChanges();
            return Ok(result);

        }
        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(int id) 
        { 
        var result= _context.Category.Find(id);
            if (result == null) { return NotFound(); }
            _context.Category.Remove(result);
            _context.SaveChanges();
            return Ok(result);
        
        }




    }
}
