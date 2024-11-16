using XTI_Core;
using XTI_TempLog.Abstractions;

namespace HubWebApp.Tests;

internal sealed class LogSessionDetailsTest
{
    [Test]
    public async Task ShouldAddSession()
    {
        var sp = await Setup();
        var clock = sp.GetRequiredService<IClock>();
        var sessionKey = GenerateKey();
        var requesterKey = GenerateKey();
        var timeEnded = clock.Now().AddHours(1);
        await LogSessionDetails
        (
            sp,
            new TempLogSessionDetailModel
            (
                session: new TempLogSessionModel
                {
                    UserName = "test.user",
                    TimeStarted = clock.Now(),
                    TimeEnded = timeEnded,
                    UserAgent = "Opera",
                    RemoteAddress = "127.0.0.1",
                    SessionKey = sessionKey,
                    RequesterKey = requesterKey
                },
                requestDetails: []
            )
        );
        var sessionDetails = await GetSessionDetails(sp);
        Assert.That(sessionDetails.Length, Is.EqualTo(1), "Should add session");
        Assert.That(sessionDetails[0].User.UserName, Is.EqualTo(new AppUserName("test.user")), "Should add session");
        Assert.That(sessionDetails[0].Session.SessionKey, Is.EqualTo(sessionKey), "Should add session");
        Assert.That(sessionDetails[0].Session.RequesterKey, Is.EqualTo(requesterKey), "Should add session");
        Assert.That(sessionDetails[0].Session.TimeStarted, Is.EqualTo(clock.Now()), "Should add session");
        Assert.That(sessionDetails[0].Session.TimeEnded, Is.EqualTo(timeEnded), "Should add session");
        Assert.That(sessionDetails[0].Session.UserAgent, Is.EqualTo("Opera"), "Should add session");
        Assert.That(sessionDetails[0].Session.RemoteAddress, Is.EqualTo("127.0.0.1"), "Should add session");
    }

    [Test]
    public async Task ShouldAddMultipleSessions()
    {
        var sp = await Setup();
        var session1 = CreateSession(sp);
        var session2 = CreateSession(sp);
        await LogSessionDetails
        (
            sp,
            new TempLogSessionDetailModel
            (
                session: session1,
                requestDetails: []
            ),
            new TempLogSessionDetailModel
            (
                session: session2,
                requestDetails: []
            )
        );
        var sessionDetails = await GetSessionDetails(sp);
        Assert.That(sessionDetails.Length, Is.EqualTo(2), "Should add multliple sessions");
    }

    [Test]
    public async Task ShouldNotAddMultipleSessionsWithTheSameKey()
    {
        var sp = await Setup();
        var session1 = CreateSession(sp);
        var session2 = CreateSession(sp, session1.SessionKey);
        await LogSessionDetails
        (
            sp,
            new TempLogSessionDetailModel
            (
                session: session1,
                requestDetails: []
            )
        );
        await LogSessionDetails
        (
            sp,
            new TempLogSessionDetailModel
            (
                session: session2,
                requestDetails: []
            )
        );
        var sessionDetails = await GetSessionDetails(sp);
        Assert.That(sessionDetails.Length, Is.EqualTo(1), "Should add not add multiple sessions with the same key");
    }

    [Test]
    public async Task ShouldNotChangeSessionUserNameToAnon()
    {
        var sp = await Setup();
        var session1 = CreateSession(sp);
        session1.UserName = "test.user";
        var session2 = CreateSession(sp, session1.SessionKey);
        session2.UserName = AppUserName.Anon.Value;
        await LogSessionDetails
        (
            sp,
            new TempLogSessionDetailModel
            (
                session: session1,
                requestDetails: []
            )
        );
        await LogSessionDetails
        (
            sp,
            new TempLogSessionDetailModel
            (
                session: session2,
                requestDetails: []
            )
        );
        var sessionDetails = await GetSessionDetails(sp);
        var sessionDetail = sessionDetails.FirstOrDefault() ?? new();
        Assert.That(sessionDetail.User.UserName, Is.EqualTo(new AppUserName("test.user")), "Should not change session user name to anon");
    }

