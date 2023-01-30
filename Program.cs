var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!")
    .AddEndpointFilter(async (context, next) => {
        var endpoint = context.HttpContext.GetEndpoint();
        var sourceKey = endpoint?.Metadata.OfType<SourceKey>();
        var result = await next(context);
        if (sourceKey?.Any() == true)
        {
            return $"Source generator ran: {result}";
        }
        return "Source generator did not run...";
    });

app.Run();
