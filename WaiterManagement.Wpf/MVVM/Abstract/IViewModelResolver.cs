namespace WaiterManagement.Wpf.MVVM.Abstract
{
	public interface IViewModelResolver
	{
		T Resolve<T>() where T : IViewModel;
	}
}