using System;
using System.Linq;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Ninject;
using WaiterManagement.Common.Views;
using WaiterManagement.Common.Views.Abstract;

namespace WaiterManagement.Service.Security
{
	public class CustomAuthorizeAttribute : AuthorizeAttribute
	{
		private readonly IViewProvider _viewProvider;

		public CustomAuthorizeAttribute()
		{
			_viewProvider = ServiceBootstrapper.GetInstance().Kernel.Get<IViewProvider>();
		}

		public override bool AuthorizeHubConnection(HubDescriptor hubDescriptor, IRequest request)
		{
			try
			{
				var login = request.Headers["login"];
				var token = new Guid(request.Headers["token"]);
				var authenticated = _viewProvider.Get<AuthenticatedUsersView>().Any(x => x.Login == login && x.Token == token);
				return authenticated;
			}
			catch (Exception ex)
			{
				return false;
			}
		}

		public override bool AuthorizeHubMethodInvocation(IHubIncomingInvokerContext hubIncomingInvokerContext, bool appliesToMethod)
		{
			return true;
		}
	}
}