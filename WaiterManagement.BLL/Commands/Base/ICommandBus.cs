namespace WaiterManagement.BLL.Commands.Base
{
	public interface ICommandBus
	{
		void SendCommand<T>(T command) where T : ICommand;
	}
}