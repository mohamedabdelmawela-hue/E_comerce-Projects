using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using myStore.DTO;
using myStore.ServiceLayer;

namespace myStore.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryServices _categoryServices;

        public CategoryController(ICategoryServices categoryServices)
        {
            _categoryServices = categoryServices;
        }

        // GET: api/Category
        [HttpGet]
        public IActionResult GetAll()
        {
            var categories = _categoryServices.GetAllCategories();
            return Ok(categories);
        }

        // GET: api/Category/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var category = _categoryServices.GetCategoryID(id);

            if (category == null)
            {
                return NotFound("Category Not Found");
            }

            return Ok(category);
        }

        // POST: api/Category
        [HttpPost]
        public IActionResult Add(CategoryDTO categoryDTO)
        {
            _categoryServices.Add(categoryDTO);

            return Ok("Category Added Successfully");
        }

        // PUT: api/Category/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, CategoryDTO categoryDTO)
        {
             
                _categoryServices.Update(categoryDTO, id);

                return Ok("Category Updated Successfully");
             
        }

        // DELETE: api/Category/5
        [HttpDelete("{id}")]
        public IActionResult Delete( CategoryDTO categoryDTO,int id)
        {
             
                _categoryServices.Delete( categoryDTO,id);

                return Ok("Category Deleted Successfully");
             
        }
    }
}