using OrderManager.Entities;

namespace OrderManager.API.Repositories
{
	public interface IOrderRepository
	{
		Task<IEnumerable<Order>> GetAllOrdersAsync(bool includeOrderLines = false);
		Task<Order?> GetOrderByIdAsync(int orderId, bool includeOrderLines = false);
		Task<Order> CreateOrderAsync(Order order);
		Task UpdateOrderAsync(Order order);
		Task DeteteOrderAsync(int orderId);
	}
}