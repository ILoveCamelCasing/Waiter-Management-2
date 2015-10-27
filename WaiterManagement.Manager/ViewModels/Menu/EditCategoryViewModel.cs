using WaiterManagement.BLL.Commands.Base;
using WaiterManagement.BLL.Commands.Concrete;
using WaiterManagement.Common.Views;
using WaiterManagement.Wpf.MVVM;
using WaiterManagement.Wpf.MVVM.Abstract;

namespace WaiterManagement.Manager.ViewModels.Menu
{
	[UseView("Menu.CategoryView")]
	public class EditCategoryViewModel: ViewModelBase
	{
		private readonly ICommandBus _commandBus;

		private int _id;

		public string Title { get; set; }
		public string Description { get; set; }

		public EditCategoryViewModel(IViewModelResolver viewModelResolver, ICommandBus commandBus)
			: base(viewModelResolver)
		{
			_commandBus = commandBus;
		}

		public void Save()
		{
			_commandBus.SendCommand(new EditCategoryCommand(){Id = _id,Title = Title, Description = Description});
			Close();
		}

		public void Initialize(CategoryView category)
		{
			_id = category.CategoryId;
			Title = category.Title;
			Description = category.Description;
		}
	}
}