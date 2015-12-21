using WaiterManagement.Wpf.MVVM.Abstract;

namespace WaiterManagement.Waiter.ViewModels
{
	public sealed class MainWindowViewModel : ParentViewModelBase
	{
		public MainWindowViewModel(IViewModelResolver viewModelResolver) 
			: base(viewModelResolver)
		{
			DisplayName = "Waiter application";

			Get<AccessViewModel>().ShowOn(this);
		}
	}
}