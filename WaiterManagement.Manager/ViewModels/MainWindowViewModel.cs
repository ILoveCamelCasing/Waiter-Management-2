using WaiterManagement.Manager.Bootstrapper.Abstract;
using WaiterManagement.Manager.ViewModels.Abstract;

namespace WaiterManagement.Manager.ViewModels
{
	public class MainWindowViewModel : ParentViewModelBase
	{
		public MainWindowViewModel(IViewModelResolver dependencyResolver)
			: base(dependencyResolver)
		{

		}
	}
}