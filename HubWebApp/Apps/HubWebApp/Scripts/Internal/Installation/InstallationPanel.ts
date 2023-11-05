import { Awaitable } from "@jasonbenfield/sharedwebapp/Awaitable";
import { AsyncCommand, Command } from "@jasonbenfield/sharedwebapp/Components/Command";
import { MessageAlert } from "@jasonbenfield/sharedwebapp/Components/MessageAlert";
import { ModalConfirm } from "@jasonbenfield/sharedwebapp/Components/ModalConfirm";
import { TextComponent } from "@jasonbenfield/sharedwebapp/Components/TextComponent";
import { TextLinkComponent } from "@jasonbenfield/sharedwebapp/Components/TextLinkComponent";
import { FormattedDate } from "@jasonbenfield/sharedwebapp/FormattedDate";
import { TextValueFormGroup } from "@jasonbenfield/sharedwebapp/Forms/TextValueFormGroup";
import { HubAppClient } from "../../Lib/Http/HubAppClient";
import { InstallationPanelView } from "./InstallationPanelView";

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
    private readonly appKey: TextValueFormGroup;
    private readonly versionKey: TextComponent;
    private readonly versionStatus: TextComponent;
    private readonly installationStatus: TextValueFormGroup;
    private readonly location: TextComponent;
    private readonly current: TextComponent;
    private readonly domain: TextValueFormGroup;
    private readonly siteName: TextValueFormGroup;
    private readonly mostRecentRequest: TextValueFormGroup;
    private readonly appLink: TextLinkComponent;
    private readonly logEntriesLink: TextLinkComponent;
    private readonly requestsLink: TextLinkComponent;
    private installationID: number;

    constructor(private readonly hubClient: HubAppClient, private readonly view: InstallationPanelView) {
        this.alert = new MessageAlert(view.alert);
        this.confirm = new ModalConfirm(view.confirm);
        this.appKey = new TextValueFormGroup(view.appKey);
        this.versionKey = new TextComponent(view.versionKey);
        this.versionStatus = new TextComponent(view.versionStatus);
        this.installationStatus = new TextValueFormGroup(view.installationStatus);
        this.location = new TextComponent(view.location);
        this.current = new TextComponent(view.current);
        this.domain = new TextValueFormGroup(view.domain);
        this.siteName = new TextValueFormGroup(view.siteName);
        this.mostRecentRequest = new TextValueFormGroup(view.mostRecentRequest);
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
        this.requestsLink.setHref(this.hubClient.Logs.AppRequests.getUrl({ SessionID: null, InstallationID: installationID }));
        this.logEntriesLink.setHref(this.hubClient.Logs.LogEntries.getUrl({ RequestID: null, InstallationID: installationID }));
    }

    async refresh() {
        const detail = await this.alert.infoAction(
            'Loading...',
            () => this.hubClient.Installations.GetInstallationDetail(this.installationID)
        );
        this.appKey.setValue(
            detail.App.AppKey.Name.DisplayText + ' ' + detail.App.AppKey.Type.DisplayText
        );
        this.versionKey.setText(detail.Version.VersionKey.DisplayText);
        this.versionStatus.setText(`[ ${detail.Version.Status.DisplayText} ]`);
        this.installationStatus.setValue(detail.Installation.Status.DisplayText);
        this.location.setText(detail.InstallLocation.QualifiedMachineName);
        this.current.setText(detail.Installation.IsCurrent ? '[ Current ]' : '');
        if (detail.Installation.Domain) {
            this.domain.setValue(detail.Installation.Domain);
            this.view.showDomain();
        }
        else {
            this.view.hideDomain();
        }
        if (detail.Installation.SiteName) {
            this.siteName.setValue(detail.Installation.SiteName);
            this.view.showSiteName();
        }
        else {
            this.view.hideSiteName();
        }
        if (detail.MostRecentRequest.ID) {
            this.mostRecentRequest.setValue(new FormattedDate(detail.MostRecentRequest.TimeStarted).formatDateTime());
            this.view.showMostRecentRequest();
        }
        else {
            this.view.hideMostRecentRequest();
        }
        this.appLink.setHref(this.hubClient.App.Index.getModifierUrl(detail.App.PublicKey.Value, {}));
    }

    start() { return this.awaitable.start(); }

    activate() { this.view.show(); }

    deactivate() { this.view.hide(); }

}