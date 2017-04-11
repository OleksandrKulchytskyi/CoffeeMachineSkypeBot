using CoffeeMachine.Abstraction;
using CoffeeMachine.Abstraction.Dto;
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
		private readonly string DateFormat = "yyyy/MM/dd HH:mm:ss";

		public StatisticsApiController(IDataService dataService)
		{
			this.dataService = dataService;
		}

		public async Task<List<ImportValidationResult>> UploadSingleFile()
		{
			var streamProvider = new MultipartFormDataStreamProvider(HostingEnvironment.MapPath("~/App_Data"));
			var dataStreamProvider = await Request.Content.ReadAsMultipartAsync(streamProvider);

			var contents = dataStreamProvider.Contents;
			var bytes = await contents[0].ReadAsByteArrayAsync();

			var importResult = new List<ImportDataContainer>(50);

			using (var ms = new MemoryStream(bytes))
			using (StreamReader sr = new StreamReader(ms))
			{
				string line = null;
				int rowNumber = 0;
				while ((line = sr.ReadLine()) != null)
				{
					var exportData = ParseLine(line);
					if (exportData != null)
					{
						exportData.RowId = ++rowNumber;
						importResult.Add(exportData);
					}
				}
			}

			var validationResult = await dataService.ImportUserActivity(importResult);


			return validationResult;
		}

		private ImportDataContainer ParseLine(string line)
		{
			if (String.IsNullOrEmpty(line))
			{
				return null;
			}

			var splitted = line.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
			if (splitted.Length == 2)
			{
				return new ImportDataContainer
				{
					Date = DateTime.ParseExact(splitted[0], DateFormat, provider),
					UserIdentifier = splitted[1]
				};
			}
			else if (splitted.Length == 3)
			{
				return new ImportDataContainer
				{
					Date = DateTime.ParseExact(splitted[0], DateFormat, provider),
					SkypeId = splitted[1],
					UserIdentifier = splitted[2]
				};
			}

			return null;
		}
	}
}
