using WaiterManagement.Common.Views;
using WaiterManagement.Wpf.MVVM.Abstract;

namespace WaiterManagement.Manager.ViewModels.Abstract
{
	public interface IEditTableViewModel : IViewModel
	{
		void Initialize(TableView table);
	}
}