    [Test]
    public async Task ShouldNotOverrideSessionTimeStartedWithLaterTime()
    {
        var sp = await Setup();
        var clock = sp.GetRequiredService<IClock>();
        var session1 = CreateSession(sp);
        session1.TimeStarted = clock.Now();
        var session2 = CreateSession(sp, session1.SessionKey);
        session2.TimeStarted = clock.Now().AddHours(1);
        await LogSessionDetails
        (
            sp,
            new TempLogSessionDetailModel
            (
                session: session1,
                requestDetails: []
            )
        );
        await LogSessionDetails
        (
            sp,
            new TempLogSessionDetailModel
            (
                session: session2,
                requestDetails: []
            )
        );
        var sessionDetails = await GetSessionDetails(sp);
        var sessionDetail = sessionDetails.FirstOrDefault() ?? new();
        Assert.That(sessionDetail.Session.TimeStarted, Is.EqualTo(session1.TimeStarted), "Should not override session time started with later time");
    }

    [Test]
    public async Task ShouldNotOverrideSessionTimeEndedWithMax()
    {
        var sp = await Setup();
        var clock = sp.GetRequiredService<IClock>();
        var session1 = CreateSession(sp);
        session1.TimeEnded = clock.Now().AddHours(1);
        var session2 = CreateSession(sp, session1.SessionKey);
        session2.TimeEnded = DateTimeOffset.MaxValue;
        await LogSessionDetails
        (
            sp,
            new TempLogSessionDetailModel
            (
                session: session1,
                requestDetails: []
            )
        );
        await LogSessionDetails
        (
            sp,
            new TempLogSessionDetailModel
            (
                session: session2,
                requestDetails: []
            )
        );
        var sessionDetails = await GetSessionDetails(sp);
        var sessionDetail = sessionDetails.FirstOrDefault() ?? new();
        Assert.That(sessionDetail.Session.TimeEnded, Is.EqualTo(session1.TimeEnded), "Should not override session time ended with max");
    }

    [Test]
    public async Task ShouldAddRequest()
    {
        var sp = await Setup();
        var clock = sp.GetRequiredService<IClock>();
        var requestKey = GenerateKey();
        var timeEnded = clock.Now().AddHours(1);
        var installation = await BeginInstallation(sp);
        var session = CreateSession(sp);
        await LogSessionDetails
        (
            sp,
            new TempLogSessionDetailModel
            (
                session: session,
                requestDetails: [
                    new TempLogRequestDetailModel
                    {
                        Request = new TempLogRequestModel
                        {
                            RequestKey = requestKey,
                            Path = "/Fake/Current",
                            TimeStarted = clock.Now(),
                            TimeEnded = timeEnded,
                            ActualCount = 5,
                            InstallationID = installation.CurrentInstallationID
                        }
                    }
                ]
            )
        );
        var sessionDetail = await GetSessionDetail(sp, session.SessionKey);
        var requestDetails = await GetRequestDetails(sp, sessionDetail);
        Assert.That(requestDetails.Length, Is.EqualTo(1), "Should add request");
        Assert.That(requestDetails[0].Request.Path, Is.EqualTo("/Fake/Current"), "Should add request");
        Assert.That(requestDetails[0].Request.TimeStarted, Is.EqualTo(clock.Now()), "Should add request");
        Assert.That(requestDetails[0].Request.TimeEnded, Is.EqualTo(timeEnded), "Should add request");
        Assert.That(requestDetails[0].Installation.ID, Is.EqualTo(installation.CurrentInstallationID), "Should add request");
        Assert.That(requestDetails[0].Request.ActualCount, Is.EqualTo(5), "Should add request");
    }

