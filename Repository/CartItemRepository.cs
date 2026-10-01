using myStore.DataBase;
using myStore.Models;

namespace myStore.Repository
{
    public interface ICartItemRepository
    {
        List<cartItemModel> GetCartItems(int cartId);

        cartItemModel? GetById(int id);

        void Add(cartItemModel cartItem);

        void Update(cartItemModel cartItem);

        void Delete(cartItemModel cartItem);
    }
    public class CartItemRepository:ICartItemRepository
    {
        
            private readonly E_comerceContext _context;

            public CartItemRepository(E_comerceContext context)
            {
                _context = context;
            }

            public List<cartItemModel> GetCartItems(int cartId)
            {
                return _context.CartItems
                    .Where(x => x.cartid == cartId)
                    .ToList();
            }

            public cartItemModel? GetById(int id)
            {
                return _context.CartItems
                    .FirstOrDefault(x => x.cartItemModelId == id);
            }

            public void Add(cartItemModel cartItem)
            {
                _context.CartItems.Add(cartItem);
                _context.SaveChanges();
            }

            public void Update(cartItemModel cartItem)
            {
                _context.CartItems.Update(cartItem);
                _context.SaveChanges();
            }

            public void Delete(cartItemModel cartItem)
            {
                _context.CartItems.Remove(cartItem);
                _context.SaveChanges();
            }
        
    }
}
