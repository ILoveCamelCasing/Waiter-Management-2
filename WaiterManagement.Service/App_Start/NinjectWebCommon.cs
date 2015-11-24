using System.Web.Http;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using Ninject.Web.Common;
using Ninject.Web.WebApi;
using WaiterManagement.Service;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(NinjectWebCommon), "Stop")]

namespace WaiterManagement.Service
{
	public static class NinjectWebCommon
	{

		private static ServiceBootstrapper _bootstrapper;

		/// <summary>
		/// Starts the application
		/// </summary>
		public static void Start()
		{
			DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
			DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));

			_bootstrapper = ServiceBootstrapper.GetInstance();

			GlobalConfiguration.Configuration.DependencyResolver = new NinjectDependencyResolver(_bootstrapper.Kernel);
		}

		/// <summary>
		/// Stops the application.
		/// </summary>
		public static void Stop()
		{
			_bootstrapper.ShutDown();
		}
	}
}
