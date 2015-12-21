﻿using System;
using System.Collections.Generic;
using System.Windows;
using Caliburn.Micro;
using Ninject;
using Ninject.Extensions.Conventions;
using WaiterManagement.Common.Apps;
using WaiterManagement.Common.Security;
using WaiterManagement.Table.Connection;
using WaiterManagement.Table.Model;
using WaiterManagement.Table.ViewModels;
using WaiterManagement.Wpf.MVVM;
using WaiterManagement.Wpf.MVVM.Abstract;

namespace WaiterManagement.Table.Bootstrapper
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
			_kernel.Bind<IPasswordManager>().To<PasswordManager>().InSingletonScope();
			_kernel.Bind<IAccessProvider>().To<AccessProvider>().InSingletonScope();
			_kernel.Bind<ITableConnectionProvider>().To<TableConnectionProvider>().InSingletonScope();

			RegisterViewModels();

			var tableApp = new TableApp();
			_kernel.Bind<ITableApp>().ToConstant(tableApp);
			_kernel.Bind<ITableAppSubscriber>().ToConstant(tableApp);

			_kernel.Bind<ICurrentOrder>().To<CurrentOrder>().InSingletonScope();

			UseViewAttribute.ConfigureViewLocator();
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
	}
}