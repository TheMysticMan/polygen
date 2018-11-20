using Autofac;
using Polygen.Templates.HandlebarsNet.Helpers;

namespace Polygen.Plugins.HandlebarsHelpers.CSharp
{
    public class AutofacModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // Register all template helpers
            builder
                .RegisterAssemblyTypes(typeof(AutofacModule).Assembly)
                .Where(x => x.IsAssignableTo<IHandlebarsHelperBase>())
                .As<IHandlebarsHelperBase>()
                .PropertiesAutowired()
                .SingleInstance();
        }
    }
}
