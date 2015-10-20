using System.Collections.Generic;
using System.Linq;

namespace WaiterManagement.Wpf.MVVM.Abstract
{
	public abstract class ParentViewModelBase : ViewModelBase, IParentViewModel
	{

		#region Private Fields

		private readonly Stack<IViewModel> _activatedItems;

		#endregion

		#region Constructor

		protected ParentViewModelBase(IViewModelResolver viewModelResolver) : base(viewModelResolver)
		{
			_activatedItems = new Stack<IViewModel>();
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