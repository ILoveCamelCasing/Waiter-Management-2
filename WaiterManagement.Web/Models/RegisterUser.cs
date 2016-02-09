using System.ComponentModel.DataAnnotations;

namespace WaiterManagement.Web.Models
{
	public class RegisterUser : LogInUser
	{
		[Display(Name = "First name")]
		[Required(ErrorMessage = "Please provide First name", AllowEmptyStrings = false)]
		public string FirstName { get; set; }

		[Display(Name = "Last name")]
		[Required(ErrorMessage = "Please provide Last name", AllowEmptyStrings = false)]
		public string LastName { get; set; }

		[Display(Name = "Phone number")]
		[Required(ErrorMessage = "Please provide Phone", AllowEmptyStrings = false)]
		[DataType(DataType.PhoneNumber)]
		[RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{3})$", ErrorMessage = "Not a valid Phone number: xxx-xxx-xxx")]
		public string Phone { get; set; }

		[Display(Name = "Mail address")]
		[Required(ErrorMessage = "Please provide Mail", AllowEmptyStrings = false)]
		[EmailAddress(ErrorMessage = "Invalid Email Address")]
		public string Mail { get; set; }
	}
}