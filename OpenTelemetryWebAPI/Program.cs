using OpenTelemetry.Logs;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using OpenTelemetryWebAPI;

var builder = WebApplication.CreateBuilder(args);

builder.Logging
            .ClearProviders()
            .AddConsole()
            .AddDebug()
            .AddOpenTelemetry(opt =>
            {
                opt.AddConsoleExporter()
                    .SetResourceBuilder(ResourceBuilder
                                                    .CreateDefault().AddService("Logging.NET"))
                    .AddProcessor(new ActivityEventLogProcessor())
                    .IncludeScopes = true;
            });

builder.Services.AddOpenTelemetry()
    .WithTracing(builder =>
                    builder
                            .AddSource("Tracing.NET")
                            .AddAspNetCoreInstrumentation()
                            .SetResourceBuilder(ResourceBuilder
                                                    .CreateDefault().AddService("Tracing.NET"))
                            .AddConsoleExporter()
                            .AddJaegerExporter());

// Add services to the container.

builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
