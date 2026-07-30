using System.ComponentModel.DataAnnotations.Schema;

namespace myStore.DTO
{
    public class ProductDTO
    {
        public string productName { get; set; } = string.Empty;
        public string productDescription { get; set; } = string.Empty;
        public double productprice { get; set; }
        public int productQuantity { get; set; }
        
        
        public int categoryId { get; set; }
    }
}
