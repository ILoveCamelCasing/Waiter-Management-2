using WaiterManagement.Wpf.MVVM;
using WaiterManagement.Wpf.MVVM.Abstract;

namespace WaiterManagement.Manager.ViewModels.Waiter
{
	[UseView("TabView")]
	public class WaiterTabViewModel : ParentViewModelBase
	{
		#region Constructors
		public WaiterTabViewModel(IViewModelResolver viewModelResolver)
			: base(viewModelResolver)
		{
			DisplayName = "Waiters";

			Get<WaiterListViewModel>().ShowOn(this);
		}
		#endregion
	}
}
