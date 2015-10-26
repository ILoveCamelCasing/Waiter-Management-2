using WaiterManagement.BLL.Commands.Base;
using WaiterManagement.BLL.Commands.Concrete;
using WaiterManagement.Common.Views;
using WaiterManagement.Wpf.MVVM;
using WaiterManagement.Wpf.MVVM.Abstract;

namespace WaiterManagement.Manager.ViewModels.Table
{
	[UseView("Table.TableView")]
	public class EditTableViewModel: ViewModelBase
	{
		private readonly ICommandBus _commandBus;

		private int _id;

		public string Title { get; set; }
		public string Description { get; set; }

		public EditTableViewModel(IViewModelResolver viewModelResolver, ICommandBus commandBus)
			: base(viewModelResolver)
		{
			_commandBus = commandBus;
		}

		public void Save()
		{
			_commandBus.SendCommand(new EditTableCommand(){Id = _id,Title = Title, Description = Description});
			Close();
		}

		public void Initialize(TableView table)
		{
			_id = table.TableId;
			Title = table.Title;
			Description = table.Description;
		}
	}
}