using Autofac;
using Polygen.Core.Stage;
using Polygen.Plugins.NHibernate.Output.Backend;
using Polygen.Plugins.NHibernate.Output.Backend.Entity;
using Polygen.Plugins.NHibernate.Output.Backend.Repository;

namespace Polygen.Plugins.NHibernate
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // Register all stage handlers.
            builder
                .RegisterAssemblyTypes(typeof(AutofacModule).Assembly)
                .Where(x => x.IsAssignableTo<IStageHandler>())
                .As<IStageHandler>()
                .PropertiesAutowired()
                .SingleInstance();

            builder
                .RegisterType<EntityConverter>()
                .AsSelf()
                .SingleInstance();
            builder
                .RegisterType<EntityOutputModelGenerator>()
                .AsSelf()
                .SingleInstance();

            builder
                .RegisterType<RepositoryConverter>()
                .AsSelf()
                .SingleInstance();
            builder
                .RegisterType<RepositoryOutputModelGenerator>()
                .AsSelf()
                .SingleInstance();
        }
    }
}
