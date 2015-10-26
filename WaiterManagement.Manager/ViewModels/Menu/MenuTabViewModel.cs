using WaiterManagement.Wpf.MVVM;
using WaiterManagement.Wpf.MVVM.Abstract;

namespace WaiterManagement.Manager.ViewModels.Menu
{
	[UseView("TabView")]
	public sealed class MenuTabViewModel : ParentViewModelBase
	{
		public MenuTabViewModel(IViewModelResolver viewModelResolver) : base(viewModelResolver)
		{
			DisplayName = "Menu";

			Get<MenuListViewModel>().ShowOn(this);
		}
	}
}