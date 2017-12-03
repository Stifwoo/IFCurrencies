using Autofac;
using IFCurrenciesApi.Context;

namespace IFCurrenciesApi.AutofacModules
{
    public class EFModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType(typeof(IFCurrenciesContext))
                .As(typeof(IContext)).InstancePerLifetimeScope();
        }
    }
}