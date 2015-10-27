﻿
using WaiterManagement.BLL.Commands.Base;

namespace WaiterManagement.BLL.Commands.Concrete
{
  public class AddWaiterCommand : ICommand
  {
    public string FirstName { get; set; }
    public string LastName { get; set; }
  }
}
