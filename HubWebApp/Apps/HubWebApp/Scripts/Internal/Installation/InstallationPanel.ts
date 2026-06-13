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
import { InstallStatus } from "../../Lib/Http/InstallStatus";
import { WebPage } from "@jasonbenfield/sharedwebapp/Http/WebPage";
import { AppDeleteCommandDetail } from "../../Lib/AppDeleteCommandDetail";

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
    private readonly appKeyFormGroup: FormGroupText;
    private readonly versionKeyTextComponent: TextComponent;
    private readonly versionStatusTextComponent: TextComponent;
    private readonly installationStatusFormGroup: FormGroupText;
    private readonly locationTextComponent: TextComponent;
    private readonly currentTextComponent: TextComponent;
    private readonly domainTextComponent: FormGroupText;
    private readonly siteNameFormGroup: FormGroupText;
    private readonly mostRecentRequestFormGroup: FormGroupText;
    private readonly appLink: TextLinkComponent;
    private readonly logEntriesLink: TextLinkComponent;
    private readonly requestsLink: TextLinkComponent;
    private readonly deleteCommand: AsyncCommand;
    private installationDetail = new InstallationDetail();
    private installationID = 0;

    constructor(private readonly hubClient: HubAppClient, private readonly view: InstallationPanelView) {
        this.alert = new MessageAlert(view.alert);
        this.confirm = new ModalConfirm(view.confirm);
        this.appKeyFormGroup = new FormGroupText(view.appKey);
        this.versionKeyTextComponent = new TextComponent(view.versionKey);
        this.versionStatusTextComponent = new TextComponent(view.versionStatus);
        this.installationStatusFormGroup = new FormGroupText(view.installationStatus);
        this.locationTextComponent = new TextComponent(view.location);
        this.currentTextComponent = new TextComponent(view.current);
        this.domainTextComponent = new FormGroupText(view.domain);
        this.siteNameFormGroup = new FormGroupText(view.siteName);
        this.mostRecentRequestFormGroup = new FormGroupText(view.mostRecentRequest);
        this.appLink = new TextLinkComponent(view.appLink);
        this.logEntriesLink = new TextLinkComponent(view.logEntriesLink);
        this.requestsLink = new TextLinkComponent(view.requestsLink);
        new Command(this.menu.bind(this)).add(view.menuButton);
        this.deleteCommand = new AsyncCommand(this.onDelete.bind(this));
        this.deleteCommand.add(view.deleteButton);
    }

    private menu() { this.awaitable.resolve(Result.menuRequested()); }

    private async onDelete() {
        const isConfirmed = await this.confirm.confirm("Delete this installation?", "Confirm Delete");
        if (isConfirmed) {
            const sourceCommandDetail =await this.alert.infoAction(
                "Deleting...",
                () => this.hubClient.Installation.AddDeleteCommand({
                    InstallationID: this.installationID
                })
            );
            const commandDetail = new AppDeleteCommandDetail(sourceCommandDetail);
            new WebPage(
                this.hubClient.Command.Index.getModifierUrl(
                    this.installationDetail.app.getModifier(),
                    { CommandID: commandDetail.command.id }
                )
            );
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
        this.deleteCommand.hide();
        const sourceDetail = await this.alert.infoAction(
            "Loading...",
            () => this.hubClient.Installation.GetInstallationDetail({
                InstallationID: this.installationID
            })
        );
        const detail = new InstallationDetail(sourceDetail);
        this.appKeyFormGroup.setValue(detail.app.appKey.format());
        this.versionKeyTextComponent.setText(detail.version.versionKey.displayText);
        this.versionStatusTextComponent.setText(`[ ${detail.version.status.DisplayText} ]`);
        this.installationStatusFormGroup.setValue(detail.installation.status.DisplayText);
        this.locationTextComponent.setText(detail.installLocation.qualifiedMachineName);
        this.currentTextComponent.setText(detail.installation.isCurrent ? "[ Current ]" : "");
        if (detail.installation.domain) {
            this.domainTextComponent.setValue(detail.installation.domain);
            this.view.showDomain();
        }
        else {
            this.view.hideDomain();
        }
        if (detail.installation.siteName) {
            this.siteNameFormGroup.setValue(detail.installation.siteName);
            this.view.showSiteName();
        }
        else {
            this.view.hideSiteName();
        }
        if (detail.mostRecentRequest.id) {
            this.mostRecentRequestFormGroup.setValue(detail.mostRecentRequest.timeStarted.format());
            this.view.showMostRecentRequest();
        }
        else {
            this.view.hideMostRecentRequest();
        }
        this.appLink.setHref(this.hubClient.App.Index.getModifierUrl(detail.app.getModifier(), {}));
        if (detail.installation.status.equals(InstallStatus.values.Installed)) {
            this.deleteCommand.show();
        }
    }

    start() { return this.awaitable.start(); }

    activate() { this.view.show(); }

    deactivate() { this.view.hide(); }

}