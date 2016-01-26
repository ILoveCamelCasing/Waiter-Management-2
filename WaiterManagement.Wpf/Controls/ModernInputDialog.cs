using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using FirstFloor.ModernUI.Presentation;
using FirstFloor.ModernUI.Windows.Controls;

namespace WaiterManagement.Wpf.Controls
{
	public class ModernInputDialog : ModernDialog
	{
		#region Private Fields
		private static TextBox textBox;
		#endregion

		#region Public Methods
		public static ModernInputDialogMessageBoxResult ShowInputMessage(string text, string title, MessageBoxButton button, Window owner = null)
		{
			var dlg = new ModernDialog
			{
				Title = title,
				Content = CreateContent(text),
				MinHeight = 0,
				MinWidth = 0,
				MaxHeight = 480,
				MaxWidth = 640,
			};
			if (owner != null)
			{
				dlg.Owner = owner;
			}

			dlg.Buttons = GetButtons(dlg, button);
			dlg.ShowDialog();

			return new ModernInputDialogMessageBoxResult() { MessageBoxResult = dlg.MessageBoxResult, Input = textBox.Text};
		}
		#endregion

		#region Private Methods
		//Skopiowane z kodu źródłowego
		private static IEnumerable<Button> GetButtons(ModernDialog owner, MessageBoxButton button)
		{
			if (button == MessageBoxButton.OK)
			{
				yield return owner.OkButton;
			}
			else if (button == MessageBoxButton.OKCancel)
			{
				yield return owner.OkButton;
				yield return owner.CancelButton;
			}
			else if (button == MessageBoxButton.YesNo)
			{
				yield return owner.YesButton;
				yield return owner.NoButton;
			}
			else if (button == MessageBoxButton.YesNoCancel)
			{
				yield return owner.YesButton;
				yield return owner.NoButton;
				yield return owner.CancelButton;
			}
		}

		private static object CreateContent(string text)
		{
			var grid = new Grid();

			grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
			grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(125) });

			grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });
			grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(400) });

			var bbCodeBlock = new BBCodeBlock { BBCode = text, Margin = new Thickness(0, 2, 0, 8) };
			grid.Children.Add(bbCodeBlock);
			textBox = new TextBox() { Margin = new Thickness(2, 0, 0, 8), TextWrapping = TextWrapping.Wrap, AcceptsReturn = true, VerticalAlignment = VerticalAlignment.Stretch, HorizontalAlignment = HorizontalAlignment.Stretch };

			grid.Children.Add(textBox);

			Grid.SetRow(bbCodeBlock, 0);
			Grid.SetColumn(bbCodeBlock, 0);

			Grid.SetRow(textBox, 0);
			Grid.SetRowSpan(textBox, 2);
			Grid.SetColumn(textBox, 1);

			return grid;
		}
		#endregion
	}

	public class ModernInputDialogMessageBoxResult
	{
		public MessageBoxResult MessageBoxResult { get; set; }
		public string Input { get; set; }
	}
}
