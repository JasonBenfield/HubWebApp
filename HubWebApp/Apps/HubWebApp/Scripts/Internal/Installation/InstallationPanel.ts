import { Awaitable } from "@jasonbenfield/sharedwebapp/Awaitable";
import { AsyncCommand, Command } from "@jasonbenfield/sharedwebapp/Components/Command";
import { MessageAlert } from "@jasonbenfield/sharedwebapp/Components/MessageAlert";
import { ModalConfirm } from "@jasonbenfield/sharedwebapp/Components/ModalConfirm";
import { TextComponent } from "@jasonbenfield/sharedwebapp/Components/TextComponent";
import { TextLinkComponent } from "@jasonbenfield/sharedwebapp/Components/TextLinkComponent";
import { FormGroupText } from "@jasonbenfield/sharedwebapp/Forms/FormGroupText";
import { HubAppClient } from "../../Lib/Http/HubAppClient";
import { InstallationPanelView } from "./InstallationPanelView";
import { InstallationDetail } from "../../Lib/InstallationDetail";

interface IResult {
    menuRequested?: boolean;
}

class Result {
    static menuRequested() { return new Result({ menuRequested: true }); }

    private constructor(private readonly result: IResult) { }

    get menuRequested() { return this.result.menuRequested; }
}

export class InstallationPanel implements IPanel {
    private readonly awaitable = new Awaitable<Result>();
    private readonly alert: MessageAlert;
    private readonly confirm: ModalConfirm;
    private readonly appKey: FormGroupText;
    private readonly versionKey: TextComponent;
    private readonly versionStatus: TextComponent;
    private readonly installationStatus: FormGroupText;
    private readonly location: TextComponent;
    private readonly current: TextComponent;
    private readonly domain: FormGroupText;
    private readonly siteName: FormGroupText;
    private readonly mostRecentRequest: FormGroupText;
    private readonly appLink: TextLinkComponent;
    private readonly logEntriesLink: TextLinkComponent;
    private readonly requestsLink: TextLinkComponent;
    private installationID: number;

    constructor(private readonly hubClient: HubAppClient, private readonly view: InstallationPanelView) {
        this.alert = new MessageAlert(view.alert);
        this.confirm = new ModalConfirm(view.confirm);
        this.appKey = new FormGroupText(view.appKey);
        this.versionKey = new TextComponent(view.versionKey);
        this.versionStatus = new TextComponent(view.versionStatus);
        this.installationStatus = new FormGroupText(view.installationStatus);
        this.location = new TextComponent(view.location);
        this.current = new TextComponent(view.current);
        this.domain = new FormGroupText(view.domain);
        this.siteName = new FormGroupText(view.siteName);
        this.mostRecentRequest = new FormGroupText(view.mostRecentRequest);
        this.appLink = new TextLinkComponent(view.appLink);
        this.logEntriesLink = new TextLinkComponent(view.logEntriesLink);
        this.requestsLink = new TextLinkComponent(view.requestsLink);
        new Command(this.menu.bind(this)).add(view.menuButton);
        new AsyncCommand(this.onDelete.bind(this)).add(view.deleteButton);
    }

    private menu() { this.awaitable.resolve(Result.menuRequested()); }

    private async onDelete() {
        const isConfirmed = await this.confirm.confirm('Delete this installation?', 'Confirm Delete');
        if (isConfirmed) {
            await this.alert.infoAction(
                'Deleting...',
                () => this.hubClient.Installations.RequestDelete({
                    InstallationID: this.installationID
                })
            );
            await this.refresh();
        }
    }

    setInstallationID(installationID: number) {
        this.installationID = installationID;
        this.requestsLink.setHref(this.hubClient.Logs.AppRequests.getUrl({
            SessionID: null,
            InstallationID: installationID,
            SourceRequestID: null
        }));
        this.logEntriesLink.setHref(this.hubClient.Logs.LogEntries.getUrl({ RequestID: null, InstallationID: installationID }));
    }

    async refresh() {
        const sourceDetail = await this.alert.infoAction(
            'Loading...',
            () => this.hubClient.Installations.GetInstallationDetail(this.installationID)
        );
        const detail = new InstallationDetail(sourceDetail);
        this.appKey.setValue(detail.app.appKey.format());
        this.versionKey.setText(detail.version.versionKey.displayText);
        this.versionStatus.setText(`[ ${detail.version.status.DisplayText} ]`);
        this.installationStatus.setValue(detail.installation.status.DisplayText);
        this.location.setText(detail.installLocation.qualifiedMachineName);
        this.current.setText(detail.installation.isCurrent ? '[ Current ]' : '');
        if (detail.installation.domain) {
            this.domain.setValue(detail.installation.domain);
            this.view.showDomain();
        }
        else {
            this.view.hideDomain();
        }
        if (detail.installation.siteName) {
            this.siteName.setValue(detail.installation.siteName);
            this.view.showSiteName();
        }
        else {
            this.view.hideSiteName();
        }
        if (detail.mostRecentRequest.id) {
            this.mostRecentRequest.setValue(detail.mostRecentRequest.timeStarted.format());
            this.view.showMostRecentRequest();
        }
        else {
            this.view.hideMostRecentRequest();
        }
        this.appLink.setHref(this.hubClient.App.Index.getModifierUrl(detail.app.getModifier(), {}));
    }

    start() { return this.awaitable.start(); }

    activate() { this.view.show(); }

    deactivate() { this.view.hide(); }

}