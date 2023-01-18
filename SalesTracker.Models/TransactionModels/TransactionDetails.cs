namespace SalesTracker.Models.TransactionModels
{
    public class TransactionDetails
    {
        public int Id { get; set; }

        public List<OrderListItem> Orderlist { get; set; } = new List<OrderListItem>();

        public string PaymentMethod { get; set; }

        public DateTime DateOfTransaction { get; set; }

        public int CustomerId { get; set; }

        public Decimal GrandTotal { get; set; }
    }
}