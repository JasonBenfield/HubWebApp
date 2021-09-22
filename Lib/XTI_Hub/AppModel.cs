using XTI_App.Abstractions;

namespace XTI_Hub
{
    public sealed class AppModel
    {
        public int ID { get; set; }
        public AppType Type { get; set; }
        public string AppName { get; set; }
        public string Title { get; set; }
    }
}
