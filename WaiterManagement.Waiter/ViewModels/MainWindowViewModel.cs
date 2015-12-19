using WaiterManagement.Common.Apps;
using WaiterManagement.Waiter.Bootstrapper;
using WaiterManagement.Wpf.MVVM.Abstract;

namespace WaiterManagement.Waiter.ViewModels
{
	public sealed class MainWindowViewModel : ParentViewModelBase
	{
		public MainWindowViewModel(IViewModelResolver viewModelResolver, IWaiterAppSubscriber waiterApp) 
			: base(viewModelResolver)
		{
			DisplayName = "Waiter application";

			Get<AccessViewModel>().ShowOn(this);

			waiterApp.NotifyNewOrder += order =>
			{
				var notifyWindow = Get<NotifyViewModel>();
				notifyWindow.Initialize(string.Format("New order from {0} table", order.TableTitle));
				notifyWindow.ShowOn(this);
			};
		}
	}
}