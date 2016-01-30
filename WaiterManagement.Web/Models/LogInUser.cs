using System.ComponentModel.DataAnnotations;

namespace WaiterManagement.Web.Models
{
	public class LogInUser
	{
		[Required(ErrorMessage = "Please provide Username",AllowEmptyStrings = false)]
		public string Username { get; set; }

		[Required(ErrorMessage = "Please provide password", AllowEmptyStrings = false)]
		[DataType(System.ComponentModel.DataAnnotations.DataType.Password)]
		public string Password { get; set; }
	}
}