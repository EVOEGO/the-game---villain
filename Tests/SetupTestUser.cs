using System;
namespace ApiTest.Tests
{
	public class SetupTestUser
	{
        Guid newUser = Guid.NewGuid();

        public string CreateUser()
		{
			string user = newUser.ToString();

			return user;
		}

		public string CreateUserEmail()
		{
			string email = newUser.ToString() + "@gmail.com";

			return email;
		}
	}
}

