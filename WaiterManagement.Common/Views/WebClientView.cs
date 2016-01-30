using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WaiterManagement.Common.Views.Abstract;

namespace WaiterManagement.Common.Views
{
	[Table("WebClientsView")]
	public class WebClientView : ILoginableView
	{
		[Key]
		public int WebClientId { get; set; }
		public Guid WebClientGuid { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Phone { get; set; }
		public string Mail { get; set; }
		public string Login { get; set; }
		public string SecondHash { get; set; }
		public Guid UserId { get; set; }
	}
}