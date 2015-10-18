using Caliburn.Micro;

namespace WaiterManagement.Manager.ViewModels.Abstract
{
	public class ViewModelBase : Conductor<object>, IViewModel
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