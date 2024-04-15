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
    private readonly appKeyFormGroup: FormGroupText;
    private readonly versionKeyTextComponent: TextComponent;
    private readonly versionStatusTextComponent: TextComponent;
    private readonly userNameFormGroup: FormGroupText;
    private readonly userAgentFormGroup: FormGroupText;
    private readonly remoteAddressFormGroup: FormGroupText;
    private readonly currentInstallationFormGroup: FormGroupText;
    private readonly timeRangeFormGroup: FormGroupText;
    private readonly pathFormGroup: FormGroupText;
    private readonly sourceRequestLink: TextLinkComponent;
    private readonly targetRequestLink: TextLinkComponent;
    private readonly sessionLink: TextLinkComponent;
    private readonly installationLink: TextLinkComponent;
    private readonly logEntriesLink: TextLinkComponent;
    private requestID: number;

    constructor(private readonly hubClient: HubAppClient, private readonly view: RequestPanelView) {
        this.alert = new MessageAlert(view.alert);
        this.appKeyFormGroup = new FormGroupText(view.appKeyFormGroupView);
        this.versionKeyTextComponent = new TextComponent(view.versionKeyFormGroupView);
        this.versionStatusTextComponent = new TextComponent(view.versionStatusFormGroupView);
        this.userNameFormGroup = new FormGroupText(view.userNameFormGroupView);
        this.userAgentFormGroup = new FormGroupText(view.userAgentFormGroupView);
        this.userAgentFormGroup.setCaption('User Agent');
        this.remoteAddressFormGroup = new FormGroupText(view.remoteAddressFormGroupView);
        this.remoteAddressFormGroup.setCaption('Remote Address');
        this.currentInstallationFormGroup = new FormGroupText(view.currentInstallationFormGroupView);
        this.timeRangeFormGroup = new FormGroupText(view.timeRangeFormGroupView);
        this.pathFormGroup = new FormGroupText(view.pathFormGroupView);
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
        this.sourceRequestLink.hide();
        this.targetRequestLink.hide();
        this.userAgentFormGroup.hide();
        this.remoteAddressFormGroup.hide();
        const sourceDetail = await this.alert.infoAction(
            'Loading...',
            () => this.hubClient.Logs.GetRequestDetail(this.requestID)
        );
        const detail = new AppRequestDetail(sourceDetail);
        this.appKeyFormGroup.setValue(detail.app.appKey.format());
        this.versionKeyTextComponent.setText(detail.version.versionKey.displayText);
        this.versionStatusTextComponent.setText(`[ ${detail.version.status.DisplayText} ]`);
        this.userNameFormGroup.setValue(detail.user.userName.displayText);
        if (detail.installation.isCurrent) {
            this.currentInstallationFormGroup.setValue('Current');
            this.view.showCurrentInstallation();
        }
        else {
            this.view.hideCurrentInstallation();
        }
        this.userAgentFormGroup.setValue(detail.session.userAgent);
        this.userAgentFormGroup.setTitle(detail.session.rawUserAgent);
        if (detail.session.userAgent) {
            this.userAgentFormGroup.show();
        }
        this.remoteAddressFormGroup.setValue(detail.session.remoteAddress);
        if (detail.session.remoteAddress) {
            this.remoteAddressFormGroup.show();
        }
        this.timeRangeFormGroup.setValue(new FormattedTimeRange(detail.request.timeStarted, detail.request.timeEnded).format());
        this.pathFormGroup.setValue(detail.request.path);
        this.sessionLink.setHref(this.hubClient.Logs.Session.getUrl({ SessionID: detail.session.id }));
        if (detail.sourceRequestID) {
            this.sourceRequestLink.setHref(this.hubClient.Logs.AppRequest.getUrl({ RequestID: detail.sourceRequestID }));
            this.sourceRequestLink.show();
        }
        if (detail.targetRequestIDs.length > 0) {
            this.targetRequestLink.setHref(this.hubClient.Logs.AppRequests.getUrl({
                SessionID: null,
                InstallationID: null,
                SourceRequestID: detail.request.id
            }));
            this.targetRequestLink.show();
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