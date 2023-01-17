
using System.Data.Common;
using System.Collections.Specialized;
using System.Collections.Concurrent;
using System.Net.Security;
using Microsoft.EntityFrameworkCore;

public class OrderService : IOrderService
{
    private AppDbContext _context;
    public OrderService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<OrderDetails> CreateOrder(OrderCreate orderCreate)
        {  
            var orderDetails = new OrderDetails();
            var order = new OrderEntity(){
                location = orderCreate.location
            };

            foreach (var id in orderCreate.ItemIds)
            {
                var item = _context.Items.SingleOrDefault(i => i.Id == id);
                if(item == null)
                return null;

                order.Items.Add(item);
            }
            await _context.Order.AddAsync(order);
            await _context.SaveChangesAsync();

            orderDetails.Id=order.Id; 
            orderDetails.Location=order.location;
            orderDetails.Items=order.Items.Select(i=>new ItemListItem{
                Id = i.Id,
                Name = i.Name
            }).ToList();

            return orderDetails;
        }

    public async Task<List<OrderListItem>> GetOrders()
    {
        var orders = await _context.Order.Include(o=>o.Items).Select(s => new OrderListItem
        {
            Id = s.Id,
            location = s.location,
            Items = s.Items.Select(i=>new ItemListItem{
                Id = i.Id,
                Name = i.Name
            }).ToList()
            
        }).ToListAsync();

        return orders;
    }

    public async Task<OrderDetails> GetOrderById(int id)
    {
        var order = await _context.Order.Include(o=>o.Items).FirstOrDefaultAsync(c => c.Id == id);

        if (order is null) return null;

        return new OrderDetails{
            Id = order.Id,
            Location = order.location, 
            Items = order.Items.Select(i=>new ItemListItem{
                Id = i.Id,
                Name = i.Name
            }).ToList()
        };
    }

     public async Task<bool> DeleteOrder(int id)
    {
        var order = await _context.Order.FindAsync(id);
        if (order is null)
            return false;
        else
        {
            _context.Order.Remove(order);
            return await _context.SaveChangesAsync() > 0;
        }
    }

    public async Task<OrderDetails> EditOrder(int id, OrderEdit updateorder)
        {   
            var order = await _context.Order.Include(o=>o.Items).FirstOrDefaultAsync(c => c.Id == id);
            if (order == null) return null;
            
            order.location=updateorder.location;
            order.Items.Clear();
           
                  foreach (var things in updateorder.ItemIds)
            {
                var item = _context.Items.SingleOrDefault(i => i.Id == things);
                if(item == null)
                return null;

                order.Items.Add(item);
            }
            await _context.SaveChangesAsync();

             return new OrderDetails{
            Id = order.Id,
            Location = order.location, 
            Items = order.Items.Select(i=>new ItemListItem{
                Id = i.Id,
                Name = i.Name
            }).ToList()
        };        
        }
}
