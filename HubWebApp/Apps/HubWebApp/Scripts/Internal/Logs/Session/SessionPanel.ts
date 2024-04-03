import { Awaitable } from "@jasonbenfield/sharedwebapp/Awaitable";
import { Command } from "@jasonbenfield/sharedwebapp/Components/Command";
import { MessageAlert } from "@jasonbenfield/sharedwebapp/Components/MessageAlert";
import { TextLinkComponent } from "@jasonbenfield/sharedwebapp/Components/TextLinkComponent";
import { FormGroupText } from "@jasonbenfield/sharedwebapp/Forms/FormGroupText";
import { AppSessionDetail } from "../../../Lib/AppSessionDetail";
import { HubAppClient } from "../../../Lib/Http/HubAppClient";
import { FormattedTimeRange } from "../../../lib/FormattedTimeRange";
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
        this.timeRangeFormGroup = new FormGroupText(view.timeRangeTextView);
        this.userNameFormGroup = new FormGroupText(view.userNameFormGroupView);
        this.remoteAddressFormGroup = new FormGroupText(view.remoteAddressFormGroupView);
        this.userAgentFormGroup = new FormGroupText(view.userAgentFormGroupView);
        this.userLink = new TextLinkComponent(view.userLink);
        this.requestsLink = new TextLinkComponent(view.requestsLink);
        new Command(this.menu.bind(this)).add(view.menuButton);
    }

    private menu() { this.awaitable.resolve(Result.menuRequested()); }

    setSessionID(sessionID: number) {
        this.sessionID = sessionID;
    }

    async refresh() {
        this.userAgentFormGroup.hide();
        this.remoteAddressFormGroup.hide();
        const sourceDetail = await this.alert.infoAction(
            'Loading...',
            () => this.hubClient.Logs.GetSessionDetail(this.sessionID)
        );
        const detail = new AppSessionDetail(sourceDetail);
        this.timeRangeFormGroup.setValue(
            new FormattedTimeRange(detail.session.timeStarted, detail.session.timeEnded).format()
        );
        this.userNameFormGroup.setValue(detail.user.userName.displayText);
        this.remoteAddressFormGroup.setValue(detail.session.remoteAddress);
        if (detail.session.remoteAddress) {
            this.remoteAddressFormGroup.show();
        }
        this.userAgentFormGroup.setValue(detail.session.userAgent);
        if (detail.session.userAgent) {
            this.userAgentFormGroup.show();
        }
        this.userLink.setHref(
            this.hubClient.Users.Index.getModifierUrl(
                detail.userGroup.getModifier(),
                { UserID: detail.user.id, ReturnTo: '' }
            )
        );
        this.requestsLink.setHref(this.hubClient.Logs.AppRequests.getUrl({
            SessionID: this.sessionID,
            InstallationID: null,
            SourceRequestID: null
        }));
    }

    start() { return this.awaitable.start(); }

    activate() { this.view.show(); }

    deactivate() { this.view.hide(); }

}