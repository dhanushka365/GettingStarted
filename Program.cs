//using GettingStarted;

//IHost host = Host.CreateDefaultBuilder(args)
//    .ConfigureServices(services =>
//    {
//        services.AddHostedService<Worker>();
//    })
//    .Build();

//host.Run();


using MassTransit;

// rabbitmq exchange name is getting from here
namespace GettingStarted
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    // Add MassTransit services
                    services.AddMassTransit(x =>
                    {
                        x.AddConsumer<MessageConsumer>();

                        x.UsingRabbitMq((context, cfg) =>
                        {
                            cfg.ConfigureEndpoints(context);
                        });
                    });

                    // Add MassTransit hosted service
                    services.AddMassTransitHostedService(true);

                    // Add your custom hosted service (Worker)
                    services.AddHostedService<Worker>();
                });
        }
    }
}