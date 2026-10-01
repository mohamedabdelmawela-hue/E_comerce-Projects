using myStore.DTO;
using myStore.Middleware;
using myStore.Models;
using myStore.Repository;

namespace myStore.ServiceLayer
{
    public interface ICartService
    {
 
        CartDTO? GetById(int id);

        void Add(CartDTO cartDTO);

        void Delete(int id);
    }
    public class CartServiceLayer : ICartService
    {
        
        
            private readonly ICartRepository _cartRepository;

            public CartServiceLayer(ICartRepository cartRepository)
            {
                _cartRepository = cartRepository;
            }

            

            public CartDTO? GetById(int id)
            {
                var cart = _cartRepository.GetCartByUserId(id);

                if (cart == null)
                    return null;

                return new CartDTO
                {
                    userId = cart.userId
                };
            }

            public void Add(CartDTO cartDTO)
            {
            var cart = _cartRepository.GetCartByUserId(cartDTO.userId);

            if (cart != null)
            {
                throw new NotFoundException("Cart Already Exists");
            }
            cartModel cart1 = new cartModel();

                cart1.userId = cartDTO.userId;

            _cartRepository.AddCart(cart1);            }

            public void Delete(int id)
            {
                var cart = _cartRepository.GetCartByUserId(id);

                if (cart == null)
                {
                    throw new NotFoundException("Cart Not Found");
                }

                _cartRepository.DeleteCart(cart);
            }
        }
    }

