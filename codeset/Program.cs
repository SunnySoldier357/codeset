using Autofac;

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

            return builder.Build();
        }
    }
}