    [Test]
    public async Task ShouldNotAddMultipleRequestsWithTheSameKey()
    {
        var sp = await Setup();
        var session = CreateSession(sp);
        var request1 = await CreateRequest(sp);
        var request2 = await CreateRequest(sp, request1.RequestKey);
        await LogSessionDetails
        (
            sp,
            new TempLogSessionDetailModel
            (
                session: session,
                requestDetails: [
                    new TempLogRequestDetailModel
                    {
                        Request = request1
                    }
                ]
            )
        );
        await LogSessionDetails
        (
            sp,
            new TempLogSessionDetailModel
            (
                session: session,
                requestDetails: [
                    new TempLogRequestDetailModel
                    {
                        Request = request2
                    }
                ]
            )
        );
        var sessionDetail = await GetSessionDetail(sp, session.SessionKey);
        var requestDetails = await GetRequestDetails(sp, sessionDetail);
        Assert.That(requestDetails.Length, Is.EqualTo(1), "Should not add multiple requests with the same key");
    }

    [Test]
    public async Task ShouldNotOverrideRequestTimeStartedWithALaterTime()
    {
        var sp = await Setup();
        var clock = sp.GetRequiredService<IClock>();
        var session = CreateSession(sp);
        var request1 = await CreateRequest(sp);
        var request2 = await CreateRequest(sp, request1.RequestKey);
        request2.TimeStarted = request1.TimeStarted.AddMinutes(1);
        await LogSessionDetails
        (
            sp,
            new TempLogSessionDetailModel
            (
                session: session,
                requestDetails: [
                    new TempLogRequestDetailModel
                    {
                        Request = request1
                    }
                ]
            )
        );
        await LogSessionDetails
        (
            sp,
            new TempLogSessionDetailModel
            (
                session: session,
                requestDetails: [
                    new TempLogRequestDetailModel
                    {
                        Request = request2
                    }
                ]
            )
        );
        var sessionDetail = await GetSessionDetail(sp, session.SessionKey);
        var requestDetails = await GetRequestDetails(sp, sessionDetail);
        var requestDetail = requestDetails.FirstOrDefault() ?? new();
        Assert.That(requestDetail.Request.TimeStarted, Is.EqualTo(request1.TimeStarted), "Should not override time started with later time started");
    }

    [Test]
    public async Task ShouldNotOverrideRequestTimeEndedWithMaxValue()
    {
        var sp = await Setup();
        var clock = sp.GetRequiredService<IClock>();
        var session = CreateSession(sp);
        var request1 = await CreateRequest(sp);
        request1.TimeEnded = clock.Now().AddHours(1);
        var request2 = await CreateRequest(sp, request1.RequestKey);
        request2.TimeEnded = DateTimeOffset.MaxValue;
        await LogSessionDetails
        (
            sp,
            new TempLogSessionDetailModel
            (
                session: session,
                requestDetails: [
                    new TempLogRequestDetailModel
                    {
                        Request = request1
                    }
                ]
            )
        );
        await LogSessionDetails
        (
            sp,
            new TempLogSessionDetailModel
            (
                session: session,
                requestDetails: [
                    new TempLogRequestDetailModel
                    {
                        Request = request2
                    }
                ]
            )
        );
        var sessionDetail = await GetSessionDetail(sp, session.SessionKey);
        var requestDetails = await GetRequestDetails(sp, sessionDetail);
        var requestDetail = requestDetails.FirstOrDefault() ?? new();
        Assert.That(requestDetail.Request.TimeEnded, Is.EqualTo(request1.TimeEnded), "Should not override request time ended with max value");
    }

    [Test]
    public async Task ShouldAddMultipleRequestsToSameSession()
    {
        var sp = await Setup();
        var session = CreateSession(sp);
        var request1 = await CreateRequest(sp);
        var request2 = await CreateRequest(sp);
        await LogSessionDetails
        (
            sp,
            new TempLogSessionDetailModel
            (
                session: session,
                requestDetails: [
                    new TempLogRequestDetailModel
                    {
                        Request = request1
                    },
                    new TempLogRequestDetailModel
                    {
                        Request = request2
                    }
                ]
            )
        );
        var sessionDetail = await GetSessionDetail(sp, session.SessionKey);
        var requestDetails = await GetRequestDetails(sp, sessionDetail);
        Assert.That(requestDetails.Length, Is.EqualTo(2), "Should add multiple requests to same session");
    }

