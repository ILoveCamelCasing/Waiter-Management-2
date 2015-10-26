using WaiterManagement.Manager.ViewModels.Menu;
using WaiterManagement.Manager.ViewModels.Table;
using WaiterManagement.Wpf.MVVM.Abstract;

namespace WaiterManagement.Manager.ViewModels
{
	public sealed class MainWindowViewModel : ViewModelBase
	{
		public MainWindowViewModel(IViewModelResolver viewModelResolver)
			: base(viewModelResolver)
		{
			DisplayName = "Waiter manager";

			Items.Add(Get<TableTabViewModel>());
			Items.Add(Get<MenuTabViewModel>());
			//Items.Add(waiterListViewModel);
		}
	}
}