using CoffeeMachine.Abstraction;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Hosting;
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

		public async Task<FileResult> UploadSingleFile()
		{
			var streamProvider = new MultipartFormDataStreamProvider(HostingEnvironment.MapPath("~/App_Data"));
			var dataStreamProvider = await Request.Content.ReadAsMultipartAsync(streamProvider);

			var contents = dataStreamProvider.Contents;

			var bytes = await contents[0].ReadAsByteArrayAsync();

			var result = new List<DataContainer>(50);

			using (var ms = new MemoryStream(bytes))
			using (StreamReader sr = new StreamReader(ms))
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

			result.ForEach(x => dataService.AddActivity(x.SkypeId));

			var fResult = new FileResult
			{
				FileNames = streamProvider.FileData.Select(entry => entry.LocalFileName),
				Names = streamProvider.FileData.Select(entry => entry.Headers.ContentDisposition.FileName),
				ContentTypes = streamProvider.FileData.Select(entry => entry.Headers.ContentType.MediaType),
				Description = streamProvider.FormData["description"],
				CreatedTimestamp = DateTime.UtcNow,
				UpdatedTimestamp = DateTime.UtcNow,
				DownloadLink = "TODO, will implement when file is persisited"
			};

			return fResult;
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

	public class FileResult
	{
		public IEnumerable<string> FileNames { get; set; }
		public string Description { get; set; }
		public DateTime CreatedTimestamp { get; set; }
		public DateTime UpdatedTimestamp { get; set; }
		public string DownloadLink { get; set; }
		public IEnumerable<string> ContentTypes { get; set; }
		public IEnumerable<string> Names { get; set; }
	}

	public class DataContainer
	{
		public DateTime Date { get; set; }
		public string UserIdentifier { get; set; }
		public string SkypeId { get; set; }
	}
}
