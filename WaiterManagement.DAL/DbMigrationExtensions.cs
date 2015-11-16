namespace WaiterManagement.DAL
{
	public static class DbMigrationExtensions
	{
		public static string CreateViewQuery(string viewName, string selectQuery)
		{
			return string.Format("CREATE VIEW {0} AS {1}", viewName, selectQuery);
		}

		public static string DropViewQuery(string viewName)
		{
			return string.Format("DROP VIEW {0}", viewName);
		}

		public static string AlterViewQuery(string viewName, string selectQuery)
		{
			return string.Format("ALTER VIEW {0} AS {1}", viewName, selectQuery);
		}
	}
}