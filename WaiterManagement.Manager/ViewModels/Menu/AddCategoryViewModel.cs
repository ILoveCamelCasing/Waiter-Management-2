using WaiterManagement.BLL.Commands.Base;
using WaiterManagement.BLL.Commands.Concrete;
using WaiterManagement.BLL.Commands.Concrete.ManagerCommands;
using WaiterManagement.Wpf.MVVM;
using WaiterManagement.Wpf.MVVM.Abstract;

namespace WaiterManagement.Manager.ViewModels.Menu
{
	[UseView("Menu.CategoryView")]
	public class AddCategoryViewModel : ViewModelBase
	{
		private readonly ICommandBus _commandBus;

		public string Title { get; set; }
		public string Description { get; set; }

		public AddCategoryViewModel(IViewModelResolver viewModelResolver, ICommandBus commandBus)
			: base(viewModelResolver)
		{
			_commandBus = commandBus;
		}

		public void Save()
		{
			_commandBus.SendCommand(new AddCategoryCommand(){Title = Title, Description = Description});
			Close();
		}
	}
}