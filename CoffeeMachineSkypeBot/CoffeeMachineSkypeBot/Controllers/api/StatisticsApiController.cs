using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Hosting;
using System.Web.Http;
using CoffeeMachine.Abstraction;
using CoffeeMachine.Abstraction.Dto;
using CoffeeMachine.Abstraction.Interfaces;

namespace CoffeeMachineSkypeBot.Controllers.api
{
	//[Authorize]
	public class StatisticsApiController : ApiController
	{
		private readonly IDataService dataService;
		private readonly IUserActivityImporter importer;

		public StatisticsApiController(IDataService dataService,
										IUserActivityImporter activityImporter)
		{
			this.dataService = dataService;
			this.importer = activityImporter;
		}

		public async Task<List<ImportValidationResult>> UploadSingleFile()
		{
			var streamProvider = new MultipartFormDataStreamProvider(HostingEnvironment.MapPath("~/App_Data"));
			var dataStreamProvider = await Request.Content.ReadAsMultipartAsync(streamProvider);

			var contents = dataStreamProvider.Contents;
			var bytes = await contents[0].ReadAsByteArrayAsync();

			var importResult = importer.ImportFrom(new MemoryStream(bytes));

			var validationResult = await dataService.ImportUserActivity(importResult);

			if (importer.Errors.Count > 0)
			{
				return validationResult.Union(importer.Errors)
										.OrderBy(x => x.RowId).ToList();
			}

			return validationResult;
		}
	}
}
