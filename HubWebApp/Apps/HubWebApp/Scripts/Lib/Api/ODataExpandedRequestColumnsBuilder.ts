// Generated code
import { ODataColumnBuilder } from "@jasonbenfield/sharedwebapp/OData/ODataColumnBuilder";
import { ODataColumnViewBuilder } from "@jasonbenfield/sharedwebapp/OData/ODataColumnViewBuilder";
import { SourceType } from "@jasonbenfield/sharedwebapp/OData/SourceType";
import { ODataColumns } from "@jasonbenfield/sharedwebapp/OData/Types";

export class ODataExpandedRequestColumnViewsBuilder {
	readonly RequestID = new ODataColumnViewBuilder();
	readonly Path = new ODataColumnViewBuilder();
	readonly AppName = new ODataColumnViewBuilder();
	readonly AppType = new ODataColumnViewBuilder();
	readonly ResourceGroupName = new ODataColumnViewBuilder();
	readonly ResourceName = new ODataColumnViewBuilder();
	readonly ModCategoryName = new ODataColumnViewBuilder();
	readonly ModKey = new ODataColumnViewBuilder();
	readonly ModTargetKey = new ODataColumnViewBuilder();
	readonly ModDisplayText = new ODataColumnViewBuilder();
	readonly ActualCount = new ODataColumnViewBuilder();
	readonly UserName = new ODataColumnViewBuilder();
	readonly UserGroupName = new ODataColumnViewBuilder();
	readonly TimeStarted = new ODataColumnViewBuilder();
	readonly TimeEnded = new ODataColumnViewBuilder();
	readonly TimeElapsed = new ODataColumnViewBuilder();
	readonly Succeeded = new ODataColumnViewBuilder();
	readonly CriticalErrorCount = new ODataColumnViewBuilder();
	readonly VersionName = new ODataColumnViewBuilder();
	readonly VersionKey = new ODataColumnViewBuilder();
	readonly VersionType = new ODataColumnViewBuilder();
	readonly InstallLocation = new ODataColumnViewBuilder();
	readonly IsCurrentInstallation = new ODataColumnViewBuilder();
}

export class ODataExpandedRequestColumnsBuilder {
	constructor(views: ODataExpandedRequestColumnViewsBuilder) {
		this.RequestID = new ODataColumnBuilder('RequestID', new SourceType('Int32'), views.RequestID);
		this.RequestID.setDisplayText('Request ID');
		this.Path = new ODataColumnBuilder('Path', new SourceType('String'), views.Path);
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
		this.ActualCount = new ODataColumnBuilder('ActualCount', new SourceType('Int32'), views.ActualCount);
		this.ActualCount.setDisplayText('Actual Count');
		this.UserName = new ODataColumnBuilder('UserName', new SourceType('String'), views.UserName);
		this.UserName.setDisplayText('User Name');
		this.UserGroupName = new ODataColumnBuilder('UserGroupName', new SourceType('String'), views.UserGroupName);
		this.UserGroupName.setDisplayText('User Group Name');
		this.TimeStarted = new ODataColumnBuilder('TimeStarted', new SourceType('DateTimeOffset'), views.TimeStarted);
		this.TimeStarted.setDisplayText('Time Started');
		this.TimeEnded = new ODataColumnBuilder('TimeEnded', new SourceType('DateTimeOffset'), views.TimeEnded);
		this.TimeEnded.setDisplayText('Time Ended');
		this.TimeElapsed = new ODataColumnBuilder('TimeElapsed', new SourceType('Nullable`1'), views.TimeElapsed);
		this.TimeElapsed.setDisplayText('Time Elapsed');
		this.Succeeded = new ODataColumnBuilder('Succeeded', new SourceType('Boolean'), views.Succeeded);
		this.CriticalErrorCount = new ODataColumnBuilder('CriticalErrorCount', new SourceType('Int32'), views.CriticalErrorCount);
		this.CriticalErrorCount.setDisplayText('Critical Error Count');
		this.VersionName = new ODataColumnBuilder('VersionName', new SourceType('String'), views.VersionName);
		this.VersionName.setDisplayText('Version Name');
		this.VersionKey = new ODataColumnBuilder('VersionKey', new SourceType('String'), views.VersionKey);
		this.VersionKey.setDisplayText('Version Key');
		this.VersionType = new ODataColumnBuilder('VersionType', new SourceType('String'), views.VersionType);
		this.VersionType.setDisplayText('Version Type');
		this.InstallLocation = new ODataColumnBuilder('InstallLocation', new SourceType('String'), views.InstallLocation);
		this.InstallLocation.setDisplayText('Install Location');
		this.IsCurrentInstallation = new ODataColumnBuilder('IsCurrentInstallation', new SourceType('Boolean'), views.IsCurrentInstallation);
		this.IsCurrentInstallation.setDisplayText('Is Current Installation');
	}
	readonly RequestID: ODataColumnBuilder;
	readonly Path: ODataColumnBuilder;
	readonly AppName: ODataColumnBuilder;
	readonly AppType: ODataColumnBuilder;
	readonly ResourceGroupName: ODataColumnBuilder;
	readonly ResourceName: ODataColumnBuilder;
	readonly ModCategoryName: ODataColumnBuilder;
	readonly ModKey: ODataColumnBuilder;
	readonly ModTargetKey: ODataColumnBuilder;
	readonly ModDisplayText: ODataColumnBuilder;
	readonly ActualCount: ODataColumnBuilder;
	readonly UserName: ODataColumnBuilder;
	readonly UserGroupName: ODataColumnBuilder;
	readonly TimeStarted: ODataColumnBuilder;
	readonly TimeEnded: ODataColumnBuilder;
	readonly TimeElapsed: ODataColumnBuilder;
	readonly Succeeded: ODataColumnBuilder;
	readonly CriticalErrorCount: ODataColumnBuilder;
	readonly VersionName: ODataColumnBuilder;
	readonly VersionKey: ODataColumnBuilder;
	readonly VersionType: ODataColumnBuilder;
	readonly InstallLocation: ODataColumnBuilder;
	readonly IsCurrentInstallation: ODataColumnBuilder;
	
	build() {
		return {
			RequestID: this.RequestID.build(),
			Path: this.Path.build(),
			AppName: this.AppName.build(),
			AppType: this.AppType.build(),
			ResourceGroupName: this.ResourceGroupName.build(),
			ResourceName: this.ResourceName.build(),
			ModCategoryName: this.ModCategoryName.build(),
			ModKey: this.ModKey.build(),
			ModTargetKey: this.ModTargetKey.build(),
			ModDisplayText: this.ModDisplayText.build(),
			ActualCount: this.ActualCount.build(),
			UserName: this.UserName.build(),
			UserGroupName: this.UserGroupName.build(),
			TimeStarted: this.TimeStarted.build(),
			TimeEnded: this.TimeEnded.build(),
			TimeElapsed: this.TimeElapsed.build(),
			Succeeded: this.Succeeded.build(),
			CriticalErrorCount: this.CriticalErrorCount.build(),
			VersionName: this.VersionName.build(),
			VersionKey: this.VersionKey.build(),
			VersionType: this.VersionType.build(),
			InstallLocation: this.InstallLocation.build(),
			IsCurrentInstallation: this.IsCurrentInstallation.build()
		} as ODataColumns<IExpandedRequest>;
	}
}