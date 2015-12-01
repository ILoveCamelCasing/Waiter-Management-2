namespace WaiterManagement.Waiter.Connection
{
	public class TableConnectionProvider : ITableConnectionProvider
	{
		private readonly IAccessProvider _accessProvider;

		public TableConnectionProvider(IAccessProvider accessProvider)
		{
			_accessProvider = accessProvider;
		}
	}
}