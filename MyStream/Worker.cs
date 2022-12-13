using Microsoft.Extensions.Options;
using Streamiz.Kafka.Net;

namespace MyStream;

public class Worker : BackgroundService
{
    private readonly StreamOptions _options;

    public Worker(IOptions<StreamOptions> options)
    {
        _options = options.Value;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // var configuration = new Dictionary<string, dynamic>
        // {
        //     ["ApplicationId"] = "Test",
        //     ["BootstrapServers"] = "localhost:9092"
        // };
        
        var configuration = new Dictionary<string, dynamic>
        {
            ["application.id"] = "Test",
            ["bootstrap.servers"] = "localhost:9092"
        };
        
        var streamConfig = new StreamConfig(configuration)
        {
            AutoOffsetReset = Confluent.Kafka.AutoOffsetReset.Earliest
        };

        var streamBuilder = new StreamBuilder();
        streamBuilder
            .Stream<byte[]?, byte[]?>(_options.InputTopic)
            .To(_options.OutputTopic);
        
        var topology = streamBuilder.Build();
        var stream = new KafkaStream(topology, streamConfig);
        await stream.StartAsync(stoppingToken);
    }
}