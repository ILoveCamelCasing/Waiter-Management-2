using WaiterManagement.Wpf.MVVM;
using WaiterManagement.Wpf.MVVM.Abstract;

namespace WaiterManagement.Manager.ViewModels.Table
{
	[UseView("TabView")]
	public sealed class TableTabViewModel : ParentViewModelBase
	{
		public TableTabViewModel(IViewModelResolver viewModelResolver) : base(viewModelResolver)
		{
			DisplayName = "Tables";

			Get<TableListViewModel>().ShowOn(this);
		}
	}
}