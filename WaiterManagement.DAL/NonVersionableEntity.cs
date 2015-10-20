namespace WaiterManagement.DAL
{
	public class NonVersionableEntity : IEntity
	{
		public int Id { get; private set; }
	}
}