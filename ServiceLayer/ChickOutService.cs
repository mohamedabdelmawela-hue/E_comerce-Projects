using myStore.DataBase;
using myStore.Middleware;
using myStore.Models;
using myStore.Repository;

namespace myStore.ServiceLayer
{
    public interface ICheckoutService
    {
        void Checkout(int userId);
    }
    public class ChickOutService:ICheckoutService
    {
        private readonly ICartRepository _cartRepository;
        private readonly ICartItemRepository _cartItemRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IproductRepository _productRepository;
        private readonly E_comerceContext _context;

        public ChickOutService(
            ICartRepository cartRepository,
            ICartItemRepository cartItemRepository,
            IOrderRepository orderRepository,
            IOrderItemRepository orderItemRepository,
            IproductRepository productRepository,
            E_comerceContext e_ComerceContext)
        {
            _cartRepository = cartRepository;
            _cartItemRepository = cartItemRepository;
            _orderRepository = orderRepository;
            _orderItemRepository = orderItemRepository;
            _productRepository = productRepository;
            _context = e_ComerceContext;
        }
        public void Checkout(int userId)
        {//استخدام الترانساكشن 
            //ي كل العمليات تتنفذ
            //ي كل العمليات تفشل
            using var transaction = _context.Database.BeginTransaction();
            try { 
            //1. هات Cart 
            var cart = _cartRepository.GetCartByUserId(userId);

            if (cart == null)
            {
                throw new NotFoundException("Cart Not Found");
            }
            //2. هات المنتجات الموجودة في السلة
            var cartItems =_cartItemRepository.GetCartItems(cart.cartModelId);

            if (!cartItems.Any())
            {
                throw new NotFoundException("Cart Is Empty");
            }
            //3. احسب الإجمالي
            double totalPrice = 0;

            foreach (var item in cartItems)
            {
                totalPrice += item.perice * item.quantity;
            }
            //نتحقق من المخزون
            foreach (var item in cartItems)
            {
                var product = _productRepository.GetByID(item.productId);

                if (product == null)
                {
                    throw new NotFoundException("Product Not Found");
                }

                if (product.productQuantity < item.quantity)
                {
                    throw new Exception(
                         "not  quantity found");
                }
            }
            //4. أنشئ Order
            orderModel order = new orderModel();

            order.userId = userId;
            order.orderDate = DateTime.Now;
            order.totalPrice = totalPrice;

            _orderRepository.Add(order);

            //بعد SaveChanges()
            //هيبقى عندك: ال في دالة الاد
            //order.OrderId متولد تلفائي
            //5. أنشئ OrderItems
            foreach (var item in cartItems)
            {
                orderItemModel orderItem =
                    new orderItemModel();

                orderItem.orderId = order.orderModelId;

                orderItem.productId = item.productId;

                orderItem.Quantity = item.quantity;

                orderItem.perice = item.perice;

                _orderItemRepository.Add(orderItem);
            }
            //خصم الكميه
            foreach (var item in cartItems)
            {
                var product = _productRepository.GetByID(item.productId);

                if (product == null)
                {
                    throw new NotFoundException("Product Not Found");
                }

                product.productQuantity -= item.quantity;

                _productRepository.Update(product);
            }
            //6. فضي السلة
            foreach (var item in cartItems)
            {
                _cartItemRepository.Delete(item);
            }
                transaction.Commit();
            }
            catch
            {
                // إلغاء كل العمليات
                transaction.Rollback();

                throw;
            }
        }

    }
}
