import { Awaitable } from "@jasonbenfield/sharedwebapp/Awaitable";
import { Command } from "@jasonbenfield/sharedwebapp/Components/Command";
import { MessageAlert } from "@jasonbenfield/sharedwebapp/Components/MessageAlert";
import { TextComponent } from "@jasonbenfield/sharedwebapp/Components/TextComponent";
import { TextLinkComponent } from "@jasonbenfield/sharedwebapp/Components/TextLinkComponent";
import { DateRange } from "@jasonbenfield/sharedwebapp/DateRange";
import { FormattedDate } from "@jasonbenfield/sharedwebapp/FormattedDate";
import { TextValueFormGroup } from "@jasonbenfield/sharedwebapp/Forms/TextValueFormGroup";
import { TimeSpan } from "@jasonbenfield/sharedwebapp/TimeSpan";
import { ValueRangeBound } from "@jasonbenfield/sharedwebapp/ValueRangeBound";
import { HubAppApi } from "../../../Lib/Api/HubAppApi";
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
    private readonly appKey: TextValueFormGroup;
    private readonly versionKey: TextComponent;
    private readonly versionStatus: TextComponent;
    private readonly userName: TextValueFormGroup;
    private readonly currentInstallation: TextValueFormGroup;
    private readonly timeRange: TextValueFormGroup;
    private readonly path: TextValueFormGroup;
    private readonly installationLink: TextLinkComponent;
    private readonly logEntriesLink: TextLinkComponent;
    private requestID: number;

    constructor(private readonly hubApi: HubAppApi, private readonly view: RequestPanelView) {
        this.alert = new MessageAlert(view.alert);
        this.appKey = new TextValueFormGroup(view.appKey);
        this.versionKey = new TextComponent(view.versionKey);
        this.versionStatus = new TextComponent(view.versionStatus);
        this.userName = new TextValueFormGroup(view.userName);
        this.currentInstallation = new TextValueFormGroup(view.currentInstallation);
        this.timeRange = new TextValueFormGroup(view.timeRange);
        this.path = new TextValueFormGroup(view.path);
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
            () => this.hubApi.Logs.GetRequestDetail(this.requestID)
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
        this.installationLink.setHref(this.hubApi.Installations.Installation.getUrl({ InstallationID: detail.Installation.ID }));
        this.logEntriesLink.setHref(this.hubApi.Logs.LogEntries.getUrl({ RequestID: this.requestID, InstallationID: null }));
    }

    start() { return this.awaitable.start(); }

    activate() { this.view.show(); }

    deactivate() { this.view.hide(); }

}