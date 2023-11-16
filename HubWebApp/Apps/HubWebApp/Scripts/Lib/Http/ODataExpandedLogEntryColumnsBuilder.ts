// Generated code
import { ODataColumnBuilder } from "@jasonbenfield/sharedwebapp/OData/ODataColumnBuilder";
import { ODataColumnViewBuilder } from "@jasonbenfield/sharedwebapp/OData/ODataColumnViewBuilder";
import { SourceType } from "@jasonbenfield/sharedwebapp/OData/SourceType";
import { ODataColumns } from "@jasonbenfield/sharedwebapp/OData/Types";

export class ODataExpandedLogEntryColumnViewsBuilder {
	readonly EventID = new ODataColumnViewBuilder();
	readonly TimeOccurred = new ODataColumnViewBuilder();
	readonly Severity = new ODataColumnViewBuilder();
	readonly Caption = new ODataColumnViewBuilder();
	readonly Message = new ODataColumnViewBuilder();
	readonly Detail = new ODataColumnViewBuilder();
	readonly Path = new ODataColumnViewBuilder();
	readonly ActualCount = new ODataColumnViewBuilder();
	readonly AppID = new ODataColumnViewBuilder();
	readonly AppKey = new ODataColumnViewBuilder();
	readonly AppName = new ODataColumnViewBuilder();
	readonly AppType = new ODataColumnViewBuilder();
	readonly ResourceGroupName = new ODataColumnViewBuilder();
	readonly ResourceName = new ODataColumnViewBuilder();
	readonly ModCategoryName = new ODataColumnViewBuilder();
	readonly ModKey = new ODataColumnViewBuilder();
	readonly ModTargetKey = new ODataColumnViewBuilder();
	readonly ModDisplayText = new ODataColumnViewBuilder();
	readonly UserName = new ODataColumnViewBuilder();
	readonly UserGroupID = new ODataColumnViewBuilder();
	readonly UserGroupName = new ODataColumnViewBuilder();
	readonly UserGroupDisplayText = new ODataColumnViewBuilder();
	readonly RequestID = new ODataColumnViewBuilder();
	readonly RequestTimeStarted = new ODataColumnViewBuilder();
	readonly RequestTimeEnded = new ODataColumnViewBuilder();
	readonly RequestTimeElapsed = new ODataColumnViewBuilder();
	readonly VersionName = new ODataColumnViewBuilder();
	readonly VersionKey = new ODataColumnViewBuilder();
	readonly VersionRelease = new ODataColumnViewBuilder();
	readonly VersionStatus = new ODataColumnViewBuilder();
	readonly VersionType = new ODataColumnViewBuilder();
	readonly InstallationID = new ODataColumnViewBuilder();
	readonly InstallLocation = new ODataColumnViewBuilder();
	readonly IsCurrentInstallation = new ODataColumnViewBuilder();
	readonly SourceID = new ODataColumnViewBuilder();
}

