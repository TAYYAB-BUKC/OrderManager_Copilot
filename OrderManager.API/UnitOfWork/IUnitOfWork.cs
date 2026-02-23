namespace OrderManager.API.UnitOfWork
{
	public interface IUnitOfWork
	{
		Task<int> SaveChangesAsync();
		Task RollbackAsync();
	}
}