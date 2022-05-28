namespace XTI_Admin;

public interface IAdminTokenAccessor
{
    void UseAnonymousToken();

    void UseInstallerToken();
}
