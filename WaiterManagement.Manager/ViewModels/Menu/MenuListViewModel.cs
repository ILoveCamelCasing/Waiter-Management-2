using WaiterManagement.Wpf.MVVM;
using WaiterManagement.Wpf.MVVM.Abstract;

namespace WaiterManagement.Manager.ViewModels.Menu
{
	public class MenuListViewModel : ViewModelBase
	{
		public MenuListViewModel(IViewModelResolver viewModelResolver) : base(viewModelResolver)
		{
		}

		public void ManageCategories()
		{
			Get<CategoryListViewModel>().ShowOn(ParentWindow);
		}
	}
}