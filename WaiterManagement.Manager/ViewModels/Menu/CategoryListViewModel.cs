using WaiterManagement.Wpf.MVVM.Abstract;

namespace WaiterManagement.Manager.ViewModels.Menu
{
	public class CategoryListViewModel: ViewModelBase
	{
		public CategoryListViewModel(IViewModelResolver viewModelResolver)
			: base(viewModelResolver)
		{
		}

		public void AddCategory()
		{
			Get<AddCategoryViewModel>().ShowOn(ParentWindow);
		}

		public void BackToMenuItems()
		{
			Close();
		}
	}
}