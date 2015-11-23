using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(WaiterManagement.Service.OwinStartup))]

namespace WaiterManagement.Service
{
	public class OwinStartup
	{
		public void Configuration(IAppBuilder app)
		{
			app.MapSignalR();
		}
	}
}
