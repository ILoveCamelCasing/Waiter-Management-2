using System;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
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

		protected override bool IsAuthorized(HttpActionContext actionContext)
		{
			try
			{
				var login = actionContext.Request.Headers.GetValues("login").FirstOrDefault();//["login"];
				var tokenString = actionContext.Request.Headers.GetValues("token").FirstOrDefault();
				var token = new Guid(tokenString);

				return _authorizeStrategy.Authorize(login, token);
			}
			catch
			{
				return false;
			}
		}

		//protected override bool AuthorizeCore(HttpContextBase httpContext)
		//{
		//	try
		//	{
		//		var login = httpContext.Request.Headers["login"];
		//		var token = new Guid(httpContext.Request.Headers["token"]);

		//		return _authorizeStrategy.Authorize(login, token);
		//	}
		//	catch
		//	{
		//		return false;
		//	}
		//}
	}
}