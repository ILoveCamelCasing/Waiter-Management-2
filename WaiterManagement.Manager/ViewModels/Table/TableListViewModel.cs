using Caliburn.Micro;
using WaiterManagement.BLL.Commands.Base;
using WaiterManagement.BLL.Commands.Concrete;
using WaiterManagement.BLL.Commands.Concrete.ManagerCommands;
using WaiterManagement.Common.Views;
using WaiterManagement.Common.Views.Abstract;
using WaiterManagement.Wpf.MVVM.Abstract;

namespace WaiterManagement.Manager.ViewModels.Table
{
	public sealed class TableListViewModel : ViewModelBase
	{
		#region Dependencies

		private readonly IViewProvider _viewProvider;
		private readonly ICommandBus _commandBus;

		#endregion

		#region Private Fields
		private bool _isBusy;

		private TableView _selectedElement;
		#endregion

		#region Public Properties

		public TableView SelectedElement
		{
			get { return _selectedElement; }
			set
			{
				_selectedElement = value;
				NotifyOfPropertyChange(() => CanDeleteTable);
			}
		}
		public BindableCollection<TableView> Elements { get; private set; }

		public bool IsBusy
		{
			get
			{
				return _isBusy;
			}
			set
			{
				_isBusy = value;
				NotifyOfPropertyChange(() => IsBusy);
			}
		}

		#endregion

		#region Constructors

		public TableListViewModel(IViewModelResolver viewModelResolver, IViewProvider viewProvider, ICommandBus commandBus)
			: base(viewModelResolver)
		{
			_viewProvider = viewProvider;
			_commandBus = commandBus;
			DisplayName = "Tables";

			Elements = new BindableCollection<TableView>();
			IsBusy = true;
		}

		#endregion

		#region Public methods

		public void AddTable()
		{
			Get<AddTableViewModel>().ShowOn(ParentWindow);
		}

		public void EditTable()
		{
			var vm = Get<EditTableViewModel>();
			vm.Initialize(SelectedElement);
			vm.ShowOn(ParentWindow);
		}

		public void DeleteTable()
		{
			_commandBus.SendCommand(new DeleteTableCommand() { Id = SelectedElement.TableId });
			OnActivate();
		}

		public bool CanDeleteTable
		{
			get { return SelectedElement != null; }
		}

		#endregion

		#region Ovverides

		protected override async void OnActivate()
		{
			base.OnActivate();

			Elements.Clear();
			Elements.AddRange(await _viewProvider.GetAsync<TableView>());
			IsBusy = false;
		}

		#endregion
	}
}