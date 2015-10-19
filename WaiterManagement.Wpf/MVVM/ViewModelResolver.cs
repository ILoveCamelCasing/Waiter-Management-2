using Caliburn.Micro;
using WaiterManagement.Wpf.MVVM.Abstract;

namespace WaiterManagement.Wpf.MVVM
{
	public sealed class ViewModelResolver : IViewModelResolver
	{
		public T Resolve<T>() where T : IViewModel
		{
			return IoC.Get<T>();
		}
	}
}