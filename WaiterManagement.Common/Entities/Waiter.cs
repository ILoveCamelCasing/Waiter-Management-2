using WaiterManagement.Common.Entities.Abstract;

namespace WaiterManagement.Common.Entities
{
  public class Waiter : VersionableEntity
  {
    public string FirstName { get; set; }
    public string LastName { get; set; }
  }
}
