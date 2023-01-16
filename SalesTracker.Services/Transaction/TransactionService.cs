using System;
using Microsoft.AspNetCore.Http;
using SalesTracker.Data;
using Microsoft.EntityFrameworkCore;
using SalesTracker.Models.TransactionModels;

namespace SalesTracker.Services.Transaction
{
    public class TransactionService : ITransactionService //this IService is only for ease of testing, unless you're writing libraries and stuff or more sophisticated code
    {
        private readonly AppDbContext _context;

        public TransactionService(IHttpContextAccessor httpContextAccessor, AppDbContext context)
        {
            _context = context;
        }

        public async Task<TransactionEntity> CreateTransactionAsync(TransactionCreate transactionToCreate)
        {
            var transaction = new TransactionEntity()
            {
                PaymentMethod = transactionToCreate.PaymentMethod,
                DateOfTransaction = transactionToCreate.DateOfTransaction,
                CustomerId = transactionToCreate.CustomerId
            };

            foreach (var id in transactionToCreate.OrderIdList)
            {
                var order = _context.Order.SingleOrDefault(i => i.Id == id);
                if (order == null)
                    return null;

                transaction.Orderlist.Add(order);
            }
            await _context.Transactions.AddAsync(transaction);
            await _context.SaveChangesAsync();

            return transaction;

            // var TransactionEntity = new TransactionEntity
            // {

            //     // Orderlist = transactionToCreate.OrderIdlist,
            //     // //var matched = cars.Where(car => intList.Contains(car.id)).ToList();
            //     // PaymentMethod = transactionToCreate.PaymentMethod,
            //     // DateOfTransaction = DateTime.Now,
            //     // CustomerId = customerId
            // };

            // await _context.AddAsync(TransactionEntity);
            // var numberOfChanges = await _context.SaveChangesAsync();
            // return numberOfChanges == 1;
        }

        public async Task<bool> DeleteTransactionAsync(int transactionId)
        {
            var transactionEntity = await _context.Transactions.FindAsync(transactionId);

            _context.Transactions.Remove(transactionEntity);
            return await _context.SaveChangesAsync() == 1;
        }

        // public async Task<TransactionDetails> GetTransactionByIdAsync(int transactionId)
        // {
        //     var transactionFromDatabase = await _context.Transactions.FirstOrDefaultAsync(entity => entity.Id == transactionId);

        //     return transactionFromDatabase is null ? null : new TransactionDetails
        //     {
        //         Id = transactionFromDatabase.Id,
        //         Orderlist = transactionFromDatabase.Orderlist,
        //         PaymentMethod = transactionFromDatabase.PaymentMethod,
        //         DateOfTransaction = transactionFromDatabase.DateOfTransaction,
        //         Customer = transactionFromDatabase.Customer,
        //         Order = transactionFromDatabase.Order,
        //     };
        // }

        public async Task<IEnumerable<TransactionListItem>> GetAllTransactionsAsync()
        {
            var transactions = await _context.Transactions
            .Select(entity => new TransactionListItem
            {
                Orderlist = entity.Orderlist,
                PaymentMethod = entity.PaymentMethod,
                DateOfTransaction = DateTime.Now,
                Customer = entity.Customer,
                Order = entity.Order
            }).ToListAsync();

            return transactions;
        }

        // public async Task<bool> UpdateTransactionAsync(TransactionDetails request)
        // {
        //     var transactionToBeUpdated = await _context.Transactions.FindAsync(request.Id);

        //     if (transactionToBeUpdated == null)
        //         return false;

        //     transactionToBeUpdated.Orderlist = request.Orderlist;
        //     transactionToBeUpdated.PaymentMethod = request.PaymentMethod;
        //     transactionToBeUpdated.DateOfTransaction = request.DateOfTransaction;
        //     transactionToBeUpdated.Customer = request.Customer;
        //     transactionToBeUpdated.Order = request.Order;

        //     var numberOfChanges = await _context.SaveChangesAsync();
        //     return numberOfChanges == 1;
        // }
    }
}