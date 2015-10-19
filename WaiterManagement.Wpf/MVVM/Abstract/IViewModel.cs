namespace WaiterManagement.Wpf.MVVM.Abstract
{
	public interface IViewModel
	{
		IParentViewModel ParentWindow { get; }
		void ShowOn(IParentViewModel parentParentViewModel);
		void Close();
	}
}