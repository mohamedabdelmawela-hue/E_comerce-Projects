using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace myStore.Models
{
    public class cartModel
    {
        [Key]
        public int cartModelId { get; set; }
        [ForeignKey("users")]
       public int userId {  get; set; }
        public UsersModel users { get; set; } = null!;
        public List<cartItemModel> items { get; set; } = new List<cartItemModel>();
    }
}
