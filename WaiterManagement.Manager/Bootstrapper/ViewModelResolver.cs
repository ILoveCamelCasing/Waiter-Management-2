using Caliburn.Micro;
using WaiterManagement.Manager.Bootstrapper.Abstract;
using WaiterManagement.Manager.ViewModels.Abstract;

namespace WaiterManagement.Manager.Bootstrapper
{
	public sealed class ViewModelResolver : IViewModelResolver
	{
		public T Resolve<T>() where T : IViewModel
		{
			return IoC.Get<T>();
		}
	}
}