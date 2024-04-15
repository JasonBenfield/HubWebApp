import { Awaitable } from "@jasonbenfield/sharedwebapp/Awaitable";
import { Command } from "@jasonbenfield/sharedwebapp/Components/Command";
import { MessageAlert } from "@jasonbenfield/sharedwebapp/Components/MessageAlert";
import { TextComponent } from "@jasonbenfield/sharedwebapp/Components/TextComponent";
import { TextLinkComponent } from "@jasonbenfield/sharedwebapp/Components/TextLinkComponent";
import { FormGroupText } from "@jasonbenfield/sharedwebapp/Forms/FormGroupText";
import { HubAppClient } from "../../../Lib/Http/HubAppClient";
import { LogEntryPanelView } from "./LogEntryPanelView";
import { AppLogEntryDetail } from "../../../Lib/AppLogEntryDetail";

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
        const sourceDetail = await this.alert.infoAction(
            'Loading...',
            () => this.hubClient.Logs.GetLogEntryDetail(this.logEntryID)
        );
        const detail = new AppLogEntryDetail(sourceDetail);
        this.appKey.setText(detail.app.appKey.format());
        this.appKey.setHref(
            this.hubClient.App.Index.getModifierUrl(detail.app.publicKey.displayText, {})
        );
        this.versionKey.setText(detail.version.versionKey.displayText);
        this.versionStatus.setText(`[ ${detail.version.status.DisplayText} ]`);
        this.userName.setValue(detail.user.userName.displayText);
        this.path.setValue(detail.request.path);
        this.timeOccurred.setValue(detail.logEntry.timeOccurred.format());
        this.severity.setValue(detail.logEntry.severity.DisplayText);
        if (detail.logEntry.caption) {
            this.caption.setValue(detail.logEntry.caption);
            this.view.showCaption();
        }
        else {
            this.view.hideCaption();
        }
        if (detail.logEntry.message) {
            this.message.setValue(detail.logEntry.message);
            this.view.showMessage();
        }
        else {
            this.view.hideMessage();
        }
        if (detail.logEntry.detail) {
            this.detail.setValue(detail.logEntry.detail);
            this.view.showDetail();
        }
        else {
            this.view.hideDetail();
        }
        if (detail.sourceLogEntryID) {
            this.sourceLogEntryLink.setHref(
                this.hubClient.Logs.LogEntry.getUrl({ LogEntryID: detail.sourceLogEntryID })
            );
            this.sourceLogEntryLink.show();
        }
        else {
            this.sourceLogEntryLink.hide();
        }
        if (detail.targetLogEntryID) {
            this.targetLogEntryLink.setHref(
                this.hubClient.Logs.LogEntry.getUrl({ LogEntryID: detail.targetLogEntryID })
            );
            this.targetLogEntryLink.show();
        }
        else {
            this.targetLogEntryLink.hide();
        }
        this.requestLink.setHref(
            this.hubClient.Logs.AppRequest.getUrl({ RequestID: detail.request.id })
        );
        this.installationLink.setHref(
            this.hubClient.Installations.Installation.getUrl({ InstallationID: detail.installation.id })
        );
    }

    start() { return this.awaitable.start(); }

    activate() { this.view.show(); }

    deactivate() { this.view.hide(); }

}