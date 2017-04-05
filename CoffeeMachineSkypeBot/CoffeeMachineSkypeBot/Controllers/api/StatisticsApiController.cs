using CoffeeMachine.Abstraction;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using System.Web.Http;

namespace CoffeeMachineSkypeBot.Controllers.api
{
	//[Authorize]
	public class StatisticsApiController : ApiController
	{
		private readonly IDataService dataService;
		private readonly CultureInfo provider = CultureInfo.InvariantCulture;
		private readonly string dateFomat = "yyyy/MM/dd HH:mm:ss";

		public StatisticsApiController(IDataService dataService)
		{
			this.dataService = dataService;
		}

		[HttpPost]
		public async Task<IHttpActionResult> Upload()
		{
			//line template: 2017/01/06 19:20:38, 228-13-239-26, "Unknown"
			//line templ #2: 2017/01/09 14:29:35, 36-246-239-26, "29:1kelKuLf_HvkRGUXnCpmPvbky6EePEVAaAxgsgr29sow"
			var stream = await Request.Content.ReadAsStreamAsync();
			var result = new List<DataContainer>(50);

			using (StreamReader sr = new StreamReader(stream))
			{
				string line = null;
				while ((line = sr.ReadLine()) != null)
				{
					DataContainer parsed = ParseLine(line);
					if (parsed != null)
					{
						result.Add(parsed);
					}
				}
			}

			return Ok();
		}

		private DataContainer ParseLine(string line)
		{
			if (String.IsNullOrEmpty(line))
			{
				return null;
			}

			var splitted = line.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
			if (splitted.Length == 2)
			{
				return new DataContainer
				{
					Date = DateTime.ParseExact(splitted[0], dateFomat, provider),
					SkypeId = splitted[1]
				};
			}
			else if (splitted.Length == 3)
			{
				return new DataContainer
				{
					Date = DateTime.ParseExact(splitted[0], dateFomat, provider),
					UserIdentifier = splitted[1],
					SkypeId = splitted[2]
				};
			}

			return null;
		}
	}

	public class DataContainer
	{
		public DateTime Date { get; set; }
		public string UserIdentifier { get; set; }
		public string SkypeId { get; set; }
	}
}
}