    [Test]
    public async Task ShouldAddRequestToSession()
    {
        var sp = await Setup();
        var session1 = CreateSession(sp);
        var request1 = await CreateRequest(sp);
        await LogSessionDetails
        (
            sp,
            new TempLogSessionDetailModel
            (
                session: session1,
                requestDetails: [
                    new TempLogRequestDetailModel
                    {
                        Request = request1
                    }
                ]
            )
        );
        var session2 = CreateSession(sp);
        var request2 = await CreateRequest(sp);
        await LogSessionDetails
        (
            sp,
            new TempLogSessionDetailModel
            (
                session: session2,
                requestDetails: [
                    new TempLogRequestDetailModel
                    {
                        Request = request2
                    }
                ]
            )
        );
        var sessionDetail1 = await GetSessionDetail(sp, session1.SessionKey);
        var requestDetails1 = await GetRequestDetails(sp, sessionDetail1);
        Assert.That(requestDetails1.Length, Is.EqualTo(1), "Should add requests to session");
        var sessionDetail2 = await GetSessionDetail(sp, session1.SessionKey);
        var requestDetails2 = await GetRequestDetails(sp, sessionDetail2);
        Assert.That(requestDetails2.Length, Is.EqualTo(1), "Should add requests to session");
    }

    [Test]
    public async Task ShouldAddPlaceholderForSourceRequest()
    {
        var sp = await Setup();
        var session1 = CreateSession(sp);
        var request1 = await CreateRequest(sp);
        var session2 = CreateSession(sp);
        var request2 = await CreateRequest(sp);
        request2.Path = "/Fake/Current/Request2";
        request1.SourceRequestKey = request2.RequestKey;
        await LogSessionDetails
        (
            sp,
            new TempLogSessionDetailModel
            (
                session: session1,
                requestDetails: [
                    new TempLogRequestDetailModel
                    {
                        Request = request1
                    }
                ]
            )
        );
        var placeholderRequestDetail = await GetRequestDetail(sp, request2.RequestKey);
        Assert.That(placeholderRequestDetail.Request.IsFound(), Is.True, "Should add placeholder for source request");
        Assert.That(placeholderRequestDetail.Request.Path, Is.EqualTo(""), "Should add placeholder for source request");
        Assert.That(placeholderRequestDetail.App.AppKey, Is.EqualTo(AppKey.Unknown), "Should add placeholder for source request");
        await LogSessionDetails
        (
            sp,
            new TempLogSessionDetailModel
            (
                session: session2,
                requestDetails: [
                    new TempLogRequestDetailModel
                    {
                        Request = request2
                    }
                ]
            )
        );
        var sourceRequestDetail = await GetRequestDetail(sp, request2.RequestKey);
        Assert.That(placeholderRequestDetail.Request.Path, Is.EqualTo("/Fake/Current/Request2"), "Should add placeholder for source request");
        var requestDetail = await GetRequestDetail(sp, request1.RequestKey);
        Assert.That(requestDetail.SourceRequestID, Is.EqualTo(sourceRequestDetail.Request.ID), "Should add placeholder for source request");
    }

    [Test]
    public async Task ShouldAddLogEntry()
    {
        var sp = await Setup();
        var clock = sp.GetRequiredService<IClock>();
        var session1 = CreateSession(sp);
        var request1 = await CreateRequest(sp);
        var logEntryKey = GenerateKey();
        await LogSessionDetails
        (
            sp,
            new TempLogSessionDetailModel
            (
                session: session1,
                requestDetails: [
                    new TempLogRequestDetailModel
                    {
                        Request = request1,
                        LogEntries =[
                            new LogEntryModel
                            {
                                EventKey = logEntryKey,
                                Caption = "Test",
                                Severity = AppEventSeverity.Values.CriticalError,
                                Message = "Test Message",
                                Detail = "Test Detail",
                                ActualCount = 2,
                                Category = "Whatever",
                                TimeOccurred = clock.Now()
                            }
                        ]
                    }
                ]
            )
        );
        var logEntries = await GetLogEntries(sp, request1.RequestKey);
        Assert.That(logEntries.Length, Is.EqualTo(1), "Should add log entry");
        Assert.That(logEntries[0].Caption, Is.EqualTo("Test"), "Should add log entry");
        Assert.That(logEntries[0].Severity, Is.EqualTo(AppEventSeverity.Values.CriticalError), "Should add log entry");
        Assert.That(logEntries[0].Message, Is.EqualTo("Test Message"), "Should add log entry");
        Assert.That(logEntries[0].Detail, Is.EqualTo("Test Detail"), "Should add log entry");
        Assert.That(logEntries[0].ActualCount, Is.EqualTo(2), "Should add log entry");
        Assert.That(logEntries[0].Category, Is.EqualTo("Whatever"), "Should add log entry");
        Assert.That(logEntries[0].TimeOccurred, Is.EqualTo(clock.Now()), "Should add log entry");
    }

