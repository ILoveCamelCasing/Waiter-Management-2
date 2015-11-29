using System.Windows;
using System.Windows.Controls;
using Caliburn.Micro;

namespace WaiterManagement.Wpf.Controls
{
	public static class PasswordBoxHelper
	{
		//public static void Register()
		//{
		//	ConventionManager.AddElementConvention<PasswordBox>(
		//	PasswordBoxHelper.BoundPasswordProperty,
		//	"Password",
		//	"PasswordChanged");
		//}

		private static bool _updating = false;

		public static readonly DependencyProperty BoundPasswordProperty =
			DependencyProperty.RegisterAttached("BoundPassword",
				typeof(string),
				typeof(PasswordBoxHelper),
				new FrameworkPropertyMetadata(string.Empty, OnBoundPasswordChanged));

		public static string GetBoundPassword(DependencyObject d)
		{
			return (string)d.GetValue(BoundPasswordProperty);
		}

		public static void SetBoundPassword(DependencyObject d, string value)
		{
			d.SetValue(BoundPasswordProperty, value);
		}

		private static void OnBoundPasswordChanged(
			DependencyObject d,
			DependencyPropertyChangedEventArgs e)
		{
			PasswordBox password = d as PasswordBox;
			if (password != null)
			{
				// Disconnect the handler while we're updating.
				password.PasswordChanged -= PasswordChanged;
			}

			if (e.NewValue != null)
			{
				if (!_updating)
				{
					password.Password = e.NewValue.ToString();
				}
			}
			else
			{
				password.Password = string.Empty;
			}
			// Now, reconnect the handler.
			password.PasswordChanged += PasswordChanged;
		}

		private static void PasswordChanged(object sender, RoutedEventArgs e)
		{
			PasswordBox password = sender as PasswordBox;
			_updating = true;
			SetBoundPassword(password, password.Password);
			_updating = false;
		}

	}
}