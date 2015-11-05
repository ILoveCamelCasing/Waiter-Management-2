using WaiterManagement.Wpf.MVVM.Abstract;

namespace WaiterManagement.Table.ViewModels
{
	public sealed class MainWindowViewModel : ViewModelBase
	{
		public MainWindowViewModel(IViewModelResolver viewModelResolver) 
			: base(viewModelResolver)
		{
			DisplayName = "Table application";
		}
	}
}