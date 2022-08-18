using XTI_Core;
using XTI_Hub.Abstractions;
using XTI_HubWebAppApi.AppInstall;
using XTI_HubWebAppApi.AppPublish;
using XTI_TempLog.Abstractions;

namespace HubWebApp.Tests;

internal sealed class PermanentLogTest
{
    [Test]
    public async Task ShouldStartSessionOnPermanentLog()
    {
        var tester = await setup();
        await tester.LoginAsAdmin();
        var sessionKey = generateKey();
        await startSession(tester, sessionKey);
        var factory = tester.Services.GetRequiredService<HubFactory>();
        var session = await factory.Sessions.Session(sessionKey);
        Assert.That(session.HasStarted(), Is.True, "Should start session on permanent log");
        Assert.That(session.HasEnded(), Is.False, "Should start session on permanent log");
    }

    [Test]
    public async Task ShouldStartRequestOnPermanentLog()
    {
        var tester = await setup();
        await tester.LoginAsAdmin();
        var sessionKey = generateKey();
        await startSession(tester, sessionKey);
        var requestKey = generateKey();
        await startRequest(tester, sessionKey, requestKey);
        var factory = tester.Services.GetRequiredService<HubFactory>();
        var session = await factory.Sessions.Session(sessionKey);
        var requests = await session.Requests();
        Assert.That(requests.Length, Is.EqualTo(1), "Should start request on permanent log");
    }

    [Test]
    public async Task ShouldEndRequestOnPermanentLog()
    {
        var tester = await setup();
        await tester.LoginAsAdmin();
        var sessionKey = generateKey();
        await startSession(tester, sessionKey);
        var requestKey = generateKey();
        await startRequest(tester, sessionKey, requestKey);
        await endRequest(tester, requestKey);
        var factory = tester.Services.GetRequiredService<HubFactory>();
        var session = await factory.Sessions.Session(sessionKey);
        var requests = await session.Requests();
        Assert.That(requests[0].HasEnded(), Is.True, "Should end request on permanent log");
    }

    [Test]
    public async Task ShouldEndSessionOnPermanentLog()
    {
        var tester = await setup();
        await tester.LoginAsAdmin();
        var sessionKey = generateKey();
        await startSession(tester, sessionKey);
        var requestKey = generateKey();
        await startRequest(tester, sessionKey, requestKey);
        await endRequest(tester, requestKey);
        await endSession(tester, sessionKey);
        var factory = tester.Services.GetRequiredService<HubFactory>();
        var session = await factory.Sessions.Session(sessionKey);
        Assert.That(session.HasEnded(), Is.True, "Should end session on permanent log");
    }

    [Test]
    public async Task ShouldAuthenticateSessionOnPermanentLog()
    {
        var tester = await setup();
        await tester.LoginAsAdmin();
        var sessionKey = generateKey();
        await startSession(tester, sessionKey);
        await authenticateSession(tester, sessionKey);
        var factory = tester.Services.GetRequiredService<HubFactory>();
        var session = await factory.Sessions.Session(sessionKey);
        var user = await factory.Users.User(session.UserID);
        Assert.That(user.ToModel().UserName, Is.EqualTo("someone"), "Should authenticate session on permanent log");
    }

    [Test]
    public async Task ShouldLogEventOnPermanentLog()
    {
        var tester = await setup();
        await tester.LoginAsAdmin();
        var sessionKey = generateKey();
        await startSession(tester, sessionKey);
        var requestKey = generateKey();
        await startRequest(tester, sessionKey, requestKey);
        Exception exception;
        try
        {
            throw new Exception("Test");
        }
        catch (Exception ex)
        {
            exception = ex;
        }
        await logEvent(tester, requestKey, exception);
        var factory = tester.Services.GetRequiredService<HubFactory>();
        var session = await factory.Sessions.Session(sessionKey);
        var requests = (await session.Requests()).ToArray();
        var events = (await requests[0].Events()).ToArray();
        Assert.That(events.Length, Is.EqualTo(1), "Should log event on permanent log");
    }

    private static Task startSession(HubActionTester<LogBatchModel, EmptyActionResult> tester, string sessionKey)
    {
        var clock = tester.Services.GetRequiredService<IClock>();
        var startSessionModel = new StartSessionModel
        {
            SessionKey = sessionKey,
            RemoteAddress = "my-computer",
            UserAgent = "Windows 10",
            TimeStarted = clock.Now(),
            UserName = "someone"
        };
        return tester.Execute(new LogBatchModel { StartSessions = new[] { startSessionModel } });
    }

