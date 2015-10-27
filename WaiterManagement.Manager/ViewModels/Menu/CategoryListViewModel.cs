using Caliburn.Micro;
using WaiterManagement.BLL.Commands.Base;
using WaiterManagement.BLL.Commands.Concrete;
using WaiterManagement.Common.Views;
using WaiterManagement.Common.Views.Abstract;
using WaiterManagement.Wpf.MVVM.Abstract;

namespace WaiterManagement.Manager.ViewModels.Menu
{
	public class CategoryListViewModel: ViewModelBase
	{
		#region Dependencies

		private readonly IViewProvider _viewProvider;
		private readonly ICommandBus _commandBus;

		#endregion

		#region Private fields

		private CategoryView _selectedElement;

		#endregion

		#region Public Properties

		public CategoryView SelectedElement
		{
			get { return _selectedElement; }
			set
			{
				_selectedElement = value;
				NotifyOfPropertyChange(() => CanDeleteCategory);
			}
		}
		public BindableCollection<CategoryView> Elements { get; private set; }

		public bool CanDeleteCategory
		{
			get { return SelectedElement != null; }
		}

		#endregion

		#region Constructor

		public CategoryListViewModel(IViewModelResolver viewModelResolver, IViewProvider viewProvider, ICommandBus commandBus)
			: base(viewModelResolver)
		{
			_viewProvider = viewProvider;
			_commandBus = commandBus;

			Elements = new BindableCollection<CategoryView>();
		}

		#endregion

		#region Public methods

		public void AddCategory()
		{
			Get<AddCategoryViewModel>().ShowOn(ParentWindow);
		}
		
		public void DeleteCategory()
		{
			_commandBus.SendCommand(new DeleteCategoryCommand() { Id = SelectedElement.CategoryId });
			OnActivate();
		}

		public void BackToMenuItems()
		{
			Close();
		}

		public void EditCategory()
		{
			var vm = Get<EditCategoryViewModel>();
			vm.Initialize(SelectedElement);
			vm.ShowOn(ParentWindow);
		}

		#endregion

		#region Ovverrides

		protected override void OnActivate()
		{
			base.OnActivate();

			Elements.Clear();
			Elements.AddRange(_viewProvider.Get<CategoryView>());

			NotifyOfPropertyChange(() => Elements);
		}

		#endregion
	}
}