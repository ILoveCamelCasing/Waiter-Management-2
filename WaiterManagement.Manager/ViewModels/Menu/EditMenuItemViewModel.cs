using System.Linq;
using Caliburn.Micro;
using WaiterManagement.BLL.Commands.Base;
using WaiterManagement.BLL.Commands.Concrete;
using WaiterManagement.BLL.Commands.Concrete.ManagerCommands;
using WaiterManagement.Common.Views;
using WaiterManagement.Common.Views.Abstract;
using WaiterManagement.Wpf.MVVM;
using WaiterManagement.Wpf.MVVM.Abstract;

namespace WaiterManagement.Manager.ViewModels.Menu
{
	[UseView("Menu.MenuItemView")]
	public class EditMenuItemViewModel: ViewModelBase
	{
		#region Dependencies

		private readonly IViewProvider _viewProvider;
		private readonly ICommandBus _commandBus;

		#endregion

		#region Private fields

		private int _id;
		private string _title;
		private string _description;
		private CategoryView _selectedCategory;

		#endregion

		#region Public properties

		public string Title { get { return _title; } set { _title = value; NotifyOfPropertyChange(() => CanSave); } }
		public string Description { get { return _description; } set { _description = value; NotifyOfPropertyChange(() => CanSave); } }
		public CategoryView SelectedCategory { get { return _selectedCategory; } set { _selectedCategory = value; NotifyOfPropertyChange(() => CanSave); } }

		public BindableCollection<CategoryView> Categories { get; private set; }
		public bool CanSave
		{
			get
			{
				return SelectedCategory != null && !string.IsNullOrEmpty(Title) && !string.IsNullOrEmpty(Description);
			}
		}

		#endregion

		#region Constructor

		public EditMenuItemViewModel(IViewModelResolver viewModelResolver, IViewProvider viewProvider, ICommandBus commandBus)
			: base(viewModelResolver)
		{
			_viewProvider = viewProvider;
			_commandBus = commandBus;

			Categories = new BindableCollection<CategoryView>();
		}

		#endregion

		#region Public methods

		public void Save()
		{
			_commandBus.SendCommand(new EditMenuItemCommand() { Id= _id, Title = Title, Description = Description, CategoryId = SelectedCategory.CategoryId });
			Close();
		}

		public void Initialize(MenuItemView menuItem)
		{
			Categories.Clear();
			Categories.AddRange(_viewProvider.Get<CategoryView>());

			_id = menuItem.MenuItemId;
			Title = menuItem.Title;
			Description = menuItem.Description;
			SelectedCategory = Categories.First(x => x.CategoryId == menuItem.CategoryId);
		}

		#endregion
	}
}