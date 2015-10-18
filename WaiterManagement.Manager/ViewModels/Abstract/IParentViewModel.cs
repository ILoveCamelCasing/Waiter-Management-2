namespace WaiterManagement.Manager.ViewModels.Abstract
{
	public interface IParentViewModel : IViewModel
	{
		void ActivateItem(IViewModel parentViewModel);
		void CloseItem(IViewModel parentViewModel);
	}
}