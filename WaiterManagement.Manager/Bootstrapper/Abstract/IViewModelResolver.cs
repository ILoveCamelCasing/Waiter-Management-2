using WaiterManagement.Manager.ViewModels.Abstract;

namespace WaiterManagement.Manager.Bootstrapper.Abstract
{
	public interface IViewModelResolver
	{
		T Resolve<T>() where T : IViewModel;
	}
}