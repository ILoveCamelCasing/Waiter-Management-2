using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using WaiterManagement.BLL.Events.Base;
using WaiterManagement.BLL.Events.Concrete.Service;

namespace WaiterManagement.BLL.Events.Handlers.Service
{
	public class WhenAddedWebOrder_SendEmailToClient : IHandleEvent<AddedWebOrder>
	{
		const string Subject = "New Order";

		public void Handle(AddedWebOrder addedWebOrder)
		{
			var fromAddress = new MailAddress("testdotnetbar@gmail.com", "From Bar");
			var toAddress = new MailAddress(addedWebOrder.Order.Client.Mail, $"{addedWebOrder.Order.Client.FirstName} {addedWebOrder.Order.Client.LastName}");
			const string fromPassword = "1qazXSW@3edc";
			var body = new StringBuilder()
				.AppendLine("Thanks for order.")
				.AppendLine($"Your code is here: {addedWebOrder.Order.UnlockCode}.")
				.AppendLine($"Code should be use with table {addedWebOrder.Order.Table.Title}.")
				.AppendLine()
				.AppendLine("Order details:")
				.AppendLine($"Reservation date {addedWebOrder.Order.ReservationTime}.")
				.AppendLine();

			foreach (var menuItem in addedWebOrder.MenuItems)
			{
				var subtotal = menuItem.Quantity*menuItem.Item.Price;
				body.AppendLine($"{menuItem.Quantity} x {menuItem.Item.Title} (subtotal: {subtotal:c})");
			}

			body.AppendLine()
				.AppendLine($"Total order value: {addedWebOrder.MenuItems.Sum(x => x.Quantity*x.Item.Price):c}");

			var smtp = new SmtpClient
			{
				Host = "smtp.gmail.com",
				Port = 587,
				EnableSsl = true,
				DeliveryMethod = SmtpDeliveryMethod.Network,
				UseDefaultCredentials = false,
				Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
			};
			using (var message = new MailMessage(fromAddress, toAddress)
			{
				Subject = Subject,
				Body = body.ToString()
			})
			{
				smtp.Send(message);
			}

		}
	}
}