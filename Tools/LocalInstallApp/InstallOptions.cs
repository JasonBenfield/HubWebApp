namespace LocalInstallApp
{
    public sealed class InstallOptions
    {
        public string AppName { get; set; }
        public string AppType { get; set; }
        public string VersionKey { get; set; }
        public string SystemUserName { get; set; }
        public string SystemPassword { get; set; }
        public string RepoOwner { get; set; }
        public string RepoName { get; set; }
        public string Release { get; set; }
    }
}