    [Test]
    public async Task ShouldNotAddLogEntryWithTheSameKey()
    {
        var sp = await Setup();
        var session = CreateSession(sp);
        var request = await CreateRequest(sp);
        var logEntry1 = CreateLogEntry(sp);
        var logEntry2 = CreateLogEntry(sp, logEntry1.EventKey);
        await LogSessionDetails
        (
            sp,
            new TempLogSessionDetailModel
            (
                session: session,
                requestDetails: [
                    new TempLogRequestDetailModel
                    {
                        Request = request,
                        LogEntries =[
                            logEntry1,
                            logEntry2
                        ]
                    }
                ]
            )
        );
        var logEntries = await GetLogEntries(sp, request.RequestKey);
        Assert.That(logEntries.Length, Is.EqualTo(1), "Should not add log entry with the same key");
    }

    [Test]
    public async Task ShouldAddPlaceholderSourceLogEntry()
    {
        var sp = await Setup();
        var session1 = CreateSession(sp);
        var request1 = await CreateRequest(sp);
        var logEntry1 = CreateLogEntry(sp);
        logEntry1.ParentEventKey = GenerateKey();
        await LogSessionDetails
        (
            sp,
            new TempLogSessionDetailModel
            (
                session: session1,
                requestDetails: [
                    new TempLogRequestDetailModel
                    {
                        Request = request1,
                        LogEntries =[
                            logEntry1
                        ]
                    }
                ]
            )
        );
        var logEntryDetail = await GetLogEntryDetail(sp, logEntry1.EventKey);
        var placeholderLogEntryDetail = await GetLogEntryDetail(sp, logEntry1.ParentEventKey);
        Assert.That(placeholderLogEntryDetail.LogEntry.IsPlaceholder(), Is.True, "Should add placeholder source log entry");
        var request2 = await CreateRequest(sp);
        var logEntry2 = CreateLogEntry(sp, logEntry1.ParentEventKey);
        logEntry2.Caption = "Source Log Entry";
        await LogSessionDetails
        (
            sp,
            new TempLogSessionDetailModel
            (
                session: session1,
                requestDetails: [
                    new TempLogRequestDetailModel
                    {
                        Request = request2,
                        LogEntries =[
                            logEntry2
                        ]
                    }
                ]
            )
        );
        var sourceLogEntryDetail = await GetLogEntryDetail(sp, placeholderLogEntryDetail.LogEntry.ID);
        Assert.That(sourceLogEntryDetail.LogEntry.Caption, Is.EqualTo("Source Log Entry"), "Should add placeholder source log entry");
        Assert.That(logEntryDetail.SourceLogEntryID, Is.EqualTo(sourceLogEntryDetail.LogEntry.ID), "Should add placeholder source log entry");
    }

    private static LogEntryModel CreateLogEntry(IServiceProvider sp, string logEntryKey = "") =>
        new LogEntryModel
        {
            EventKey = string.IsNullOrWhiteSpace(logEntryKey) ? GenerateKey() : logEntryKey,
            Caption = "Test",
            Severity = AppEventSeverity.Values.CriticalError,
            Message = "Test Message",
            Detail = "Test Detail",
            ActualCount = 2,
            Category = "Whatever",
            TimeOccurred = sp.GetRequiredService<IClock>().Now()
        };

