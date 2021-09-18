namespace HubSetupApp
{
    public sealed class SetupOptions
    {
        public static readonly string Setup = nameof(Setup);

        public string VersionKey { get; set; }
        public string VersionsPath { get; set; }
    }
}