export class ODataExpandedLogEntryColumnsBuilder {
	constructor(views: ODataExpandedLogEntryColumnViewsBuilder) {
		this.EventID = new ODataColumnBuilder('EventID', new SourceType('Int32'), views.EventID);
		this.EventID.setDisplayText('Event ID');
		this.TimeOccurred = new ODataColumnBuilder('TimeOccurred', new SourceType('DateTimeOffset'), views.TimeOccurred);
		this.TimeOccurred.setDisplayText('Time Occurred');
		this.Severity = new ODataColumnBuilder('Severity', new SourceType('String'), views.Severity);
		this.Caption = new ODataColumnBuilder('Caption', new SourceType('String'), views.Caption);
		this.Message = new ODataColumnBuilder('Message', new SourceType('String'), views.Message);
		this.Detail = new ODataColumnBuilder('Detail', new SourceType('String'), views.Detail);
		this.Path = new ODataColumnBuilder('Path', new SourceType('String'), views.Path);
		this.ActualCount = new ODataColumnBuilder('ActualCount', new SourceType('Int32'), views.ActualCount);
		this.ActualCount.setDisplayText('Actual Count');
		this.AppID = new ODataColumnBuilder('AppID', new SourceType('Int32'), views.AppID);
		this.AppID.setDisplayText('App ID');
		this.AppKey = new ODataColumnBuilder('AppKey', new SourceType('String'), views.AppKey);
		this.AppKey.setDisplayText('App Key');
		this.AppName = new ODataColumnBuilder('AppName', new SourceType('String'), views.AppName);
		this.AppName.setDisplayText('App Name');
		this.AppType = new ODataColumnBuilder('AppType', new SourceType('String'), views.AppType);
		this.AppType.setDisplayText('App Type');
		this.ResourceGroupName = new ODataColumnBuilder('ResourceGroupName', new SourceType('String'), views.ResourceGroupName);
		this.ResourceGroupName.setDisplayText('Resource Group Name');
		this.ResourceName = new ODataColumnBuilder('ResourceName', new SourceType('String'), views.ResourceName);
		this.ResourceName.setDisplayText('Resource Name');
		this.ModCategoryName = new ODataColumnBuilder('ModCategoryName', new SourceType('String'), views.ModCategoryName);
		this.ModCategoryName.setDisplayText('Mod Category Name');
		this.ModKey = new ODataColumnBuilder('ModKey', new SourceType('String'), views.ModKey);
		this.ModKey.setDisplayText('Mod Key');
		this.ModTargetKey = new ODataColumnBuilder('ModTargetKey', new SourceType('String'), views.ModTargetKey);
		this.ModTargetKey.setDisplayText('Mod Target Key');
		this.ModDisplayText = new ODataColumnBuilder('ModDisplayText', new SourceType('String'), views.ModDisplayText);
		this.ModDisplayText.setDisplayText('Mod Display Text');
		this.UserName = new ODataColumnBuilder('UserName', new SourceType('String'), views.UserName);
		this.UserName.setDisplayText('User Name');
		this.UserGroupID = new ODataColumnBuilder('UserGroupID', new SourceType('Int32'), views.UserGroupID);
		this.UserGroupID.setDisplayText('User Group ID');
		this.UserGroupName = new ODataColumnBuilder('UserGroupName', new SourceType('String'), views.UserGroupName);
		this.UserGroupName.setDisplayText('User Group Name');
		this.UserGroupDisplayText = new ODataColumnBuilder('UserGroupDisplayText', new SourceType('String'), views.UserGroupDisplayText);
		this.UserGroupDisplayText.setDisplayText('User Group Display Text');
		this.RequestID = new ODataColumnBuilder('RequestID', new SourceType('Int32'), views.RequestID);
		this.RequestID.setDisplayText('Request ID');
		this.RequestTimeStarted = new ODataColumnBuilder('RequestTimeStarted', new SourceType('DateTimeOffset'), views.RequestTimeStarted);
		this.RequestTimeStarted.setDisplayText('Request Time Started');
		this.RequestTimeEnded = new ODataColumnBuilder('RequestTimeEnded', new SourceType('DateTimeOffset'), views.RequestTimeEnded);
		this.RequestTimeEnded.setDisplayText('Request Time Ended');
		this.RequestTimeElapsed = new ODataColumnBuilder('RequestTimeElapsed', new SourceType('String'), views.RequestTimeElapsed);
		this.RequestTimeElapsed.setDisplayText('Request Time Elapsed');
		this.VersionName = new ODataColumnBuilder('VersionName', new SourceType('String'), views.VersionName);
		this.VersionName.setDisplayText('Version Name');
		this.VersionKey = new ODataColumnBuilder('VersionKey', new SourceType('String'), views.VersionKey);
		this.VersionKey.setDisplayText('Version Key');
		this.VersionRelease = new ODataColumnBuilder('VersionRelease', new SourceType('String'), views.VersionRelease);
		this.VersionRelease.setDisplayText('Version Release');
		this.VersionStatus = new ODataColumnBuilder('VersionStatus', new SourceType('String'), views.VersionStatus);
		this.VersionStatus.setDisplayText('Version Status');
		this.VersionType = new ODataColumnBuilder('VersionType', new SourceType('String'), views.VersionType);
		this.VersionType.setDisplayText('Version Type');
		this.InstallationID = new ODataColumnBuilder('InstallationID', new SourceType('Int32'), views.InstallationID);
		this.InstallationID.setDisplayText('Installation ID');
		this.InstallLocation = new ODataColumnBuilder('InstallLocation', new SourceType('String'), views.InstallLocation);
		this.InstallLocation.setDisplayText('Install Location');
		this.IsCurrentInstallation = new ODataColumnBuilder('IsCurrentInstallation', new SourceType('Boolean'), views.IsCurrentInstallation);
		this.IsCurrentInstallation.setDisplayText('Is Current Installation');
		this.SourceID = new ODataColumnBuilder('SourceID', new SourceType('Int32'), views.SourceID);
		this.SourceID.setDisplayText('Source ID');
	}
	readonly EventID: ODataColumnBuilder;
	readonly TimeOccurred: ODataColumnBuilder;
	readonly Severity: ODataColumnBuilder;
	readonly Caption: ODataColumnBuilder;
	readonly Message: ODataColumnBuilder;
	readonly Detail: ODataColumnBuilder;
	readonly Path: ODataColumnBuilder;
	readonly ActualCount: ODataColumnBuilder;
	readonly AppID: ODataColumnBuilder;
	readonly AppKey: ODataColumnBuilder;
	readonly AppName: ODataColumnBuilder;
	readonly AppType: ODataColumnBuilder;
	readonly ResourceGroupName: ODataColumnBuilder;
	readonly ResourceName: ODataColumnBuilder;
	readonly ModCategoryName: ODataColumnBuilder;
	readonly ModKey: ODataColumnBuilder;
	readonly ModTargetKey: ODataColumnBuilder;
	readonly ModDisplayText: ODataColumnBuilder;
	readonly UserName: ODataColumnBuilder;
	readonly UserGroupID: ODataColumnBuilder;
	readonly UserGroupName: ODataColumnBuilder;
	readonly UserGroupDisplayText: ODataColumnBuilder;
	readonly RequestID: ODataColumnBuilder;
	readonly RequestTimeStarted: ODataColumnBuilder;
	readonly RequestTimeEnded: ODataColumnBuilder;
	readonly RequestTimeElapsed: ODataColumnBuilder;
	readonly VersionName: ODataColumnBuilder;
	readonly VersionKey: ODataColumnBuilder;
	readonly VersionRelease: ODataColumnBuilder;
	readonly VersionStatus: ODataColumnBuilder;
	readonly VersionType: ODataColumnBuilder;
	readonly InstallationID: ODataColumnBuilder;
	readonly InstallLocation: ODataColumnBuilder;
	readonly IsCurrentInstallation: ODataColumnBuilder;
	readonly SourceID: ODataColumnBuilder;
	
