using System.Text.Json;

namespace XTI_HubAppClient;

partial class HubAppClient : IAuthClient
{
    IAuthApiClientGroup IAuthClient.AuthApi { get => AuthApi; }

    protected override void ConfigureJsonSerializerOptions(JsonSerializerOptions options)
    {
        base.ConfigureJsonSerializerOptions(options);
        options.Converters.Add(new NumericValueJsonConverter());
        options.Converters.Add(new AppKeyJsonConverter());
        options.Converters.Add(new AppVersionKeyJsonConverter());
    }
}