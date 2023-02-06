using XTI_App.Abstractions;
using XTI_WebApp.Api;

namespace HubWebApp.Extensions;

public sealed class MainMenuDefinition
{
    public MainMenuDefinition()
    {
        HomeLink = new LinkModel("home", "Home", "~/");
        UserLink = new LinkModel("user", "{User.FullName}", "~/User/UserProfile");
        LogoutLink = new LinkModel("logout", "Logout", "~/User/Logout");
        Value = new MenuDefinition
        (
            "main",
            HomeLink,
            UserLink,
            LogoutLink
        );
    }

    public MenuDefinition Value { get; }
    public LinkModel HomeLink { get; }
    public LinkModel UserLink { get; }
    public LinkModel LogoutLink { get; }

    public MenuDefinition Modify(params LinkModel[] links) =>
        new MenuDefinition(Value.MenuName, links);
}
