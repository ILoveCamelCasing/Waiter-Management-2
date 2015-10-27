using System;
using WaiterManagement.BLL.Commands.Base;
using WaiterManagement.BLL.Commands.Concrete;
using WaiterManagement.Wpf.MVVM;
using WaiterManagement.Wpf.MVVM.Abstract;

namespace WaiterManagement.Manager.ViewModels.Waiter
{
  [UseView("Waiter.WaiterView")]
  public class AddWaiterViewModel : ViewModelBase
  {
    #region Dependencies
    private readonly ICommandBus _commandBus;
    #endregion

    #region Public Properties

    public string FirstName { get; set; }
    public string LastName { get; set; }

    #endregion

    #region Constructors
    public AddWaiterViewModel(IViewModelResolver viewModelResolver, ICommandBus commandBus)
      :base(viewModelResolver)
    {
      if (commandBus == null)
        throw new ArgumentNullException("commandBus");

      _commandBus = commandBus;
    }
    #endregion

    #region Public Methods
    public void Save()
    {
      _commandBus.SendCommand(new AddWaiterCommand() { FirstName = FirstName, LastName = LastName });
      Close();
    }

    public void Cancel()
    {
      Close();
    }
    #endregion
  }
}
