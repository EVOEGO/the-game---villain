using System;
using ApiTest.Models;
using Newtonsoft.Json;

namespace ApiTest.Requests
{
	public class RequestBuilder
	{
		GenTokenBody genTokenBody;
		RegisterUserBody registerUserBody;
		UserDetails userDetails;

		public string AuthBody(string key, string email, Boolean returnKey)
		{
			genTokenBody = new GenTokenBody();

			genTokenBody.key = key;
			genTokenBody.email = email;
			genTokenBody.returnKey = returnKey;

			String SerializedGenTokenBody = JsonConvert.SerializeObject(genTokenBody);

			return SerializedGenTokenBody;
        }

		public string UserBody(string username, string password)
		{
			registerUserBody = new RegisterUserBody();

			registerUserBody.username = username;
			registerUserBody.password = password;

			String SerialisedRegisterUserBody = JsonConvert.SerializeObject(registerUserBody);

			return SerialisedRegisterUserBody;
		}

		public string AddNewUserBody(string username, int score)
		{
			userDetails = new UserDetails();

			userDetails.username = username;
			userDetails.score = score;

			String SerializeAddNewUserBody = JsonConvert.SerializeObject(userDetails);

			return SerializeAddNewUserBody;
		}
	}
}

