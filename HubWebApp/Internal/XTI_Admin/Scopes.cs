using Microsoft.Extensions.DependencyInjection;
using XTI_Core;

namespace XTI_Admin;

public sealed class Scopes
{
    private readonly IServiceProvider sp;
    private IServiceScope? currentScope;
    private IServiceScope? productionScope;

    public Scopes(IServiceProvider sp)
    {
        this.sp = sp;
    }

    public T GetRequiredService<T>() where T : notnull => Current().GetRequiredService<T>();

    private IServiceProvider Current()
    {
        currentScope ??= sp.CreateScope();
        return currentScope.ServiceProvider;
    }

    public IServiceProvider Production()
    {
        if (productionScope == null)
        {
            productionScope = sp.CreateScope();
            var xtiEnvAccessor = productionScope.ServiceProvider.GetRequiredService<XtiEnvironmentAccessor>();
            xtiEnvAccessor.UseProduction();
        }
        return productionScope.ServiceProvider;
    }
}
