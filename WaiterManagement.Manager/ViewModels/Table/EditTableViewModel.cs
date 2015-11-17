using WaiterManagement.BLL.Commands.Base;
using WaiterManagement.BLL.Commands.Concrete;
using WaiterManagement.Common.Views;
using WaiterManagement.Wpf.MVVM.Abstract;

namespace WaiterManagement.Manager.ViewModels.Table
{
	public class EditTableViewModel: ViewModelBase
	{
		private readonly ICommandBus _commandBus;

		private int _id;
		private string _login;

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

		public void ChangePassword()
		{
			Close();

			var changePasswordViewModel = Get<ChangePasswordViewModel>();
			changePasswordViewModel.Initialize(_id, _login, typeof(Common.Entities.Table));
			changePasswordViewModel.ShowOn(ParentWindow);
		}

		public void Initialize(TableView table)
		{
			_id = table.TableId;
			_login = table.Login;

			Title = table.Title;
			Description = table.Description;
		}
	}
}