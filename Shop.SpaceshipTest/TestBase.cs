using Microsoft.Extensions.DependencyInjection;
using Shop.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Shop.SpaceshipTest.Macros;
using Shop.Core.ServiceInterface;
using Shop.ApplicationServices.Services;
using Microsoft.AspNetCore.Hosting;
using Shop.SpaceshipTest.Mock;
using Microsoft.Extensions.Hosting;

namespace Shop.SpaceshipTest
{
	public abstract class TestBase
	{
		protected IServiceProvider serviceProvider { get; }

		protected TestBase()
		{
			var services = new ServiceCollection();
			SetupServices(services);
			serviceProvider = services.BuildServiceProvider();
		}
		public void Dispose()
		{

		}

		protected T Svc<T>()
		{
			return serviceProvider.GetService<T>();
		}

		protected T Macro<T>() where T : IMacros
		{
			return serviceProvider.GetService<T>();
		}

		public virtual void SetupServices(IServiceCollection services)
		{
			//var testHostingEnvironment = new MockHostingEnvironment();
			//var builder = new WebHostBuilder()
			//            .Configure(app => { })
			//            .ConfigureServices(services =>
			//            {
			//                services.AddSingleton<IWebHostEnvironment>(testHostingEnvironment);
			//            });
			//var server = new ShopContext(builder);

			services.AddScoped<ISpaceshipsServices, SpaceshipsServices>();
			services.AddScoped<IFilesServices, FilesServices>();
			services.AddScoped<IHostEnvironment, MockIHostEnvironment>();


			services.AddDbContext<ShopContext>(x =>
			{
				x.UseInMemoryDatabase("TEST");
				x.ConfigureWarnings(e => e.Ignore(InMemoryEventId.TransactionIgnoredWarning));
			});

			RegisterMacros(services);
		}

		private void RegisterMacros(IServiceCollection services)
		{
			var macroBaseType = typeof(IMacros);

			var macros = macroBaseType.Assembly.GetTypes()
				.Where(x => macroBaseType.IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract);

			foreach (var macro in macros)
			{

				services.AddSingleton(macro);
			}
		}

	}
}
