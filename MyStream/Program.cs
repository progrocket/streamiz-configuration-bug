using MyStream;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.Configure<StreamOptions>(context.Configuration.GetSection(nameof(StreamOptions)));
        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();