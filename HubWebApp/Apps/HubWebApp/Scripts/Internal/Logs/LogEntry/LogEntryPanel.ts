import { Awaitable } from "@jasonbenfield/sharedwebapp/Awaitable";
import { Command } from "@jasonbenfield/sharedwebapp/Components/Command";
import { MessageAlert } from "@jasonbenfield/sharedwebapp/Components/MessageAlert";
import { TextComponent } from "@jasonbenfield/sharedwebapp/Components/TextComponent";
import { TextLinkComponent } from "@jasonbenfield/sharedwebapp/Components/TextLinkComponent";
import { FormattedDate } from "@jasonbenfield/sharedwebapp/FormattedDate";
import { FormGroupText } from "@jasonbenfield/sharedwebapp/Forms/FormGroupText";
import { HubAppClient } from "../../../Lib/Http/HubAppClient";
import { LogEntryPanelView } from "./LogEntryPanelView";

interface IResult {
    menuRequested?: boolean;
}

class Result {
    static menuRequested() { return new Result({ menuRequested: true }); }

    private constructor(private readonly result: IResult) { }

    get menuRequested() { return this.result.menuRequested; }
}

export class LogEntryPanel implements IPanel {
    private readonly awaitable = new Awaitable<Result>();
    private readonly alert: MessageAlert;
    private readonly appKey: TextLinkComponent;
    private readonly versionKey: TextComponent;
    private readonly versionStatus: TextComponent;
    private readonly userName: FormGroupText;
    private readonly path: FormGroupText;
    private readonly timeOccurred: FormGroupText;
    private readonly severity: FormGroupText;
    private readonly caption: FormGroupText;
    private readonly message: FormGroupText;
    private readonly detail: FormGroupText;
    private readonly sourceLogEntryLink: TextLinkComponent;
    private readonly targetLogEntryLink: TextLinkComponent;
    private readonly requestLink: TextLinkComponent;
    private readonly installationLink: TextLinkComponent;
    private logEntryID: number;

    constructor(private readonly hubClient: HubAppClient, private readonly view: LogEntryPanelView) {
        this.alert = new MessageAlert(view.alert);
        this.appKey = new TextLinkComponent(view.appKeyLink);
        this.versionKey = new TextComponent(view.versionKey);
        this.versionStatus = new TextComponent(view.versionStatus);
        this.userName = new FormGroupText(view.userName);
        this.path = new FormGroupText(view.path);
        this.timeOccurred = new FormGroupText(view.timeOccurred);
        this.severity = new FormGroupText(view.severity);
        this.caption = new FormGroupText(view.caption);
        this.message = new FormGroupText(view.message);
        this.detail = new FormGroupText(view.detail);
        this.sourceLogEntryLink = new TextLinkComponent(view.sourceLogEntryLink);
        this.targetLogEntryLink = new TextLinkComponent(view.targetLogEntryLink);
        this.requestLink = new TextLinkComponent(view.requestLink);
        this.installationLink = new TextLinkComponent(view.installationLink);
        new Command(this.menu.bind(this)).add(view.menuButton);
    }

    private menu() { this.awaitable.resolve(Result.menuRequested()); }

    setLogEntryID(logEntryID: number) {
        this.logEntryID = logEntryID;
    }

    async refresh() {
        const detail = await this.alert.infoAction(
            'Loading...',
            () => this.hubClient.Logs.GetLogEntryDetail(this.logEntryID)
        );
        this.appKey.setText(
            detail.App.AppKey.Name.DisplayText + ' ' + detail.App.AppKey.Type.DisplayText
        );
        this.appKey.setHref(
            this.hubClient.App.Index.getModifierUrl(detail.App.PublicKey.DisplayText, {})
        );
        this.versionKey.setText(detail.Version.VersionKey.DisplayText);
        this.versionStatus.setText(`[ ${detail.Version.Status.DisplayText} ]`);
        this.userName.setValue(detail.User.UserName.DisplayText);
        this.path.setValue(detail.Request.Path);
        this.timeOccurred.setValue(
            new FormattedDate(detail.LogEntry.TimeOccurred).formatDateTime()
        );
        this.severity.setValue(detail.LogEntry.Severity.DisplayText);
        if (detail.LogEntry.Caption) {
            this.caption.setValue(detail.LogEntry.Caption);
            this.view.showCaption();
        }
        else {
            this.view.hideCaption();
        }
        if (detail.LogEntry.Message) {
            this.message.setValue(detail.LogEntry.Message);
            this.view.showMessage();
        }
        else {
            this.view.hideMessage();
        }
        if (detail.LogEntry.Detail) {
            this.detail.setValue(detail.LogEntry.Detail);
            this.view.showDetail();
        }
        else {
            this.view.hideDetail();
        }
        if (detail.SourceLogEntryID) {
            this.sourceLogEntryLink.setHref(this.hubClient.Logs.LogEntry.getUrl({ LogEntryID: detail.SourceLogEntryID }));
            this.sourceLogEntryLink.show();
        }
        else {
            this.sourceLogEntryLink.hide();
        }
        if (detail.TargetLogEntryID) {
            this.targetLogEntryLink.setHref(this.hubClient.Logs.LogEntry.getUrl({ LogEntryID: detail.TargetLogEntryID }));
            this.targetLogEntryLink.show();
        }
        else {
            this.targetLogEntryLink.hide();
        }
        this.requestLink.setHref(this.hubClient.Logs.AppRequest.getUrl({ RequestID: detail.Request.ID }));
        this.installationLink.setHref(this.hubClient.Installations.Installation.getUrl({ InstallationID: detail.Installation.ID }));
    }

    start() { return this.awaitable.start(); }

    activate() { this.view.show(); }

    deactivate() { this.view.hide(); }

}