using SalesTracker.Models.TransactionModels;

public interface ITransactionService
{
    Task<TransactionDetails> CreateTransactionAsync(TransactionCreate transactionToCreate);
    Task<bool> DeleteTransactionAsync(int transactionId);
    //Task<TransactionDetails> GetTransactionByIdAsync(int transactionId);
    Task<IEnumerable<TransactionListItem>> GetAllTransactionsAsync();
    //Task<bool> UpdateTransactionAsync(TransactionDetails request);
}