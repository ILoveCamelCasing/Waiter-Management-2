using Caliburn.Micro;
using WaiterManagement.Common.Views;
using WaiterManagement.Common.Views.Abstract;
using WaiterManagement.Manager.ViewModels.Abstract;
using WaiterManagement.Wpf.MVVM.Abstract;

namespace WaiterManagement.Manager.ViewModels
{
	public sealed class TableListViewModel : ViewModelBase, ITableListViewModel
	{
		#region Dependencies

		private readonly IViewProvider _viewProvider;

		#endregion

		#region Public Properties

		public TableView SelectedElement { get; set; }
		public BindableCollection<TableView> Elements { get; private set; }

		#endregion

		#region Constructors

		public TableListViewModel(IViewModelResolver viewModelResolver, IViewProvider viewProvider) : base(viewModelResolver)
		{
			_viewProvider = viewProvider;
			DisplayName = "Tables";

			Elements = new BindableCollection<TableView>();
		}

		#endregion

		#region Public methods

		public void AddTable()
		{
			Get<IAddTableViewModel>().ShowOn(ParentWindow);
		}

		public void EditTable()
		{
			var vm = Get<IEditTableViewModel>();
			vm.Initialize(SelectedElement);
			vm.ShowOn(ParentWindow);

		}

		#endregion

		#region Ovverides

		protected override void OnActivate()
		{
			base.OnActivate();

			Elements.Clear();
			Elements.AddRange(_viewProvider.Get<TableView>());

			NotifyOfPropertyChange(() => Elements);
		}

		#endregion
	}
}