using System;
using WaiterManagement.Common.Entities.Abstract;

namespace WaiterManagement.Common.Entities
{
  public class Waiter : VersionableEntity, ILoginableEntity
  {
    public string FirstName { get; set; }
    public string LastName { get; set; }

    #region ILoginableEntity
    public string Login
    {
      get;
      set;
    }
    #endregion
  }
}
