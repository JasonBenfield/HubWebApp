import { DateTimeOffset } from "@jasonbenfield/sharedwebapp/DateTimeOffset";
import { AppVersionKey } from "./AppVersionKey";
import { AppVersionName } from "./AppVersionName";
import { AppVersionNumber } from "./AppVersionNumber";
import { AppVersionStatus } from "./Http/AppVersionStatus";
import { AppVersionType } from "./Http/AppVersionType";

export class XtiVersion {
    readonly id: number;
    readonly versionName: AppVersionName;
    readonly versionKey: AppVersionKey;
    readonly versionNumber: AppVersionNumber;
    readonly versionType: AppVersionType;
    readonly status: AppVersionStatus;
    readonly timeAdded: DateTimeOffset;

    constructor(source?: IXtiVersionModel) {
        this.id = source ? source.ID : 0;
        this.versionName = new AppVersionName(source && source.VersionName);
        this.versionKey = new AppVersionKey(source && source.VersionKey);
        this.versionNumber = new AppVersionNumber(source && source.VersionNumber);
        this.versionType = source ? AppVersionType.values.value(source.VersionType) : AppVersionType.values.NotSet;
        this.status = source ? AppVersionStatus.values.value(source.Status) : AppVersionStatus.values.NotSet;
        this.timeAdded = source ? source.TimeAdded : DateTimeOffset.max();
    }
}