import { Awaitable } from "@jasonbenfield/sharedwebapp/Awaitable";
import { Command } from "@jasonbenfield/sharedwebapp/Components/Command";
import { LinkComponent } from "@jasonbenfield/sharedwebapp/Components/LinkComponent";
import { HubAppApi } from "../Lib/Api/HubAppApi";
import { MainMenuPanelView } from "./MainMenuPanelView";

interface IResult {
    back?: {};
}

class Result {
    static back() { return new Result({ back: {} }); }

    private constructor(private readonly results: IResult) { }

    get back() { return this.results.back; }
}

export class MainMenuPanel implements IPanel {
    private readonly awaitable = new Awaitable<Result>();
    private readonly appsLink: LinkComponent;
    private readonly userGroupsLink: LinkComponent;
    private readonly sessionsLink: LinkComponent;
    private readonly requestsLink: LinkComponent;
    private readonly logEntriesLink: LinkComponent;
    private readonly installationsLink: LinkComponent;
            
    constructor(hubApi: HubAppApi, private readonly view: MainMenuPanelView) {
        this.appsLink = new LinkComponent(view.appsLink);
        this.appsLink.setHref(hubApi.Apps.Index.getUrl({}));
        this.userGroupsLink = new LinkComponent(view.userGroupsLink);
        this.userGroupsLink.setHref(hubApi.UserGroups.Index.getUrl({}));
        this.sessionsLink = new LinkComponent(view.sessionsLink);
        this.sessionsLink.setHref(hubApi.Logs.Sessions.getUrl({}));
        this.requestsLink = new LinkComponent(view.requestsLink);
        this.requestsLink.setHref(hubApi.Logs.Requests.getUrl({ SessionID: null }));
        this.logEntriesLink = new LinkComponent(view.logEntriesLink);
        this.logEntriesLink.setHref(hubApi.Logs.LogEntries.getUrl({ RequestID: null }));
        this.installationsLink = new LinkComponent(view.installationsLink);
        this.installationsLink.setHref(hubApi.Installations.Index.getUrl({ QueryType: 0 }));
        new Command(this.back.bind(this)).add(view.backButton);
    }

    private back() { this.awaitable.resolve(Result.back()); }

    start() { return this.awaitable.start(); }

    activate() {
        this.view.show();
    }

    deactivate() { this.view.hide(); }
}