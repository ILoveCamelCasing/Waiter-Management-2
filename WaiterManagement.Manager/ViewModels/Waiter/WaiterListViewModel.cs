﻿using Caliburn.Micro;
using System;
using WaiterManagement.BLL.Commands.Base;
using WaiterManagement.BLL.Commands.Concrete.ManagerCommands;
using WaiterManagement.Common.Views;
using WaiterManagement.Common.Views.Abstract;
using WaiterManagement.Wpf.MVVM.Abstract;

namespace WaiterManagement.Manager.ViewModels.Waiter
{
  public sealed class WaiterListViewModel : ViewModelBase
  {
    #region Dependencies
    private readonly IViewProvider _viewProvider;
    private readonly ICommandBus _commandBus;
    #endregion

    #region Private Fields
    private WaiterView _selectedElement;
    private bool _isBusy;
    #endregion

    #region Constructors
    public WaiterListViewModel(IViewModelResolver viewModelResolver, IViewProvider viewProvider, ICommandBus commandBus)
      : base(viewModelResolver)
    {
      if (viewProvider == null)
        throw new ArgumentNullException("viewProvider");
      if (commandBus == null)
        throw new ArgumentNullException("commandBus");

      _viewProvider = viewProvider;
      _commandBus = commandBus;

      DisplayName = "Waiters";
      Elements = new BindableCollection<WaiterView>();
      IsBusy = true;
    }
    #endregion

    #region Public Properties
    public WaiterView SelectedElement
    {
      get
      {
        return _selectedElement;
      }
      set
      {
        _selectedElement = value;
        NotifyOfPropertyChange(() => SelectedElement);
        NotifyOfPropertyChange(() => CanDeleteWaiter);
      }
    }

    public BindableCollection<WaiterView> Elements { get; private set; }

    public bool IsBusy
    {
      get
      {
        return _isBusy;
      }
      private set
      {
        _isBusy = value;
        NotifyOfPropertyChange(() => IsBusy);
      }
    }
    #endregion

    #region Public Methods
    public void AddWaiter()
    {
      Get<AddWaiterViewModel>().ShowOn(ParentWindow);
    }

    public void EditWaiter()
    {
      var editWaiterViewModel = Get<EditWaiterViewModel>();
      editWaiterViewModel.Initialize(SelectedElement);
      editWaiterViewModel.ShowOn(ParentWindow);
    }

    public void DeleteWaiter()
    {
      _commandBus.SendCommand(new DeleteWaiterCommand() { Id = SelectedElement.WaiterId });
      OnActivate();
    }

    public bool CanDeleteWaiter
    {
      get
      {
        return SelectedElement != null;
      }
    }
    #endregion

    #region Overrides
    protected override async void OnActivate()
    {
      base.OnActivate();

      Elements.Clear();      
      Elements.AddRange(await _viewProvider.GetAsync<WaiterView>());
      IsBusy = false;
    }
    #endregion
  }
}