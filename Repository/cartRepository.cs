using myStore.DataBase;
using myStore.Models;

namespace myStore.Repository
{
    public interface ICartRepository
    {
        cartModel? GetCartByUserId(int userId);

        void AddCart(cartModel cart);

        void UpdateCart(cartModel cart);

        void DeleteCart(cartModel cart);
    }   
    public class cartRepository : ICartRepository
    {
      
         
            private readonly E_comerceContext _context;

            public cartRepository(E_comerceContext context)
            {
                _context = context;
            }

            public cartModel? GetCartByUserId(int userId)
            {
                return _context.Carts
                    .FirstOrDefault(x => x.userId == userId);
            }

            public void AddCart(cartModel cart)
            {
                _context.Carts.Add(cart);
                _context.SaveChanges();
            }

            public void UpdateCart(cartModel cart)
            {
                _context.Carts.Update(cart);
                _context.SaveChanges();
            }

            public void DeleteCart(cartModel cart)
            {
                _context.Carts.Remove(cart);
                _context.SaveChanges();
            }
        }
    }

