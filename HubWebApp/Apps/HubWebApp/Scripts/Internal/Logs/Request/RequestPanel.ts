﻿import { Awaitable } from "@jasonbenfield/sharedwebapp/Awaitable";
import { Command } from "@jasonbenfield/sharedwebapp/Components/Command";
import { MessageAlert } from "@jasonbenfield/sharedwebapp/Components/MessageAlert";
import { TextComponent } from "@jasonbenfield/sharedwebapp/Components/TextComponent";
import { TextLinkComponent } from "@jasonbenfield/sharedwebapp/Components/TextLinkComponent";
import { FormattedDate } from "@jasonbenfield/sharedwebapp/FormattedDate";
import { FormGroupText } from "@jasonbenfield/sharedwebapp/Forms/FormGroupText";
import { TimeSpan } from "@jasonbenfield/sharedwebapp/TimeSpan";
import { HubAppClient } from "../../../Lib/Http/HubAppClient";
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
        const detail = await this.alert.infoAction(
            'Loading...',
            () => this.hubClient.Logs.GetRequestDetail(this.requestID)
        );
        this.appKey.setValue(
            detail.App.AppKey.Name.DisplayText + ' ' + detail.App.AppKey.Type.DisplayText
        );
        this.versionKey.setText(detail.Version.VersionKey.DisplayText);
        this.versionStatus.setText(`[ ${detail.Version.Status.DisplayText} ]`);
        this.userName.setValue(detail.User.UserName.DisplayText);
        if (detail.Installation.IsCurrent) {
            this.currentInstallation.setValue('Current');
            this.view.showCurrentInstallation();
        }
        else {
            this.view.hideCurrentInstallation();
        }
        let timeRange: string;
        const timeStarted = new FormattedDate(detail.Request.TimeStarted).formatDateTime();
        if (detail.Request.TimeEnded.getFullYear() === 9999) {
            timeRange = `${timeStarted} to ???`;
        }
        else {
            let timeEnded: string;
            const dateStarted = new Date(detail.Request.TimeStarted.getFullYear(), detail.Request.TimeStarted.getMonth(), detail.Request.TimeStarted.getDate());
            const dateEnded = new Date(detail.Request.TimeEnded.getFullYear(), detail.Request.TimeEnded.getMonth(), detail.Request.TimeEnded.getDate());
            if (dateStarted.getTime() === dateEnded.getTime()) {
                timeEnded = new FormattedDate(detail.Request.TimeEnded).formatTime();
            }
            else {
                timeEnded =  new FormattedDate(detail.Request.TimeEnded).formatDateTime();
            }
            const ts = TimeSpan.dateDiff(detail.Request.TimeEnded, detail.Request.TimeStarted);
            timeRange = `${timeStarted} to ${timeEnded} [ ${ts} ]`;
        }
        this.timeRange.setValue(timeRange);
        this.path.setValue(detail.Request.Path);
        this.sessionLink.setHref(this.hubClient.Logs.Session.getUrl({ SessionID: detail.Session.ID }));
        if (detail.SourceRequestID) {
            this.sourceRequestLink.setHref(this.hubClient.Logs.AppRequest.getUrl({ RequestID: detail.SourceRequestID }));
            this.sourceRequestLink.show();
        }
        else {
            this.sourceRequestLink.hide();
        }
        if (detail.TargetRequestIDs.length > 0) {
            this.targetRequestLink.setHref(this.hubClient.Logs.AppRequests.getUrl({
                SessionID: null,
                InstallationID: null,
                SourceRequestID: detail.Request.ID
            }));
            this.targetRequestLink.show();
        }
        else {
            this.targetRequestLink.hide();
        }
        this.installationLink.setHref(this.hubClient.Installations.Installation.getUrl({ InstallationID: detail.Installation.ID }));
        this.logEntriesLink.setHref(this.hubClient.Logs.LogEntries.getUrl({ RequestID: this.requestID, InstallationID: null }));
    }

    start() { return this.awaitable.start(); }

    activate() { this.view.show(); }

    deactivate() { this.view.hide(); }

}