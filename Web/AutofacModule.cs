using Autofac;
using Web.Repository;

namespace Web
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterGeneric(typeof(BaseRepository<>))
                .As(typeof(IRepository<>))
                .SingleInstance();
        }
    }
}