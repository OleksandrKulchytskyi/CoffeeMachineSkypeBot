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
	[System.Web.Http.Cors.EnableCors(origins: "*", headers: "*", methods: "*")]
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
			byte[] bytesData;
			if (Request.Content.IsMimeMultipartContent())
			{
				var streamProvider = new MultipartFormDataStreamProvider(HostingEnvironment.MapPath("~/App_Data"));
				var dataStreamProvider = await Request.Content.ReadAsMultipartAsync(streamProvider);

				var contents = dataStreamProvider.Contents;
				bytesData = await contents[0].ReadAsByteArrayAsync();
			}
			else
			{
				bytesData = await Request.Content.ReadAsByteArrayAsync();
			}

			var importResult = importer.ImportFrom(new MemoryStream(bytesData));

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
