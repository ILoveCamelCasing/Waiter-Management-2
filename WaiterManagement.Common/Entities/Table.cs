using WaiterManagement.Common.Entities.Abstract;

namespace WaiterManagement.Common.Entities
{
	public class Table : VersionableEntity
	{
		public string Title { get; set; }
		public string Description { get; set; }
	}
}