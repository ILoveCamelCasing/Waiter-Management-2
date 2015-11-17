using WaiterManagement.Common.Entities.Abstract;

namespace WaiterManagement.Common.Entities
{
	public class Waiter : VersionableEntity, ILoginableEntity
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }

		#region ILoginableEntity

		public User User { get; set; }

		#endregion

		public override void LoadAll(IUnitOfWork unitOfWork)
		{
			unitOfWork.Load(this, p => p.User);
		}

	}
}
