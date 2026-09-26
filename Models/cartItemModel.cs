using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace myStore.Models
{
    public class cartItemModel
    {
        [Key]
        public int cartItemModelId { get; set; }
        [ForeignKey("cartModel")]
        public int cartid {  get; set; }
        public cartModel cartModel { get; set; } = null!;
        [ForeignKey("productModel")]
        public int productId {  get; set; }
        public productModel productModel { get; set; } = null!;
        public double perice { get; set; }
        public int quantity {  get; set; }
    }
}
