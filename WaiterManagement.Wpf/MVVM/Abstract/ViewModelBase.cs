using Caliburn.Micro;

namespace WaiterManagement.Wpf.MVVM.Abstract
{
	public class ViewModelBase : Conductor<object>.Collection.OneActive, IViewModel
	{
		public IParentViewModel ParentWindow { get; private set; }

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