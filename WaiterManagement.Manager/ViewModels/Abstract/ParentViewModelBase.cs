using System.Collections.Generic;
using System.Linq;
using WaiterManagement.Manager.Bootstrapper.Abstract;

namespace WaiterManagement.Manager.ViewModels.Abstract
{
	public abstract class ParentViewModelBase : ViewModelBase, IParentViewModel
	{
		#region Dependenices

		private readonly IViewModelResolver _viewModelResolver;

		#endregion

		#region Private Fields

		private Stack<IViewModel> _activatedItems;

		#endregion

		#region Constructor

		protected ParentViewModelBase(IViewModelResolver viewModelResolver)
		{
			_viewModelResolver = viewModelResolver;
			_activatedItems = new Stack<IViewModel>();
		}

		#endregion

		#region Protected methods

		protected T Get<T>() where T : IViewModel
		{
			return _viewModelResolver.Resolve<T>();
		}

		#endregion

		#region Public methods

		public void CloseItem(IViewModel parentViewModel)
		{
			DeactivateItem(parentViewModel, true);
			_activatedItems.Pop();
			if (_activatedItems.Any())
				base.ActivateItem(_activatedItems.Peek());
		}

		public void ActivateItem(IViewModel parentViewModel)
		{
			base.ActivateItem(parentViewModel);
			_activatedItems.Push(parentViewModel);
		}

		#endregion
	}
}