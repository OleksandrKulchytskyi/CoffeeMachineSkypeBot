using Autofac;
using Autofac.Core;
using Autofac.Integration.WebApi;
using CoffeeMachine.Abstraction;
using CoffeeMachine.Abstraction.Interfaces;
using CoffeeMachine.Infrastructure;
using System.Reflection;
using System.Web.Configuration;
using System.Web.Http;

namespace CoffeeMachineSkypeBot
{
	public class WebApiApplication : System.Web.HttpApplication
	{
		protected void Application_Start()
		{
			var builder = new ContainerBuilder();

			ConfigureBuilder(builder);

			var container = builder.Build();
			GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
			GlobalConfiguration.Configure(WebApiConfig.Register);
		}

		private static void ConfigureBuilder(ContainerBuilder builder)
		{
			builder.RegisterType<CommandHandler>().As<ICommandHandler>()
					.InstancePerRequest();
			builder.RegisterType<SQLDataService>().As<IDataService>()
					.InstancePerRequest();
			builder.RegisterType<StringDataProtector>().As<IDataProtector>()
					.InstancePerLifetimeScope();

			builder.RegisterType<UserActivityImporter>().As<IUserActivityImporter>()
					.InstancePerRequest();

			builder.RegisterType<ConnectionProducer>().As<IConnection>()
					.WithParameters(new[] { new ResolvedParameter((p, c) => p.Name == "protector", (p, c) => c.Resolve<IDataProtector>()),
											new ResolvedParameter((p,c)=> p.Name == "decrypted", (p,c)=> WebConfigurationManager.AppSettings["decrypted"])
										  })
					.InstancePerRequest();

			builder.RegisterType<CoffeeMachineContext>().As<CoffeeMachineContext>()
					.InstancePerRequest();

			builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
		}
	}
}
