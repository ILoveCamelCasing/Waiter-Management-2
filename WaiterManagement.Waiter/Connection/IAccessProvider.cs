namespace WaiterManagement.Waiter.Connection
{
	public interface IAccessProvider
	{
		bool Login(string login, string password);
		string WaiterLogin { get; }
	}
}