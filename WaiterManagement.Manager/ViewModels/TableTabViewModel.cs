using WaiterManagement.Manager.ViewModels.Abstract;
using WaiterManagement.Wpf.MVVM;
using WaiterManagement.Wpf.MVVM.Abstract;

namespace WaiterManagement.Manager.ViewModels
{
	[UseView("TabView")]
	public sealed class TableTabViewModel : ParentViewModelBase, ITableTabViewModel
	{
		public TableTabViewModel(IViewModelResolver viewModelResolver) : base(viewModelResolver)
		{
			DisplayName = "Tables";

			Get<ITableListViewModel>().ShowOn(this);
		}
	}
}