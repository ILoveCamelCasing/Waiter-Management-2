using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using WaiterManagement.BLL.Commands.Base;
using WaiterManagement.BLL.Commands.Concrete.ServiceCommands;
using WaiterManagement.Common.Views;
using WaiterManagement.Common.Views.Abstract;
using WaiterManagement.Service.Models;
using WaiterManagement.Service.Security;

namespace WaiterManagement.Service.Controllers
{
	public class OrderController : ApiController
	{
		private readonly ICommandBus _commandBus;
		private readonly IViewProvider _viewProvider;

		public OrderController(ICommandBus commandBus, IViewProvider viewProvider)
		{
			_commandBus = commandBus;
			_viewProvider = viewProvider;
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

		[ResponseType(typeof(IEnumerable<ReservationView>))]
		[HttpGet]
		[WebAuthorizeAttribiute]
		public IHttpActionResult UserAll()
		{
			var login = HttpContext.Current.Request.Headers["login"];

			return Ok(_viewProvider.Get<ReservationView>().Where(x => x.ClientLogin == login).AsEnumerable());
		}

		
	}
}