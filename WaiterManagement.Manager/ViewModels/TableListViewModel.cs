using WaiterManagement.Manager.ViewModels.Abstract;
using WaiterManagement.Wpf.MVVM.Abstract;

namespace WaiterManagement.Manager.ViewModels
{
	public sealed class TableListViewModel : ViewModelBase, ITableListViewModel
	{
		public TableListViewModel(IViewModelResolver viewModelResolver) : base(viewModelResolver)
		{
			DisplayName = "Tables";
		}

		public void AddTable()
		{
			Get<IAddTableViewModel>().ShowOn(ParentWindow);
		}
	}
}