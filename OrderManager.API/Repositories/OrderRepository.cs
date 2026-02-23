using Microsoft.EntityFrameworkCore;
using OrderManager.DbContexts;
using OrderManager.Entities;

namespace OrderManager.API.Repositories
{
	public class OrderRepository : IOrderRepository
	{
		private readonly OrderManagerDbContext _context;

		public OrderRepository(OrderManagerDbContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<Order>> GetAllOrdersAsync(bool includeOrderLines = false)
		{
			if (includeOrderLines)
			{
				return await _context.Orders.Include(o => o.OrderLines).ToListAsync();
			}
			else
			{
				return await _context.Orders.ToListAsync();
			}
		}

		public async Task<Order?> GetOrderByIdAsync(int orderId, bool includeOrderLines = false)
		{
			if (includeOrderLines)
			{
				return await _context.Orders.Include(o => o.OrderLines).FirstOrDefaultAsync(o => o.Id == orderId);
			}
			else
			{
				return await _context.Orders.FirstOrDefaultAsync(o => o.Id == orderId);
			}
		}

		public async Task<Order> CreateOrderAsync(Order order)
		{
			_context.Orders.Add(order);
			await _context.SaveChangesAsync() ;
			return order;
		}

		public async Task UpdateOrderAsync(Order order)
		{
			_context.Orders.Update(order);
			await _context.SaveChangesAsync();
		}

		public async Task DeteteOrderAsync(int orderId)
		{
			var order = await _context.Orders.FindAsync(orderId);
			if (order is not null)
			{
				_context.Orders.Remove(order);
				await _context.SaveChangesAsync();
			}
		}
	}
}