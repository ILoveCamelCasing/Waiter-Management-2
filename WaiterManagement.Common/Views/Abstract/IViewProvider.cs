using System.Linq;

namespace WaiterManagement.Common.Views.Abstract
{
	public interface IViewProvider
	{
		IQueryable<T> Get<T>() where T : class, IView;
	}
}