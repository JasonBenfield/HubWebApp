using XTI_AdminService;
using XTI_Core;
using XTI_Core.Extensions;

#pragma warning disable CA1416 // Validate platform compatibility
var hostBuilder = WebApplication.CreateBuilder(args);
hostBuilder.Configuration.UseXtiConfiguration(hostBuilder.Environment, "", "", args);
hostBuilder.Services.AddSingleton(_ => XtiEnvironment.Parse(hostBuilder.Environment.EnvironmentName));
hostBuilder.Services.AddSingleton<AdminToolRunner>();
hostBuilder.Services.AddWindowsService();
if (args.Length == 0 || args[0].Equals("mode=console", StringComparison.OrdinalIgnoreCase))
{
    hostBuilder.Host.UseWindowsService();
}
var app = hostBuilder.Build();
app.Run((context) =>
{
    var runner = context.RequestServices.GetRequiredService<AdminToolRunner>();
    return runner.Run(context);
});
await app.RunAsync("http://*:61862");
#pragma warning restore CA1416 // Validate platform compatibility