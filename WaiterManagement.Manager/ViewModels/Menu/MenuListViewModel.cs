using Caliburn.Micro;
using WaiterManagement.BLL.Commands.Base;
using WaiterManagement.BLL.Commands.Concrete;
using WaiterManagement.Common.Views;
using WaiterManagement.Common.Views.Abstract;
using WaiterManagement.Wpf.MVVM.Abstract;

namespace WaiterManagement.Manager.ViewModels.Menu
{
	public class MenuListViewModel : ViewModelBase
	{
		#region Dependencies

		private readonly IViewProvider _viewProvider;
		private readonly ICommandBus _commandBus;

		#endregion

		#region Private fields

		private bool _isBusy;
		private MenuItemView _selectedElement;

		#endregion

		#region Public Properties

		public MenuItemView SelectedElement
		{
			get { return _selectedElement; }
			set
			{
				_selectedElement = value;
				NotifyOfPropertyChange(() => CanDeleteMenuItem);
			}
		}
		public BindableCollection<MenuItemView> Elements { get; private set; }

		public bool CanDeleteMenuItem
		{
			get { return SelectedElement != null; }
		}

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

		#region Constructor

		public MenuListViewModel(IViewModelResolver viewModelResolver, IViewProvider viewProvider, ICommandBus commandBus)
			: base(viewModelResolver)
		{
			_viewProvider = viewProvider;
			_commandBus = commandBus;

			Elements = new BindableCollection<MenuItemView>();
		}

		#endregion

		#region Public methods

		public void AddMenuItem()
		{
			Get<AddMenuItemViewModel>().ShowOn(ParentWindow);
		}
    #endregion

    #region Public Methods
		public void ManageCategories()
		{
			Get<CategoryListViewModel>().ShowOn(ParentWindow);
		}

		public void DeleteMenuItem()
		{
			_commandBus.SendCommand(new DeleteMenuItemCommand() { Id = SelectedElement.MenuItemId });
			OnActivate();
		}

		public void EditMenuItem()
		{
			var vm = Get<EditMenuItemViewModel>();
			vm.Initialize(SelectedElement);
			vm.ShowOn(ParentWindow);
		}

		#endregion

		#region Ovverrides

		protected override void OnActivate()
		{
			base.OnActivate();

			Elements.Clear();
			Elements.AddRange(_viewProvider.Get<MenuItemView>());

			NotifyOfPropertyChange(() => Elements);
			IsBusy = false;
		}

		#endregion
	}
}