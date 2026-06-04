import { Awaitable } from "@jasonbenfield/sharedwebapp/Awaitable";
import { Command } from "@jasonbenfield/sharedwebapp/Components/Command";
import { MenuComponent } from "@jasonbenfield/sharedwebapp/Components/MenuComponent";
import { HubAppClient } from "../Lib/Http/HubAppClient";
import { MainMenuPanelView } from "./MainMenuPanelView";
import { AppClient } from "@jasonbenfield/sharedwebapp/Http/AppClient";
import { LinkComponent } from "@jasonbenfield/sharedwebapp/Components/LinkComponent";
import { MessageAlert } from "@jasonbenfield/sharedwebapp/Components/MessageAlert";
import { HubPermissions } from "../Lib/HubPermissions";

interface IResult {
    back?: boolean;
}

class Result {
    static back() { return new Result({ back: true }); }

    private constructor(private readonly results: IResult) { }

    get back() { return this.results.back; }
}

export class MainMenuPanel implements IPanel {
    private readonly awaitable = new Awaitable<Result>();
    private readonly alert: MessageAlert;
    private readonly userRolesLink: LinkComponent;
    private readonly sessionLogLink: LinkComponent;
    private readonly accessLogLink: LinkComponent;
    private readonly eventLogLink: LinkComponent;
    private readonly installationsLink: LinkComponent;
    private hasLoaded = false;

            
    constructor(private readonly hubClient: HubAppClient, private readonly view: MainMenuPanelView) {
        this.alert = new MessageAlert(view.alertView);
        const appLink = new LinkComponent(view.appsButton);
        appLink.setHref(hubClient.Apps.Index.getUrl({}));
        const userGroupsLink = new LinkComponent(view.userGroupsButton);
        userGroupsLink.setHref(hubClient.UserGroups.Index.getUrl({}));
        this.userRolesLink = new LinkComponent(view.userRolesButton);
        this.userRolesLink.setHref(hubClient.UserRoles.Index.getUrl({ AppID: 0 }));
        this.userRolesLink.hide();
        this.sessionLogLink = new LinkComponent(view.sessionLogButton);
        this.sessionLogLink.setHref(hubClient.Logs.Sessions.getUrl({}));
        this.sessionLogLink.hide();
        this.accessLogLink = new LinkComponent(view.accessLogButton);
        this.accessLogLink.setHref(hubClient.Logs.AppRequests.getUrl({ SessionID: 0, InstallationID: 0, SourceRequestID: 0 }));
        this.accessLogLink.hide();
        this.eventLogLink = new LinkComponent(view.eventLogButton);
        this.eventLogLink.setHref(hubClient.Logs.LogEntries.getUrl({ InstallationID: 0, RequestID: 0 }));
        this.eventLogLink.hide();
        this.installationsLink = new LinkComponent(view.installationsButton);
        this.installationsLink.setHref(hubClient.Installations.Index.getUrl({ QueryType: 0 }));
        this.installationsLink.hide();
        new Command(this.back.bind(this)).add(view.backButton);
    }

    private back() { this.awaitable.resolve(Result.back()); }

    start() {
        if (!this.hasLoaded) {
            this.load();
        }
        return this.awaitable.start();
    }

    private async load() {
        const permissions = await this.alert.infoAction(
            "Loading...",
            () => new HubPermissions(this.hubClient).value()
        );
        if (permissions.canViewUserRoles) {
            this.userRolesLink.show();
        }
        if (permissions.canViewLogs) {
            this.sessionLogLink.show();
            this.accessLogLink.show();
            this.eventLogLink.show();
        }
        if (permissions.canManageInstallations) {
            this.installationsLink.show();
        }
        this.hasLoaded = true;
    }

    activate() {
        this.view.show();
    }

    deactivate() { this.view.hide(); }
}