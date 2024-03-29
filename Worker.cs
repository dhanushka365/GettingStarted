using MassTransit;

namespace GettingStarted;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;

    private readonly IBus _bus;

    public Worker(ILogger<Worker> logger, IBus bus)
    {
        _bus = bus;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            // queue name is getting from here object name
            await _bus.Publish(new Message { Text = $"The time is { DateTimeOffset.Now}"});
            //_logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            await Task.Delay(1000, stoppingToken);
        }
    }
}
