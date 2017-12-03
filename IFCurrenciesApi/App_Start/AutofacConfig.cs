using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using IFCurrenciesApi.AutofacModules;

namespace IFCurrenciesApi
{
    public class AutofacConfig
    {
        public static void Configure(HttpConfiguration config)
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule<ServiceModule>();
            builder.RegisterModule<EFModule>();

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly()).PropertiesAutowired().InstancePerRequest();

            var container = builder.Build();
            // Set the dependency resolver to be Autofac (For Web API)
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

    }
}