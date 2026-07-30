using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using myStore.DataBase;
using myStore.DTO;
using myStore.Models;

namespace myStore.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class cartController : ControllerBase
    {
        private readonly E_comerceContext context;
        public cartController(E_comerceContext e_ComerceContext)
        {
            context = e_ComerceContext;
        }
        [HttpPost("CreatCart")]
        //create cart
        public IActionResult AddCart(CartDTO cartDTO)
        {
            var addcart = new cartModel
            {
                userId = cartDTO.userId
            };
            context.Carts.Add(addcart);
            context.SaveChanges();
            return Ok(addcart);


        }
        [HttpPost("AddToCART")]
        //Add Cartitem in cart
        public IActionResult AddToCart(CartItemDTO cartItemDTO)
        {
            var cart = context.Carts.Find(cartItemDTO.cartid);

            if (cart == null)
            {
                return NotFound("Cart Not Found");
            }
            var product = context.Products.Find(cartItemDTO.productId);

            if (product == null)
            {
                return NotFound("Product Not Found");
            }
            var resultcart=context.CartItems.
            FirstOrDefault(x=>x.cartid==cartItemDTO.cartid&&x.productId==cartItemDTO.productId);
            if (resultcart != null)
            {
                resultcart.quantity += cartItemDTO.quantity;
            }
            /*
            var resultproduct = context.Products.Find(cartItemDTO.productId);
            if (resultproduct == null) 
            { 
            return NotFound("product Not found");
            }*/
            else
            {
                var cartItem = new cartItemModel
                {
                    cartid = cartItemDTO.cartid,
                    productId = cartItemDTO.productId,
                    quantity = cartItemDTO.quantity,
                };
                context.CartItems.Add(cartItem);

            }

            context.SaveChanges();
            return Ok( );



        }
        [HttpGet("{cartid}")]
        //get itemFromCard
        public IActionResult GetItemFromCard(int cartid) { 
        var result=context.CartItems.
                Where(x=>x.cartid==cartid)
                .ToList();
            return Ok(result);
        }
        [HttpDelete("{cartitemid}")]
        public IActionResult RemoveItemFromCart(int cartitemid) 
        { 
        var Removecartitem = context.CartItems.Find(cartitemid);
            if (Removecartitem != null)
            {
                context.CartItems.Remove(Removecartitem);
                context.SaveChanges();
            }
            return Ok( );
        }

        //******************chickout*****************

        [HttpPost("{cartID}")]
        public IActionResult chickout(int cartID) {
            double totalprice = 0;
            //هات السله من جدول ال كارت
            var cartId= context.Carts.Find(cartID);
            if (cartId == null) {
                return NotFound("Not Find"); 
            }
            // 2) هات كل المنتجات الموجودة داخل السلة من جدول CartItem
            //get Allitem From this Cart 
            var cartItem = context.CartItems .Where(x=>x.cartid==cartID).ToList(); 
            //if not contain
            if (cartItem == null) 
            {
                return NotFound("Not Found"); 
            }
            //If contain
            // 3) حساب إجمالي السعر
            foreach (var item in cartItem)
            {// Foreach for allitems in cart
                var res = context.Products.Find(item.productId);
                //If itrmProduct not found
                if (res == null) { 
                return NotFound();
                }
                //If exist
                // السعر × الكمية
                totalprice += res.productprice*item.quantity; 
               
            }
            //بعد محسبت الاجمالي هعمل اوردر جديد 
            // 4) إنشاء Order جديد (الفاتورة)
            var orders = new orderModel
            {//get userid from cart
                //فيه اليوزم بتاع السلة
                userId = cartId.userId,
                //التاريخ
                orderDate = DateTime.Now,
                //we are count it above
                //اجمالي السعر
                totalPrice = totalprice,

                 

            };
            //بعد كده احفظ الاوردر في الداتا بيز
            context.Orders.Add(orders);
            context.SaveChanges();
            // 5) تحويل CartItems إلى OrderItems
            foreach (var item in cartItem)
            {
                var resultpro = context.Products.Find(item.productId);
                var orderitems = new orderItemModel
                {
                   //رقم الاوردر
                   orderId = orders.orderModelId,
                  // المنتج
                    productId = item.productId,
                   // الكمية
                    Quantity = item.quantity,
                    //السعر الاجمالي من الاوردر
                    perice =orders.totalPrice

                };
                context.OrderItems.Add(orderitems);
                context.CartItems.RemoveRange(cartItem);
                context.SaveChanges();

            }
            


            return Ok(orders ); }

    } 
}
