using Microsoft.AspNet.SignalR;

namespace WaiterManagement.Service.Hubs
{
	public class CustomIdUserProvider : IUserIdProvider
	{
		public string GetUserId(IRequest request)
		{
			var userId = request.Headers["login"];
			return userId;
		}
	}
}