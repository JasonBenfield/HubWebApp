// Generated Code
using System;
using System.Collections.Generic;

namespace XTI_HubAppClient
{
    public sealed partial class AppApiGroupTemplateModel
    {
        public string Name { get; set; }

        public string ModCategory { get; set; }

        public bool IsAnonymousAllowed { get; set; }

        public string[] Roles { get; set; }

        public AppApiActionTemplateModel[] ActionTemplates { get; set; }
    }
}