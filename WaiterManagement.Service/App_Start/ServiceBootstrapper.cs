using System;
using System.Linq;
using System.Reflection;
using System.Web;
using Ninject;
using Ninject.Extensions.Conventions;
using Ninject.Web.Common;
using WaiterManagement.BLL.Commands.Base;
using WaiterManagement.Common.Entities.Abstract;
using WaiterManagement.Common.Security;
using WaiterManagement.Common.Views.Abstract;
using WaiterManagement.DAL;

namespace WaiterManagement.Service
{
	public class ServiceBootstrapper
	{
		#region Singleton

		private static ServiceBootstrapper _instance;

		public static ServiceBootstrapper GetInstance()
		{
			return _instance ?? ( _instance = new ServiceBootstrapper());
		}

		private ServiceBootstrapper()
		{
			Initialize();
		}

		#endregion

		#region Private fields

		private static readonly Bootstrapper Bootstrapper = new Bootstrapper();

		#endregion

		#region Public properties

		public IKernel Kernel { get; private set; }

		#endregion

		#region Public methods

		public void ShutDown()
		{
			Bootstrapper.ShutDown();
		}

		#endregion

		#region Private fields

		private void Initialize()
		{
			Kernel = CreateKernel();
			Bootstrapper.Initialize(() => Kernel);
		}

		/// <summary>
		/// Creates the kernel that will manage your application.
		/// </summary>
		/// <returns>The created kernel.</returns>
		private static IKernel CreateKernel()
		{
			var kernel = new StandardKernel();
			try
			{
				kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
				kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

				RegisterServices(kernel);

				return kernel;
			}
			catch
			{
				kernel.Dispose();
				throw;
			}
		}

		/// <summary>
		/// Load your modules or register your services here!
		/// </summary>
		/// <param name="kernel">The kernel.</param>
		private static void RegisterServices(IKernel kernel)
		{
			kernel.Bind<IViewProvider>().To<ViewProvider>().InSingletonScope();
			kernel.Bind<IUnitOfWork>().To<UnitOfWork>().InTransientScope();
			kernel.Bind<IPasswordManager>().To<PasswordManager>().InSingletonScope();

			RegisterHandlers(kernel);
		}

		private static void RegisterHandlers(IKernel kernel)
		{
			kernel.Bind(
				convention =>
					convention.From(Assembly.GetAssembly(typeof(CommandBus)))
						.SelectAllClasses()
						.InNamespaces("WaiterManagement.BLL.Commands.Handlers")
						.BindAllInterfaces());

			var commandBus = new CommandBus(x => kernel.GetAll<IHandleCommand>().First(y => y.GetType().GetInterfaces()[1].GetGenericArguments()[0] == x));

			kernel.Bind<ICommandBus>().ToConstant(commandBus);
		}

		#endregion
	}
}