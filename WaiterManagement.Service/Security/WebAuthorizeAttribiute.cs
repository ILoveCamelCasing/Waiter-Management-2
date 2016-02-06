using System;
using System.Web;
using System.Web.Mvc;
using Ninject;

namespace WaiterManagement.Service.Security
{
	public class WebAuthorizeAttribiute : AuthorizeAttribute
	{
		private readonly IAuthorizeStrategy _authorizeStrategy;

		public WebAuthorizeAttribiute()
		{
			_authorizeStrategy = ServiceBootstrapper.GetInstance().Kernel.Get<IAuthorizeStrategy>();
		}

		protected override bool AuthorizeCore(HttpContextBase httpContext)
		{
			try
			{
				var login = httpContext.Request.Headers["login"];
				var token = new Guid(httpContext.Request.Headers["token"]);

				return _authorizeStrategy.Authorize(login, token);
			}
			catch
			{
				return false;
			}
		}
	}
}