using myStore.DataBase;
using myStore.DTO;
using myStore.Models;

namespace myStore.Repository
{
    public interface ICategoryRepository
    {
        List<categoryModel> GetCategories();
        categoryModel? GetCategoryID(int id);
        void AddCategory(categoryModel category);
        void UpdateCategory(categoryModel category);
        void DeleteCategory(categoryModel categoryModel);
     }
    public class CategoryRepository:ICategoryRepository
    {
        private readonly E_comerceContext _comerceContext;
        public CategoryRepository(E_comerceContext comerceContext)
        {
            _comerceContext = comerceContext;
        }
        public List<categoryModel> GetCategories()
        {
          var getAll=  _comerceContext.Category.ToList();
            return getAll;
        }
        public categoryModel? GetCategoryID(int id)
        {
            var getById=_comerceContext.Category.FirstOrDefault(x=>x.categoryId==id);
            return getById;
        }
        public void AddCategory(categoryModel category) { 
            _comerceContext.Category.Add(category);
            _comerceContext.SaveChanges();
        }
        public void UpdateCategory(categoryModel category)
        {
            _comerceContext.Category.Update(category);
            _comerceContext.SaveChanges();
        }
        public void DeleteCategory(categoryModel categoryModel) {
            _comerceContext.Category.Remove(categoryModel);
            _comerceContext.SaveChanges();
        }

    }
}
