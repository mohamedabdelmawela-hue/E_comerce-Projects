using myStore.DTO;
using myStore.Middleware;
using myStore.Models;
using myStore.Repository;

namespace myStore.ServiceLayer
{
    public interface ICartItemService
    {
        void AddToCart(int cartId, int productId, int quantity);

        void RemoveFromCart(int cartItemId);

        void UpdateQuantity(int cartItemId, int quantity);

        List<CartItemDTO> GetCartItems(int cartId);
    }
    public class cartItemServices:ICartItemService
    {
        private readonly IproductRepository _productRepository;
        private readonly ICartItemRepository _cartItemRepository;
        public cartItemServices(ICartItemRepository cartItemRepository,IproductRepository iproductRepository) { 
        _cartItemRepository = cartItemRepository;
            _productRepository = iproductRepository;
        }
        public void AddToCart(int cartId, int productId, int quantity)
        { //هدور علي النتج الاول في جدول المنتجات لو  مش موجود هقوله مش موجود 
            //لو موجود  دور في الكارت ايتم هل هو موجود ولا لا 
            // لو موجود زود واحد
            //لو مش موجود ضيفه
            var product = _productRepository.GetByID(productId);

            if (product == null)
            {
                throw new NotFoundException("Product Not Found");
            }
            var existingItem = _cartItemRepository
       .GetCartItems(cartId)
       .FirstOrDefault(x => x.productId == productId);

            // 3. لو المنتج موجود
            if (existingItem != null)
            {
                existingItem.quantity += quantity;

                _cartItemRepository.Update(existingItem);
            }
            else
            {
                cartItemModel item = new cartItemModel();

                item.cartid = cartId;
                item.productId = productId;
                item.quantity = quantity;
                item.perice = product.productprice;

                _cartItemRepository.Add(item);
            }
        }
        public void RemoveFromCart(int cartItemId)
        {
            var item = _cartItemRepository.GetById(cartItemId);

            if (item == null)
            {
                throw new Exception("Item Not Found");
            }

            _cartItemRepository.Delete(item);
        }
        public void UpdateQuantity(int cartItemId, int quantity)
        {
            var item = _cartItemRepository.GetById(cartItemId);

            if (item == null)
            {
                throw new Exception("Item Not Found");
            }

            item.quantity = quantity;

            _cartItemRepository.Update(item);
        }
        public List<CartItemDTO> GetCartItems(int cartId)
        {
            var items =  _cartItemRepository.GetCartItems(cartId);

            return items.Select(x => new CartItemDTO
            {cartid=x.cartid,
                productId = x.productId,
                quantity = x.quantity,
                perice = x.perice,
                
            }).ToList();
        }
    }
}