    private static async Task startRequest(HubActionTester<LogBatchModel, EmptyActionResult> tester, string sessionKey, string requestKey)
    {
        var clock = tester.Services.GetRequiredService<IClock>();
        var installationID = await tester.Services.GetRequiredService<InstallationIDAccessor>().Value();
        var startRequestModel = new StartRequestModel
        {
            RequestKey = requestKey,
            SessionKey = sessionKey,
            TimeStarted = clock.Now(),
            InstallationID = installationID,
            Path = "/Fake/Current/Test/Action1"
        };
        await tester.Execute(new LogBatchModel { StartRequests = new[] { startRequestModel } });
    }

    private static Task endRequest(HubActionTester<LogBatchModel, EmptyActionResult> tester, string requestKey)
    {
        var clock = tester.Services.GetRequiredService<IClock>();
        return tester.Execute
        (
            new LogBatchModel
            {
                EndRequests = new[]
                {
                    new EndRequestModel
                    {
                        RequestKey = requestKey,
                        TimeEnded = clock.Now()
                    }
                }
            }
        );
    }

    private static Task endSession(HubActionTester<LogBatchModel, EmptyActionResult> tester, string sessionKey)
    {
        var clock = tester.Services.GetRequiredService<IClock>();
        return tester.Execute
        (
            new LogBatchModel
            {
                EndSessions = new[]
                {
                    new EndSessionModel
                    {
                        SessionKey = sessionKey,
                        TimeEnded = clock.Now()
                    }
                }
            }
        );
    }

    private static Task authenticateSession(HubActionTester<LogBatchModel, EmptyActionResult> tester, string sessionKey) =>
        tester.Execute
        (
            new LogBatchModel
            {
                AuthenticateSessions = new[]
                {
                    new AuthenticateSessionModel
                    {
                        SessionKey = sessionKey,
                        UserName = "someone"
                    }
                }
            }
        );

    private static Task logEvent(HubActionTester<LogBatchModel, EmptyActionResult> tester, string requestKey, Exception exception) =>
        tester.Execute
        (
            new LogBatchModel
            {
                LogEntries = new[]
                {
                    new LogEntryModel
                    {
                        EventKey = generateKey(),
                        RequestKey = requestKey,
                        Severity = AppEventSeverity.Values.CriticalError,
                        Caption = "An unexpected error occurred",
                        Message = exception.Message,
                        Detail = exception.StackTrace ?? ""
                    }
                }
            }
        );

    private static string generateKey() => Guid.NewGuid().ToString("N");

    private async Task<HubActionTester<LogBatchModel, EmptyActionResult>> setup()
    {
        var host = new HubTestHost();
        var sp = await host.Setup();
        var hubFactory = sp.GetRequiredService<HubFactory>();
        var clock = sp.GetRequiredService<IClock>();
        var apiFactory = sp.GetRequiredService<HubAppApiFactory>();
        var hubApi = apiFactory.CreateForSuperUser();
        await hubApi.Install.AddOrUpdateApps.Invoke(new AddOrUpdateAppsRequest
        {
            VersionName = new AppVersionName("FakeWebApp"),
            Apps = new[] { new AppDefinitionModel(AppKey.WebApp("Fake")) }
        });
        var version = await hubApi.Publish.NewVersion.Invoke(new NewVersionRequest
        {
            VersionName = new AppVersionName("FakeWebApp"),
            VersionType = AppVersionType.Values.Major
        });
        await hubApi.Publish.BeginPublish.Invoke(new PublishVersionRequest
        {
            VersionName = version.VersionName,
            VersionKey = version.VersionKey
        });
        await hubApi.Publish.EndPublish.Invoke(new PublishVersionRequest
        {
            VersionName = version.VersionName,
            VersionKey = version.VersionKey
        });
        var newInstResult = await hubApi.Install.NewInstallation.Invoke(new NewInstallationRequest
        {
            AppKey = AppKey.WebApp("Fake"),
            VersionName = version.VersionName,
            QualifiedMachineName = "destination.xartogg.com",
            Domain = "test.xartogg.com"
        });
        await hubApi.Install.BeginInstallation.Invoke(new InstallationRequest
        {
            InstallationID = newInstResult.CurrentInstallationID
        });
        var installationIDAccessor = sp.GetRequiredService<FakeInstallationIDAccessor>();
        installationIDAccessor.SetInstallationID(newInstResult.CurrentInstallationID);
        var userGroup = await hubFactory.UserGroups.GetGeneral();
        await userGroup.AddOrUpdate(new AppUserName("test.user"), new FakeHashedPassword("Password12345"), DateTime.Now);
        await userGroup.AddOrUpdate(new AppUserName("Someone"), new FakeHashedPassword("Password12345"), DateTime.Now);

        return HubActionTester.Create(sp, hubApi => hubApi.PermanentLog.LogBatch);
    }
}