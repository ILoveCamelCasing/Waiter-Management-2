using WaiterManagement.Manager.ViewModels.Abstract;
using WaiterManagement.Wpf.MVVM.Abstract;

namespace WaiterManagement.Manager.ViewModels
{
	public sealed class WaiterListViewModel : ParentViewModelBase ,IWaiterListViewModel
	{
		public WaiterListViewModel(IViewModelResolver viewModelResolver) 
			: base(viewModelResolver)
		{
			DisplayName = "Waiters";
		}
	}
}