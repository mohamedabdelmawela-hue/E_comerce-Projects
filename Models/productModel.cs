using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace myStore.Models
{
    public class productModel
    {

        [Key]
       public int productId {  get; set; }
        public string productName { get; set; } = string.Empty;
        public string productDescription { get; set; }= string.Empty;
        public double productprice { get; set; }
        public int productQuantity { get; set; }
        [ForeignKey("category")]
        public int categoryId {  get; set; }
        public categoryModel category { get; set; } = null!;
        public List<orderItemModel> orderItems { get; set; } = new List<orderItemModel>();
        public  List<cartItemModel> cartItems { get; set; }=new List<cartItemModel>();
   

    }
}
