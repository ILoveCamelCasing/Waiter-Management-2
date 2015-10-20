using System.Collections.Generic;
using Caliburn.Micro;

namespace WaiterManagement.Wpf.MVVM.Abstract
{
	public class ViewModelBase : Conductor<object>.Collection.OneActive, IViewModel
	{
		#region Dependenices

		private readonly IViewModelResolver _viewModelResolver;

		#endregion

		public IParentViewModel ParentWindow { get; private set; }

		#region Constructor

		protected ViewModelBase(IViewModelResolver viewModelResolver)
		{
			_viewModelResolver = viewModelResolver;
		}

		#endregion

		#region Protected methods

		protected T Get<T>() where T : IViewModel
		{
			return _viewModelResolver.Resolve<T>();
		}

		#endregion

		public void ShowOn(IParentViewModel parentParentViewModel)
		{
			ParentWindow = parentParentViewModel;
			parentParentViewModel.ActivateItem(this);
		}

		public void Close()
		{
			ParentWindow.CloseItem(this);
		}
	}
}