using WaiterManagement.Common.Entities.Abstract;

namespace WaiterManagement.Common.Entities
{
  public class Table : VersionableEntity, ILoginableEntity
	{
		public string Title { get; set; }
		public string Description { get; set; }

    #region ILoginableEntity
    public string Login
    {
      get;
      set;
    }
    #endregion
  }
}