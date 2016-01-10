using WaiterManagement.BLL.Commands.Base;
using WaiterManagement.BLL.Commands.Concrete.ServiceCommands;
using WaiterManagement.BLL.Events.Concrete.Service;

namespace WaiterManagement.BLL.Commands.Handlers.ServiceHandlers
{
	public class UpdateAfterWaiterLoginHandler : Handler, IHandleCommand<UpdateAfterWaiterLoginCommand>
	{
		public void Handle(UpdateAfterWaiterLoginCommand command)
		{
			//Tu się nic nie dzieje, publikujemy zdarzenie, aby odesłać kelnerowi oczekujące zadania
			
			EventBus.PublishEvent(new WaiterLoggedIn() {WaiterLogin = command.WaiterLogin});			
		}
	}
}
