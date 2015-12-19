namespace WaiterManagement.Common.Security
{
	public interface IAccessProvider
	{
		bool LogIn(string login, string password);
		string Login { get; }
		string Token { get; }
	}
}