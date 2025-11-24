import { App } from "./App";
import { AppLogEntry } from "./AppLogEntry";
import { AppRequest } from "./AppRequest";
import { AppResource } from "./AppResource";
import { AppResourceGroup } from "./AppResourceGroup";
import { AppSession } from "./AppSession";
import { AppUser } from "./AppUser";
import { AppUserGroup } from "./AppUserGroup";
import { InstallLocation } from "./InstallLocation";
import { Installation } from "./Installation";
import { Modifier } from "./Modifier";
import { ModifierCategory } from "./ModifierCategory";
import { XtiVersion } from "./XtiVersion";

export class AppLogEntryDetail {
    readonly logEntry: AppLogEntry;
    readonly request: AppRequest;
    readonly resourceGroup: AppResourceGroup;
    readonly resource: AppResource;
    readonly modCategory: ModifierCategory;
    readonly modifier: Modifier;
    readonly installLocation: InstallLocation;
    readonly installation: Installation;
    readonly version: XtiVersion;
    readonly app: App;
    readonly session: AppSession;
    readonly userGroup: AppUserGroup;
    readonly user: AppUser;
    readonly sourceLogEntryID: number;
    readonly targetLogEntryID: number;

    constructor(source?: IAppLogEntryDetailModel) {
        this.logEntry = new AppLogEntry(source && source.LogEntry);
        this.request = new AppRequest(source && source.Request);
        this.resourceGroup = new AppResourceGroup(source && source.ResourceGroup);
        this.resource = new AppResource(source && source.Resource);
        this.modCategory = new ModifierCategory(source && source.ModCategory);
        this.modifier = new Modifier(source && source.Modifier);
        this.installLocation = new InstallLocation(source && source.InstallLocation);
        this.installation = new Installation(source && source.Installation);
        this.version = new XtiVersion(source && source.Version);
        this.app = new App(source && source.App);
        this.session = new AppSession(source && source.Session);
        this.userGroup = new AppUserGroup(source && source.UserGroup);
        this.user = new AppUser(source && source.User);
        this.sourceLogEntryID = source ? source.SourceLogEntryID : 0;
        this.targetLogEntryID = source ? source.TargetLogEntryID : 0;
    }
}