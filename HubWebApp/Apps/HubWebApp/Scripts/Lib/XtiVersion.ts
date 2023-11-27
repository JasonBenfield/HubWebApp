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

    constructor(source: IXtiVersionModel) {
		this.id = source.ID;
		this.versionName = new AppVersionName(source.VersionName);
		this.versionKey = new AppVersionKey(source.VersionKey);
		this.versionNumber = new AppVersionNumber(source.VersionNumber);
		this.versionType = AppVersionType.values.value(source.VersionType);
		this.status = AppVersionStatus.values.value(source.Status);
		this.timeAdded = source.TimeAdded;
    }
}