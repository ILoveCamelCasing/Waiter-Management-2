using WaiterManagement.Common.Entities.Abstract;

namespace WaiterManagement.BLL.Commands.Base
{
	public abstract class Handler : IHandleCommand
	{
		public IUnitOfWork UnitOfWork { get; private set; }

		protected Handler(IUnitOfWork unitUnitOfWork)
		{
			UnitOfWork = unitUnitOfWork;
		}
	}
}