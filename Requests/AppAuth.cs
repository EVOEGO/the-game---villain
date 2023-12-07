using System;
using RestSharp;
using RestSharp.Authenticators;
using NUnit.Framework;
using static System.Formats.Asn1.AsnWriter;

namespace ApiTest.Requests
{
	public class AppAuth
	{
		//If i had more time i would have this urls stored inside of a runsettings file
		private string genTokenUrl = "https://supervillan-81ce46d107ff.herokuapp.com/auth/gentoken";
		private string verifyTokenUrl = "https://supervillan-81ce46d107ff.herokuapp.com/auth/verifytoken";
		private string registerUserUrl = "https://supervillan-81ce46d107ff.herokuapp.com/auth/user/register";
		private string userloginurl = "https://supervillan-81ce46d107ff.herokuapp.com/auth/user/login";
		private string listofusersurl = "https://supervillan-81ce46d107ff.herokuapp.com/v1/user";
		private string addnewuserurl = "https://supervillan-81ce46d107ff.herokuapp.com/v1/user";
        private string updatenewuserurl = "https://supervillan-81ce46d107ff.herokuapp.com/v1/user";
        private string getuserurl = "https://supervillan-81ce46d107ff.herokuapp.com/v1/user/";
        private string healthcheckurl = "https://supervillan-81ce46d107ff.herokuapp.com/health";
        private string loadcpuurl = "https://supervillan-81ce46d107ff.herokuapp.com/fibonacci/";

        private string url = "";
        private string requestBody;

		//As this repo will be run outside of my computer this authtoken will need to be updated by the person running the collection
        private string AuthorizationToken = "token";

		RequestBuilder requestBuilder;	

        public string[] genToken(string key, string email, Boolean returnKey)
		{
			url = genTokenUrl;

			var Client = new RestClient(url);
			var Request = new RestRequest();

			requestBuilder = new RequestBuilder();

            requestBody = requestBuilder.AuthBody(key, email, returnKey);

			Request.AddJsonBody(requestBody);
			Request.AddHeader("Authorization", AuthorizationToken);
			Request.AddHeader("Content-Type", "application/json");

			var Response = Client.ExecutePostAsync(Request);

			RestResponse restResponse = Response.Result;

			string[] ResponseDetails = { restResponse.Content, restResponse.StatusCode.ToString() };

			return ResponseDetails;
		}

		public string[] verifyToken(string authtoken)
		{
			url = verifyTokenUrl;

			var Client = new RestClient(url);
			var Request = new RestRequest();

			Request.AddHeader("Content-Type", "application/json");
			Request.AddHeader("Authorization", authtoken);

			var Response = Client.ExecuteGetAsync(Request);

			RestResponse restResponse = Response.Result;

            string[] ResponseDetails = { restResponse.Content, restResponse.StatusCode.ToString() };

            return ResponseDetails;
        }

		public string[] RegisterUser(string authtoken, string username, string password)
		{
			url = registerUserUrl;
			requestBody = "";

			var Client = new RestClient(url);
			var Request = new RestRequest();

			requestBuilder = new RequestBuilder();
			requestBody = requestBuilder.UserBody(username, password); 

			Request.AddHeader("Authorization", authtoken);
			Request.AddHeader("Accept", "*/*");
			Request.AddHeader("Content-Type", "application/json");
			Request.AddJsonBody(requestBody);

            var Response = Client.ExecutePostAsync(Request);

            RestResponse restResponse = Response.Result;

            string[] ResponseDetails = { restResponse.Content, restResponse.StatusCode.ToString() };

            return ResponseDetails;
        }

