using WaiterManagement.BLL.Commands.Base;
using WaiterManagement.BLL.Commands.Concrete;
using WaiterManagement.Manager.ViewModels.Abstract;
using WaiterManagement.Wpf.MVVM;
using WaiterManagement.Wpf.MVVM.Abstract;

namespace WaiterManagement.Manager.ViewModels
{
	[UseView("TableView")]
	public class AddTableViewModel : ViewModelBase , IAddTableViewModel
	{
		private readonly ICommandBus _commandBus;

		public string Title { get; set; }
		public string Description { get; set; }

		public AddTableViewModel(IViewModelResolver viewModelResolver, ICommandBus commandBus) : base(viewModelResolver)
		{
			_commandBus = commandBus;
		}

		public void Save()
		{
			_commandBus.SendCommand(new AddTableCommand(){Title = Title, Description = Description});
			Close();
		}
	}
}