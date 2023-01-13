namespace SalesTracker.Models.TransactionModels
{
    public class TransactionListItem
    {
        public int Id { get; set; }
        public List<OrderEntity> Orderlist { get; set; }
        public string PaymentMethod { get; set; }
        public DateTime DateOfTransaction { get; set; }
        public int CustomerId { get; set; }
        public CustomerEntity Customer { get; set; }
        public OrderEntity Order { get; set; }
    }
}