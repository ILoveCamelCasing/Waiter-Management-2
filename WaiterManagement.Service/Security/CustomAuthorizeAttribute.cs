using System;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Ninject;

namespace WaiterManagement.Service.Security
{
	public class CustomAuthorizeAttribute : AuthorizeAttribute
	{
		private readonly IAuthorizeStrategy _authorizeStrategy;

		public CustomAuthorizeAttribute()
		{
			_authorizeStrategy = ServiceBootstrapper.GetInstance().Kernel.Get<IAuthorizeStrategy>();
		}

		public override bool AuthorizeHubConnection(HubDescriptor hubDescriptor, IRequest request)
		{
			try
			{
				var login = request.Headers["login"];
				var token = new Guid(request.Headers["token"]);
				return _authorizeStrategy.Authorize(login, token);
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