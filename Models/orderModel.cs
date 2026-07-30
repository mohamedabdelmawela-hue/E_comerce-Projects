using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace myStore.Models
{
    public class orderModel
    {
        [Key]
        public int orderModelId { get; set; }
        public DateTime orderDate { get; set; }
        public double totalPrice {  get; set; }
        public string status {  get; set; }=string.Empty;
        [ForeignKey("users")]
        public int userId { get; set; }
        //هنا الكومبيلر بيحذرك لان هو خرج من الكونستراكتور وهو ب null 
        //فاحنا قايلين مينفعش يكون ب null
        //الحل 
        //1-null!==> يعني بفوله خليه null
        //دلوقتي علي ميجيلك بيانات ولكن مينفعش يكون ب
        //==>null
        //إنما ؟ بقوله ممكن يكون ب null عادي يعني فيه قيم فارغة
        public UsersModel users { get; set; } = null!;
        public List<orderItemModel> orderitems { get; set; }=new List<orderItemModel>();
    }
}
