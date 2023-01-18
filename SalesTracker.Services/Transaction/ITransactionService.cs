public interface ITransactionService
{
    Task<bool> CreateTransactionAsync(TransactionCreate transactionToCreate);
    //Task<bool> DeleteTransactionAsync(int transactionId);
    //Task<IEnumerable<TransactionListItem>> GetAllTransactionsAsync();
}