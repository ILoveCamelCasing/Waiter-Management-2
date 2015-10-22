using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;
using Caliburn.Micro;
using Ninject;
using Ninject.Extensions.Conventions;
using WaiterManagement.BLL.Commands.Base;
using WaiterManagement.Common.Entities.Abstract;
using WaiterManagement.DAL;
using WaiterManagement.Manager.ViewModels;
using WaiterManagement.Wpf.MVVM;
using WaiterManagement.Wpf.MVVM.Abstract;

namespace WaiterManagement.Manager.Bootstrapper
{
	public class AppBootstrapper : BootstrapperBase
	{
		private IKernel _kernel;

		public AppBootstrapper()
		{
			Initialize();
		}

		protected override void OnStartup(object sender, StartupEventArgs e)
		{
			DisplayRootViewFor<MainWindowViewModel>();
		}

		protected override void Configure()
		{
			_kernel = new StandardKernel();

			_kernel.Bind<IUnitOfWork>().To<UnitOfWork>().InTransientScope();

			RegisterViewModels();

			UseViewAttribute.ConfigureViewLocator();

			RegisterHandlers();
		}

		protected override void OnExit(object sender, EventArgs e)
		{
			_kernel.Dispose();
			base.OnExit(sender, e);
		}

		protected override object GetInstance(Type service, string key)
		{
			if (service == null)
				throw new ArgumentNullException("service");

			return _kernel.Get(service);
		}

		protected override IEnumerable<object> GetAllInstances(Type service)
		{
			return _kernel.GetAll(service);
		}

		protected override void BuildUp(object instance)
		{
			_kernel.Inject(instance);
		}

		private void RegisterViewModels()
		{
			_kernel.Bind<IWindowManager>().To<WindowManager>().InSingletonScope();

			_kernel.Bind<IViewModelResolver>().To<ViewModelResolver>().InSingletonScope();

			_kernel.Bind(
				convention =>
					convention.FromThisAssembly()
						.SelectAllClasses()
						.InNamespaces("WaiterManagement.Manager.ViewModels")
						.BindAllInterfaces());
		}

		private void RegisterHandlers()
		{
			_kernel.Bind(
				convention =>
					convention.From(Assembly.GetAssembly(typeof(CommandBus)))
						.SelectAllClasses()
						.InNamespaces("WaiterManagement.BLL.Commands.Handlers")
						.BindAllInterfaces());

			var commandBus = new CommandBus(x => _kernel.GetAll<IHandleCommand>().First(y => y.GetType().GetInterfaces()[1].GetGenericArguments()[0] == x));

			_kernel.Bind<ICommandBus>().ToConstant(commandBus);
		}



	}
}