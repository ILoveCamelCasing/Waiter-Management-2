using WaiterManagement.Wpf.MVVM.Abstract;

namespace WaiterManagement.Table.ViewModels
{
	public sealed class MainWindowViewModel : ParentViewModelBase
	{
		public MainWindowViewModel(IViewModelResolver viewModelResolver) 
			: base(viewModelResolver)
		{
			DisplayName = "Table application";

			Get<AccessViewModel>().ShowOn(this);
		}
	}
}