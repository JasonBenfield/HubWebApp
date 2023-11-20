// Generated code
import { ODataColumnBuilder } from "@jasonbenfield/sharedwebapp/OData/ODataColumnBuilder";
import { ODataColumnViewBuilder } from "@jasonbenfield/sharedwebapp/OData/ODataColumnViewBuilder";
import { SourceType } from "@jasonbenfield/sharedwebapp/OData/SourceType";
import { ODataColumns } from "@jasonbenfield/sharedwebapp/OData/Types";

export class ODataExpandedRequestColumnViewsBuilder {
	readonly RequestID = new ODataColumnViewBuilder();
	readonly Path = new ODataColumnViewBuilder();
	readonly AppID = new ODataColumnViewBuilder();
	readonly AppKey = new ODataColumnViewBuilder();
	readonly AppName = new ODataColumnViewBuilder();
	readonly AppTypeText = new ODataColumnViewBuilder();
	readonly ResourceGroupName = new ODataColumnViewBuilder();
	readonly ResourceName = new ODataColumnViewBuilder();
	readonly ModCategoryName = new ODataColumnViewBuilder();
	readonly ModKey = new ODataColumnViewBuilder();
	readonly ModTargetKey = new ODataColumnViewBuilder();
	readonly ModDisplayText = new ODataColumnViewBuilder();
	readonly ActualCount = new ODataColumnViewBuilder();
	readonly SessionID = new ODataColumnViewBuilder();
	readonly UserName = new ODataColumnViewBuilder();
	readonly UserGroupID = new ODataColumnViewBuilder();
	readonly UserGroupName = new ODataColumnViewBuilder();
	readonly UserGroupDisplayText = new ODataColumnViewBuilder();
	readonly RequestTimeStarted = new ODataColumnViewBuilder();
	readonly RequestTimeEnded = new ODataColumnViewBuilder();
	readonly RequestTimeElapsed = new ODataColumnViewBuilder();
	readonly Succeeded = new ODataColumnViewBuilder();
	readonly CriticalErrorCount = new ODataColumnViewBuilder();
	readonly ValidationFailedCount = new ODataColumnViewBuilder();
	readonly AppErrorCount = new ODataColumnViewBuilder();
	readonly TotalErrorCount = new ODataColumnViewBuilder();
	readonly InformationMessageCount = new ODataColumnViewBuilder();
	readonly VersionName = new ODataColumnViewBuilder();
	readonly VersionKey = new ODataColumnViewBuilder();
	readonly VersionRelease = new ODataColumnViewBuilder();
	readonly VersionStatus = new ODataColumnViewBuilder();
	readonly VersionType = new ODataColumnViewBuilder();
	readonly InstallationID = new ODataColumnViewBuilder();
	readonly InstallLocation = new ODataColumnViewBuilder();
	readonly IsCurrentInstallation = new ODataColumnViewBuilder();
	readonly SourceRequestID = new ODataColumnViewBuilder();
}