	build() {
		return {
			EventID: this.EventID.build(),
			TimeOccurred: this.TimeOccurred.build(),
			Severity: this.Severity.build(),
			Caption: this.Caption.build(),
			Message: this.Message.build(),
			Detail: this.Detail.build(),
			Path: this.Path.build(),
			ActualCount: this.ActualCount.build(),
			AppID: this.AppID.build(),
			AppKey: this.AppKey.build(),
			AppName: this.AppName.build(),
			AppType: this.AppType.build(),
			ResourceGroupName: this.ResourceGroupName.build(),
			ResourceName: this.ResourceName.build(),
			ModCategoryName: this.ModCategoryName.build(),
			ModKey: this.ModKey.build(),
			ModTargetKey: this.ModTargetKey.build(),
			ModDisplayText: this.ModDisplayText.build(),
			UserName: this.UserName.build(),
			UserGroupID: this.UserGroupID.build(),
			UserGroupName: this.UserGroupName.build(),
			UserGroupDisplayText: this.UserGroupDisplayText.build(),
			RequestID: this.RequestID.build(),
			RequestTimeStarted: this.RequestTimeStarted.build(),
			RequestTimeEnded: this.RequestTimeEnded.build(),
			RequestTimeElapsed: this.RequestTimeElapsed.build(),
			VersionName: this.VersionName.build(),
			VersionKey: this.VersionKey.build(),
			VersionRelease: this.VersionRelease.build(),
			VersionStatus: this.VersionStatus.build(),
			VersionType: this.VersionType.build(),
			InstallationID: this.InstallationID.build(),
			InstallLocation: this.InstallLocation.build(),
			IsCurrentInstallation: this.IsCurrentInstallation.build(),
			SourceID: this.SourceID.build()
		} as ODataColumns<IExpandedLogEntry>;
	}
}