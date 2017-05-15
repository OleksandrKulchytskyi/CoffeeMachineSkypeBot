using Newtonsoft.Json;
using System.Web.Http;
using Newtonsoft.Json.Serialization;

namespace CoffeeMachineSkypeBot
{
	public static class WebApiConfig
	{
		public static void Register(HttpConfiguration config)
		{
			// Web API configuration and services
			var cors = new System.Web.Http.Cors.EnableCorsAttribute("*", "*", "*");
			config.EnableCors(cors);

			// Json settings
			config.Formatters.JsonFormatter.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
			config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
			config.Formatters.JsonFormatter.SerializerSettings.Formatting = Formatting.Indented;
			JsonConvert.DefaultSettings = () => new JsonSerializerSettings()
			{
				ContractResolver = new CamelCasePropertyNamesContractResolver(),
				Formatting = Formatting.Indented,
				NullValueHandling = NullValueHandling.Ignore,
			};

			// Web API routes
			config.MapHttpAttributeRoutes();

			config.Routes.MapHttpRoute(
				name: "DefaultActionApi",
				routeTemplate: "api/{controller}/{action}",
				defaults: new { }
			);

			config.Routes.MapHttpRoute(
				name: "DefaultActionApiParam",
				routeTemplate: "api/{controller}/{action}/{id}",
				defaults: new { id = RouteParameter.Optional }
			);

			config.Routes.MapHttpRoute(
				name: "DefaultApi",
				routeTemplate: "api/{controller}/{id}",
				defaults: new { id = RouteParameter.Optional }
			);
		}
	}
}
