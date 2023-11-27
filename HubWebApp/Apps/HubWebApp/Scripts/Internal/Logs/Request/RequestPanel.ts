import { Awaitable } from "@jasonbenfield/sharedwebapp/Awaitable";
import { Command } from "@jasonbenfield/sharedwebapp/Components/Command";
import { MessageAlert } from "@jasonbenfield/sharedwebapp/Components/MessageAlert";
import { TextComponent } from "@jasonbenfield/sharedwebapp/Components/TextComponent";
import { TextLinkComponent } from "@jasonbenfield/sharedwebapp/Components/TextLinkComponent";
import { FormGroupText } from "@jasonbenfield/sharedwebapp/Forms/FormGroupText";
import { AppRequestDetail } from "../../../Lib/AppRequestDetail";
import { HubAppClient } from "../../../Lib/Http/HubAppClient";
import { FormattedTimeRange } from "../../../lib/FormattedTimeRange";
import { RequestPanelView } from "./RequestPanelView";

interface IResult {
    menuRequested?: boolean;
}

class Result {
    static menuRequested() { return new Result({ menuRequested: true }); }

    private constructor(private readonly result: IResult) { }

    get menuRequested() { return this.result.menuRequested; }
}

export class RequestPanel implements IPanel {
    private readonly awaitable = new Awaitable<Result>();
    private readonly alert: MessageAlert;
    private readonly appKey: FormGroupText;
    private readonly versionKey: TextComponent;
    private readonly versionStatus: TextComponent;
    private readonly userName: FormGroupText;
    private readonly currentInstallation: FormGroupText;
    private readonly timeRange: FormGroupText;
    private readonly path: FormGroupText;
    private readonly sourceRequestLink: TextLinkComponent;
    private readonly targetRequestLink: TextLinkComponent;
    private readonly sessionLink: TextLinkComponent;
    private readonly installationLink: TextLinkComponent;
    private readonly logEntriesLink: TextLinkComponent;
    private requestID: number;

    constructor(private readonly hubClient: HubAppClient, private readonly view: RequestPanelView) {
        this.alert = new MessageAlert(view.alert);
        this.appKey = new FormGroupText(view.appKey);
        this.versionKey = new TextComponent(view.versionKey);
        this.versionStatus = new TextComponent(view.versionStatus);
        this.userName = new FormGroupText(view.userName);
        this.currentInstallation = new FormGroupText(view.currentInstallation);
        this.timeRange = new FormGroupText(view.timeRange);
        this.path = new FormGroupText(view.path);
        this.sourceRequestLink = new TextLinkComponent(view.sourceRequestLink);
        this.targetRequestLink = new TextLinkComponent(view.targetRequestLink);
        this.sessionLink = new TextLinkComponent(view.sessionLink);
        this.installationLink = new TextLinkComponent(view.installationLink);
        this.logEntriesLink = new TextLinkComponent(view.logEntriesLink);
        new Command(this.menu.bind(this)).add(view.menuButton);
    }

    private menu() { this.awaitable.resolve(Result.menuRequested()); }

    setRequestID(requestID: number) {
        this.requestID = requestID;
    }

    async refresh() {
        const sourceDetail = await this.alert.infoAction(
            'Loading...',
            () => this.hubClient.Logs.GetRequestDetail(this.requestID)
        );
        const detail = new AppRequestDetail(sourceDetail);
        this.appKey.setValue(detail.app.appKey.format());
        this.versionKey.setText(detail.version.versionKey.displayText);
        this.versionStatus.setText(`[ ${detail.version.status.DisplayText} ]`);
        this.userName.setValue(detail.user.userName.displayText);
        if (detail.installation.isCurrent) {
            this.currentInstallation.setValue('Current');
            this.view.showCurrentInstallation();
        }
        else {
            this.view.hideCurrentInstallation();
        }
        this.timeRange.setValue(new FormattedTimeRange(detail.request.timeStarted, detail.request.timeEnded).format());
        this.path.setValue(detail.request.path);
        this.sessionLink.setHref(this.hubClient.Logs.Session.getUrl({ SessionID: detail.session.id }));
        if (detail.sourceRequestID) {
            this.sourceRequestLink.setHref(this.hubClient.Logs.AppRequest.getUrl({ RequestID: detail.sourceRequestID }));
            this.sourceRequestLink.show();
        }
        else {
            this.sourceRequestLink.hide();
        }
        if (detail.targetRequestIDs.length > 0) {
            this.targetRequestLink.setHref(this.hubClient.Logs.AppRequests.getUrl({
                SessionID: null,
                InstallationID: null,
                SourceRequestID: detail.request.id
            }));
            this.targetRequestLink.show();
        }
        else {
            this.targetRequestLink.hide();
        }
        this.installationLink.setHref(
            this.hubClient.Installations.Installation.getUrl({ InstallationID: detail.installation.id })
        );
        this.logEntriesLink.setHref(this.hubClient.Logs.LogEntries.getUrl({ RequestID: this.requestID, InstallationID: null }));
    }

    start() { return this.awaitable.start(); }

    activate() { this.view.show(); }

    deactivate() { this.view.hide(); }

}