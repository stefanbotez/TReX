using Autofac;
using TReX.Discovery.Documents.Archeology.Twitter;
using TReX.Discovery.Documents.Archeology.Wikipedia;
using TReX.Discovery.Shared.Business;

namespace TReX.Discovery.Documents.DependencyInjection
{
    public sealed class ArcheologyModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<TwitterDocumentArcheologist>()
                .As<IArcheologist>()
                .InstancePerLifetimeScope();

            builder.RegisterType<TwitterDocumentProvider>()
                .InstancePerLifetimeScope();

            //builder.RegisterType<WikipediaDocumentArcheologist>()
            //    .As<IArcheologist>()
            //    .InstancePerLifetimeScope();

            builder.RegisterType<WikipediaDocumentProvider>()
                .InstancePerLifetimeScope();
        }
    }
}