    private static async Task<TempLogRequestModel> CreateRequest(IServiceProvider sp, string requestKey = "")
    {
        var clock = sp.GetRequiredService<IClock>();
        var timeEnded = clock.Now().AddHours(1);
        var installation = await BeginInstallation(sp);
        return new TempLogRequestModel
        {
            RequestKey = string.IsNullOrWhiteSpace(requestKey) ? GenerateKey() : requestKey,
            Path = "/Fake/Current",
            TimeStarted = clock.Now(),
            TimeEnded = timeEnded,
            ActualCount = 5,
            InstallationID = installation.CurrentInstallationID
        };
    }

    private static string GenerateKey() => Guid.NewGuid().ToString("N");

    private static async Task<AppSessionDetailModel[]> GetSessionDetails(IServiceProvider sp)
    {
        var clock = sp.GetRequiredService<IClock>();
        var hubFactory = sp.GetRequiredService<HubFactory>();
        var sessions = await hubFactory.Sessions.SessionsByTimeRange
        (
            new DateTimeRange(clock.Now().AddDays(-1), clock.Now().AddDays(1))
        );
        var sessionDetails = new List<AppSessionDetailModel>();
        foreach (var session in sessions)
        {
            var user = await session.User();
            var userGroup = await user.UserGroup();
            var sessionDetail = await GetSessionDetail(sp, session);
            sessionDetails.Add(sessionDetail);
        }
        return sessionDetails.ToArray();
    }

    private static async Task<AppSessionDetailModel> GetSessionDetail(IServiceProvider sp, string sessionKey)
    {
        var clock = sp.GetRequiredService<IClock>();
        var hubFactory = sp.GetRequiredService<HubFactory>();
        var session = await hubFactory.Sessions.Session(sessionKey);
        var sessionDetail = await GetSessionDetail(sp, session);
        return sessionDetail;
    }

    private static async Task<AppRequestDetailModel[]> GetRequestDetails(IServiceProvider sp, AppSessionDetailModel sessionDetail)
    {
        var hubFactory = sp.GetRequiredService<HubFactory>();
        var session = await hubFactory.Sessions.Session(sessionDetail.Session.ID);
        var requests = await session.Requests();
        var requestDetails = new List<AppRequestDetailModel>();
        foreach (var request in requests)
        {
            var requestDetail = await GetRequestDetail(sp, request);
            requestDetails.Add(requestDetail);
        }
        return requestDetails.ToArray();
    }

    private static async Task<AppRequestDetailModel> GetRequestDetail(IServiceProvider sp, string requestKey)
    {
        var hubFactory = sp.GetRequiredService<HubFactory>();
        var request = await hubFactory.Requests.RequestOrDefault(requestKey);
        var requestDetail = await GetRequestDetail(sp, request);
        return requestDetail;
    }

    private static async Task<AppLogEntryModel[]> GetLogEntries(IServiceProvider sp, string requestKey)
    {
        var clock = sp.GetRequiredService<IClock>();
        var hubFactory = sp.GetRequiredService<HubFactory>();
        var request = await hubFactory.Requests.RequestOrPlaceHolder(requestKey, clock.Now());
        var logEntries = await request.Events();
        return logEntries.Select(le => le.ToModel()).ToArray();
    }

    private static async Task<AppLogEntryDetailModel> GetLogEntryDetail(IServiceProvider sp, string logEntryKey)
    {
        var clock = sp.GetRequiredService<IClock>();
        var hubFactory = sp.GetRequiredService<HubFactory>();
        var logEntry = await hubFactory.LogEntries.LogEntryOrDefaultByKey(logEntryKey);
        var logEntryDetail = await GetLogEntryDetail(sp, logEntry);
        return logEntryDetail;
    }

    private async Task<IServiceProvider> Setup()
    {
        var host = new HubTestHost();
        var sp = await host.Setup();
        var hubFactory = sp.GetRequiredService<HubFactory>();
        var userGroup = await hubFactory.UserGroups.GetGeneral();
        await userGroup.AddOrUpdate(new AppUserName("test.user"), new FakeHashedPassword("Password12345"), DateTime.Now);
        await userGroup.AddOrUpdate(new AppUserName("Someone"), new FakeHashedPassword("Password12345"), DateTime.Now);
        return sp;
    }

