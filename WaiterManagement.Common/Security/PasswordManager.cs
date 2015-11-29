namespace WaiterManagement.Common.Security
{
	public class PasswordManager : IPasswordManager
	{
		public string CreateSecondHash(string login, string password)
		{
			return HashUtility.CreateSecondHash(password, login);
		}

		public string CreateFirstHash(string login, string password)
		{
			return HashUtility.CreateFirstHash(password, login);
		}
	}
}