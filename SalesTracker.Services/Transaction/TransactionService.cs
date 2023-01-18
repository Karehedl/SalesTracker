using Microsoft.EntityFrameworkCore;


public class TransactionService : ITransactionService //this IService is only for ease of testing, unless you're writing libraries and stuff or more sophisticated code
{
    private readonly AppDbContext _context;

    public TransactionService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> CreateTransactionAsync(TransactionCreate transactionToCreate)
    {
        var entity = new TransactionEntity
        {
            OrderId = transactionToCreate.OrderId,
            CustomerId = transactionToCreate.CustomerId,
        };

        await _context.Transactions.AddAsync(entity);
        return await _context.SaveChangesAsync() > 0;
    }

    // public async Task<bool> DeleteTransactionAsync(int transactionId)
    // {
    //     var transactionEntity = await _context.Transactions.FindAsync(transactionId);

    //     _context.Transactions.Remove(transactionEntity);
    //     return await _context.SaveChangesAsync() == 1;
    // }

    // public async Task<IEnumerable<TransactionListItem>> GetAllTransactionsAsync()
    // {
    //     var transactions = await _context.Transactions
    //     .Select(entity => new TransactionListItem
    //     {
    //         Orderlist = entity.Orderlist,
    //         PaymentMethod = entity.PaymentMethod,
    //         DateOfTransaction = DateTime.Now,
    //         Customer = entity.Customer,
    //         Order = entity.Order
    //     }).ToListAsync();

    //     return transactions;
    // }
}