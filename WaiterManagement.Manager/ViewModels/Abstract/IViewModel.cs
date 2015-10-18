namespace WaiterManagement.Manager.ViewModels.Abstract
{
	public interface IViewModel
	{
		void ShowOn(IParentViewModel parentParentViewModel);
		IParentViewModel ParentWindow { get; }
		void Close();
	}
}