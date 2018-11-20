using Autofac;
using Polygen.Core.Template;

namespace Polygen.Templates.Razor
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<TemplateCollection>()
                .As<ITemplateCollection>()
                .SingleInstance();
        }
    }
}
