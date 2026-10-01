using myStore.DataBase;
using myStore.Models;

namespace myStore.Repository
{
    public interface IOrderRepository
    {
        List<orderModel> GetAll();

        orderModel? GetById(int id);

        void Add(orderModel order);

        void Update(orderModel order);

        void Delete(orderModel order);
    }
    public class orderRepository : IOrderRepository
    {
         
            private readonly E_comerceContext _context;

            public orderRepository(E_comerceContext context)
            {
                _context = context;
            }

            public List<orderModel> GetAll()
            {
                return _context.Orders.ToList();
            }

            public orderModel? GetById(int id)
            {
                return _context.Orders
                    .FirstOrDefault(x => x.orderModelId == id);
            }

            public void Add(orderModel order)
            {
                _context.Orders.Add(order);
                _context.SaveChanges();
            }

            public void Update(orderModel order)
            {
                _context.Orders.Update(order);
                _context.SaveChanges();
            }

            public void Delete(orderModel order)
            {
                _context.Orders.Remove(order);
                _context.SaveChanges();
            }
        }
    
}
