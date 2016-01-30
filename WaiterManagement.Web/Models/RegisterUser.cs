using System.ComponentModel.DataAnnotations;

namespace WaiterManagement.Web.Models
{
	public class RegisterUser : LogInUser
	{
		[Required(ErrorMessage = "Please provide First name", AllowEmptyStrings = false)]
		public string FirstName { get; set; }

		[Required(ErrorMessage = "Please provide Last name", AllowEmptyStrings = false)]
		public string LastName { get; set; }

		[Required(ErrorMessage = "Please provide Phone", AllowEmptyStrings = false)]
		public string Phone { get; set; }

		[Required(ErrorMessage = "Please provide Mail", AllowEmptyStrings = false)]
		public string Mail { get; set; }
	}
}