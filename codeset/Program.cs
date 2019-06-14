using System.Linq;
using System.Reflection;

using Autofac;

using codeset.Services;

namespace codeset
{
    public class Program
    {
        //* Public Methods
        public static void Main(string[] args)
        {
            var container = ConfigureContainer();

            using (var scope = container.BeginLifetimeScope())
            {
                var app = scope.Resolve<IApplication>();
                app.Run(args);
            }
        }

        public static IContainer ConfigureContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<Application>().As<IApplication>();

            //* Services.Wrappers
            builder.RegisterAssemblyTypes(Assembly.Load(nameof(codeset)))
                .Where(type => type.Namespace.EndsWith(nameof(codeset.Services.Wrappers)))
                .As(type => type.GetInterfaces()
                    .FirstOrDefault(i => i.Name == "I" + type.Name));

            //* Services
            builder.RegisterType<CommandService>().As<ICommandService>();

            builder.RegisterType<PlatformService>().As<IPlatformService>()
                .SingleInstance();
            builder.RegisterType<SettingsService>().As<ISettingsService>()
                .SingleInstance();

            return builder.Build();
        }
    }
}