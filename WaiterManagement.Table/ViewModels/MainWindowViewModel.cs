using System.Windows;
using WaiterManagement.Wpf.MVVM.Abstract;

namespace WaiterManagement.Table.ViewModels
{
	public sealed class MainWindowViewModel : ParentViewModelBase
	{
		#region Constructors
		public MainWindowViewModel(IViewModelResolver viewModelResolver)
			: base(viewModelResolver)
		{
			DisplayName = "Table application";

			Get<AccessViewModel>().ShowOn(this);
		}
		#endregion

		#region Public Properties
		public Visibility LogoutButtonVisible => ActiveItem != null
			? ActiveItem.GetType() == typeof(AccessViewModel)
				? Visibility.Collapsed
				: Visibility.Visible
			: Visibility.Hidden;
		#endregion

		#region Public Methods

		public void Logout()
		{
			CloseAll();

			Get<AccessViewModel>().ShowOn(this);
		}
		#endregion
	}
}