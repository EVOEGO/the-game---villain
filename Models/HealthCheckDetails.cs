using System;
namespace ApiTest.Models
{
	public class HealthCheckDetails
	{
		public string uptime { get; set; }
		public string message { get; set; }
		public string date { get; set; }
		public List<CpuDetails> cpu { get; set; }
	}
}

