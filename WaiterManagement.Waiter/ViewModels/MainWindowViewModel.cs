using System.Runtime.CompilerServices;
using System.Windows;
using WaiterManagement.Wpf.MVVM.Abstract;

namespace WaiterManagement.Waiter.ViewModels
{
	public sealed class MainWindowViewModel : ParentViewModelBase
	{
		#region Private Fields
		private Visibility _logoutButtonVisible;
		#endregion

		#region Constructors
		public MainWindowViewModel(IViewModelResolver viewModelResolver) 
			: base(viewModelResolver)
		{
			DisplayName = "Waiter application";
			Get<AccessViewModel>().ShowOn(this);
		}
		#endregion

		#region Public Properties
		public Visibility LogoutButtonVisible => ActiveItem != null
			? ActiveItem.GetType() == typeof (AccessViewModel)
				? Visibility.Collapsed
				: Visibility.Visible
			: Visibility.Hidden;
		#endregion

		#region Public Methods
		public void Logout()
		{
			//TODO: Przerwanie zamówień, etc
			CloseAll();

			Get<AccessViewModel>().ShowOn(this);
		}
		#endregion
	}
}