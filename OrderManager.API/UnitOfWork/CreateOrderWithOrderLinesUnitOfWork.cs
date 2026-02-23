using OrderManager.API.Repositories;
using OrderManager.DbContexts;
using OrderManager.Entities;

namespace OrderManager.API.UnitOfWork
{
	public class CreateOrderWithOrderLinesUnitOfWork : IUnitOfWork
	{
		private readonly OrderManagerDbContext _context;
		private readonly IOrderRepository _orderRepository;
		private readonly IOrderLineRepository _orderLineRepository;

		public CreateOrderWithOrderLinesUnitOfWork(OrderManagerDbContext context,
			IOrderRepository orderRepository,
			IOrderLineRepository orderLineRepository)
		{
			_context = context;
			_orderRepository = orderRepository;
			_orderLineRepository = orderLineRepository;
		}

		public async Task<int> SaveChangesAsync()
		{
			return await _context.SaveChangesAsync();
		}

		public async Task RollbackAsync()
		{
			_context.ChangeTracker.Clear();
			await Task.CompletedTask;
		}

		public async Task CreateOrderWithOrderLinesAsync(Order order,
			IEnumerable<OrderLine> orderLines)
		{
			await _orderRepository.CreateOrderAsync(order);
			foreach (var line in orderLines)
			{
				line.Order = order;
				await _orderLineRepository.CreateOrderLineAsync(line);
			}
		}

		public void Dispose()
		{
			// nothing to see here
		}
	}
}