    private static async Task<NewInstallationResult> BeginInstallation(IServiceProvider sp)
    {
        var clock = sp.GetRequiredService<IClock>();
        var apiFactory = sp.GetRequiredService<HubAppApiFactory>();
        var hubApi = apiFactory.CreateForSuperUser();
        var fakeAppFactory = new FakeAppApiFactory(sp);
        var fakeApp = fakeAppFactory.CreateForSuperUser();
        await hubApi.Install.AddOrUpdateApps.Invoke
        (
            new AddOrUpdateAppsRequest
            (
                versionName: new AppVersionName("FakeWebApp"),
                appKeys: [fakeApp.AppKey]
            )
        );
        await hubApi.Install.RegisterApp.Invoke
        (
            new RegisterAppRequest
            (
                AppVersionKey.Current,
                fakeApp.Template().ToModel()
            )
        );
        var version = await hubApi.Publish.NewVersion.Invoke
        (
            new NewVersionRequest
            (
                versionName: new AppVersionName("FakeWebApp"),
                versionType: AppVersionType.Values.Major
            )
        );
        await hubApi.Publish.BeginPublish.Invoke
        (
            new PublishVersionRequest
            (
                versionName: version.VersionName,
                versionKey: version.VersionKey
            )
        );
        await hubApi.Publish.EndPublish.Invoke
        (
            new PublishVersionRequest
            (
                versionName: version.VersionName,
                versionKey: version.VersionKey
            )
        );
        var newInstResult = await hubApi.Install.NewInstallation.Invoke
        (
            new NewInstallationRequest
            (
                appKey: fakeApp.AppKey,
                versionName: version.VersionName,
                qualifiedMachineName: "destination.xartogg.com",
                domain: "test.xartogg.com",
                siteName: ""
            )
        );
        await hubApi.Install.BeginInstallation.Invoke(new GetInstallationRequest(newInstResult.CurrentInstallationID));
        return newInstResult;
    }

    private static Task LogSessionDetails(IServiceProvider sp, params TempLogSessionDetailModel[] sessionDetails)
    {
        var tester = HubActionTester.Create(sp, api => api.PermanentLog.LogSessionDetails);
        return tester.Execute(new(sessionDetails));
    }

    private static TempLogSessionModel CreateSession(IServiceProvider sp, string sessionKey = "")
    {
        var clock = sp.GetRequiredService<IClock>();
        return new TempLogSessionModel
        {
            UserName = "test.user",
            TimeStarted = clock.Now(),
            TimeEnded = clock.Now().AddHours(1),
            UserAgent = "Opera",
            RemoteAddress = "127.0.0.1",
            SessionKey = string.IsNullOrWhiteSpace(sessionKey) ? GenerateKey() : sessionKey,
            RequesterKey = GenerateKey()
        };
    }

    private static Task<AppSessionDetailModel> GetSessionDetail(IServiceProvider sp, AppSession session)
    {
        var tester = HubActionTester.Create(sp, api => api.Logs.GetSessionDetail);
        return tester.Execute(session.ID);
    }

    private static Task<AppRequestDetailModel> GetRequestDetail(IServiceProvider sp, AppRequest request)
    {
        var tester = HubActionTester.Create(sp, api => api.Logs.GetRequestDetail);
        return tester.Execute(request.ToModel().ID);
    }

    private static Task<AppLogEntryDetailModel> GetLogEntryDetail(IServiceProvider sp, LogEntry logEntry) =>
        GetLogEntryDetail(sp, logEntry.ToModel().ID);

    private static Task<AppLogEntryDetailModel> GetLogEntryDetail(IServiceProvider sp, int logEntryID)
    {
        var tester = HubActionTester.Create(sp, api => api.Logs.GetLogEntryDetail);
        return tester.Execute(logEntryID);
    }
}
