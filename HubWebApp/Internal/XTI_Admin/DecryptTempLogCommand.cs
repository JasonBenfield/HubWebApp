using System.Text.Json;
using XTI_Core;
using XTI_TempLog;
using XTI_TempLog.Abstractions;

namespace XTI_Admin;

internal sealed class DecryptTempLogCommand : ICommand
{
    private readonly IClock clock;
    private readonly TempLog tempLog;
    private readonly XtiFolder xtiFolder;

    public DecryptTempLogCommand(IClock clock, TempLog tempLog, XtiFolder xtiFolder)
    {
        this.clock = clock;
        this.tempLog = tempLog;
        this.xtiFolder = xtiFolder;
    }

    public async Task Execute(CancellationToken ct)
    {
        var modifiedBefore = clock.Now();
        var appDataFolder = xtiFolder.AppDataFolder()
            .WithSubFolder("DecryptedTempLog");
        appDataFolder.TryCreate();
        var sessionDetails = await GetSessionDetails(modifiedBefore);
        await WriteSessionDetails(appDataFolder, sessionDetails);
    }

    private async Task<TempLogSessionDetailModel[]> GetSessionDetails(DateTimeOffset modifiedBefore)
    {
        var logFiles = tempLog.Files(modifiedBefore);
        var sessionDetails = new List<TempLogSessionDetailModel>();
        foreach(var logFile in logFiles)
        {
            var fileSessionDetails = await logFile.Read();
            sessionDetails.AddRange(fileSessionDetails);
        }
        return sessionDetails.ToArray();
    }

    private static async Task WriteSessionDetails(AppDataFolder folder, TempLogSessionDetailModel[] sessionDetails)
    {
        var path = folder.FilePath($"decrypted_{DateTime.Now:yyMMddHHmmssfff}.json");
        using var writer = new StreamWriter(path, false);
        var serialized = JsonSerializer.Serialize(sessionDetails, new JsonSerializerOptions { WriteIndented = true });
        await writer.WriteAsync(serialized);
    }
}
