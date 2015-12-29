using System.Threading.Tasks;

namespace WaiterManagement.Common.Security
{
	public interface IAccessProvider
	{
		Task<LoginResult> LogIn(string login, string password);
		string Login { get; }
		string Token { get; }
	}
}