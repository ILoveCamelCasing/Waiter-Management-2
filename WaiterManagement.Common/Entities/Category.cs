using System.Collections.Generic;
using WaiterManagement.Common.Entities.Abstract;

namespace WaiterManagement.Common.Entities
{
	public class Category : VersionableEntity
	{
		public string Title { get; set; }
		public string Description { get; set; }
		public virtual ICollection<MenuItem> MenuItems { get; set; }
	}
}