using System.ComponentModel.DataAnnotations;

namespace myStore.Models
{
    public class UsersModel
    {
        [Key]
        public int userId { get; set; }
        public string userName { get; set; }=string.Empty;
        public string userEmail { get; set; }= string.Empty;
        public string userPassword { get; set; } = string.Empty;
        public string userAddress {  get; set; }=string.Empty ;
        //هنا عملت initialize نسخة من الكولكشن new list<orderModel>
        //بدل ما خليها null بستخدام علامة الاشتفهام؟
        public List<orderModel> orders { get; set; }=new List<orderModel>();
        
    }
}
