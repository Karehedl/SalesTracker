namespace SalesTracker.Models.TransactionModels

//Models does not need to represent database fields the way Entities does. Models only needs to include properties which the user would enter/needing to be displayed

{
    public class TransactionCreate
    {
        public List<int> OrderIdList { get; set; } //How do we associate this list of ints with OrderEntities?

        public string PaymentMethod { get; set; }

        public DateTime DateOfTransaction { get; set; }

        public int CustomerId { get; set; }
    }
}