export class ODataExpandedRequestColumnsBuilder {
	constructor(views: ODataExpandedRequestColumnViewsBuilder) {
		this.RequestID = new ODataColumnBuilder('RequestID', new SourceType('Int32'), views.RequestID);
		this.RequestID.setDisplayText('Request ID');
		this.Path = new ODataColumnBuilder('Path', new SourceType('String'), views.Path);
		this.AppID = new ODataColumnBuilder('AppID', new SourceType('Int32'), views.AppID);
		this.AppID.setDisplayText('App ID');
		this.AppKey = new ODataColumnBuilder('AppKey', new SourceType('String'), views.AppKey);
		this.AppKey.setDisplayText('App Key');
		this.AppName = new ODataColumnBuilder('AppName', new SourceType('String'), views.AppName);
		this.AppName.setDisplayText('App Name');
		this.AppTypeText = new ODataColumnBuilder('AppTypeText', new SourceType('String'), views.AppTypeText);
		this.AppTypeText.setDisplayText('App Type Text');
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
		this.ActualCount = new ODataColumnBuilder('ActualCount', new SourceType('Int32'), views.ActualCount);
		this.ActualCount.setDisplayText('Actual Count');
		this.SessionID = new ODataColumnBuilder('SessionID', new SourceType('Int32'), views.SessionID);
		this.SessionID.setDisplayText('Session ID');
		this.UserName = new ODataColumnBuilder('UserName', new SourceType('String'), views.UserName);
		this.UserName.setDisplayText('User Name');
		this.UserGroupID = new ODataColumnBuilder('UserGroupID', new SourceType('Int32'), views.UserGroupID);
		this.UserGroupID.setDisplayText('User Group ID');
		this.UserGroupName = new ODataColumnBuilder('UserGroupName', new SourceType('String'), views.UserGroupName);
		this.UserGroupName.setDisplayText('User Group Name');
		this.UserGroupDisplayText = new ODataColumnBuilder('UserGroupDisplayText', new SourceType('String'), views.UserGroupDisplayText);
		this.UserGroupDisplayText.setDisplayText('User Group Display Text');
		this.RequestTimeStarted = new ODataColumnBuilder('RequestTimeStarted', new SourceType('DateTimeOffset'), views.RequestTimeStarted);
		this.RequestTimeStarted.setDisplayText('Request Time Started');
		this.RequestTimeEnded = new ODataColumnBuilder('RequestTimeEnded', new SourceType('DateTimeOffset'), views.RequestTimeEnded);
		this.RequestTimeEnded.setDisplayText('Request Time Ended');
		this.RequestTimeElapsed = new ODataColumnBuilder('RequestTimeElapsed', new SourceType('String'), views.RequestTimeElapsed);
		this.RequestTimeElapsed.setDisplayText('Request Time Elapsed');
		this.Succeeded = new ODataColumnBuilder('Succeeded', new SourceType('Boolean'), views.Succeeded);
		this.CriticalErrorCount = new ODataColumnBuilder('CriticalErrorCount', new SourceType('Int32'), views.CriticalErrorCount);
		this.CriticalErrorCount.setDisplayText('Critical Error Count');
		this.ValidationFailedCount = new ODataColumnBuilder('ValidationFailedCount', new SourceType('Int32'), views.ValidationFailedCount);
		this.ValidationFailedCount.setDisplayText('Validation Failed Count');
		this.AppErrorCount = new ODataColumnBuilder('AppErrorCount', new SourceType('Int32'), views.AppErrorCount);
		this.AppErrorCount.setDisplayText('App Error Count');
		this.TotalErrorCount = new ODataColumnBuilder('TotalErrorCount', new SourceType('Int32'), views.TotalErrorCount);
		this.TotalErrorCount.setDisplayText('Total Error Count');
		this.InformationMessageCount = new ODataColumnBuilder('InformationMessageCount', new SourceType('Int32'), views.InformationMessageCount);
		this.InformationMessageCount.setDisplayText('Information Message Count');
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
		this.SourceRequestID = new ODataColumnBuilder('SourceRequestID', new SourceType('Int32'), views.SourceRequestID);
		this.SourceRequestID.setDisplayText('Source Request ID');
	}
	readonly RequestID: ODataColumnBuilder;
	readonly Path: ODataColumnBuilder;
	readonly AppID: ODataColumnBuilder;
	readonly AppKey: ODataColumnBuilder;
	readonly AppName: ODataColumnBuilder;
	readonly AppTypeText: ODataColumnBuilder;
	readonly ResourceGroupName: ODataColumnBuilder;
	readonly ResourceName: ODataColumnBuilder;
	readonly ModCategoryName: ODataColumnBuilder;
	readonly ModKey: ODataColumnBuilder;
	readonly ModTargetKey: ODataColumnBuilder;
	readonly ModDisplayText: ODataColumnBuilder;
	readonly ActualCount: ODataColumnBuilder;
	readonly SessionID: ODataColumnBuilder;
	readonly UserName: ODataColumnBuilder;
	readonly UserGroupID: ODataColumnBuilder;
	readonly UserGroupName: ODataColumnBuilder;
	readonly UserGroupDisplayText: ODataColumnBuilder;
	readonly RequestTimeStarted: ODataColumnBuilder;
	readonly RequestTimeEnded: ODataColumnBuilder;
	readonly RequestTimeElapsed: ODataColumnBuilder;
	readonly Succeeded: ODataColumnBuilder;
	readonly CriticalErrorCount: ODataColumnBuilder;
	readonly ValidationFailedCount: ODataColumnBuilder;
	readonly AppErrorCount: ODataColumnBuilder;
	readonly TotalErrorCount: ODataColumnBuilder;
	readonly InformationMessageCount: ODataColumnBuilder;
	readonly VersionName: ODataColumnBuilder;
	readonly VersionKey: ODataColumnBuilder;
	readonly VersionRelease: ODataColumnBuilder;
	readonly VersionStatus: ODataColumnBuilder;
	readonly VersionType: ODataColumnBuilder;
	readonly InstallationID: ODataColumnBuilder;
	readonly InstallLocation: ODataColumnBuilder;
	readonly IsCurrentInstallation: ODataColumnBuilder;
	readonly SourceRequestID: ODataColumnBuilder;
	
	build() {
		return {
			RequestID: this.RequestID.build(),
			Path: this.Path.build(),
			AppID: this.AppID.build(),
			AppKey: this.AppKey.build(),
			AppName: this.AppName.build(),
			AppTypeText: this.AppTypeText.build(),
			ResourceGroupName: this.ResourceGroupName.build(),
			ResourceName: this.ResourceName.build(),
			ModCategoryName: this.ModCategoryName.build(),
			ModKey: this.ModKey.build(),
			ModTargetKey: this.ModTargetKey.build(),
			ModDisplayText: this.ModDisplayText.build(),
			ActualCount: this.ActualCount.build(),
			SessionID: this.SessionID.build(),
			UserName: this.UserName.build(),
			UserGroupID: this.UserGroupID.build(),
			UserGroupName: this.UserGroupName.build(),
			UserGroupDisplayText: this.UserGroupDisplayText.build(),
			RequestTimeStarted: this.RequestTimeStarted.build(),
			RequestTimeEnded: this.RequestTimeEnded.build(),
			RequestTimeElapsed: this.RequestTimeElapsed.build(),
			Succeeded: this.Succeeded.build(),
			CriticalErrorCount: this.CriticalErrorCount.build(),
			ValidationFailedCount: this.ValidationFailedCount.build(),
			AppErrorCount: this.AppErrorCount.build(),
			TotalErrorCount: this.TotalErrorCount.build(),
			InformationMessageCount: this.InformationMessageCount.build(),
			VersionName: this.VersionName.build(),
			VersionKey: this.VersionKey.build(),
			VersionRelease: this.VersionRelease.build(),
			VersionStatus: this.VersionStatus.build(),
			VersionType: this.VersionType.build(),
			InstallationID: this.InstallationID.build(),
			InstallLocation: this.InstallLocation.build(),
			IsCurrentInstallation: this.IsCurrentInstallation.build(),
			SourceRequestID: this.SourceRequestID.build()
		} as ODataColumns<IExpandedRequest>;
	}
}