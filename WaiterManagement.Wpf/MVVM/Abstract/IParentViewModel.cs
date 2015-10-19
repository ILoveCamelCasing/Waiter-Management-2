namespace WaiterManagement.Wpf.MVVM.Abstract
{
	public interface IParentViewModel : IViewModel
	{
		void ActivateItem(IViewModel parentViewModel);
		void CloseItem(IViewModel parentViewModel);
	}
}