using myStore.DTO;
using myStore.Repository;

namespace myStore.ServiceLayer
{
    public interface IOrderService
    {
        List<OrderDTO> GetAll();

       OrderDTO? GetById(int id);

        void Delete(int id);
    }
    public class OrderServiceLayer:IOrderService
    {
        private readonly IOrderRepository orderRepository;
        public OrderServiceLayer(IOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }
        public List<OrderDTO> GetAll()
        {
            var orders = orderRepository.GetAll();

            return orders.Select(x => new OrderDTO
            {
               // orderId = x.OrderId,
                totalPrice = x.totalPrice,
                orderDate = x.orderDate
            }).ToList();
        }
        public OrderDTO? GetById(int id) {
            var order = orderRepository.GetById(id);
            if (order==null)
            {
                return null;
            }
            return new OrderDTO
            {
                totalPrice = order.totalPrice,
                orderDate = order.orderDate
            };
        }
        public void Delete(int id)
        {
            
            var order = orderRepository.GetById(id);
            if (order == null)
            {
                throw new Exception("Not found ");
            }
            orderRepository.Delete(order);
        }


        }
}
