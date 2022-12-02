using XTI_App.Abstractions;

namespace XTI_Hub.Abstractions;

public sealed record AppVersionInstallationModel(AppModel App, XtiVersionModel Version, InstallationModel Installation)
{
	public AppVersionInstallationModel()
		:this(new AppModel(), new XtiVersionModel(), new InstallationModel())
	{
	}

	public bool IsWebApp() => App.AppKey.Type.Equals(AppType.Values.WebApp);

    public bool IsServiceApp() => App.AppKey.Type.Equals(AppType.Values.ServiceApp);

    public bool IsConsoleApp() => App.AppKey.Type.Equals(AppType.Values.ConsoleApp);
}
