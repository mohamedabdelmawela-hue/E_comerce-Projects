using myStore.DataBase;
using myStore.Models;

namespace myStore.Repository
{public interface IproductRepository
    {
        List<productModel> GetAll();
        productModel? GetByID(int id);
        void Add(productModel model);
        void Update(productModel model);
        void Delete(productModel model);

    }
    public class productRepository:IproductRepository
    {private readonly E_comerceContext _context;
        public productRepository(E_comerceContext context)
        {
            _context = context;
        }

        public List<productModel> GetAll() { 
        return _context.Products.ToList();
        }
        public productModel? GetByID(int id)
        {
            return _context.Products.Find(id);
        }
        public void Add(productModel model) { 
        _context.Products.Add(model);
            _context.SaveChanges();
        }
        public void Update(productModel model) {
            _context.Products.Update(model);
            _context.SaveChanges();
        }
        public void Delete(productModel model)
        {
            _context.Products.Remove(model);
            _context.SaveChanges();
        }
    }
}
