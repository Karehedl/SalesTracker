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

        public async Task<TransactionDetails> CreateTransactionAsync(TransactionCreate transactionToCreate)
        {
            var transactionDetails = new TransactionDetails();
            var transaction = new TransactionEntity()
            {

                PaymentMethod = transactionToCreate.PaymentMethod,
                DateOfTransaction = transactionToCreate.DateOfTransaction,
                CustomerId = transactionToCreate.CustomerId,
                OrderId = transactionToCreate.OrderId
            };
            //load customer data
            CustomerEntity customer = await _context.Customers.FindAsync(transaction.CustomerId);
            if (customer is null)
                return null;

            transaction.Customer = customer;

            var order = await _context.Order.FindAsync(transaction.OrderId);
            if (order is null)
                return null;

            transaction.Order = order;


            await _context.Transactions.AddAsync(transaction);
            await _context.SaveChangesAsync();

            // transactionDetails.Id = transaction.Id;
            // transactionDetails.PaymentMethod = transaction.PaymentMethod;
            // transactionDetails.DateOfTransaction = transaction.DateOfTransaction;
            // transactionDetails.CustomerId = transaction.CustomerId;
            // transactionDetails.Orderlist = transaction.Orderlist.Select(o => new OrderListItem
            // {
            //     Id = o.Id,
            //     location = o.location,
            //     Items = o.Items.Select(i => new ItemListItem
            //     {
            //         Id = i.Id,
            //         Name = i.Name
            //     }).ToList()
            // }).ToList();
            // return transactionDetails;
            // transactionDetails.GrandTotal = transaction.GrandTotal;
            var GrandTotal = transaction.Order.ItemsInCart.Sum(i => i.Cost);
            return new TransactionDetails
            {
                Id = transaction.Id,
                // Orderlist = transaction.Orderlist.Select(o => new OrderListItem
                // {
                //     Id = o.Id,
                //     location = o.location,
                //     Items = o.Items.Select(i => new ItemListItem
                //     {
                //         Id = i.Id,
                //         Name = i.Name
                //     }).ToList()
                // }).ToList(),
                CustomerId = transaction.CustomerId,
                PaymentMethod = transaction.PaymentMethod,
                DateOfTransaction = transaction.DateOfTransaction,
                GrandTotal = transaction.Order.ItemsInCart.Sum(i => i.Cost)
            };

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