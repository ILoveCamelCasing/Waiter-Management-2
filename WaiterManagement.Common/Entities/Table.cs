using System.ComponentModel.DataAnnotations;
using WaiterManagement.Common.Entities.Abstract;

namespace WaiterManagement.Common.Entities
{
	public class Table : VersionableEntity, ILoginableEntity
	{
		[Required]
		public string Title { get; set; }

		public string Description { get; set; }

		#region ILoginableEntity

		public User User { get; set; }

		#endregion

	}
}