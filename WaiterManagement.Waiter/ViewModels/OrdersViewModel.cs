using Caliburn.Micro;
using WaiterManagement.Common.Models;
using WaiterManagement.Waiter.Bootstrapper;
using WaiterManagement.Wpf.MVVM.Abstract;

namespace WaiterManagement.Waiter.ViewModels
{
	public class OrdersViewModel : ViewModelBase
	{
		#region Private Fields
		private OrderModel _selectedOrder;
		#endregion

		#region Constructors
		public OrdersViewModel(IViewModelResolver viewModelResolver, IWaiterAppSubscriber waiterApp) : base(viewModelResolver)
		{
			Orders = new BindableCollection<OrderModel>();

			waiterApp.NotifyNewOrder += order =>
			{
				Orders.Add(order);
			};
		}
		#endregion

		#region Public Properties
		public OrderModel SelectedOrder
		{
			get { return _selectedOrder; }
			set
			{
				_selectedOrder = value;
				NotifyOfPropertyChange(() => SelectedOrder);
			}
		}

		public BindableCollection<OrderModel> Orders
		{ get; private set; }
		#endregion
	}
}