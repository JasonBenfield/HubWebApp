import { Awaitable } from "@jasonbenfield/sharedwebapp/Awaitable";
import { Command } from "@jasonbenfield/sharedwebapp/Components/Command";
import { MessageAlert } from "@jasonbenfield/sharedwebapp/Components/MessageAlert";
import { TextLinkComponent } from "@jasonbenfield/sharedwebapp/Components/TextLinkComponent";
import { FormattedDate } from "@jasonbenfield/sharedwebapp/FormattedDate";
import { FormGroupText } from "@jasonbenfield/sharedwebapp/Forms/FormGroupText";
import { TimeSpan } from "@jasonbenfield/sharedwebapp/TimeSpan";
import { HubAppClient } from "../../../Lib/Http/HubAppClient";
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
    private readonly timeRangeFormGroup: FormGroupText;
    private readonly userNameFormGroup: FormGroupText;
    private readonly remoteAddressFormGroup: FormGroupText;
    private readonly userAgentFormGroup: FormGroupText;
    private readonly userLink: TextLinkComponent;
    private readonly requestsLink: TextLinkComponent;
    private sessionID: number;

    constructor(private readonly hubClient: HubAppClient, private readonly view: SessionPanelView) {
        this.alert = new MessageAlert(view.alert);
        this.timeRangeFormGroup = new FormGroupText(view.timeRange);
        this.userNameFormGroup = new FormGroupText(view.userName);
        this.remoteAddressFormGroup = new FormGroupText(view.remoteAddress);
        this.userAgentFormGroup = new FormGroupText(view.userAgent);
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
            () => this.hubClient.Logs.GetSessionDetail(this.sessionID)
        );
        let timeRange: string;
        const timeStarted = new FormattedDate(detail.Session.TimeStarted).formatDateTime();
        if (detail.Session.TimeEnded.getFullYear() === 9999) {
            timeRange = `${timeStarted} to ???`;
        }
        else {
            let timeEnded: string;
            const dateStarted = new Date(detail.Session.TimeStarted.getFullYear(), detail.Session.TimeStarted.getMonth(), detail.Session.TimeStarted.getDate());
            const dateEnded = new Date(detail.Session.TimeEnded.getFullYear(), detail.Session.TimeEnded.getMonth(), detail.Session.TimeEnded.getDate());
            if (dateStarted.getTime() === dateEnded.getTime()) {
                timeEnded = new FormattedDate(detail.Session.TimeEnded).formatTime();
            }
            else {
                timeEnded = new FormattedDate(detail.Session.TimeEnded).formatDateTime();
            }
            const ts = TimeSpan.dateDiff(detail.Session.TimeEnded, detail.Session.TimeStarted);
            timeRange = `${timeStarted} to ${timeEnded} [ ${ts} ]`;
        }
        this.timeRangeFormGroup.setValue(timeRange);
        this.userNameFormGroup.setValue(detail.User.UserName.DisplayText);
        if (detail.Session.RemoteAddress) {
            this.remoteAddressFormGroup.setValue(detail.Session.RemoteAddress);
            this.view.showRemoteAddress();
        }
        else {
            this.view.hideRemoteAddress();
        }
        if (detail.Session.UserAgent) {
            this.userAgentFormGroup.setValue(detail.Session.UserAgent);
            this.view.showUserAgent();
        }
        else {
            this.view.hideUserAgent();
        }
        this.userLink.setHref(
            this.hubClient.Users.Index.getModifierUrl(
                detail.UserGroup.PublicKey.Value,
                { UserID: detail.User.ID }
            )
        );
        this.requestsLink.setHref(this.hubClient.Logs.AppRequests.getUrl({ SessionID: this.sessionID, InstallationID: null }));
    }

    start() { return this.awaitable.start(); }

    activate() { this.view.show(); }

    deactivate() { this.view.hide(); }

}