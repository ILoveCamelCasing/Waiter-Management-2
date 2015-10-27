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
    #region Dependencies
    private readonly IViewProvider _viewProvider;
		private readonly ICommandBus _commandBus;
    #endregion

    #region Private Fields
    private CategoryView _selectedElement;

    private bool _isBusy;
    #endregion

    #region Public Properties
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

    #region Constructors
    public CategoryListViewModel(IViewModelResolver viewModelResolver, IViewProvider viewProvider, ICommandBus commandBus)
			: base(viewModelResolver)
		{
			_viewProvider = viewProvider;
			_commandBus = commandBus;

			Elements = new BindableCollection<CategoryView>();
      IsBusy = true;
		}
    #endregion

    #region Public Methods
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
    #endregion

    #region Overrides
    protected override async void OnActivate()
		{
			base.OnActivate();

			Elements.Clear();
			Elements.AddRange(await _viewProvider.GetAsync<CategoryView>());
      IsBusy = false;
		}
    #endregion
  }
}