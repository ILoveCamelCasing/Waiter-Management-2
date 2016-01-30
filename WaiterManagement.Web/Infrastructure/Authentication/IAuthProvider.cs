using WaiterManagement.Web.Models;

namespace WaiterManagement.Web.Infrastructure.Authentication
{
	public interface IAuthProvider
	{
		bool IsLogged { get; }
		void LogIn(LogInUser logInUser);
		void Register(RegisterUser user);
	}
}