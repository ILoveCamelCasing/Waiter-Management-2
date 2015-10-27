using Caliburn.Micro;
using WaiterManagement.BLL.Commands.Base;
using WaiterManagement.BLL.Commands.Concrete;
using WaiterManagement.Common.Views;
using WaiterManagement.Common.Views.Abstract;
using WaiterManagement.Manager.ViewModels.Table;
using WaiterManagement.Wpf.MVVM.Abstract;

namespace WaiterManagement.Manager.ViewModels.Menu
{
	public class CategoryListViewModel: ViewModelBase
	{
		private readonly IViewProvider _viewProvider;
		private readonly ICommandBus _commandBus;

		#region Public Properties

		private CategoryView _selectedElement;

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

		#endregion

		public CategoryListViewModel(IViewModelResolver viewModelResolver, IViewProvider viewProvider, ICommandBus commandBus)
			: base(viewModelResolver)
		{
			_viewProvider = viewProvider;
			_commandBus = commandBus;

			Elements = new BindableCollection<CategoryView>();
		}

		public void AddCategory()
		{
			Get<AddCategoryViewModel>().ShowOn(ParentWindow);
		}
		public void DeleteCategory()
		{
			_commandBus.SendCommand(new DeleteCategoryCommand() { Id = SelectedElement.CategoryId });
			OnActivate();
		}

		public bool CanDeleteCategory
		{
			get { return SelectedElement != null; }
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

		protected override void OnActivate()
		{
			base.OnActivate();

			Elements.Clear();
			Elements.AddRange(_viewProvider.Get<CategoryView>());

			NotifyOfPropertyChange(() => Elements);
		}
	}
}