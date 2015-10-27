using WaiterManagement.Wpf.MVVM;
using WaiterManagement.Wpf.MVVM.Abstract;

namespace WaiterManagement.Manager.ViewModels.Menu
{
	public class MenuListViewModel : ViewModelBase
	{
    #region Private Fields
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
    #endregion

    #region Constructors
    public MenuListViewModel(IViewModelResolver viewModelResolver) : base(viewModelResolver)
		{
      IsBusy = true;
		}
    #endregion

    #region Public Methods
    public void ManageCategories()
		{
			Get<CategoryListViewModel>().ShowOn(ParentWindow);
		}
    #endregion

    #region Overrides
    protected override void OnActivate()
    {
      base.OnActivate();
      //TODO...
      IsBusy = false;
    }
    #endregion
  }
}