		public string[] UserLogin(string authtoken, string username, string password)
		{
			url = userloginurl;
			requestBody = "";

			var Client = new RestClient(url);
			var Request = new RestRequest();

            requestBuilder = new RequestBuilder();
            requestBody = requestBuilder.UserBody(username, password);

            Request.AddHeader("Authorization", authtoken);
            Request.AddHeader("Accept", "*/*");
            Request.AddHeader("Content-Type", "application/json");
            Request.AddJsonBody(requestBody);

            var Response = Client.ExecutePostAsync(Request);

            RestResponse restResponse = Response.Result;

            string[] ResponseDetails = { restResponse.Content, restResponse.StatusCode.ToString() };

            return ResponseDetails;
        }

		public string[] ReturnAllUsers(string authtoken)
		{
			url = listofusersurl;

            var Client = new RestClient(url);
            var Request = new RestRequest();

            Request.AddHeader("Authorization", authtoken);
            Request.AddHeader("Content-Type", "application/json");

            var Response = Client.ExecuteGetAsync(Request);

            RestResponse restResponse = Response.Result;

            string[] ResponseDetails = { restResponse.Content, restResponse.StatusCode.ToString() };

            return ResponseDetails;
        }

		public string[] AddNewUser(string authtoken, string username, int score)
		{
            url = addnewuserurl;
            requestBody = "";

            var Client = new RestClient(url);
            var Request = new RestRequest();

            requestBuilder = new RequestBuilder();
            requestBody = requestBuilder.AddNewUserBody(username, score);

            Request.AddHeader("Authorization", authtoken);
            Request.AddHeader("Accept", "*/*");
            Request.AddHeader("Content-Type", "application/json");
            Request.AddJsonBody(requestBody);

            var Response = Client.ExecutePostAsync(Request);

            RestResponse restResponse = Response.Result;

            string[] ResponseDetails = { restResponse.Content, restResponse.StatusCode.ToString() };

            return ResponseDetails;
        }

		public string[] UpdateUser(string authtoken, string username, int updatedscore)
		{
            url = updatenewuserurl;
            requestBody = "";

            var Client = new RestClient(url);
            var Request = new RestRequest();

            requestBuilder = new RequestBuilder();
            requestBody = requestBuilder.AddNewUserBody(username, updatedscore);

            Request.AddHeader("Authorization", authtoken);
            Request.AddHeader("Accept", "*/*");
            Request.AddHeader("Content-Type", "application/json");
            Request.AddJsonBody(requestBody);

            var Response = Client.ExecutePutAsync(Request);

            RestResponse restResponse = Response.Result;

            string[] ResponseDetails = { restResponse.Content, restResponse.StatusCode.ToString() };

            return ResponseDetails;
        }

        public string[] GetUserByUsername(string authtoken, string username)
        {
            url = getuserurl + username;

            var Client = new RestClient(url);
            var Request = new RestRequest();

            Request.AddHeader("Authorization", authtoken);
            Request.AddHeader("Content-Type", "application/json");

            var Response = Client.ExecuteGetAsync(Request);

            RestResponse restResponse = Response.Result;

            string[] ResponseDetails = { restResponse.Content, restResponse.StatusCode.ToString() };

            return ResponseDetails;
        }

        public string[] HealthCheck(string authtoken)
        {
            url = healthcheckurl;

            var Client = new RestClient(url);
            var Request = new RestRequest();

            Request.AddHeader("Authorization", authtoken);
            Request.AddHeader("Content-Type", "application/json");

            var Response = Client.ExecuteGetAsync(Request);

            RestResponse restResponse = Response.Result;

            string[] ResponseDetails = { restResponse.Content, restResponse.StatusCode.ToString() };

            return ResponseDetails;
        }

        public string[] LoadCpu(string authtoken, int fibnumber)
        {
            url = loadcpuurl + fibnumber;

            var Client = new RestClient(url);
            var Request = new RestRequest();

            Request.AddHeader("Authorization", authtoken);
            Request.AddHeader("accept", "text/html");

            var Response = Client.ExecuteGetAsync(Request);

            RestResponse restResponse = Response.Result;

            string[] ResponseDetails = { restResponse.Content, restResponse.StatusCode.ToString() };

            return ResponseDetails;
        }
	}
}

