using System;
using System.Linq;
using WaiterManagement.Common.Views;
using WaiterManagement.Common.Views.Abstract;

namespace WaiterManagement.Service.Security
{
	public interface IAuthorizeStrategy
	{
		bool Authorize(string login, Guid token);
	}

	public class AuthorizeStrategy : IAuthorizeStrategy
	{
		private readonly IViewProvider _viewProvider;

		public AuthorizeStrategy(IViewProvider viewProvider)
		{
			_viewProvider = viewProvider;
		}

		public bool Authorize(string login, Guid token)
		{
			try
			{
				var authenticated = _viewProvider.Get<AuthenticatedUsersView>().Any(x => x.Login == login && x.Token == token);
				return authenticated;
			}
			catch (Exception ex)
			{
				return false;
			}
		}
	}
}