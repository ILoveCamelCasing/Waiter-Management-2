using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WaiterManagement.Common.Entities.Abstract;

namespace WaiterManagement.Common.Entities
{
	public class Table : VersionableEntity
	{
		[Required]
		public string Title { get; set; }

		public string Description { get; set; }
	}
}