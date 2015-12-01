namespace WaiterManagement.Table.Connection
{
	public interface IAccessProvider
	{
		bool Login(string login, string password);
		string TableLogin { get; }
	}
}