namespace myStore.DTO
{
    public class OrderDTO
    {
        public DateTime orderDate { get; set; }
        public double totalPrice { get; set; }
        public string status { get; set; } = string.Empty;
    }
}
