using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using XTI_App.Abstractions;
using XTI_App.Secrets;
using XTI_Core;
using XTI_Credentials;
using XTI_Hub;
using XTI_Secrets;

namespace XTI_UserApp;

internal sealed class HostedService : IHostedService
{
    private readonly IServiceProvider services;

    public HostedService(IServiceProvider services)
    {
        this.services = services;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = services.CreateScope();
        try
        {
            var userOptions = scope.ServiceProvider.GetRequiredService<IOptions<UserOptions>>().Value;
            if (userOptions.Command.Equals("user", StringComparison.OrdinalIgnoreCase))
            {
                await addUser(scope.ServiceProvider, userOptions);
            }
            else if (userOptions.Command.Equals("systemUser", StringComparison.OrdinalIgnoreCase))
            {
                await addSystemUser(scope.ServiceProvider, userOptions);
            }
            else if (userOptions.Command.Equals("installationUser", StringComparison.OrdinalIgnoreCase))
            {
                await addInstallationUser(scope.ServiceProvider, userOptions);
            }
            else if (userOptions.Command.Equals("userroles", StringComparison.OrdinalIgnoreCase))
            {
                await addUserRoles(scope.ServiceProvider, userOptions);
            }
            else if (userOptions.Command.Equals("credentials", StringComparison.OrdinalIgnoreCase))
            {
                await storeCredentials(scope.ServiceProvider, userOptions);
            }
            else
            {
                throw new NotSupportedException($"Command {userOptions.Command} is not supported");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            Environment.ExitCode = 999;
        }
        var lifetime = scope.ServiceProvider.GetRequiredService<IHostApplicationLifetime>();
        lifetime.StopApplication();
    }

    private static async Task addUser(IServiceProvider sp, UserOptions userOptions)
    {
        if (string.IsNullOrWhiteSpace(userOptions.UserName)) { throw new ArgumentException("User name is required"); }
        var appFactory = sp.GetRequiredService<AppFactory>();
        var clock = sp.GetRequiredService<IClock>();
        var hashedPasswordFactory = sp.GetRequiredService<IHashedPasswordFactory>();
        string password;
        if (string.IsNullOrWhiteSpace(userOptions.Password))
        {
            password = generatePassword();
        }
        else
        {
            password = userOptions.Password;
        }
        var userName = new AppUserName(userOptions.UserName);
        var hashedPassword = hashedPasswordFactory.Create(password);
        var userExists = await appFactory.Users.UserNameExists(userName);
        if (userExists)
        {
            var user = await appFactory.Users.UserByUserName(userName);
            await user.ChangePassword(hashedPassword);
        }
        else
        {
            await appFactory.Users.Add(userName, hashedPassword, clock.Now());
        }
    }

    private static async Task addSystemUser(IServiceProvider sp, UserOptions userOptions)
    {
        if (string.IsNullOrWhiteSpace(userOptions.AppName)) { throw new ArgumentException("App name is required"); }
        if (string.IsNullOrWhiteSpace(userOptions.AppType)) { throw new ArgumentException("App type is required"); }
        string password;
        if (string.IsNullOrWhiteSpace(userOptions.Password))
        {
            password = generatePassword();
        }
        else
        {
            password = userOptions.Password;
        }
        var appKey = new AppKey
        (
            new AppName(userOptions.AppName),
            AppType.Values.Value(userOptions.AppType)
        );
        if(appKey.Type.Equals(AppType.Values.WebApp) && string.IsNullOrWhiteSpace(userOptions.Domain))
        {
            throw new Exception($"Domain is required when app is a web app");
        }
        var appFactory = sp.GetRequiredService<AppFactory>();
        var hashedPasswordFactory = sp.GetRequiredService<IHashedPasswordFactory>();
        var clock = sp.GetRequiredService<IClock>();
        var hashedPassword = hashedPasswordFactory.Create(password);
        var machineName = string.IsNullOrWhiteSpace(userOptions.MachineName)
            ? Environment.MachineName
            : userOptions.MachineName;
        var systemUser = await appFactory.SystemUsers.AddOrUpdateSystemUser
        (
            appKey, 
            machineName, 
            userOptions.Domain, 
            hashedPassword, 
            clock.Now()
        );
        if (string.IsNullOrWhiteSpace(userOptions.MachineName))
        {
            var credentials = sp.GetRequiredService<SystemUserCredentials>();
            await credentials.Update
            (
                new CredentialValue
                (
                    systemUser.UserName().Value,
                    password
                )
            );
        }
    }

    private static async Task addInstallationUser(IServiceProvider sp, UserOptions userOptions)
    {
        string password;
        if (string.IsNullOrWhiteSpace(userOptions.Password))
        {
            password = generatePassword();
        }
        else
        {
            password = userOptions.Password;
        }
        var appFactory = sp.GetRequiredService<AppFactory>();
        var hashedPasswordFactory = sp.GetRequiredService<IHashedPasswordFactory>();
        var clock = sp.GetRequiredService<IClock>();
        var hashedPassword = hashedPasswordFactory.Create(password);
        var machineName = string.IsNullOrWhiteSpace(userOptions.MachineName)
            ? Environment.MachineName
            : userOptions.MachineName;
        var installationUser = await appFactory.InstallationUsers.AddOrUpdateInstallationUser(machineName, hashedPassword, clock.Now());
        if (string.IsNullOrWhiteSpace(userOptions.MachineName))
        {
            var credentials = sp.GetRequiredService<SystemUserCredentials>();
            await credentials.Update
            (
                new CredentialValue
                (
                    installationUser.UserName().Value,
                    password
                )
            );
        }
    }

    private static string generatePassword() => Guid.NewGuid().ToString("N") + "!?";

    private static async Task addUserRoles(IServiceProvider sp, UserOptions userOptions)
    {
        if (string.IsNullOrWhiteSpace(userOptions.UserName)) { throw new ArgumentException("User name is required"); }
        if (string.IsNullOrWhiteSpace(userOptions.AppName)) { throw new ArgumentException("App name is required"); }
        if (string.IsNullOrWhiteSpace(userOptions.AppType)) { throw new ArgumentException("App type is required"); }
        var appFactory = sp.GetRequiredService<AppFactory>();
        var userName = new AppUserName(userOptions.UserName);
        var user = await appFactory.Users.UserByUserName(userName);
        var appType = string.IsNullOrWhiteSpace(userOptions.AppType)
            ? AppType.Values.WebApp
            : AppType.Values.Value(userOptions.AppType);
        var app = await appFactory.Apps.App(new AppKey(userOptions.AppName, appType));
        var roles = new List<AppRole>();
        if (!string.IsNullOrWhiteSpace(userOptions.RoleNames))
        {
            foreach (var roleName in userOptions.RoleNames.Split(","))
            {
                if (!string.IsNullOrWhiteSpace(roleName))
                {
                    await app.AddRoleIfNotFound(new AppRoleName(roleName));
                }
            }
        }
        Modifier modifier;
        if (string.IsNullOrWhiteSpace(userOptions.ModKey))
        {
            modifier = await app.DefaultModifier();
        }
        else
        {
            var modCategory = await app.ModCategory(new ModifierCategoryName(userOptions.ModCategoryName));
            modifier = await modCategory.ModifierByTargetKey(userOptions.ModKey);
        }
        foreach (var role in roles)
        {
            await user.Modifier(modifier).AssignRole(role);
        }
    }

    private static async Task storeCredentials(IServiceProvider sp, UserOptions userOptions)
    {
        if (string.IsNullOrWhiteSpace(userOptions.CredentialKey)) { throw new ArgumentException("Credential Key is required"); }
        if (string.IsNullOrWhiteSpace(userOptions.UserName)) { throw new ArgumentException("User name is required"); }
        if (string.IsNullOrWhiteSpace(userOptions.Password)) { throw new ArgumentException("Password is required"); }
        var secretCredentialsFactory = sp.GetRequiredService<SecretCredentialsFactory>();
        var secretCredentials = secretCredentialsFactory.Create(userOptions.CredentialKey);
        await secretCredentials.Update
        (
            new CredentialValue(userOptions.UserName, userOptions.Password)
        );
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}