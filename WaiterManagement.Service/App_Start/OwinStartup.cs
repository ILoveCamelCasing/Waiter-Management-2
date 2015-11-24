using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Ninject;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(WaiterManagement.Service.OwinStartup))]
namespace WaiterManagement.Service
{
	public class OwinStartup
	{
		public void Configuration(IAppBuilder app)
		{
			app.MapSignalR(Configure());
		}

		private static HubConfiguration Configure()
		{
			return new HubConfiguration
			{
				EnableDetailedErrors = true,
				Resolver = new NinjectDependencyResolver(ServiceBootstrapper.GetInstance().Kernel)
			};
		}
	}
}
