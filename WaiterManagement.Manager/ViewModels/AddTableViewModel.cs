using WaiterManagement.Manager.ViewModels.Abstract;
using WaiterManagement.Wpf.MVVM;
using WaiterManagement.Wpf.MVVM.Abstract;

namespace WaiterManagement.Manager.ViewModels
{
	[UseView("TableView")]
	public class AddTableViewModel : ViewModelBase , IAddTableViewModel
	{
		public string Title { get; set; }
		public string Description { get; set; }

		public AddTableViewModel(IViewModelResolver viewModelResolver) : base(viewModelResolver)
		{
		}











	}
}