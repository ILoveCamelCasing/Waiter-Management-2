using System;
using WaiterManagement.BLL.Commands.Base;
using WaiterManagement.BLL.Commands.Concrete;
using WaiterManagement.Common.Views;
using WaiterManagement.Wpf.MVVM;
using WaiterManagement.Wpf.MVVM.Abstract;

namespace WaiterManagement.Manager.ViewModels.Waiter
{
  [UseView("Waiter.WaiterView")]
  public class EditWaiterViewModel : ViewModelBase
  {
    #region Dependencies
    private readonly ICommandBus _commandBus;
    #endregion

    #region Private Fields
    private int _id;
    #endregion

    #region Constructors
    public EditWaiterViewModel(IViewModelResolver viewModelResolver, ICommandBus commandBus)
      :base(viewModelResolver)
    {
      if (commandBus == null)
        throw new ArgumentNullException("commandBus");

      _commandBus = commandBus;
    }
    #endregion

    #region Public Properties
    public string FirstName { get; set; }
    public string LastName { get; set; }
    #endregion

    #region Public Methods
    public void Save()
    {
      _commandBus.SendCommand(new EditWaiterCommand() { Id = _id, FirstName = FirstName, LastName = LastName });
      Close();
    }

    public void Cancel()
    {
      Close();
    }

    public void Initialize(WaiterView waiter)
    {
      _id = waiter.WaiterId;
      FirstName = waiter.FirstName;
      LastName = waiter.LastName;
    }
    #endregion
  }
}
