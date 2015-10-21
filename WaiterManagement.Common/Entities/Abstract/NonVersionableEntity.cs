namespace WaiterManagement.Common.Entities.Abstract
{
	public class NonVersionableEntity : IEntity
	{
		public int Id { get; private set; }
	}
}