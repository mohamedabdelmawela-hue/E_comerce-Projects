using System.ComponentModel.DataAnnotations;

namespace myStore.Models
{
    public class categoryModel
    {
        [Key]
        public int categoryId { get; set; }
        public string categoryName { get; set; } = string.Empty;
        public List<productModel> productModel { get; set; }= new List<productModel>();
    }
}
