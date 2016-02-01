using WaiterManagement.Common.Entities.Abstract;

namespace WaiterManagement.Common.Entities
{
	public class MenuItem : VersionableEntity
	{
		public string Title { get; set; }
		public string Description { get; set; }
		public decimal Price { get; set; }
		public Category Category { get; set; }
	}
}