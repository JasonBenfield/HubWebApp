using XTI_Core;
using XTI_HubWebAppApiActions;

namespace XTI_HubWebAppApi;

partial class HubAppApiBuilder
{
    partial void Configure()
    {
        source.SerializedDefaultOptions = XtiSerializer.Serialize(new HubWebAppOptions());
        source.ConfigureTemplate
        (
            template =>
            {
                template.ExcludeValueTemplates(IsValueTemplateExcluded);
            }
        );
        UserQuery.ResetAccess();
    }

    private static bool IsValueTemplateExcluded(ValueTemplate template, ApiCodeGenerators codeGenerator)
    {
        if (codeGenerator == ApiCodeGenerators.Dotnet)
        {
            return template.DataType == typeof(AuthenticateRequest) ||
                template.DataType.Namespace == "XTI_App.Abstractions" ||
                template.DataType.Namespace == "XTI_WebApp.Abstractions" ||
                template.DataType.Namespace == "XTI_TempLog.Abstractions" ||
                template.DataType.Namespace == "XTI_Hub.Abstractions";
        }
        return false;
    }
}