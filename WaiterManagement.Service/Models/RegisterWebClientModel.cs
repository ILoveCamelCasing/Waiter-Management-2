namespace WaiterManagement.Service.Models
{
	public class RegisterWebClientModel : LoginModel
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Phone { get; set; }
		public string Mail { get; set; }
	}
}