using WaiterManagement.Wpf.MVVM.Abstract;

namespace WaiterManagement.Manager.ViewModels
{
	public sealed class WaiterListViewModel : ParentViewModelBase
	{
		public WaiterListViewModel(IViewModelResolver viewModelResolver) 
			: base(viewModelResolver)
		{
			DisplayName = "Waiters";
		}
	}
}