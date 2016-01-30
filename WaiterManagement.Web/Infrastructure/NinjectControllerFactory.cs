using System;
using System.Web.Mvc;
using System.Web.Routing;
using Ninject;
using WaiterManagement.Common.Security;
using WaiterManagement.Web.Infrastructure.Authentication;
using WaiterManagement.Web.Infrastructure.ServerProviders;

namespace WaiterManagement.Web.Infrastructure
{
	public class NinjectControllerFactory : DefaultControllerFactory
	{
		private readonly IKernel _kernel;

		public NinjectControllerFactory()
		{
			_kernel = new StandardKernel();
			AddBindings();
		}

		protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
		{
			return controllerType == null
			? null
			: (IController)_kernel.Get(controllerType);
		}

		private void AddBindings()
		{
			_kernel.Bind<IAuthProvider>().To<AuthProvider>().InSingletonScope();
			_kernel.Bind<IMenuProvider>().To<MenuProvider>().InSingletonScope();
			_kernel.Bind<ILogInStrategy>().To<LogInStrategy>().InSingletonScope();
			_kernel.Bind<IRegisterWebUserStrategy>().To<RegisterWebUserStrategy>().InSingletonScope();
			_kernel.Bind<IPasswordManager>().To<PasswordManager>().InSingletonScope();
		}
	}
}