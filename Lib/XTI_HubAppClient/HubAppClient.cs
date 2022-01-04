// Generated Code
namespace XTI_HubAppClient;
public sealed partial class HubAppClient : AppClient
{
    public const string DefaultVersion = "V1169";
    public HubAppClient(IHttpClientFactory httpClientFactory, IXtiTokenFactory tokenFactory, AppClientUrl clientUrl, string version = DefaultVersion) : base(httpClientFactory, clientUrl, "Hub", string.IsNullOrWhiteSpace(version) ? DefaultVersion : version)
    {
        xtiToken = tokenFactory.Create(this);
        User = new UserGroup(httpClientFactory, xtiToken, clientUrl);
        SetJsonSerializerOptions(User);
        UserCache = new UserCacheGroup(httpClientFactory, xtiToken, clientUrl);
        SetJsonSerializerOptions(UserCache);
        Auth = new AuthGroup(httpClientFactory, xtiToken, clientUrl);
        SetJsonSerializerOptions(Auth);
        AuthApi = new AuthApiGroup(httpClientFactory, xtiToken, clientUrl);
        SetJsonSerializerOptions(AuthApi);
        ExternalAuth = new ExternalAuthGroup(httpClientFactory, xtiToken, clientUrl);
        SetJsonSerializerOptions(ExternalAuth);
        Authenticators = new AuthenticatorsGroup(httpClientFactory, xtiToken, clientUrl);
        SetJsonSerializerOptions(Authenticators);
        PermanentLog = new PermanentLogGroup(httpClientFactory, xtiToken, clientUrl);
        SetJsonSerializerOptions(PermanentLog);
        Apps = new AppsGroup(httpClientFactory, xtiToken, clientUrl);
        SetJsonSerializerOptions(Apps);
        App = new AppGroup(httpClientFactory, xtiToken, clientUrl);
        SetJsonSerializerOptions(App);
        Install = new InstallGroup(httpClientFactory, xtiToken, clientUrl);
        SetJsonSerializerOptions(Install);
        Publish = new PublishGroup(httpClientFactory, xtiToken, clientUrl);
        SetJsonSerializerOptions(Publish);
        Version = new VersionGroup(httpClientFactory, xtiToken, clientUrl);
        SetJsonSerializerOptions(Version);
        ResourceGroup = new ResourceGroupGroup(httpClientFactory, xtiToken, clientUrl);
        SetJsonSerializerOptions(ResourceGroup);
        Resource = new ResourceGroup(httpClientFactory, xtiToken, clientUrl);
        SetJsonSerializerOptions(Resource);
        ModCategory = new ModCategoryGroup(httpClientFactory, xtiToken, clientUrl);
        SetJsonSerializerOptions(ModCategory);
        Users = new UsersGroup(httpClientFactory, xtiToken, clientUrl);
        SetJsonSerializerOptions(Users);
        UserInquiry = new UserInquiryGroup(httpClientFactory, xtiToken, clientUrl);
        SetJsonSerializerOptions(UserInquiry);
        AppUser = new AppUserGroup(httpClientFactory, xtiToken, clientUrl);
        SetJsonSerializerOptions(AppUser);
        AppUserMaintenance = new AppUserMaintenanceGroup(httpClientFactory, xtiToken, clientUrl);
        SetJsonSerializerOptions(AppUserMaintenance);
        UserMaintenance = new UserMaintenanceGroup(httpClientFactory, xtiToken, clientUrl);
        SetJsonSerializerOptions(UserMaintenance);
    }

    public UserGroup User { get; }

    public UserCacheGroup UserCache { get; }

    public AuthGroup Auth { get; }

    public AuthApiGroup AuthApi { get; }

    public ExternalAuthGroup ExternalAuth { get; }

    public AuthenticatorsGroup Authenticators { get; }

    public PermanentLogGroup PermanentLog { get; }

    public AppsGroup Apps { get; }

    public AppGroup App { get; }

    public InstallGroup Install { get; }

    public PublishGroup Publish { get; }

    public VersionGroup Version { get; }

    public ResourceGroupGroup ResourceGroup { get; }

    public ResourceGroup Resource { get; }

    public ModCategoryGroup ModCategory { get; }

    public UsersGroup Users { get; }

    public UserInquiryGroup UserInquiry { get; }

    public AppUserGroup AppUser { get; }

    public AppUserMaintenanceGroup AppUserMaintenance { get; }

    public UserMaintenanceGroup UserMaintenance { get; }
}