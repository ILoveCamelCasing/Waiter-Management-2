using WaiterManagement.Wpf.MVVM.Abstract;

namespace WaiterManagement.Waiter.ViewModels
{
	public class NotifyViewModel : ViewModelBase
	{
		public string Message { get; private set; }

		public NotifyViewModel(IViewModelResolver viewModelResolver) : base(viewModelResolver)
		{
		}

		public void Initialize(string message)
		{
			Message = message;
		}

		
	}
}