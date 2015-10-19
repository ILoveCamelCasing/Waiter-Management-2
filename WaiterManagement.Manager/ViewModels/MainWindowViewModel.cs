using WaiterManagement.Manager.ViewModels.Abstract;
using WaiterManagement.Wpf.MVVM.Abstract;

namespace WaiterManagement.Manager.ViewModels
{
	public sealed class MainWindowViewModel : ViewModelBase, IMainWindowViewModel
	{
		public MainWindowViewModel(IMenuListViewModel menuListViewModel, ITableListViewModel tableListViewModel, IWaiterListViewModel waiterListViewModel)
		{
			DisplayName = "Waiter manager";

			Items.Add(menuListViewModel);
			Items.Add(tableListViewModel);
			Items.Add(waiterListViewModel);
		}
	}
}