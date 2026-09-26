using myStore.DTO;
using myStore.Models;
using myStore.Repository;

namespace myStore.ServiceLayer
{
    public interface ICategoryServices
    {
        List<CategoryDTO> GetAllCategories();
        CategoryDTO? GetCategoryID(int id);
        void Add(CategoryDTO categoryDTO);
        void Update(CategoryDTO categoryDTO ,int id);
        void Delete( CategoryDTO categoryDTO,int id);

    }
    public class CategoryServiceLayer:ICategoryServices
    {
         private readonly ICategoryRepository _categoryRepository;
        public CategoryServiceLayer(ICategoryRepository categoryRepository) 
        { 
            _categoryRepository = categoryRepository;
        }
        public List<CategoryDTO > GetAllCategories()
        {
            var getAll=_categoryRepository.GetCategories();
            return getAll.Select(X=>new CategoryDTO
            {
                CategoryName=X.categoryName,
                
            }).ToList();
        }
        public CategoryDTO? GetCategoryID(int id) { 
            var getId=_categoryRepository.GetCategoryID(id);
            if (getId == null) {
                return null;
            }
            return new CategoryDTO
            {
                CategoryName = getId.categoryName
            };
        }
        public void Add(CategoryDTO categoryDTO) 
        {
            categoryModel categoryModel=new categoryModel();
            categoryModel.categoryName=categoryDTO.CategoryName;
             _categoryRepository.AddCategory(categoryModel);

        }
        public void Update(CategoryDTO categoryDTO, int id) {
            var getId = _categoryRepository.GetCategoryID(id);
            if (getId == null)
            {
                throw new Exception("Not Found");
            }
             getId.categoryName= categoryDTO.CategoryName;
            _categoryRepository.UpdateCategory(getId);


        }
        public void Delete( CategoryDTO categoryDTO,int id) {
            var getId = _categoryRepository.GetCategoryID(id);
            if (getId == null)
            {
                throw new Exception("Not Found");
            }
            _categoryRepository.DeleteCategory(getId);
        }

    }
}
