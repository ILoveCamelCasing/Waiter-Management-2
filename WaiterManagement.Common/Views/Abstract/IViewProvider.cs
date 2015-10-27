using System.Linq;
using System.Threading.Tasks;

namespace WaiterManagement.Common.Views.Abstract
{
	public interface IViewProvider
	{
		IQueryable<T> Get<T>() where T : class, IView;
    Task<IQueryable<T>> GetAsync<T>() where T : class, IView;
	}
}