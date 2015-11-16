using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WaiterManagement.Common.Views.Abstract;

namespace WaiterManagement.Common.Views
{
	[Table("WaitersView")]
	public class WaiterView : ILoginableView
	{
		[Key]
		public int WaiterId { get; set; }
		public Guid WaiterGuid { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Login { get; set; }
		public string SecondHash { get; set; }
		public Guid UserId { get; set; }
	}
}
