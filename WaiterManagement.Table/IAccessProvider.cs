namespace WaiterManagement.Table
{
	public interface IAccessProvider
	{
		bool Login(string login, string password);
	}
}