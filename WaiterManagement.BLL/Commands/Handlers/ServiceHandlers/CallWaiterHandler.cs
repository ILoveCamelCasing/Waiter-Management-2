﻿using System.Linq;
using WaiterManagement.BLL.Commands.Base;
using WaiterManagement.BLL.Commands.Concrete.ServiceCommands;
using WaiterManagement.Common;
using WaiterManagement.Common.Entities;
using WaiterManagement.Common.Views;
using WaiterManagement.Common.Views.Abstract;

namespace WaiterManagement.BLL.Commands.Handlers.ServiceHandlers
{
	public class CallWaiterHandler : Handler, IHandleCommand<CallWaiterCommand>
	{
		private readonly ICallingService _callingService;
		private readonly IViewProvider _viewProvider;

		public CallWaiterHandler(ICallingService callingService, IViewProvider viewProvider)
		{
			_callingService = callingService;
			_viewProvider = viewProvider;
		}

		public void Handle(CallWaiterCommand command)
		{
			var order =
				_viewProvider.Get<OrderView>()
					.FirstOrDefault(x => x.Status == OrderStatus.Assigned && x.TableTitle == command.TableLogin);
			if (order != null)
				_callingService.GetWaiter(order.WaiterLogin);
			else
				_callingService.GetWaiters().CallWaiter(command.TableLogin);
		}
	}
}