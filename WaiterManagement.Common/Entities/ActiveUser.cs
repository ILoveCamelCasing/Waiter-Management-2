using System;
using WaiterManagement.Common.Entities.Abstract;

namespace WaiterManagement.Common.Entities
{
	public class ActiveUser : NonVersionableEntity
	{
		public Guid UserId { get; set; }
		public Guid UserToken { get; set; }
		public DateTime TokenCreation { get; set; }
	}
}
