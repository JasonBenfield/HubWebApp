// Generated Code
namespace XTI_HubAppClient;
public sealed partial class HubAppClient : AppClient
{
    public const string DefaultVersion = "V1169";
    public HubAppClient(IHttpClientFactory httpClientFactory, IXtiTokenFactory tokenFactory, string baseUrl, string version = DefaultVersion) : base(httpClientFactory, baseUrl, "Hub", string.IsNullOrWhiteSpace(version) ? DefaultVersion : version)
    {
        xtiToken = tokenFactory.Create(this);
        User = new UserGroup(httpClientFactory, xtiToken, url);
        SetJsonSerializerOptions(User);
        UserCache = new UserCacheGroup(httpClientFactory, xtiToken, url);
        SetJsonSerializerOptions(UserCache);
        Auth = new AuthGroup(httpClientFactory, xtiToken, url);
        SetJsonSerializerOptions(Auth);
        AuthApi = new AuthApiGroup(httpClientFactory, xtiToken, url);
        SetJsonSerializerOptions(AuthApi);
        PermanentLog = new PermanentLogGroup(httpClientFactory, xtiToken, url);
        SetJsonSerializerOptions(PermanentLog);
        Apps = new AppsGroup(httpClientFactory, xtiToken, url);
        SetJsonSerializerOptions(Apps);
        App = new AppGroup(httpClientFactory, xtiToken, url);
        SetJsonSerializerOptions(App);
        Install = new InstallGroup(httpClientFactory, xtiToken, url);
        SetJsonSerializerOptions(Install);
        Publish = new PublishGroup(httpClientFactory, xtiToken, url);
        SetJsonSerializerOptions(Publish);
        Version = new VersionGroup(httpClientFactory, xtiToken, url);
        SetJsonSerializerOptions(Version);
        ResourceGroup = new ResourceGroupGroup(httpClientFactory, xtiToken, url);
        SetJsonSerializerOptions(ResourceGroup);
        Resource = new ResourceGroup(httpClientFactory, xtiToken, url);
        SetJsonSerializerOptions(Resource);
        ModCategory = new ModCategoryGroup(httpClientFactory, xtiToken, url);
        SetJsonSerializerOptions(ModCategory);
        Users = new UsersGroup(httpClientFactory, xtiToken, url);
        SetJsonSerializerOptions(Users);
        UserInquiry = new UserInquiryGroup(httpClientFactory, xtiToken, url);
        SetJsonSerializerOptions(UserInquiry);
        AppUser = new AppUserGroup(httpClientFactory, xtiToken, url);
        SetJsonSerializerOptions(AppUser);
        AppUserMaintenance = new AppUserMaintenanceGroup(httpClientFactory, xtiToken, url);
        SetJsonSerializerOptions(AppUserMaintenance);
        UserMaintenance = new UserMaintenanceGroup(httpClientFactory, xtiToken, url);
        SetJsonSerializerOptions(UserMaintenance);
    }

    public UserGroup User { get; }

    public UserCacheGroup UserCache { get; }

    public AuthGroup Auth { get; }

    public AuthApiGroup AuthApi { get; }

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