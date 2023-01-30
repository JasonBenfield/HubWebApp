import { Awaitable } from "@jasonbenfield/sharedwebapp/Awaitable";
import { Command } from "@jasonbenfield/sharedwebapp/Components/Command";
import { MessageAlert } from "@jasonbenfield/sharedwebapp/Components/MessageAlert";
import { TextLinkComponent } from "@jasonbenfield/sharedwebapp/Components/TextLinkComponent";
import { TextValueFormGroup } from "@jasonbenfield/sharedwebapp/Forms/TextValueFormGroup";
import { HubAppApi } from "../../../Lib/Api/HubAppApi";
import { SessionPanelView } from "./SessionPanelView";

interface IResult {
    menuRequested?: boolean;
}

class Result {
    static menuRequested() { return new Result({ menuRequested: true }); }

    private constructor(private readonly result: IResult) { }

    get menuRequested() { return this.result.menuRequested; }
}

export class SessionPanel implements IPanel {
    private readonly awaitable = new Awaitable<Result>();
    private readonly alert: MessageAlert;
    private readonly userName: TextValueFormGroup;
    private readonly remoteAddress: TextValueFormGroup;
    private readonly userAgent: TextValueFormGroup;
    private readonly userLink: TextLinkComponent;
    private readonly requestsLink: TextLinkComponent;
    private sessionID: number;

    constructor(private readonly hubApi: HubAppApi, private readonly view: SessionPanelView) {
        this.alert = new MessageAlert(view.alert);
        this.userName = new TextValueFormGroup(view.userName);
        this.remoteAddress = new TextValueFormGroup(view.remoteAddress);
        this.userAgent = new TextValueFormGroup(view.userAgent);
        this.userLink = new TextLinkComponent(view.userLink);
        this.requestsLink = new TextLinkComponent(view.requestsLink);
        new Command(this.menu.bind(this)).add(view.menuButton);
    }

    private menu() { this.awaitable.resolve(Result.menuRequested()); }

    setSessionID(sessionID: number) {
        this.sessionID = sessionID;
    }

    async refresh() {
        const detail = await this.alert.infoAction(
            'Loading...',
            () => this.hubApi.Logs.GetSessionDetail(this.sessionID)
        );
        this.userName.setValue(detail.User.UserName.DisplayText);
        if (detail.Session.RemoteAddress) {
            this.remoteAddress.setValue(detail.Session.RemoteAddress);
            this.view.showRemoteAddress();
        }
        else {
            this.view.hideRemoteAddress();
        }
        if (detail.Session.UserAgent) {
            this.userAgent.setValue(detail.Session.UserAgent);
            this.view.showUserAgent();
        }
        else {
            this.view.hideUserAgent();
        }
        this.userLink.setHref(
            this.hubApi.Users.Index.getModifierUrl(
                detail.UserGroup.PublicKey.Value,
                { UserID: detail.User.ID }
            )
        );
        this.requestsLink.setHref(this.hubApi.Logs.Requests.getUrl({ SessionID: this.sessionID, InstallationID: null }));
    }

    start() { return this.awaitable.start(); }

    activate() { this.view.show(); }

    deactivate() { this.view.hide(); }

}