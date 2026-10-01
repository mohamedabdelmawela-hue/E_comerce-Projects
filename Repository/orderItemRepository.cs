using myStore.DataBase;
using myStore.Models;

namespace myStore.Repository
{
    public interface IOrderItemRepository
    {
        List<orderItemModel> GetAll();

        orderItemModel? GetById(int id);

        void Add(orderItemModel orderItem);

        void Update(orderItemModel orderItem);

        void Delete(orderItemModel orderItem);
    }
    public class orderItemRepository : IOrderItemRepository
    {
        
            private readonly E_comerceContext _context;

            public orderItemRepository(E_comerceContext context)
            {
                _context = context;
            }

            public List<orderItemModel> GetAll()
            {
                return _context.OrderItems.ToList();
            }

            public orderItemModel? GetById(int id)
            {
                return _context.OrderItems
                    .FirstOrDefault(x => x.orderItemModelId == id);
            }

            public void Add(orderItemModel orderItem)
            {
                _context.OrderItems.Add(orderItem);
                _context.SaveChanges();
            }

            public void Update(orderItemModel orderItem)
            {
                _context.OrderItems.Update(orderItem);
                _context.SaveChanges();
            }

            public void Delete(orderItemModel orderItem)
            {
                _context.OrderItems.Remove(orderItem);
                _context.SaveChanges();
            }
        
    }
}
