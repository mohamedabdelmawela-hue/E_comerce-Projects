using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace myStore.Models
{
    public class orderItemModel
    {
        [Key]
        public int orderItemModelId { get; set; }
        [ForeignKey("orderModel")]
        public int orderId { get; set; }
        public orderModel orderModel { get; set; } = null!;
        [ForeignKey("productModel")]
        public int productId {  get; set; }
        public productModel productModel { get; set; } = null!;
        public int Quantity {  get; set; }
        public double perice {  get; set; }
    }
}
