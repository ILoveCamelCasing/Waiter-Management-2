using System;
using WaiterManagement.Common.Entities.Abstract;

namespace WaiterManagement.Common.Entities
{
	public class User : VersionableEntity
    {
        public Guid UserId { get; set; }
        public string SecondHash { get; set; }
    }
}
