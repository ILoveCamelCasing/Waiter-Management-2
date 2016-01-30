using WaiterManagement.Common.Entities.Abstract;

namespace WaiterManagement.Common.Entities
{
	public class WebClient : VersionableEntity, ILoginableEntity
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Mail { get; set; }
		public string Phone { get; set; }

		#region ILoginableEntity

		public User User { get; set; }

		#endregion

		public override void LoadAll(IUnitOfWork unitOfWork)
		{
			unitOfWork.Load(this, p => p.User);
		}
	}
}