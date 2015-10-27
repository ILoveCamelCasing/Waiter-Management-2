using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WaiterManagement.Common.Views.Abstract;

namespace WaiterManagement.Common.Views
{
  [Table("WaitersView")]
  public class WaiterView : IView
  {
    [Key]
    public int WaiterId { get; private set; }
    public Guid WaiterGuid { get; private set; }
    public string FirstName { get; private set;}
    public string LastName { get; private set; }
  }
}
