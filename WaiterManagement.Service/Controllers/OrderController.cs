using System.Linq;
using System.Web.Http;
using WaiterManagement.BLL.Commands.Base;
using WaiterManagement.BLL.Commands.Concrete.ServiceCommands;
using WaiterManagement.Service.Models;
using WaiterManagement.Service.Security;

namespace WaiterManagement.Service.Controllers
{
	public class OrderController : ApiController
	{
		private ICommandBus _commandBus;

		public OrderController(ICommandBus commandBus)
		{
			_commandBus = commandBus;
		}

		[HttpPost]
		[WebAuthorizeAttribiute]
		public IHttpActionResult MakeNewOrder([FromBody] NewWebOrderModel newWebOrderModel)
		{
			_commandBus.SendCommand(new NewWebOrderCommand()
			{
				Login = newWebOrderModel.Login,
				OrderDate = newWebOrderModel.OrderDate,
				Items = newWebOrderModel.Items.Select(x => new NewWebOrderCommandItem() {ItemId = x.ItemId, Quantity = x.Quantity}).ToArray()
			});
			return Ok();
		}

		
	}
}