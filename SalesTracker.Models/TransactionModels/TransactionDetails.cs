namespace SalesTracker.Models.TransactionModels
{
    public class TransactionDetails
    {
        public int Id { get; set; }

        public List<OrderListItem> Orderlist { get; set; }

        public string PaymentMethod { get; set; }

        public DateTime DateOfTransaction { get; set; }

        public CustomerListItem Customer { get; set; }
    }
}