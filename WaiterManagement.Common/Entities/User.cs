using WaiterManagement.Common.Entities.Abstract;

namespace WaiterManagement.Common.Entities
{
	public class User : VersionableEntity
	{
		public string Login { get; set; }
		public string SecondHash { get; set; }
	}
}
