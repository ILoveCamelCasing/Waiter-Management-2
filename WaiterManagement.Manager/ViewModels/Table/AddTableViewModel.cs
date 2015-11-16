using WaiterManagement.BLL.Commands.Base;
using WaiterManagement.BLL.Commands.Concrete;
using WaiterManagement.Wpf.MVVM;
using WaiterManagement.Wpf.MVVM.Abstract;

namespace WaiterManagement.Manager.ViewModels.Table
{
	[UseView("Table.TableView")]
	public class AddTableViewModel : ViewModelBase
	{
		#region Dependencies

		private readonly ICommandBus _commandBus;

		#endregion

		#region Private fields

		private string _title;
		private string _description;
		private string _userPassword;

		#endregion

		#region Public Properties

		public string Title
		{
			get { return _title; }
			set
			{
				_title = value;
				NotifyOfPropertyChange(() => CanSave);
			}
		}

		public string Description
		{
			get { return _description; }
			set
			{
				_description = value;
				NotifyOfPropertyChange(() => CanSave);
			}
		}

		public string UserPassword
		{
			get { return _userPassword; }
			set
			{
				_userPassword = value;
				NotifyOfPropertyChange(() => CanSave);
			}
		}

		public bool CanSave
		{
			get
			{
				return !string.IsNullOrEmpty(Title) && !string.IsNullOrEmpty(Description) && !string.IsNullOrEmpty(UserPassword);
			}
		}

		#endregion

		#region Constructor

		public AddTableViewModel(IViewModelResolver viewModelResolver, ICommandBus commandBus) : base(viewModelResolver)
		{
			_commandBus = commandBus;
		}

		#endregion

		#region Public methods

		public void Save()
		{
			_commandBus.SendCommand(new AddTableCommand(){Title = Title, Description = Description, Password = UserPassword});
			Close();
		}

		#endregion
	}
}