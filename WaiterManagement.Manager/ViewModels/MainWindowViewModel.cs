using WaiterManagement.Manager.ViewModels.Abstract;
using WaiterManagement.Wpf.MVVM.Abstract;

namespace WaiterManagement.Manager.ViewModels
{
	public sealed class MainWindowViewModel : ViewModelBase, IMainWindowViewModel
	{
		public MainWindowViewModel(IViewModelResolver viewModelResolver)
			: base(viewModelResolver)
		{
			DisplayName = "Waiter manager";

			Items.Add(Get<ITableTabViewModel>());
			//Items.Add(tableTabViewModel);
			//Items.Add(waiterListViewModel);
		}
	}
}