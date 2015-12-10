using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WaiterManagement.Common.Views.Abstract;

namespace WaiterManagement.Common.Views
{
	[Table("AuthenticatedUsersView")]
	public class AuthenticatedUsersView : IView
	{
		[Key]
		public int UserId { get; set; }
		public string Login { get; set; }
		public UserType Type { get; set; } 
		public Guid Token { get; set; }
	}

	public enum UserType
	{
		Table = 1,
		Waiter
	}
}