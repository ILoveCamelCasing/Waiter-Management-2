﻿namespace WaiterManagement.Common.Security
{
	public interface IPasswordManager
	{
		string CreateSecondHash(string login, string password);
	}
}